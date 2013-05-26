namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;
    using System.Runtime.InteropServices;

    public abstract class InputStream : IDisposable
    {
        private DelegateInputStream.CurPosDelegate curPos_;
        private XmlInputStream is_;
        private DelegateInputStream.ReadBytesDelegate readBytes_;

        protected InputStream()
        {
            this.curPos_ = new DelegateInputStream.CurPosDelegate(this.Internal_CurrentPosition);
            this.readBytes_ = new DelegateInputStream.ReadBytesDelegate(this.Internal_ReadBytes);
            this.is_ = DelegateInputStream.create(this.curPos_, this.readBytes_);
        }

        internal InputStream(XmlInputStream i)
        {
            this.is_ = i;
        }

        public void Dispose()
        {
            this.is_.Dispose();
            GC.SuppressFinalize(this);
        }

        ~InputStream()
        {
            this.Dispose();
        }

        private uint Internal_CurrentPosition()
        {
            return (uint) this.CurrentPosition;
        }

        private uint Internal_ReadBytes(IntPtr toFill, uint maxToRead)
        {
            return (uint) this.ReadBytes(toFill, (int) maxToRead);
        }

        public int ReadBytes(byte[] toFill)
        {
            GCHandle handle = GCHandle.Alloc(toFill, GCHandleType.Pinned);
            return this.ReadBytes(handle.AddrOfPinnedObject(), toFill.Length);
        }

        public abstract int ReadBytes(IntPtr toFill, int maxToRead);

        public abstract int CurrentPosition { get; }

        internal XmlInputStream Internal
        {
            get
            {
                return this.is_;
            }
        }
    }
}

