namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlIndexDeclaration : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        public XmlIndexDeclaration() : this(DbXmlPINVOKE.new_XmlIndexDeclaration(), true)
        {
        }

        internal XmlIndexDeclaration(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlIndexDeclaration(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlIndexDeclaration()
        {
            this.Dispose();
        }

        public string get_index()
        {
            return DbXmlPINVOKE.XmlIndexDeclaration_get_index(this.swigCPtr);
        }

        public string get_name()
        {
            return DbXmlPINVOKE.XmlIndexDeclaration_get_name(this.swigCPtr);
        }

        public string get_uri()
        {
            return DbXmlPINVOKE.XmlIndexDeclaration_get_uri(this.swigCPtr);
        }

        internal static IntPtr getCPtr(XmlIndexDeclaration obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }
    }
}

