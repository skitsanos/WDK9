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
	/// Region flags which are used to determine
	/// a target region to render controls.
	/// </summary>
	/// <remarks>To extend the framework, just add additional
	/// regions to this enum and recompile.</remarks>
	public enum PortalRegion
	{
    None = 0,
    Top = 1,
    TopLeft = 2,
    TopRight = 3,
    Left1 = 4,
    Left2 = 5,
    Content =6,
    Right1 = 7,
    Right2 = 8,
    Bottom = 9,
    BottomLeft = 10,
    BottomRight = 11,
    MainMenu = 12,
    Header = 13,
    Header2 = 14,
    Footer = 15,
    Footer2 = 16,
    Navigation = 17
	}
}
