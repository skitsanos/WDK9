namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlMetaData : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        public XmlMetaData() : this(DbXmlPINVOKE.new_XmlMetaData(), true)
        {
        }

        internal XmlMetaData(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlMetaData(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlMetaData()
        {
            this.Dispose();
        }

        public string get_name()
        {
            return DbXmlPINVOKE.XmlMetaData_get_name(this.swigCPtr);
        }

        public string get_uri()
        {
            return DbXmlPINVOKE.XmlMetaData_get_uri(this.swigCPtr);
        }

        public XmlValue get_value()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlMetaData_get_value(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        internal static IntPtr getCPtr(XmlMetaData obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }
    }
}

