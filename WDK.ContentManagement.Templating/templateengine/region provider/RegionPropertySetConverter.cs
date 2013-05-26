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
using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Web.UI;


namespace WDK.ContentManagement.Templating
{
	/// <summary>
	/// Converter class which provides proper
	/// serialization of <see cref="RegionPropertySet"/>
	/// objects.
	/// </summary>
	public class RegionPropertySetConverter  : CollectionConverter
	{

    /// <summary>
    /// Provides conversion abilities of the converter class.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="destinationType"></param>
    /// <returns></returns>
    public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) 
    {
      if( destinationType == typeof(InstanceDescriptor) ) return true;
      return this.CanConvertTo(context, destinationType);
    }
            

    /// <summary>
    /// Provides conversion information for a property set
    /// through <c>InstanceDescriptor</c> objects.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="culture"></param>
    /// <param name="value"></param>
    /// <param name="destinationType"></param>
    /// <returns></returns>
    public  override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) 
    {

      RegionPropertySet propertySet = value as RegionPropertySet;


      if (propertySet != null) 
      {
        //perform string conversion
        if (destinationType == typeof(string)) 
        {
          return propertySet.TargetRegion;
        }

        //convert to InstanceDescriptor
        if (destinationType == typeof(InstanceDescriptor)) 
        {
          object[] parameters = new object[3];
          Type[] types = new Type[3];
          
          //control
          parameters[0] = propertySet.Control;
          types[0] = typeof(Control);

          // region
          parameters[1] = propertySet.TargetRegion;
          types[1] = typeof(PortalRegion);
          
          // index
          parameters[2] = propertySet.RenderIndex;
          types[2] = typeof(int);

          // Build constructor
          ConstructorInfo  ci = typeof(RegionPropertySet).GetConstructor(types);
          return new InstanceDescriptor(ci, parameters);
        }
      }

      //rely on base if neither string nor InstanceDescriptor are required
      return base.ConvertTo(context, culture, value, destinationType);
    }    
	}
}
