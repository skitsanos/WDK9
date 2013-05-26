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
	/// A panel-like container that does not produce any
	/// output of his own.
	/// </summary>
  [ToolboxData("<{0}:HelperPanel runat=server></{0}:HelperPanel>")]
  [Designer(typeof(System.Web.UI.Design.ContainerControlDesigner))]
	public class HelperPanel : Panel, INamingContainer
	{


    public HelperPanel()
    {
    }

    public override void RenderBeginTag(HtmlTextWriter writer)
    {
      
    }

    public override void RenderEndTag(HtmlTextWriter writer)
    {
      
    }
    }
}
