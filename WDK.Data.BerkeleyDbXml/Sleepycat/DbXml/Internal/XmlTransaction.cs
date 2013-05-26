namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlTransaction : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlTransaction() : this(IntPtr.Zero, false)
        {
        }

        internal XmlTransaction(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public void abort()
        {
            DbXmlPINVOKE.XmlTransaction_abort(this.swigCPtr);
        }

        public void commit()
        {
            DbXmlPINVOKE.XmlTransaction_commit__SWIG_1(this.swigCPtr);
        }

        public void commit(uint flags)
        {
            DbXmlPINVOKE.XmlTransaction_commit__SWIG_0(this.swigCPtr, flags);
        }

        public XmlTransaction createChild(uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlTransaction_createChild(this.swigCPtr, flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlTransaction(cPtr, true);
            }
            return null;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlTransaction(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlTransaction()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlTransaction obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlTransaction obj)
        {
            return obj.swigCPtr;
        }
    }
}

