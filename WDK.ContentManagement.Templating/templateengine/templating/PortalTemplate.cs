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
using System.Web.UI;

namespace WDK.ContentManagement.Templating
{
	/// <summary>
	/// User control which enables region-based templating.
	/// </summary>
	public class PortalTemplate : System.Web.UI.UserControl, IPortalTemplate
	{

    #region members

    /// <summary>
    /// Collection of <see cref="RegionPlaceHolder"/>
    /// controls that register themselves with the
    /// template.
    /// </summary>
    protected Hashtable regions = new Hashtable();

    #endregion


    #region indexer

    /// <summary>
    /// Provides access to the templates region place holders.
    /// </summary>
    public RegionPlaceHolder this[PortalRegion region]
    {
      get { 
        return this.regions[region] as RegionPlaceHolder; 
      }
    }

    #endregion


    /// <summary>
    /// Registers a region with the template.
    /// </summary>
    /// <param name="placeHolder">Placeholder which will
    /// get controls to render.</param>
    public void RegisterRegion(RegionPlaceHolder placeHolder)
    {
      this.regions.Add(placeHolder.RegionId, placeHolder);
    }


    /// <summary>
    /// This method is being called before the controls of the
    /// template are being merged with the web form.
    /// </summary>
    /// <param name="page">The <c>Page</c> class of the web form
    /// that is currently being rendered.</param>
    public virtual void BeforeTemplating(Page page)
    {
      // no implementation here
    }


    /// <summary>
    /// This method is being called by the template engine
    /// after the controls of the template have been moved
    /// to the web form.
    /// </summary>
    /// <param name="page">The <c>Page</c> class of the web form
    /// that is currently being rendered.</param>
    public virtual void AfterTemplating(Page page)
    {
      // no implementation here
    }


  }
}
