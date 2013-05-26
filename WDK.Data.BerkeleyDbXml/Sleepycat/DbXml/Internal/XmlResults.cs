namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlResults : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlResults() : this(IntPtr.Zero, false)
        {
        }

        internal XmlResults(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public void add(XmlValue value)
        {
            DbXmlPINVOKE.XmlResults_add(this.swigCPtr, XmlValue.getCPtr(value));
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlResults(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlResults()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlResults obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlResults obj)
        {
            return obj.swigCPtr;
        }

        public bool hasNext()
        {
            return DbXmlPINVOKE.XmlResults_hasNext(this.swigCPtr);
        }

        public bool hasPrevious()
        {
            return DbXmlPINVOKE.XmlResults_hasPrevious(this.swigCPtr);
        }

        public XmlValue next()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlResults_next(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public XmlValue peek()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlResults_peek(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public XmlValue previous()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlResults_previous(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public void reset()
        {
            DbXmlPINVOKE.XmlResults_reset(this.swigCPtr);
        }

        public uint size()
        {
            return DbXmlPINVOKE.XmlResults_size(this.swigCPtr);
        }
    }
}

