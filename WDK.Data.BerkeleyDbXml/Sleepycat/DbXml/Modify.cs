namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class Modify : IDisposable
    {
        private XmlModify mod_;

        private Modify(XmlModify m)
        {
            this.mod_ = m;
        }

        public void AddAppendStep(QueryExpression selectionExpr, XmlObject type, string name, string content)
        {
            this.mod_.addAppendStep(QueryExpression.ToInternal(selectionExpr), XmlObjectToInt(type), (name == null) ? "" : name, (content == null) ? "" : content, -1);
        }

        public void AddAppendStep(QueryExpression selectionExpr, XmlObject type, string name, string content, int location)
        {
            this.mod_.addAppendStep(QueryExpression.ToInternal(selectionExpr), XmlObjectToInt(type), (name == null) ? "" : name, (content == null) ? "" : content, location);
        }

        public void AddInsertAfterStep(QueryExpression selectionExpr, XmlObject type, string name, string content)
        {
            this.mod_.addInsertAfterStep(QueryExpression.ToInternal(selectionExpr), XmlObjectToInt(type), (name == null) ? "" : name, (content == null) ? "" : content);
        }

        public void AddInsertBeforeStep(QueryExpression selectionExpr, XmlObject type, string name, string content)
        {
            this.mod_.addInsertBeforeStep(QueryExpression.ToInternal(selectionExpr), XmlObjectToInt(type), (name == null) ? "" : name, (content == null) ? "" : content);
        }

        public void AddRemoveStep(QueryExpression selectionExpr)
        {
            this.mod_.addRemoveStep(QueryExpression.ToInternal(selectionExpr));
        }

        public void AddRenameStep(QueryExpression selectionExpr, string newName)
        {
            this.mod_.addRenameStep(QueryExpression.ToInternal(selectionExpr), newName);
        }

        public void AddUpdateStep(QueryExpression selectionExpr, string content)
        {
            this.mod_.addUpdateStep(QueryExpression.ToInternal(selectionExpr), (content == null) ? "" : content);
        }

        internal static Modify Create(XmlModify v)
        {
            if (v == null)
            {
                return null;
            }
            return new Modify(v);
        }

        public void Dispose()
        {
            this.mod_.Dispose();
            GC.SuppressFinalize(this);
        }

        public int Execute(Transaction txn, Results toModify, QueryContext context, UpdateContext uc)
        {
            return (int) this.mod_.execute(Transaction.ToInternal(txn), Results.ToInternal(toModify), QueryContext.ToInternal(context), UpdateContext.ToInternal(uc));
        }

        public int Execute(Transaction txn, Value toModify, QueryContext context, UpdateContext uc)
        {
            return (int) this.mod_.execute(Transaction.ToInternal(txn), Value.ToInternal(toModify), QueryContext.ToInternal(context), UpdateContext.ToInternal(uc));
        }

        ~Modify()
        {
            this.Dispose();
        }

        public void SetNewEncoding(string newEncoding)
        {
            this.mod_.setNewEncoding(newEncoding);
        }

        private static int XmlObjectToInt(XmlObject xo)
        {
            switch (xo)
            {
                case XmlObject.Element:
                    return XmlModify.Element;

                case XmlObject.Attribute:
                    return XmlModify.Attribute;

                case XmlObject.Text:
                    return XmlModify.Text;

                case XmlObject.ProcessingInstruction:
                    return XmlModify.ProcessingInstruction;

                case XmlObject.Comment:
                    return XmlModify.Comment;
            }
            throw new Exception("Enumeration not covered in switch: " + xo);
        }

        public enum XmlObject
        {
            Element,
            Attribute,
            Text,
            ProcessingInstruction,
            Comment
        }
    }
}

