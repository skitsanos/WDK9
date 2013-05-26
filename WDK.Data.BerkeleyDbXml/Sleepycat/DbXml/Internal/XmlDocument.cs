namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlDocument : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlDocument() : this(IntPtr.Zero, false)
        {
        }

        internal XmlDocument(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlDocument(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        public void fetchAllData()
        {
            DbXmlPINVOKE.XmlDocument_fetchAllData(this.swigCPtr);
        }

        ~XmlDocument()
        {
            this.Dispose();
        }

        public XmlData getContent()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlDocument_getContent(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlData(cPtr, true);
            }
            return null;
        }

        public string getContentAsString()
        {
            return DbXmlPINVOKE.XmlDocument_getContentAsString(this.swigCPtr);
        }

        public XmlInputStream getContentAsXmlInputStream()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlDocument_getContentAsXmlInputStream(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        internal static IntPtr getCPtr(XmlDocument obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlDocument obj)
        {
            return obj.swigCPtr;
        }

        public XmlValue getMetaData(string uri, string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlDocument_getMetaData__SWIG_1(this.swigCPtr, uri, name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public bool getMetaData(string uri, string name, XmlData value)
        {
            return DbXmlPINVOKE.XmlDocument_getMetaData__SWIG_0(this.swigCPtr, uri, name, XmlData.getCPtr(value));
        }

        public XmlMetaDataIterator getMetaDataIterator()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlDocument_getMetaDataIterator(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlMetaDataIterator(cPtr, true);
            }
            return null;
        }

        public string getName()
        {
            return DbXmlPINVOKE.XmlDocument_getName(this.swigCPtr);
        }

        public void removeMetaData(string uri, string name)
        {
            DbXmlPINVOKE.XmlDocument_removeMetaData(this.swigCPtr, uri, name);
        }

        public void setContent(XmlData content)
        {
            DbXmlPINVOKE.XmlDocument_setContent__SWIG_1(this.swigCPtr, XmlData.getCPtr(content));
        }

        public void setContent(string content)
        {
            DbXmlPINVOKE.XmlDocument_setContent__SWIG_0(this.swigCPtr, content);
        }

        public void setContentAsXmlInputStream(XmlInputStream adopted)
        {
            DbXmlPINVOKE.XmlDocument_setContentAsXmlInputStream(this.swigCPtr, XmlInputStream.getCPtr(adopted));
        }

        public void setMetaData(string uri, string name, XmlData value)
        {
            DbXmlPINVOKE.XmlDocument_setMetaData__SWIG_1(this.swigCPtr, uri, name, XmlData.getCPtr(value));
        }

        public void setMetaData(string uri, string name, XmlValue value)
        {
            DbXmlPINVOKE.XmlDocument_setMetaData__SWIG_0(this.swigCPtr, uri, name, XmlValue.getCPtr(value));
        }

        public void setName(string name)
        {
            DbXmlPINVOKE.XmlDocument_setName(this.swigCPtr, name);
        }
    }
}

