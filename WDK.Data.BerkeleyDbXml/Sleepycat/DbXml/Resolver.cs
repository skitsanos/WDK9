namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public abstract class Resolver : IDisposable
    {
        private XmlResolver res_;
        private DelegateResolver.ResolveCollectionDelegate resolveCollectionDelegate_;
        private DelegateResolver.ResolveDocumentDelegate resolveDocumentDelegate_;
        private DelegateResolver.ResolveEntityDelegate resolveEntityDelegate_;
        private DelegateResolver.ResolveSchemaDelegate resolveSchemaDelegate_;

        protected Resolver()
        {
            this.resolveDocumentDelegate_ = new DelegateResolver.ResolveDocumentDelegate(this.ResolveDocumentWrapper);
            this.resolveCollectionDelegate_ = new DelegateResolver.ResolveCollectionDelegate(this.ResolveCollectionWrapper);
            this.resolveSchemaDelegate_ = new DelegateResolver.ResolveSchemaDelegate(this.ResolveSchemaWrapper);
            this.resolveEntityDelegate_ = new DelegateResolver.ResolveEntityDelegate(this.ResolveEntityWrapper);
            this.res_ = new DelegateResolver(this.resolveDocumentDelegate_, this.resolveCollectionDelegate_, this.resolveSchemaDelegate_, this.resolveEntityDelegate_);
        }

        public void Dispose()
        {
            this.res_.Dispose();
            GC.SuppressFinalize(this);
        }

        ~Resolver()
        {
            this.Dispose();
        }

        public virtual Results ResolveCollection(Transaction txn, Manager mgr, string uri)
        {
            return null;
        }

        private bool ResolveCollectionWrapper(IntPtr txn, IntPtr mgr, string uri, IntPtr res)
        {
            bool flag;
            using (Transaction transaction = (txn == IntPtr.Zero) ? null : Transaction.Create(new XmlTransaction(txn, false)))
            {
                using (Manager manager = (mgr == IntPtr.Zero) ? null : new Manager(new XmlManager(mgr, false)))
                {
                    using (Results results = this.ResolveCollection(transaction, manager, uri))
                    {
                        if (results == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            using (XmlResults results2 = (res == IntPtr.Zero) ? null : new XmlResults(res, false))
                            {
                                results.Reset();
                                while (results.MoveNext())
                                {
                                    results2.add(Value.ToInternal(results.Current));
                                }
                                flag = true;
                            }
                        }
                    }
                }
            }
            return flag;
        }

        public virtual Document ResolveDocument(Transaction txn, Manager mgr, string uri)
        {
            return null;
        }

        private bool ResolveDocumentWrapper(IntPtr txn, IntPtr mgr, string uri, IntPtr val)
        {
            bool flag;
            using (Transaction transaction = (txn == IntPtr.Zero) ? null : Transaction.Create(new XmlTransaction(txn, false)))
            {
                using (Manager manager = (mgr == IntPtr.Zero) ? null : new Manager(new XmlManager(mgr, false)))
                {
                    using (Document document = this.ResolveDocument(transaction, manager, uri))
                    {
                        if (document == null)
                        {
                            flag = false;
                        }
                        else
                        {
                            using (Value value2 = new Value(document))
                            {
                                using (XmlValue value3 = (val == IntPtr.Zero) ? null : new XmlValue(val, false))
                                {
                                    XmlValue.setValue(value3, Value.ToInternal(value2));
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }
            return flag;
        }

        public virtual InputStream ResolveEntity(Transaction txn, Manager mgr, string systemId, string publicId)
        {
            return null;
        }

        private IntPtr ResolveEntityWrapper(IntPtr txn, IntPtr mgr, string systemId, string publicId)
        {
            IntPtr ptr;
            using (Transaction transaction = (txn == IntPtr.Zero) ? null : Transaction.Create(new XmlTransaction(txn, false)))
            {
                using (Manager manager = (mgr == IntPtr.Zero) ? null : new Manager(new XmlManager(mgr, false)))
                {
                    using (InputStream stream = this.ResolveEntity(transaction, manager, systemId, publicId))
                    {
                        if (stream == null)
                        {
                            return IntPtr.Zero;
                        }
                        stream.Internal.disownCPtr();
                        ptr = XmlInputStream.getCPtr(stream.Internal);
                    }
                }
            }
            return ptr;
        }

        public virtual InputStream ResolveSchema(Transaction txn, Manager mgr, string schemaLocation, string nameSpace)
        {
            return null;
        }

        private IntPtr ResolveSchemaWrapper(IntPtr txn, IntPtr mgr, string schemaLocation, string nameSpace)
        {
            IntPtr ptr;
            using (Transaction transaction = (txn == IntPtr.Zero) ? null : Transaction.Create(new XmlTransaction(txn, false)))
            {
                using (Manager manager = (mgr == IntPtr.Zero) ? null : new Manager(new XmlManager(mgr, false)))
                {
                    using (InputStream stream = this.ResolveSchema(transaction, manager, schemaLocation, nameSpace))
                    {
                        if (stream == null)
                        {
                            return IntPtr.Zero;
                        }
                        stream.Internal.disownCPtr();
                        ptr = XmlInputStream.getCPtr(stream.Internal);
                    }
                }
            }
            return ptr;
        }

        internal XmlResolver Internal
        {
            get
            {
                return this.res_;
            }
        }
    }
}

