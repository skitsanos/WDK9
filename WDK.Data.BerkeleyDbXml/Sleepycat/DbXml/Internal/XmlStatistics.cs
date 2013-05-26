namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlStatistics : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlStatistics() : this(IntPtr.Zero, false)
        {
        }

        internal XmlStatistics(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlStatistics(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlStatistics()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlStatistics obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlStatistics obj)
        {
            return obj.swigCPtr;
        }

        public double getNumberOfIndexedKeys()
        {
            return DbXmlPINVOKE.XmlStatistics_getNumberOfIndexedKeys(this.swigCPtr);
        }

        public double getNumberOfUniqueKeys()
        {
            return DbXmlPINVOKE.XmlStatistics_getNumberOfUniqueKeys(this.swigCPtr);
        }

        public double getSumKeyValueSize()
        {
            return DbXmlPINVOKE.XmlStatistics_getSumKeyValueSize(this.swigCPtr);
        }
    }
}

