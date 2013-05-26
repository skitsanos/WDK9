// Evolve Template Engine
// Copyright (c) 2004 Evolve Software Technologies
// http://www.evolvesoftware.ch
//
// This library is free software; you can redistribute it and/or
// modify it under the terms of the GNU Lesser General Public
// License as published by the Free Software Foundation; either
// version 2.1 of the License, or (at your option) any later version.
// 
// This softwareis provided "AS IS" with no warranties of any kind.
// The entire risk arising out of the use or performance of the software
// and source code is with you.
//
// THIS NOTICE MAY NOT BE REMOVED FROM THIS FILE.

using System;
using System.Reflection;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace WDK.ContentManagement.Templating
{
	/// <summary>
	/// Assembles template controls.
	/// </summary>
	public class TemplateRenderer
	{

    #region perform templating

    /// <summary>
    /// Merges a given page and its template. This overload
    /// dynamically determines the <see cref="RegionProvider"/>
    /// components of the page using reflection. If you do have
    /// a reference to your provider during design time, it is
    /// recommended to use the overload of this method.
    /// </summary>
    /// <param name="page">Page to be rendered.</param>
    public static void PerformTemplating(Page page)
    {
      //get region provider, if any
      RegionProvider provider = GetRegionProvider(page);

      //do nothing if there is no region provider or no template was set
      if (provider == null || provider.RegionTemplatePath.Equals(String.Empty))
      {
        return;
      }

      //call overload
      PerformTemplating(page, provider);
    }


    /// <summary>
    /// Performs templating for a given <see cref="RegionProvider"/>. Use
    /// this overload if you have a reference to your provider for
    /// performance reasons.
    /// </summary>
    /// <param name="page">The page to be rendered.</param>
    /// <param name="provider">The region provider that contains
    /// templating meta data.</param>
    public static void PerformTemplating(Page page, RegionProvider provider)
    {
      HtmlForm form = FindForm(page);
      if (form == null)
      {
        throw new ArgumentException("Could not find a HtmlForm control on that web form which is required by the template engine");
      }

      //load template control
      try
      {
        IPortalTemplate template = (IPortalTemplate)page.LoadControl(provider.RegionTemplatePath);

        //move controls into template
        HandleControls(template, provider, form.Controls, page);
      }
      catch (InvalidCastException castException)
      {
        string msg = "Could not cast the template '{0}'. Make sure that the template derives from '{1}'";
        msg = String.Format(msg, provider.RegionTemplatePath, "IPortalTemplate");
        throw new ArgumentException(msg, castException);
      }
    }


    /// <summary>
    /// Moves all templated controls of a given region provider
    /// into their target regions.
    /// </summary>
    /// <param name="template">The template to be used.</param>
    /// <param name="controls">Controls to move into the template.</param>
    /// <param name="provider">Provides region assignements for the controls.</param>
    /// <param name="page">The currently rendered page.</param>
    protected static void HandleControls(IPortalTemplate template, RegionProvider provider, ControlCollection controls, Page page)
    {
      //force template control creation now by adding / removing the template
      Control templateControl = (Control)template;
      controls.Add(templateControl);
      controls.Remove(templateControl);

      //call initialization code of the template
      template.BeforeTemplating(page);

      //add defined controls to their target region
      RegionPlaceHolder placeHolder;
      foreach (RegionPropertySet propertySet in provider)
      {
        placeHolder = template[propertySet.TargetRegion];
        if (placeHolder == null)
        {
          string msg = "Invalid region defined for control {0}. Template does not contain region '{1}'";
          msg = String.Format(msg, propertySet.Control.ID, propertySet.TargetRegion);
          throw new ArgumentException(msg);
        }
        
        //remove templated control from original location...
        controls.Remove(propertySet.Control);
        //...and put it into placeholder
        placeHolder.Controls.Add(propertySet.Control);
      }


      //add remaining controls to default region, if any
      if (provider.DefaultRegion != PortalRegion.None)
      {
        placeHolder = template[provider.DefaultRegion];
        if (placeHolder == null)
        {
          string msg = "Invalid default region defined: Template does not contain region '{0}'";
          msg = String.Format(msg, provider.DefaultRegion);
          throw new ArgumentException(msg);
        }

        //move remaining controls into the template's controls
        ControlUtil.MoveControls(controls, placeHolder.Controls);
      }


      //Everything is now in the template. Now move all the template's
      //controls back into the page's control collection. This is necessary
      //to keep relative links of the web form working
      ControlUtil.MoveControls(templateControl.Controls, controls);

      //call initialization code of the template
      template.AfterTemplating(page);
    }


    #endregion


    #region find HtmlForm


    /// <summary>
    /// Gets the page's form control, if any.
    /// The form is expected in the <c>Controls</c> collection
    /// of the page itself. However, if not found, a recursive search
    /// is performed (thanks to Paul Wilson for the hint).
    /// </summary>
    /// <param name="page"></param>
    /// <returns></returns>
    public static HtmlForm FindForm(Page page)
    {
      //find the page's form control
      //(fixed potential problem if the form is not directly under the form,
      //-> thanks to Paul Wilson for the hint :-)
      HtmlForm form = null;

      foreach (Control ctrl in page.Controls)
      {
        form = ctrl as HtmlForm;
        if (form != null) return form;
      }

      //browse recursively if the form is not in the top level list
      return FindFormRecursively(page.Controls);
    }


    /// <summary>
    /// Performs a recursive search for a page's HtmlForm control.
    /// </summary>
    /// <param name="controls"></param>
    /// <returns></returns>
    protected static HtmlForm FindFormRecursively(ControlCollection controls)
    {
      HtmlForm form = null;
      foreach (Control ctrl in controls)
      {
        form = ctrl as HtmlForm;
        if (form != null)
        {
          return form;
        }
        else
        {
          form = FindFormRecursively(ctrl.Controls);
          if (form != null) return form;
        }
      }

      return form;
    }

    #endregion


    #region get region provider (reflection)

    /// <summary>
    /// Gets a region provider of a deriving page class.
    /// </summary>
    /// <remarks>
    /// Gets the <c>components</c> member of the deriving class - have to use
    /// reflection to get access to the designer generated code :-/
    /// </remarks>
    protected static RegionProvider GetRegionProvider(Page page)
    {      
      //get a pointer to the components field
      //access the base type - the current type is *not* our page class but a runtime thing
      //of ASP.net. God, this reflection stuff is so ugly :-/
      Type baseType = page.GetType().BaseType;
      FieldInfo componentField = baseType.GetField("components", BindingFlags.Instance | BindingFlags.NonPublic);
      
      //do nothing if this page does not contain a component field (unlikely...)
      if (componentField == null) return null;


      //get a reference to the components instance
      IContainer components;
      components = (IContainer)componentField.GetValue(page);
      if (components != null)
      {
        RegionProvider provider;
        foreach (Component c in components.Components)
        {
          provider = c as RegionProvider;
          if (provider != null) return provider;
        }
      }

      //return null if no region provider can be found...
      return null;
    }

    #endregion

	}
}
