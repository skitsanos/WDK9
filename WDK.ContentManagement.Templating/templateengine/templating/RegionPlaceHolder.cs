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
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Web.UI.Design;


namespace WDK.ContentManagement.Templating
{
  /// <summary>
  /// Placeholder which is used to define regions on a
  /// template.
  /// </summary>
  [ToolboxData("<{0}:RegionPlaceHolder runat=server></{0}:RegionPlaceHolder>")]
  public class RegionPlaceHolder : System.Web.UI.WebControls.PlaceHolder //, INamingContainer
  {
    /// <summary>
    /// The region ID.
    /// </summary>
    private PortalRegion regionId = PortalRegion.None;

    /// <summary>
    /// Defines the region ID of the placeholder.
    /// </summary>
    [Category("Portal")]
    [DefaultValue(PortalRegion.None)]
    [Description("Defines the region ID of the placeholder.")]
    public PortalRegion RegionId
    {
      get { return this.regionId; }
      set { this.regionId = value; }
    }


    /// <summary>
    /// Inits the placeholder.
    /// </summary>
    /// <param name="e"></param>
    protected override void OnInit(EventArgs e)
    {
      this.RegisterAtParent();
      base.OnInit (e);
    }


    /// <summary>
    /// Registers the placeholder with the parent
    /// class.
    /// </summary>
    private void RegisterAtParent()
    {
      //register the region with a parent template, if possible
      IPortalTemplate template;
      
      Control ctrl = this.Parent;
      while (ctrl != null)
      {
        //check whether the parent is a template
        template = ctrl as IPortalTemplate;
        if (template != null)
        {
          template.RegisterRegion(this);
          break;
        }
        else
        {
          //move to next parent
          ctrl = ctrl.Parent;
        }
      }                             
    }

  }
}
