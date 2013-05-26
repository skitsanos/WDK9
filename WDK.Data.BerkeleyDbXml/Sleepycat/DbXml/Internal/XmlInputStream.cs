namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlInputStream : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlInputStream() : this(IntPtr.Zero, false)
        {
        }

        internal XmlInputStream(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual uint curPos()
        {
            return DbXmlPINVOKE.XmlInputStream_curPos(this.swigCPtr);
        }

        internal void disownCPtr()
        {
            this.swigCMemOwn = false;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlInputStream(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlInputStream()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlInputStream obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public virtual uint readBytes(IntPtr toFill, uint maxToRead)
        {
            return DbXmlPINVOKE.XmlInputStream_readBytes(this.swigCPtr, toFill, maxToRead);
        }
    }
}

