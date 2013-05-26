namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    internal sealed class BuiltInInputStream : InputStream
    {
        internal BuiltInInputStream(XmlInputStream i) : base(i)
        {
        }

        public override int ReadBytes(IntPtr toFill, int maxToRead)
        {
            return (int) base.Internal.readBytes(toFill, (uint) maxToRead);
        }

        public override int CurrentPosition
        {
            get
            {
                return (int) base.Internal.curPos();
            }
        }
    }
}

