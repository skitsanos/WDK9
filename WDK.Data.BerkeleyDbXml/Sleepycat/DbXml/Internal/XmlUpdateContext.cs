namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlUpdateContext : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlUpdateContext() : this(IntPtr.Zero, false)
        {
        }

        internal XmlUpdateContext(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlUpdateContext(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlUpdateContext()
        {
            this.Dispose();
        }

        public bool getApplyChangesToContainers()
        {
            return DbXmlPINVOKE.XmlUpdateContext_getApplyChangesToContainers(this.swigCPtr);
        }

        internal static IntPtr getCPtr(XmlUpdateContext obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlUpdateContext obj)
        {
            return obj.swigCPtr;
        }

        public void setApplyChangesToContainers(bool applyChanges)
        {
            DbXmlPINVOKE.XmlUpdateContext_setApplyChangesToContainers(this.swigCPtr, applyChanges);
        }
    }
}

