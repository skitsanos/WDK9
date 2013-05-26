// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System.Collections;

namespace WDK.Network.IIS
{
  // [DefaultMemberAttribute("Item")]
  public class IISWebVirtualDirectoryCollection : CollectionBase
  {

    public IISWebVirtualDirectory this[int index]
    {
      get
      {
        return (IISWebVirtualDirectory)List[index];
      }

      set
      {
        List[index] = value;
      }
    }

    public int Add(IISWebVirtualDirectory value)
    {
      return List.Add(value);
    }

    public void Insert(int index, IISWebVirtualDirectory value)
    {
      List.Insert(index, value);
    }

    public void Remove(IISWebVirtualDirectory value)
    {
      List.Remove(value);
    }
  }

}
