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
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Web.UI;

namespace WDK.ContentManagement.Templating
{

  /// <summary>
  /// Component that provides region settings for arbitrary
  /// controls.
  /// </summary>
  [ProvideProperty("TargetRegionId", typeof(Control))]
  [ProvideProperty("RenderIndex", typeof(Control))]
  [DefaultProperty("RegionTemplatePath")]
  public class RegionProvider : Component, IExtenderProvider, IEnumerable
  {

    #region members

    /// <summary>
    /// Erforderliche Designervariable.
    /// </summary>
    private Container components = null;

    /// <summary>
    /// Maintains controls and their region properties.
    /// </summary>
    private ArrayList propertySets = new ArrayList();

    /// <summary>
    /// The default region ID. If a default region is set,
    /// all controls that do not have an explicit region
    /// ID are being rendered within this region.
    /// </summary>
    private PortalRegion defaultRegion = PortalRegion.None;

    /// <summary>
    /// Path of the template to be used along with this provider.
    /// </summary>
    private string regionTemplatePath = String.Empty;

    /// <summary>
    /// The hosting page which is access to hook into the rendering
    /// procedure.
    /// </summary>
    private Page hostingPage = null;

    /// <summary>
    /// Determines when templating occurs.
    /// </summary>
    private TemplatingTime templatingTime = TemplatingTime.OnLoad;

    /// <summary>
    /// If set to true, the order index of templated controls is not
    /// inspected.
    /// </summary>
    private bool ignoreOrderIndices = true;

    #endregion


    #region properties

    /// <summary>
    /// This property enables runtime access to the Page class that
    /// hosts the control. As the property is being monitored through
    /// the designer host at design time, the designer automatically
    /// serializes a reference to itself in <c>InitializeComponent</c>.
    /// </summary>
    /// <remarks>Credits for this really nice one go to Chris Sells'.</remarks>
    [Browsable(false)]
    public Page HostingPage
    {
      get
      {
        //access page during design time - this is monitored by the page and
        //thereby serialized - credits for this one go to Chris Sells :-)
        if (hostingPage == null && this.DesignMode)
        {
          IDesignerHost designer = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
          if (designer != null)
          {
            hostingPage = designer.RootComponent as Page;
          }
        }

        return hostingPage;
      }
      set
      {
        if (!this.DesignMode)
        {
          //do not change page at runtime
          if (hostingPage != null && hostingPage != value)
          {
            throw new InvalidOperationException("You are not allowed to set the hosting page at runtime!");
          }

          //store page reference
          hostingPage = value;  

          //register page event handlers
          hostingPage.Init += new EventHandler(OnHostingPageInit);
          hostingPage.Load += new EventHandler(OnHostingPageLoad);
          hostingPage.PreRender += new EventHandler(OnHostingPagePreRender);
        }
        else
        {
          //just register the page instance
          hostingPage = value;
        }
     
      }
    }


    /// <summary>
    /// Determines when templating occurs.
    /// </summary>
    [Description("Sets the time of templating. The template render may be invoked automatically during the OnLoad or PreRender event or manually.")]
    [Category("Portal")]
    [DefaultValue(TemplatingTime.OnLoad)]
    public TemplatingTime TemplatingTime
    {
      get { return this.templatingTime; }
      set { this.templatingTime = value; }
    }


    /// <summary>
    /// Whether to place a hook into the hosting page's rendering or not.
    /// If set to <c>true</c>, a hosting webform is being templated without
    /// further investigation.
    /// THIS METHOD IS OBSOLETE - USE THE <see cref="TemplatingTime"/> PROPERTY INSTEAD!
    /// </summary>
    [Description("OBSOLETE.")]
    [Category("Portal")]
    [DefaultValue(false)]
    [Obsolete("This property is obsolete - use the TemplatingTime property insted", true)]
    [Browsable(false)]
    public bool HookIntoRendering
    {
      get { return false; }
      set { /* do nothing, won't compile anyway */ }
    }


    /// <summary>
    /// If set to true, 
    /// </summary>
    [Category("Portal")]
    [Description("If set to true, the order index of templated controls is ignored")]
    [DefaultValue(true)]
    public bool IgnoreOrderIndices
    {
      get { return this.ignoreOrderIndices; }
      set { this.ignoreOrderIndices = value; }
    }



    /// <summary>
    /// The default region ID. If a default region is set,
    /// all controls that do not have an explicit region
    /// ID are being rendered within this region.
    /// </summary>
    [Category("Portal")]
    [DefaultValue(PortalRegion.None)]
    [Description("The region that renders all controls without a defined region ID.")]
    public PortalRegion DefaultRegion
    {
      get { return this.defaultRegion; }
      set { this.defaultRegion = value; }
    }


    /// <summary>
    /// The path of the template to be used along with
    /// this provider.
    /// </summary>
    [Category("Portal")]
    [DefaultValue("")]
    [Description("Path of the template that contains the used regions.")]
    [Editor("System.Web.UI.Design.UrlEditor, System.Design", typeof(UITypeEditor))]
    public string RegionTemplatePath
    {
      get { return this.regionTemplatePath; }
      set { this.regionTemplatePath = value; }
    }



    /// <summary>
    /// Gets the region settings for a given control.
    /// </summary>
    /// <remarks>Used during design time only for performance
    /// reasons.</remarks>
    public RegionPropertySet this[Control control]
    {
      get 
      {
        if (control == null) return null;
        foreach (RegionPropertySet ps in this.propertySets)
        {
          if (ps.Control.ID == control.ID) return ps;
        }

        return null; 
      }
    }


    /// <summary>
    /// The list of templated controls which are encapsulated
    /// in <see cref="RegionPropertySet"/> objects.
    /// </summary>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Browsable(false)]
    public ArrayList PropertySets
    {
      get { return this.propertySets; }
    }


    #endregion


    #region initialization

    public RegionProvider(IContainer container)
    {
      container.Add(this);
      InitializeComponent();
    }


    public RegionProvider()
    {
      InitializeComponent();
    }

    #endregion


    #region dispose

    /// <summary> 
    /// Disposes used resources.
    /// </summary>
    protected override void Dispose( bool disposing )
    {
      if( disposing )
      {
        if(components != null)
        {
          components.Dispose();
        }
      }
      base.Dispose( disposing );
    }

    #endregion


    #region property extender

    /// <summary>
    /// Determines whether to display the extended
    /// properties or not. This extender property is available
    /// for arbitrary controls.
    /// </summary>
    /// <param name="extendee">Control to extend by the extender properties.</param>
    /// <returns><c>true</c> if the control may be extended. Currently available
    /// for every <c>Control</c> implementation.</returns>
    public bool CanExtend(object extendee)
    {
      return extendee is Control;
    }


    #region region id

    /// <summary>
    /// Provides a target region for a given control.
    /// </summary>
    /// <param name="control">The control which is rendered within
    /// a region of the provider.</param>
    /// <returns>The target region's ID if any, otherwise <see cref="PortalRegion.None"/>.</returns>
    [Category("Portal")]
    [Description("Determines the target region in which the control is rendered.")]
    [DefaultValue(PortalRegion.None)]
    public PortalRegion GetTargetRegionId(Control control)
    {
      //return PortalRegion.None if no region ID was assigned
      RegionPropertySet propertySet = this[control];
      if (propertySet == null)
        return PortalRegion.None;
      else
        return propertySet.TargetRegion;
    }


    /// <summary>
    /// Sets the target region ID for a given control.
    /// </summary>
    /// <param name="control">To control that needs to be rendered in
    /// a specific region.</param>
    /// <param name="regionId">The ID of the target region.</param>
    public void SetTargetRegionId(Control control, object regionId)
    {
      //do nothing if no region ID was set
      if (regionId == null) return;

      //if the property was removed by the user
      if (regionId.ToString() == "")
      {
        //...remove the control from the internal list
        this.propertySets.Remove(this[control]);
      }
      else
      {
        PortalRegion region = (PortalRegion)regionId;
        if (region == PortalRegion.None)
        {
          //..,remove the control from the internal list
          this.propertySets.Remove(this[control]);
        }
        else
        {
          //get the property set, if any or create a new one
          RegionPropertySet propertySet = this[control];
          if (propertySet == null)
          {
            propertySet = new RegionPropertySet(control, region, 0);
            //store the set in the internal list
            this.propertySets.Add(propertySet);
          }
          else
          {
            //adjust region ID
            propertySet.TargetRegion = region;
          }
        }
      }

      //announce change
      NotifyHost();
    }

    #endregion


    #region region index

    /// <summary>
    /// Gets the index that determines the rendering order of the
    /// control within the target region.
    /// </summary>
    /// <param name="control">The control which is rendered within
    /// a region of the provider.</param>
    /// <returns>The rendering index of the control if any, otherwise <c>0</c>.</returns>
    [Category("Portal")]
    [Description("Determines the order in which controls of the target region are rendered.")]
    [DefaultValue(0)]
    public int GetRenderIndex(Control control)
    {
      RegionPropertySet propertySet = this[control];
      if (propertySet == null)
        return 0;
      else
        return propertySet.RenderIndex;
    }



    /// <summary>
    /// Sets the render index for a given control.
    /// </summary>
    /// <param name="control">To control that needs to be rendered in
    /// a specific region.</param>
    /// <param name="renderIndex">The index of the control within its region.</param>
    public void SetRenderIndex(Control control, object renderIndex)
    {
      //do nothing if no region ID was set
      if (renderIndex == null) return;

      int index = (int)renderIndex;

      //get the property set, if any or create a new one
      RegionPropertySet propertySet = this[control];
      if (propertySet == null)
      {
        //do not create a property set if the index is the default...
        if (index == 0) return;

        //store the control in the internal list
        propertySet = new RegionPropertySet(control, PortalRegion.None, index);
        this.propertySets.Add(propertySet);
      }
      else
      {
        //set region ID
        propertySet.RenderIndex = index;
      }

      //announce change
      NotifyHost();
    }

    #endregion


    /// <summary>
    /// Announces the change of the component to the designer host.
    /// </summary>
    private void NotifyHost()
    {
      //only perform changes in design mode
      if (!this.DesignMode) return;

      //get the designer host
      IDesignerHost host = this.Site.GetService(typeof(IDesignerHost)) as IDesignerHost;

      //get the change service from the host
      IComponentChangeService service;
      service = host.GetService(typeof(IComponentChangeService)) as IComponentChangeService;

      //announce change
      service.OnComponentChanged(this, null, null, null);
    }

    #endregion


    #region enumeration

    /// <summary>
    /// Provides iteration over registered <see cref="RegionPropertySet"/>
    /// instances. If the <see cref="IgnoreOrderIndices"/> property
    /// is set to <c>false</c>, the internal list will be sorted before
    /// returning the enumerator.
    /// </summary>
    /// <returns>Enumerator.</returns>
    public IEnumerator GetEnumerator()
    {
      //sort if order is important
      if (!this.ignoreOrderIndices) propertySets.Sort();

      return this.propertySets.GetEnumerator();
    }

    #endregion


    #region designer code
    /// <summary>
    /// Erforderliche Methode für die Designerunterstützung. 
    /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
    /// </summary>
    private void InitializeComponent()
    {
      components = new Container();
    }
    #endregion



    /// <summary>
    /// Renders contents during the <c>OnLoad</c> event of the page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnHostingPageLoad(object sender, EventArgs e)
    {
      if (this.templatingTime == TemplatingTime.OnLoad)
      {
        TemplateRenderer.PerformTemplating(this.hostingPage, this);
      }
    }


    /// <summary>
    /// Renders contents during the <c>PreRender</c> event of the page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnHostingPagePreRender(object sender, EventArgs e)
    {
      if (this.templatingTime == TemplatingTime.PreRender)
      {
        TemplateRenderer.PerformTemplating(this.hostingPage, this);
      }
    }


    /// <summary>
    /// Renders contents during the <c>OnInit</c> event of the page.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnHostingPageInit(object sender, EventArgs e)
    {
      if (this.templatingTime == TemplatingTime.OnInit)
      {
        TemplateRenderer.PerformTemplating(this.hostingPage, this);
      }
    }
  }
}
