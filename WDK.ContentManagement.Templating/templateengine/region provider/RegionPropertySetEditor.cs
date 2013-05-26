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
using System.ComponentModel.Design;


namespace WDK.ContentManagement.Templating
{
  /// <summary>
  /// Editor for <see cref="RegionPropertySet"/> objects.
  /// </summary>
  public class RegionPropertySetEditor : CollectionEditor
  {
    public RegionPropertySetEditor(Type type) : base(type) 
    {
    }


    /// <summary>
    /// Gets the supported type.
    /// </summary>
    /// <returns></returns>
    protected override Type CreateCollectionItemType() 
    {
      return typeof(RegionPropertySet);
    }
  }
}
