namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlResolver : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlResolver() : this(IntPtr.Zero, false)
        {
        }

        internal XmlResolver(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlResolver(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlResolver()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlResolver obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public virtual bool resolveCollection(XmlTransaction txn, XmlManager mgr, string uri, XmlResults res)
        {
            return DbXmlPINVOKE.XmlResolver_resolveCollection(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlManager.getCPtr(mgr), uri, XmlResults.getCPtrOrThrow(res));
        }

        public virtual bool resolveDocument(XmlTransaction txn, XmlManager mgr, string uri, XmlValue res)
        {
            return DbXmlPINVOKE.XmlResolver_resolveDocument(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlManager.getCPtr(mgr), uri, XmlValue.getCPtr(res));
        }

        public virtual XmlInputStream resolveEntity(XmlTransaction txn, XmlManager mgr, string systemId, string publicId)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlResolver_resolveEntity(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlManager.getCPtr(mgr), systemId, publicId);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        public virtual XmlInputStream resolveSchema(XmlTransaction txn, XmlManager mgr, string schemaLocation, string nameSpace)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlResolver_resolveSchema(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlManager.getCPtr(mgr), schemaLocation, nameSpace);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }
    }
}

