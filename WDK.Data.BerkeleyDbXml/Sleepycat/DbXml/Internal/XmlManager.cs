namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlManager : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        public XmlManager() : this(DbXmlPINVOKE.new_XmlManager__SWIG_0(), true)
        {
        }

        public XmlManager(uint flags) : this(DbXmlPINVOKE.new_XmlManager__SWIG_1(flags), true)
        {
        }

        public XmlManager(DbEnv dbEnv, uint flags) : this(DbXmlPINVOKE.new_XmlManager__SWIG_2(DbEnv.getCPtr(dbEnv), flags), true)
        {
        }

        internal XmlManager(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public XmlContainer createContainer(string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createContainer__SWIG_0(this.swigCPtr, name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlContainer createContainer(XmlTransaction txn, string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createContainer__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlContainer createContainer(string name, uint flags, int type, int mode)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createContainer__SWIG_2(this.swigCPtr, name, flags, type, mode);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlContainer createContainer(XmlTransaction txn, string name, uint flags, int type, int mode)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createContainer__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), name, flags, type, mode);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlDocument createDocument()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createDocument(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlDocument(cPtr, true);
            }
            return null;
        }

        public XmlInputStream createLocalFileInputStream(string filename)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createLocalFileInputStream(this.swigCPtr, filename);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        public XmlInputStream createMemBufInputStream(string bytes, uint count, bool copyBuffer)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createMemBufInputStream__SWIG_1(this.swigCPtr, bytes, count, copyBuffer);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        public XmlInputStream createMemBufInputStream(string bytes, uint count, string id, bool adopt)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createMemBufInputStream__SWIG_0(this.swigCPtr, bytes, count, id, adopt);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        public XmlModify createModify()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createModify(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlModify(cPtr, true);
            }
            return null;
        }

        public XmlQueryContext createQueryContext()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createQueryContext__SWIG_1(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlQueryContext(cPtr, true);
            }
            return null;
        }

        public XmlQueryContext createQueryContext(int rt)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createQueryContext__SWIG_2(this.swigCPtr, rt);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlQueryContext(cPtr, true);
            }
            return null;
        }

        public XmlQueryContext createQueryContext(int rt, int et)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createQueryContext__SWIG_0(this.swigCPtr, rt, et);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlQueryContext(cPtr, true);
            }
            return null;
        }

        public XmlResults createResults()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createResults(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlInputStream createStdInInputStream()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createStdInInputStream(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        public XmlTransaction createTransaction()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createTransaction__SWIG_1(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlTransaction(cPtr, true);
            }
            return null;
        }

        public XmlTransaction createTransaction(uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createTransaction__SWIG_0(this.swigCPtr, flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlTransaction(cPtr, true);
            }
            return null;
        }

        public XmlUpdateContext createUpdateContext()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createUpdateContext(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlUpdateContext(cPtr, true);
            }
            return null;
        }

        public XmlInputStream createURLInputStream(string baseId, string systemId)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createURLInputStream__SWIG_1(this.swigCPtr, baseId, systemId);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        public XmlInputStream createURLInputStream(string baseId, string systemId, string publicId)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_createURLInputStream__SWIG_0(this.swigCPtr, baseId, systemId, publicId);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlInputStream(cPtr, true);
            }
            return null;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlManager(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        public void dumpContainer(string name, string filename)
        {
            DbXmlPINVOKE.XmlManager_dumpContainer(this.swigCPtr, name, filename);
        }

        ~XmlManager()
        {
            this.Dispose();
        }

        public static int get_version_major()
        {
            return DbXmlPINVOKE.XmlManager_get_version_major();
        }

        public static int get_version_minor()
        {
            return DbXmlPINVOKE.XmlManager_get_version_minor();
        }

        public static int get_version_patch()
        {
            return DbXmlPINVOKE.XmlManager_get_version_patch();
        }

        public static string get_version_string()
        {
            return DbXmlPINVOKE.XmlManager_get_version_string();
        }

        internal static IntPtr getCPtr(XmlManager obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public DbEnv getDbEnv()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_getDbEnv(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new DbEnv(cPtr, false);
            }
            return null;
        }

        public uint getDefaultContainerFlags()
        {
            return DbXmlPINVOKE.XmlManager_getDefaultContainerFlags(this.swigCPtr);
        }

        public int getDefaultContainerType()
        {
            return DbXmlPINVOKE.XmlManager_getDefaultContainerType(this.swigCPtr);
        }

        public uint getDefaultPageSize()
        {
            return DbXmlPINVOKE.XmlManager_getDefaultPageSize(this.swigCPtr);
        }

        public string getHome()
        {
            return DbXmlPINVOKE.XmlManager_getHome(this.swigCPtr);
        }

        public void loadContainer(string name, string filename, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlManager_loadContainer(this.swigCPtr, name, filename, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public XmlContainer openContainer(string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_openContainer__SWIG_1(this.swigCPtr, name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlContainer openContainer(XmlTransaction txn, string name)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_openContainer__SWIG_0(this.swigCPtr, XmlTransaction.getCPtr(txn), name);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlContainer openContainer(string name, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_openContainer__SWIG_2(this.swigCPtr, name, flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlContainer openContainer(XmlTransaction txn, string name, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_openContainer__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), name, flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlContainer(cPtr, true);
            }
            return null;
        }

        public XmlQueryExpression prepare(string query, XmlQueryContext context)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_prepare__SWIG_0(this.swigCPtr, query, XmlQueryContext.getCPtrOrThrow(context));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlQueryExpression(cPtr, true);
            }
            return null;
        }

        public XmlQueryExpression prepare(XmlTransaction txn, string query, XmlQueryContext context)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_prepare__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), query, XmlQueryContext.getCPtrOrThrow(context));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlQueryExpression(cPtr, true);
            }
            return null;
        }

        public XmlResults query(string query, XmlQueryContext context)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_query__SWIG_2(this.swigCPtr, query, XmlQueryContext.getCPtrOrThrow(context));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults query(XmlTransaction txn, string query, XmlQueryContext context)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_query__SWIG_3(this.swigCPtr, XmlTransaction.getCPtr(txn), query, XmlQueryContext.getCPtrOrThrow(context));
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults query(string query, XmlQueryContext context, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_query__SWIG_0(this.swigCPtr, query, XmlQueryContext.getCPtrOrThrow(context), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public XmlResults query(XmlTransaction txn, string query, XmlQueryContext context, uint flags)
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlManager_query__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), query, XmlQueryContext.getCPtrOrThrow(context), flags);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        public void registerResolver(XmlResolver resolver)
        {
            DbXmlPINVOKE.XmlManager_registerResolver(this.swigCPtr, XmlResolver.getCPtr(resolver));
        }

        public void removeContainer(string name)
        {
            DbXmlPINVOKE.XmlManager_removeContainer__SWIG_0(this.swigCPtr, name);
        }

        public void removeContainer(XmlTransaction txn, string name)
        {
            DbXmlPINVOKE.XmlManager_removeContainer__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), name);
        }

        public void renameContainer(string oldName, string newName)
        {
            DbXmlPINVOKE.XmlManager_renameContainer__SWIG_0(this.swigCPtr, oldName, newName);
        }

        public void renameContainer(XmlTransaction txn, string oldName, string newName)
        {
            DbXmlPINVOKE.XmlManager_renameContainer__SWIG_1(this.swigCPtr, XmlTransaction.getCPtr(txn), oldName, newName);
        }

        public void setDefaultContainerFlags(uint flags)
        {
            DbXmlPINVOKE.XmlManager_setDefaultContainerFlags(this.swigCPtr, flags);
        }

        public void setDefaultContainerType(int type)
        {
            DbXmlPINVOKE.XmlManager_setDefaultContainerType(this.swigCPtr, type);
        }

        public void setDefaultPageSize(uint pageSize)
        {
            DbXmlPINVOKE.XmlManager_setDefaultPageSize(this.swigCPtr, pageSize);
        }

        public static void setLogCategory(int category, bool enabled)
        {
            DbXmlPINVOKE.XmlManager_setLogCategory(category, enabled);
        }

        public static void setLogLevel(int level, bool enabled)
        {
            DbXmlPINVOKE.XmlManager_setLogLevel(level, enabled);
        }

        public void upgradeContainer(string name, XmlUpdateContext uc)
        {
            DbXmlPINVOKE.XmlManager_upgradeContainer(this.swigCPtr, name, XmlUpdateContext.getCPtrOrThrow(uc));
        }

        public void verifyContainer(string name, string filename, uint flags)
        {
            DbXmlPINVOKE.XmlManager_verifyContainer(this.swigCPtr, name, filename, flags);
        }
    }
}

