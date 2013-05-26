namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlIndexSpecification : IDisposable
    {
        public static readonly int KEY_EQUALITY = DbXmlPINVOKE.get_XmlIndexSpecification_KEY_EQUALITY();
        public static readonly int KEY_NONE = DbXmlPINVOKE.get_XmlIndexSpecification_KEY_NONE();
        public static readonly int KEY_PRESENCE = DbXmlPINVOKE.get_XmlIndexSpecification_KEY_PRESENCE();
        public static readonly int KEY_SUBSTRING = DbXmlPINVOKE.get_XmlIndexSpecification_KEY_SUBSTRING();
        public static readonly int NODE_ATTRIBUTE = DbXmlPINVOKE.get_XmlIndexSpecification_NODE_ATTRIBUTE();
        public static readonly int NODE_ELEMENT = DbXmlPINVOKE.get_XmlIndexSpecification_NODE_ELEMENT();
        public static readonly int NODE_METADATA = DbXmlPINVOKE.get_XmlIndexSpecification_NODE_METADATA();
        public static readonly int NODE_NONE = DbXmlPINVOKE.get_XmlIndexSpecification_NODE_NONE();
        public static readonly int PATH_EDGE = DbXmlPINVOKE.get_XmlIndexSpecification_PATH_EDGE();
        public static readonly int PATH_NODE = DbXmlPINVOKE.get_XmlIndexSpecification_PATH_NODE();
        public static readonly int PATH_NONE = DbXmlPINVOKE.get_XmlIndexSpecification_PATH_NONE();
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;
        public static readonly int UNIQUE_OFF = DbXmlPINVOKE.get_XmlIndexSpecification_UNIQUE_OFF();
        public static readonly int UNIQUE_ON = DbXmlPINVOKE.get_XmlIndexSpecification_UNIQUE_ON();

        public XmlIndexSpecification() : this(DbXmlPINVOKE.new_XmlIndexSpecification(), true)
        {
        }

        internal XmlIndexSpecification(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public void addDefaultIndex(string index)
        {
            DbXmlPINVOKE.XmlIndexSpecification_addDefaultIndex__SWIG_1(this.swigCPtr, index);
        }

        public void addDefaultIndex(int type, int syntax)
        {
            DbXmlPINVOKE.XmlIndexSpecification_addDefaultIndex__SWIG_0(this.swigCPtr, type, syntax);
        }

        public void addIndex(string uri, string name, string index)
        {
            DbXmlPINVOKE.XmlIndexSpecification_addIndex__SWIG_1(this.swigCPtr, uri, name, index);
        }

        public void addIndex(string uri, string name, int type, int syntax)
        {
            DbXmlPINVOKE.XmlIndexSpecification_addIndex__SWIG_0(this.swigCPtr, uri, name, type, syntax);
        }

        public void deleteDefaultIndex(string index)
        {
            DbXmlPINVOKE.XmlIndexSpecification_deleteDefaultIndex__SWIG_1(this.swigCPtr, index);
        }

        public void deleteDefaultIndex(int type, int syntax)
        {
            DbXmlPINVOKE.XmlIndexSpecification_deleteDefaultIndex__SWIG_0(this.swigCPtr, type, syntax);
        }

        public void deleteIndex(string uri, string name, string index)
        {
            DbXmlPINVOKE.XmlIndexSpecification_deleteIndex__SWIG_1(this.swigCPtr, uri, name, index);
        }

        public void deleteIndex(string uri, string name, int type, int syntax)
        {
            DbXmlPINVOKE.XmlIndexSpecification_deleteIndex__SWIG_0(this.swigCPtr, uri, name, type, syntax);
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlIndexSpecification(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlIndexSpecification()
        {
            this.Dispose();
        }

        public XmlIndexDeclaration find(string uri, string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlIndexSpecification_find(this.swigCPtr, uri, name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlIndexDeclaration(cPtr, true);
            }
            return null;
        }

        internal static IntPtr getCPtr(XmlIndexSpecification obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public string getDefaultIndex()
        {
            return DbXmlPINVOKE.XmlIndexSpecification_getDefaultIndex(this.swigCPtr);
        }

        public static int getValueType(string index)
        {
            return DbXmlPINVOKE.XmlIndexSpecification_getValueType(index);
        }

        public XmlIndexDeclaration next()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlIndexSpecification_next(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlIndexDeclaration(cPtr, true);
            }
            return null;
        }

        public void replaceDefaultIndex(string index)
        {
            DbXmlPINVOKE.XmlIndexSpecification_replaceDefaultIndex__SWIG_1(this.swigCPtr, index);
        }

        public void replaceDefaultIndex(int type, int syntax)
        {
            DbXmlPINVOKE.XmlIndexSpecification_replaceDefaultIndex__SWIG_0(this.swigCPtr, type, syntax);
        }

        public void replaceIndex(string uri, string name, string index)
        {
            DbXmlPINVOKE.XmlIndexSpecification_replaceIndex__SWIG_1(this.swigCPtr, uri, name, index);
        }

        public void replaceIndex(string uri, string name, int type, int syntax)
        {
            DbXmlPINVOKE.XmlIndexSpecification_replaceIndex__SWIG_0(this.swigCPtr, uri, name, type, syntax);
        }

        public void reset()
        {
            DbXmlPINVOKE.XmlIndexSpecification_reset(this.swigCPtr);
        }
    }
}

