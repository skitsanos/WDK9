namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlQueryExpression : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected XmlQueryExpression() : this(IntPtr.Zero, false)
        {
        }

        internal XmlQueryExpression(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlQueryExpression(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        public XmlResults execute(XmlQueryContext context, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlQueryExpression_execute__SWIG_0(this.swigCPtr, XmlQueryContext.getCPtrOrThrow(context), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults execute(XmlTransaction txn, XmlQueryContext context, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlQueryExpression_execute__SWIG_2(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlQueryContext.getCPtrOrThrow(context), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults execute(XmlValue contextItem, XmlQueryContext context, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlQueryExpression_execute__SWIG_1(this.swigCPtr, XmlValue.getCPtr(contextItem), XmlQueryContext.getCPtrOrThrow(context), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults execute(XmlTransaction txn, XmlValue contextItem, XmlQueryContext context, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlQueryExpression_execute__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlValue.getCPtr(contextItem), XmlQueryContext.getCPtrOrThrow(context), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        ~XmlQueryExpression()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlQueryExpression obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlQueryExpression obj)
        {
            return obj.swigCPtr;
        }

        public string getQuery()
        {
            return DbXmlPINVOKE.XmlQueryExpression_getQuery(this.swigCPtr);
        }

        public string getQueryPlan()
        {
            return DbXmlPINVOKE.XmlQueryExpression_getQueryPlan(this.swigCPtr);
        }
    }
}

