// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System.Collections;

namespace WDK.Network.IIS
{
    // [DefaultMemberAttribute("Item")]
    public class IISWebDirectoryCollection : CollectionBase
    {
        public IISWebDirectory this[int index]
        {
            get { return (IISWebDirectory) List[index]; }

            set { List[index] = value; }
        }

        public int Add(IISWebDirectory value)
        {
            return List.Add(value);
        }

        public void Insert(int index, IISWebDirectory value)
        {
            List.Insert(index, value);
        }

        public void Remove(IISWebDirectory value)
        {
            List.Remove(value);
        }
    }
}