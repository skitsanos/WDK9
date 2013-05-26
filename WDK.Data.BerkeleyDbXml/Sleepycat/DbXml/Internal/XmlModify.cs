namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlModify : IDisposable
    {
        public static readonly int Attribute = DbXmlPINVOKE.get_XmlModify_Attribute();
        public static readonly int Comment = DbXmlPINVOKE.get_XmlModify_Comment();
        public static readonly int Element = DbXmlPINVOKE.get_XmlModify_Element();
        public static readonly int ProcessingInstruction = DbXmlPINVOKE.get_XmlModify_ProcessingInstruction();
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;
        public static readonly int Text = DbXmlPINVOKE.get_XmlModify_Text();

        protected XmlModify() : this(IntPtr.Zero, false)
        {
        }

        internal XmlModify(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public void addAppendStep(XmlQueryExpression selectionExpr, int type, string name, string content, int location)
        {
            DbXmlPINVOKE.XmlModify_addAppendStep(this.swigCPtr, XmlQueryExpression.getCPtrOrThrow(selectionExpr), type, name, content, location);
        }

        public void addInsertAfterStep(XmlQueryExpression selectionExpr, int type, string name, string content)
        {
            DbXmlPINVOKE.XmlModify_addInsertAfterStep(this.swigCPtr, XmlQueryExpression.getCPtrOrThrow(selectionExpr), type, name, content);
        }

        public void addInsertBeforeStep(XmlQueryExpression selectionExpr, int type, string name, string content)
        {
            DbXmlPINVOKE.XmlModify_addInsertBeforeStep(this.swigCPtr, XmlQueryExpression.getCPtrOrThrow(selectionExpr), type, name, content);
        }

        public void addRemoveStep(XmlQueryExpression selectionExpr)
        {
            DbXmlPINVOKE.XmlModify_addRemoveStep(this.swigCPtr, XmlQueryExpression.getCPtrOrThrow(selectionExpr));
        }

        public void addRenameStep(XmlQueryExpression selectionExpr, string newName)
        {
            DbXmlPINVOKE.XmlModify_addRenameStep(this.swigCPtr, XmlQueryExpression.getCPtrOrThrow(selectionExpr), newName);
        }

        public void addUpdateStep(XmlQueryExpression selectionExpr, string content)
        {
            DbXmlPINVOKE.XmlModify_addUpdateStep(this.swigCPtr, XmlQueryExpression.getCPtrOrThrow(selectionExpr), content);
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlModify(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        public uint execute(XmlResults toModify, XmlQueryContext context, XmlUpdateContext uc)
        {
            return DbXmlPINVOKE.XmlModify_execute__SWIG_1(this.swigCPtr, XmlResults.getCPtrOrThrow(toModify), XmlQueryContext.getCPtrOrThrow(context), XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public uint execute(XmlValue toModify, XmlQueryContext context, XmlUpdateContext uc)
        {
            return DbXmlPINVOKE.XmlModify_execute__SWIG_0(this.swigCPtr, XmlValue.getCPtr(toModify), XmlQueryContext.getCPtrOrThrow(context), XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public uint execute(XmlTransaction txn, XmlResults toModify, XmlQueryContext context, XmlUpdateContext uc)
        {
            return DbXmlPINVOKE.XmlModify_execute__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlResults.getCPtrOrThrow(toModify), XmlQueryContext.getCPtrOrThrow(context), XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public uint execute(XmlTransaction txn, XmlValue toModify, XmlQueryContext context, XmlUpdateContext uc)
        {
            return DbXmlPINVOKE.XmlModify_execute__SWIG_2(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlValue.getCPtr(toModify), XmlQueryContext.getCPtrOrThrow(context), XmlUpdateContext.getCPtrOrThrow(uc));
        }

        ~XmlModify()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(XmlModify obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlModify obj)
        {
            return obj.swigCPtr;
        }

        public void setNewEncoding(string newEncoding)
        {
            DbXmlPINVOKE.XmlModify_setNewEncoding(this.swigCPtr, newEncoding);
        }
    }
}

