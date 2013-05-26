namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlData : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        public XmlData() : this(DbXmlPINVOKE.new_XmlData__SWIG_0(), true)
        {
        }

        internal XmlData(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public XmlData(IntPtr data, uint size) : this(DbXmlPINVOKE.new_XmlData__SWIG_1(data, size), true)
        {
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlData(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlData()
        {
            this.Dispose();
        }

        public IntPtr get_data()
        {
            return DbXmlPINVOKE.XmlData_get_data(this.swigCPtr);
        }

        public uint get_size()
        {
            return DbXmlPINVOKE.XmlData_get_size(this.swigCPtr);
        }

        internal static IntPtr getCPtr(XmlData obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public void set_data(IntPtr value)
        {
            DbXmlPINVOKE.XmlData_set_data(this.swigCPtr, value);
        }

        public void set_size(uint size)
        {
            DbXmlPINVOKE.XmlData_set_size(this.swigCPtr, size);
        }
    }
}

