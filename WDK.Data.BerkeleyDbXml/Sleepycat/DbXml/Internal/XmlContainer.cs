namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlContainer : IDisposable
    {
        public static readonly int NodeContainer = DbXmlPINVOKE.get_XmlContainer_NodeContainer();
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;
        public static readonly int WholedocContainer = DbXmlPINVOKE.get_XmlContainer_WholedocContainer();

        protected XmlContainer() : this(IntPtr.Zero, false)
        {
        }

        internal XmlContainer(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public bool addAlias(string alias)
        {
            return DbXmlPINVOKE.XmlContainer_addAlias(this.swigCPtr, alias);
        }

        public void addDefaultIndex(string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_addDefaultIndex__SWIG_0(this.swigCPtr, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void addDefaultIndex(XmlTransaction txn, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_addDefaultIndex__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void addIndex(string uri, string name, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_addIndex__SWIG_0(this.swigCPtr, uri, name, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void addIndex(XmlTransaction txn, string uri, string name, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_addIndex__SWIG_2(this.swigCPtr, XmlTransaction.getCPtr(txn), uri, name, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void addIndex(string uri, string name, int indexType, int syntaxType, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_addIndex__SWIG_1(this.swigCPtr, uri, name, indexType, syntaxType, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void addIndex(XmlTransaction txn, string uri, string name, int indexType, int syntaxType, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_addIndex__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), uri, name, indexType, syntaxType, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void close()
        {
            DbXmlPINVOKE.XmlContainer_close(this.swigCPtr);
        }

        public void deleteDefaultIndex(string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_deleteDefaultIndex__SWIG_0(this.swigCPtr, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void deleteDefaultIndex(XmlTransaction txn, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_deleteDefaultIndex__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void deleteDocument(XmlDocument document, XmlUpdateContext context)
        {
            DbXmlPINVOKE.XmlContainer_deleteDocument__SWIG_0(this.swigCPtr, XmlDocument.getCPtrOrThrow(document), XmlUpdateContext.getCPtrOrThrow(context));
        }

        public void deleteDocument(string name, XmlUpdateContext context)
        {
            DbXmlPINVOKE.XmlContainer_deleteDocument__SWIG_1(this.swigCPtr, name, XmlUpdateContext.getCPtrOrThrow(context));
        }

        public void deleteDocument(XmlTransaction txn, XmlDocument document, XmlUpdateContext context)
        {
            DbXmlPINVOKE.XmlContainer_deleteDocument__SWIG_2(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlDocument.getCPtrOrThrow(document), XmlUpdateContext.getCPtrOrThrow(context));
        }

        public void deleteDocument(XmlTransaction txn, string name, XmlUpdateContext context)
        {
            DbXmlPINVOKE.XmlContainer_deleteDocument__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), name, XmlUpdateContext.getCPtrOrThrow(context));
        }

        public void deleteIndex(string uri, string name, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_deleteIndex__SWIG_0(this.swigCPtr, uri, name, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void deleteIndex(XmlTransaction txn, string uri, string name, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_deleteIndex__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), uri, name, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlContainer(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~XmlContainer()
        {
            this.Dispose();
        }

        public XmlResults getAllDocuments(uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getAllDocuments__SWIG_0(this.swigCPtr, flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults getAllDocuments(XmlTransaction txn, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getAllDocuments__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public int getContainerType()
        {
            return DbXmlPINVOKE.XmlContainer_getContainerType(this.swigCPtr);
        }

        internal static IntPtr getCPtr(XmlContainer obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlContainer obj)
        {
            return obj.swigCPtr;
        }

        public XmlDocument getDocument(string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getDocument__SWIG_0(this.swigCPtr, name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlDocument(cPtr, true);
            }
            return null;
        }

        public XmlDocument getDocument(XmlTransaction txn, string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getDocument__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlDocument(cPtr, true);
            }
            return null;
        }

        public XmlDocument getDocument(string name, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getDocument__SWIG_2(this.swigCPtr, name, flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlDocument(cPtr, true);
            }
            return null;
        }

        public XmlDocument getDocument(XmlTransaction txn, string name, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getDocument__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), name, flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlDocument(cPtr, true);
            }
            return null;
        }

        public XmlIndexSpecification getIndexSpecification()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getIndexSpecification__SWIG_0(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlIndexSpecification(cPtr, true);
            }
            return null;
        }

        public XmlIndexSpecification getIndexSpecification(XmlTransaction txn)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getIndexSpecification__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlIndexSpecification(cPtr, true);
            }
            return null;
        }

        public XmlIndexSpecification getIndexSpecification(XmlTransaction txn, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getIndexSpecification__SWIG_2(this.swigCPtr, XmlTransaction.getCPtr(txn), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlIndexSpecification(cPtr, true);
            }
            return null;
        }

        public XmlManager getManager()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_getManager(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlManager(cPtr, true);
            }
            return null;
        }

        public string getName()
        {
            return DbXmlPINVOKE.XmlContainer_getName(this.swigCPtr);
        }

        public uint getNumDocuments()
        {
            return DbXmlPINVOKE.XmlContainer_getNumDocuments__SWIG_0(this.swigCPtr);
        }

        public uint getNumDocuments(XmlTransaction txn)
        {
            return DbXmlPINVOKE.XmlContainer_getNumDocuments__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn));
        }

        public XmlResults lookupIndex(XmlQueryContext context, string uri, string name, string index, XmlValue value, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupIndex__SWIG_0(this.swigCPtr, XmlQueryContext.getCPtrOrThrow(context), uri, name, index, XmlValue.getCPtr(value), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults lookupIndex(XmlTransaction txn, XmlQueryContext context, string uri, string name, string index, XmlValue value, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupIndex__SWIG_2(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlQueryContext.getCPtrOrThrow(context), uri, name, index, XmlValue.getCPtr(value), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults lookupIndex(XmlQueryContext context, string uri, string name, string parent_uri, string parent_name, string index, XmlValue value, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupIndex__SWIG_1(this.swigCPtr, XmlQueryContext.getCPtrOrThrow(context), uri, name, parent_uri, parent_name, index, XmlValue.getCPtr(value), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults lookupIndex(XmlTransaction txn, XmlQueryContext context, string uri, string name, string parent_uri, string parent_name, string index, XmlValue value, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupIndex__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlQueryContext.getCPtrOrThrow(context), uri, name, parent_uri, parent_name, index, XmlValue.getCPtr(value), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlStatistics lookupStatistics(string uri, string name, string index, XmlValue value)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupStatistics__SWIG_0(this.swigCPtr, uri, name, index, XmlValue.getCPtr(value));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlStatistics(cPtr, true);
            }
            return null;
        }

        public XmlStatistics lookupStatistics(XmlTransaction txn, string uri, string name, string index, XmlValue value)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupStatistics__SWIG_2(this.swigCPtr, XmlTransaction.getCPtr(txn), uri, name, index, XmlValue.getCPtr(value));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlStatistics(cPtr, true);
            }
            return null;
        }

        public XmlStatistics lookupStatistics(string uri, string name, string parent_uri, string parent_name, string index, XmlValue value)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupStatistics__SWIG_1(this.swigCPtr, uri, name, parent_uri, parent_name, index, XmlValue.getCPtr(value));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlStatistics(cPtr, true);
            }
            return null;
        }

        public XmlStatistics lookupStatistics(XmlTransaction txn, string uri, string name, string parent_uri, string parent_name, string index, XmlValue value)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlContainer_lookupStatistics__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), uri, name, parent_uri, parent_name, index, XmlValue.getCPtr(value));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlStatistics(cPtr, true);
            }
            return null;
        }

        public void putDocument(XmlDocument document, XmlUpdateContext context, uint flags)
        {
            DbXmlPINVOKE.XmlContainer_putDocument__SWIG_0(this.swigCPtr, XmlDocument.getCPtrOrThrow(document), XmlUpdateContext.getCPtrOrThrow(context), flags);
        }

        public void putDocument(XmlTransaction txn, XmlDocument document, XmlUpdateContext context, uint flags)
        {
            DbXmlPINVOKE.XmlContainer_putDocument__SWIG_4(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlDocument.getCPtrOrThrow(document), XmlUpdateContext.getCPtrOrThrow(context), flags);
        }

        public string putDocument(string name, XmlInputStream input, XmlUpdateContext context, uint flags)
        {
            return DbXmlPINVOKE.XmlContainer_putDocument__SWIG_2(this.swigCPtr, name, XmlInputStream.getCPtr(input), XmlUpdateContext.getCPtrOrThrow(context), flags);
        }

        public string putDocument(string name, string contents, XmlUpdateContext context, uint flags)
        {
            return DbXmlPINVOKE.XmlContainer_putDocument__SWIG_1(this.swigCPtr, name, contents, XmlUpdateContext.getCPtrOrThrow(context), flags);
        }

        public string putDocument(XmlTransaction txn, string name, XmlInputStream input, XmlUpdateContext context, uint flags)
        {
            return DbXmlPINVOKE.XmlContainer_putDocument__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), name, XmlInputStream.getCPtr(input), XmlUpdateContext.getCPtrOrThrow(context), flags);
        }

        public string putDocument(XmlTransaction txn, string name, string contents, XmlUpdateContext context, uint flags)
        {
            return DbXmlPINVOKE.XmlContainer_putDocument__SWIG_5(this.swigCPtr, XmlTransaction.getCPtr(txn), name, contents, XmlUpdateContext.getCPtrOrThrow(context), flags);
        }

        public bool removeAlias(string alias)
        {
            return DbXmlPINVOKE.XmlContainer_removeAlias(this.swigCPtr, alias);
        }

        public void replaceDefaultIndex(string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_replaceDefaultIndex__SWIG_0(this.swigCPtr, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void replaceDefaultIndex(XmlTransaction txn, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_replaceDefaultIndex__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void replaceIndex(string uri, string name, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_replaceIndex__SWIG_0(this.swigCPtr, uri, name, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void replaceIndex(XmlTransaction txn, string uri, string name, string index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_replaceIndex__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), uri, name, index, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void setIndexSpecification(XmlIndexSpecification index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_setIndexSpecification__SWIG_0(this.swigCPtr, XmlIndexSpecification.getCPtr(index), XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void setIndexSpecification(XmlTransaction txn, XmlIndexSpecification index, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlContainer_setIndexSpecification__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlIndexSpecification.getCPtr(index), XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void sync()
        {
            DbXmlPINVOKE.XmlContainer_sync(this.swigCPtr);
        }

        public void updateDocument(XmlDocument document, XmlUpdateContext context)
        {
            DbXmlPINVOKE.XmlContainer_updateDocument__SWIG_0(this.swigCPtr, XmlDocument.getCPtrOrThrow(document), XmlUpdateContext.getCPtrOrThrow(context));
        }

        public void updateDocument(XmlTransaction txn, XmlDocument document, XmlUpdateContext context)
        {
            DbXmlPINVOKE.XmlContainer_updateDocument__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), XmlDocument.getCPtrOrThrow(document), XmlUpdateContext.getCPtrOrThrow(context));
        }
    }
}

