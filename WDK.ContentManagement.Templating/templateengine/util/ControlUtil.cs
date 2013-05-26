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

namespace WDK.ContentManagement.Templating
{
  /// <summary>
  /// Contains static helper methods.
  /// </summary>
  public class ControlUtil
  {

    /// <summary>
    /// Moves controls from one control collection to the other.
    /// </summary>
    /// <param name="source">Source control collection. Will be
    /// emptied.</param>
    /// <param name="target">Target collection to be filled.</param>
    public static void MoveControls(ControlCollection source, ControlCollection target)
    {
      int count = source.Count;
      for (int i=0; i<count; i++)
      {
        Control ctrl = source[0];
        source.RemoveAt(0);
        target.Add(ctrl);
      }
    }

  }
}
