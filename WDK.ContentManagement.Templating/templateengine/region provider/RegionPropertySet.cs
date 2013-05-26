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
using System.ComponentModel;
using System.Web.UI;


namespace WDK.ContentManagement.Templating
{
	/// <summary>
	/// Helper class that encapsulates region settings
	/// for controls that use the <see cref="RegionProvider"/>
	/// component.
	/// </summary>
  [TypeConverter(typeof(RegionPropertySetConverter))]
	public class RegionPropertySet : IComparable
	{

    #region members

    /// <summary>
    /// The control to be rendered in a specific region.
    /// </summary>
    private Control control;

    /// <summary>
    /// ID of the target region that renders the control.
    /// </summary>
    private PortalRegion targetRegion;

    /// <summary>
    /// Index of the control within the region.
    /// </summary>
    private int renderIndex;

    #endregion


    #region properties

    /// <summary>
    /// The control to be rendered in a specific region.
    /// </summary>
    public Control Control
    {
      get { return this.control; }
      set { this.control = value; }
    }


    /// <summary>
    /// ID of the target region that renders the control.
    /// </summary>
    public PortalRegion TargetRegion
    {
      get { return targetRegion; }
      set { targetRegion = value; }
    }


    /// <summary>
    /// Index of the control within the region.
    /// </summary>
    public int RenderIndex
    {
      get { return renderIndex; }
      set { renderIndex = value; }
    }

    #endregion


    #region initialization

    /// <summary>
    /// Empty default contructor.
    /// </summary>
    public RegionPropertySet() : this(null, PortalRegion.None, 0)
    {
    }

    /// <summary>
    /// Inits the property set.
    /// </summary>
    /// <param name="control"></param>
    /// <param name="region"></param>
    /// <param name="renderIndex"></param>
    public RegionPropertySet(Control control, PortalRegion region, int renderIndex)
    {
      this.control = control;
      this.targetRegion = region;
      this.renderIndex = renderIndex;
    }

    #endregion


    #region IComparable

    /// <summary>
    /// Compares the property sets via the
    /// encapsulated control's <c>ID</c> property.
    /// </summary>
    /// <param name="obj">Object to compare.</param>
    /// <returns></returns>
    /// <remarks>Compares to eigher a <c>Control</c>
    /// or a <see cref="RegionPropertySet"/> object.</remarks>
    public int CompareTo(object obj)
    {
      RegionPropertySet propertySet = obj as RegionPropertySet;
      return this.renderIndex.CompareTo(propertySet.RenderIndex);

//      Control ctrl = obj as Control;
//      if (ctrl == null)
//      {
//        ctrl = ((RegionPropertySet)obj).Control;
//      }
//
//      return this.control.ID.CompareTo(ctrl.ID);
    }

    #endregion

  }
}
