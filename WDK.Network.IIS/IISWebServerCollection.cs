// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System;
using System.Collections;

namespace WDK.Network.IIS
{
    // [DefaultMemberAttribute("Item")]
    public class IISWebServerCollection : CollectionBase
    {
        public IISWebServer this[int index]
        {
            get { return (IISWebServer) List[index]; }

            set { List[index] = value; }
        }

        public int Add(IISWebServer value)
        {
            return List.Add(value);
        }

        public int IndexOf(IISWebServer value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, IISWebServer value)
        {
            List.Insert(index, value);
        }

        public void Remove(IISWebServer value)
        {
            List.Remove(value);
        }

        public bool Contains(IISWebServer value)
        {
            return List.Contains(value);
        }

        protected override void OnValidate(object value)
        {
            if (value.GetType() != Type.GetType("WDK.Network.IIS.IISWebServer"))
            {
                throw new ArgumentException("value must be of type WDK.Network.IIS.IISWebServer.");
            }
            return;
        }
    }
}