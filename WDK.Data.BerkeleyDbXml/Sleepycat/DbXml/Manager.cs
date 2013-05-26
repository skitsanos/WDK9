namespace Sleepycat.DbXml
{
    using Sleepycat.Db;
    using Sleepycat.DbXml.Internal;
    using System;
    using System.IO;

    public class Manager : IDisposable
    {
        private XmlManager mgr_;

        public Manager() : this(new Sleepycat.Db.Environment(), new ManagerConfig())
        {
        }

        public Manager(Sleepycat.Db.Environment env) : this(env, new ManagerConfig())
        {
        }

        internal Manager(XmlManager m)
        {
            this.mgr_ = m;
        }

        public Manager(ManagerConfig config) : this(new Sleepycat.Db.Environment(), config)
        {
        }

        public Manager(Sleepycat.Db.Environment env, ManagerConfig config)
        {
            if (config.AdoptEnvironment)
            {
                env.Disown();
            }
            this.mgr_ = new XmlManager(env.Internal, config.Flags);
        }

        public Container CreateContainer(Transaction txn, string name)
        {
            return Container.Create(this.mgr_.createContainer(Transaction.ToInternal(txn), name));
        }

        public Container CreateContainer(Transaction txn, string name, ContainerConfig config)
        {
            return Container.Create(this.mgr_.createContainer(Transaction.ToInternal(txn), name, config.Flags, config.RawType, config.Mode));
        }

        public Document CreateDocument()
        {
            return Document.Create(this.mgr_.createDocument());
        }

        public InputStream CreateInputStream(Stream s)
        {
            return new NativeInputStream(s);
        }

        public InputStream CreateLocalFileInputStream(string filename)
        {
            return new BuiltInInputStream(this.mgr_.createLocalFileInputStream(filename));
        }

        public Modify CreateModify()
        {
            return Modify.Create(this.mgr_.createModify());
        }

        public QueryContext CreateQueryContext()
        {
            return this.CreateQueryContext(ReturnType.LiveValues, EvaluationType.Eager);
        }

        public QueryContext CreateQueryContext(ReturnType rt)
        {
            return this.CreateQueryContext(rt, EvaluationType.Eager);
        }

        public QueryContext CreateQueryContext(ReturnType rt, EvaluationType et)
        {
            return QueryContext.Create(this.mgr_.createQueryContext(QueryContext.ReturnTypeToInt(rt), QueryContext.EvaluationTypeToInt(et)));
        }

        public Results CreateResults()
        {
            return Results.Create(this.mgr_.createResults());
        }

        public InputStream CreateStdInInputStream()
        {
            return new BuiltInInputStream(this.mgr_.createStdInInputStream());
        }

        public Transaction CreateTransaction()
        {
            return Transaction.Create(this.mgr_.createTransaction());
        }

        public Transaction CreateTransaction(TransactionConfig config)
        {
            return Transaction.Create(this.mgr_.createTransaction(config.Flags));
        }

        public UpdateContext CreateUpdateContext()
        {
            return UpdateContext.Create(this.mgr_.createUpdateContext());
        }

        public InputStream CreateURLInputStream(string baseId, string systemId)
        {
            return new BuiltInInputStream(this.mgr_.createURLInputStream(baseId, systemId));
        }

        public InputStream CreateURLInputStream(string baseId, string systemId, string publicId)
        {
            return new BuiltInInputStream(this.mgr_.createURLInputStream(baseId, systemId, publicId));
        }

        public void Dispose()
        {
            this.mgr_.Dispose();
            GC.SuppressFinalize(this);
        }

        public void DumpContainer(string name, string filename)
        {
            this.mgr_.dumpContainer(name, filename);
        }

        ~Manager()
        {
            this.Dispose();
        }

        public void LoadContainer(string name, string filename, UpdateContext uc)
        {
            this.mgr_.loadContainer(name, filename, UpdateContext.ToInternal(uc));
        }

        public Container OpenContainer(Transaction txn, string name)
        {
            return Container.Create(this.mgr_.openContainer(Transaction.ToInternal(txn), name));
        }

        public Container OpenContainer(Transaction txn, string name, ContainerConfig config)
        {
            return Container.Create(this.mgr_.openContainer(Transaction.ToInternal(txn), name, config.Flags));
        }

        public QueryExpression Prepare(Transaction txn, string query, QueryContext context)
        {
            return QueryExpression.Create(this.mgr_.prepare(Transaction.ToInternal(txn), query, QueryContext.ToInternal(context)));
        }

        public Results Query(Transaction txn, string query, QueryContext context)
        {
            return Results.Create(this.mgr_.query(Transaction.ToInternal(txn), query, QueryContext.ToInternal(context)));
        }

        public Results Query(Transaction txn, string query, QueryContext context, DocumentConfig config)
        {
            return Results.Create(this.mgr_.query(Transaction.ToInternal(txn), query, QueryContext.ToInternal(context), config.Flags));
        }

        public void RegisterResolver(Sleepycat.DbXml.Resolver resolver)
        {
            this.mgr_.registerResolver(resolver.Internal);
        }

        public void RemoveContainer(Transaction txn, string name)
        {
            this.mgr_.removeContainer(Transaction.ToInternal(txn), name);
        }

        public void RenameContainer(Transaction txn, string oldName, string newName)
        {
            this.mgr_.renameContainer(Transaction.ToInternal(txn), oldName, newName);
        }

        public static void SetLogCategory(Category category, bool enabled)
        {
            switch (category)
            {
                case Category.NONE:
                    XmlManager.setLogCategory(DbXml.CATEGORY_NONE, enabled);
                    return;

                case Category.INDEXER:
                    XmlManager.setLogCategory(DbXml.CATEGORY_INDEXER, enabled);
                    return;

                case Category.QUERY:
                    XmlManager.setLogCategory(DbXml.CATEGORY_QUERY, enabled);
                    return;

                case Category.OPTIMIZER:
                    XmlManager.setLogCategory(DbXml.CATEGORY_OPTIMIZER, enabled);
                    return;

                case Category.DICTIONARY:
                    XmlManager.setLogCategory(DbXml.CATEGORY_DICTIONARY, enabled);
                    return;

                case Category.CONTAINER:
                    XmlManager.setLogCategory(DbXml.CATEGORY_CONTAINER, enabled);
                    return;

                case Category.NODESTORE:
                    XmlManager.setLogCategory(DbXml.CATEGORY_NODESTORE, enabled);
                    return;

                case Category.MANAGER:
                    XmlManager.setLogCategory(DbXml.CATEGORY_MANAGER, enabled);
                    return;

                case Category.ALL:
                    XmlManager.setLogCategory(DbXml.CATEGORY_ALL, enabled);
                    return;
            }
        }

        public static void SetLogLevel(Level level, bool enabled)
        {
            switch (level)
            {
                case Level.NONE:
                    XmlManager.setLogLevel(DbXml.LEVEL_NONE, enabled);
                    return;

                case Level.DEBUG:
                    XmlManager.setLogLevel(DbXml.LEVEL_DEBUG, enabled);
                    return;

                case Level.INFO:
                    XmlManager.setLogLevel(DbXml.LEVEL_INFO, enabled);
                    return;

                case Level.WARNING:
                    XmlManager.setLogLevel(DbXml.LEVEL_WARNING, enabled);
                    return;

                case Level.ERROR:
                    XmlManager.setLogLevel(DbXml.LEVEL_ERROR, enabled);
                    return;

                case Level.ALL:
                    XmlManager.setLogLevel(DbXml.LEVEL_ALL, enabled);
                    return;
            }
        }

        public void UpgradeContainer(string name, UpdateContext uc)
        {
            this.mgr_.upgradeContainer(name, UpdateContext.ToInternal(uc));
        }

        public void VerifyContainer(string name, string filename, VerifyConfig config)
        {
            this.mgr_.verifyContainer(name, filename, config.Flags);
        }

        public ContainerConfig DefaultContainerConfig
        {
            get
            {
                return new ContainerConfig(this.mgr_.getDefaultContainerFlags(), this.mgr_.getDefaultContainerType(), 0);
            }
            set
            {
                this.mgr_.setDefaultContainerFlags(value.Flags);
                this.mgr_.setDefaultContainerType(value.RawType);
            }
        }

        public int DefaultPageSize
        {
            get
            {
                return (int) this.mgr_.getDefaultPageSize();
            }
            set
            {
                this.mgr_.setDefaultPageSize((uint) value);
            }
        }

        public Sleepycat.Db.Environment Environment
        {
            get
            {
                return new Sleepycat.Db.Environment(this.mgr_.getDbEnv());
            }
        }

        public string Home
        {
            get
            {
                return this.mgr_.getHome();
            }
        }

        public static string Version
        {
            get
            {
                return XmlManager.get_version_string();
            }
        }

        public static int VersionMajor
        {
            get
            {
                return XmlManager.get_version_major();
            }
        }

        public static int VersionMinor
        {
            get
            {
                return XmlManager.get_version_minor();
            }
        }

        public static int VersionPatch
        {
            get
            {
                return XmlManager.get_version_patch();
            }
        }

        public enum Category
        {
            NONE,
            INDEXER,
            QUERY,
            OPTIMIZER,
            DICTIONARY,
            CONTAINER,
            NODESTORE,
            MANAGER,
            ALL
        }

        public enum Level
        {
            NONE,
            DEBUG,
            INFO,
            WARNING,
            ERROR,
            ALL
        }
    }
}

