// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System;
using System.Collections;

namespace WDK.Network.IIS
{
  // [DefaultMemberAttribute("Item")]
  public class IISFTPServerCollection : CollectionBase
  {

    public IISFTPServer this[int index]
    {
      get
      {
        return (IISFTPServer)List[index];
      }

      set
      {
        List[index] = value;
      }
    }

    public int Add(IISFTPServer value)
    {
      return List.Add(value);
    }

    public int IndexOf(IISFTPServer value)
    {
      return List.IndexOf(value);
    }

    public void Insert(int index, IISFTPServer value)
    {
      List.Insert(index, value);
    }

    public void Remove(IISFTPServer value)
    {
      List.Remove(value);
    }

    public bool Contains(IISFTPServer value)
    {
      return List.Contains(value);
    }

    protected override void OnValidate(object value)
    {
      if (value.GetType() != Type.GetType("WDK.Network.IIS.IISFTPServer"))
      {
        throw new ArgumentException("value must be of type WDK.Network.IIS.IISFTPServer.");
      }
        return;
      
    }
  }

}
