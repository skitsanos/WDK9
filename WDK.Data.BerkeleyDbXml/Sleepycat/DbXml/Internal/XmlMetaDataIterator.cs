namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlMetaDataIterator : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlMetaDataIterator() : this(IntPtr.Zero, false)
        {
        }

        internal XmlMetaDataIterator(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlMetaDataIterator(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlMetaDataIterator()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlMetaDataIterator obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlMetaDataIterator obj)
        {
            return obj.swigCPtr;
        }

        public XmlMetaData next()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlMetaDataIterator_next(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlMetaData(cPtr, true);
            }
            return null;
        }

        public void reset()
        {
            DbXmlPINVOKE.XmlMetaDataIterator_reset(this.swigCPtr);
        }
    }
}

