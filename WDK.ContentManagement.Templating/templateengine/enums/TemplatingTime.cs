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

namespace WDK.ContentManagement.Templating
{
	/// <summary>
	/// Determines if manual or automated
	/// templating is used.
	/// </summary>
	public enum TemplatingTime
	{
    /// <summary>
    /// The template renderer is being invoked through
    /// custom code.
    /// </summary>
    Manual = 0,
    /// <summary>
    /// Templating occurs during the parent page's
    /// <c>OnLoad</c> event.
    /// </summary>
    OnLoad = 1,
    /// <summary>
    /// Templating occurs during the parent page's
    /// <c>PreRender</c> event.
    /// </summary>
    PreRender = 2,
    /// <summary>
    /// Templating occurs during the parent page's
    /// <c>OnInit</c> event.
    /// </summary>
    OnInit = 3
	}
}
