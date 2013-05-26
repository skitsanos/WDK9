namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlQueryContext : IDisposable
    {
        public static readonly int DeadValues = DbXmlPINVOKE.get_XmlQueryContext_DeadValues();
        public static readonly int Eager = DbXmlPINVOKE.get_XmlQueryContext_Eager();
        public static readonly int Lazy = DbXmlPINVOKE.get_XmlQueryContext_Lazy();
        public static readonly int LiveValues = DbXmlPINVOKE.get_XmlQueryContext_LiveValues();
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlQueryContext() : this(IntPtr.Zero, false)
        {
        }

        internal XmlQueryContext(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public void clearNamespaces()
        {
            DbXmlPINVOKE.XmlQueryContext_clearNamespaces(this.swigCPtr);
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlQueryContext(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlQueryContext()
        {
            this.Dispose();
        }

        public string getBaseURI()
        {
            return DbXmlPINVOKE.XmlQueryContext_getBaseURI(this.swigCPtr);
        }

        internal static IntPtr getCPtr(XmlQueryContext obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlQueryContext obj)
        {
            return obj.swigCPtr;
        }

        public int getEvaluationType()
        {
            return DbXmlPINVOKE.XmlQueryContext_getEvaluationType(this.swigCPtr);
        }

        public string getNamespace(string prefix)
        {
            return DbXmlPINVOKE.XmlQueryContext_getNamespace(this.swigCPtr, prefix);
        }

        public int getReturnType()
        {
            return DbXmlPINVOKE.XmlQueryContext_getReturnType(this.swigCPtr);
        }

        public XmlValue getVariableValue(string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlQueryContext_getVariableValue(this.swigCPtr, name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public void removeNamespace(string prefix)
        {
            DbXmlPINVOKE.XmlQueryContext_removeNamespace(this.swigCPtr, prefix);
        }

        public void setBaseURI(string baseURI)
        {
            DbXmlPINVOKE.XmlQueryContext_setBaseURI(this.swigCPtr, baseURI);
        }

        public void setEvaluationType(int type)
        {
            DbXmlPINVOKE.XmlQueryContext_setEvaluationType(this.swigCPtr, type);
        }

        public void setNamespace(string prefix, string uri)
        {
            DbXmlPINVOKE.XmlQueryContext_setNamespace(this.swigCPtr, prefix, uri);
        }

        public void setReturnType(int type)
        {
            DbXmlPINVOKE.XmlQueryContext_setReturnType(this.swigCPtr, type);
        }

        public void setVariableValue(string name, XmlValue value)
        {
            DbXmlPINVOKE.XmlQueryContext_setVariableValue(this.swigCPtr, name, XmlValue.getCPtr(value));
        }
    }
}

