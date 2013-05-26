namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class Container : IDisposable
    {
        private XmlContainer cont_;

        private Container(XmlContainer c)
        {
            this.cont_ = c;
        }

        public bool AddAlias(string alias)
        {
            return this.cont_.addAlias(alias);
        }

        public void AddDefaultIndex(Transaction txn, string index, UpdateContext uc)
        {
            this.cont_.addDefaultIndex(Transaction.ToInternal(txn), index, UpdateContext.ToInternal(uc));
        }

        public void AddIndex(Transaction txn, IndexSpecification.Entry entry, UpdateContext uc)
        {
            this.cont_.addIndex(Transaction.ToInternal(txn), entry.URI, entry.Name, entry.Index, UpdateContext.ToInternal(uc));
        }

        internal static Container Create(XmlContainer v)
        {
            if (v == null)
            {
                return null;
            }
            return new Container(v);
        }

        public void DeleteDefaultIndex(Transaction txn, string index, UpdateContext uc)
        {
            this.cont_.deleteDefaultIndex(Transaction.ToInternal(txn), index, UpdateContext.ToInternal(uc));
        }

        public void DeleteDocument(Transaction txn, Document document, UpdateContext context)
        {
            this.cont_.deleteDocument(Transaction.ToInternal(txn), Document.ToInternal(document), UpdateContext.ToInternal(context));
        }

        public void DeleteDocument(Transaction txn, string name, UpdateContext context)
        {
            this.cont_.deleteDocument(Transaction.ToInternal(txn), name, UpdateContext.ToInternal(context));
        }

        public void DeleteIndex(Transaction txn, IndexSpecification.Entry entry, UpdateContext uc)
        {
            this.cont_.deleteIndex(Transaction.ToInternal(txn), entry.URI, entry.Name, entry.Index, UpdateContext.ToInternal(uc));
        }

        public void Dispose()
        {
            this.cont_.Dispose();
            GC.SuppressFinalize(this);
        }

        ~Container()
        {
            this.Dispose();
        }

        public Results GetAllDocuments(Transaction txn, DocumentConfig config)
        {
            return Results.Create(this.cont_.getAllDocuments(Transaction.ToInternal(txn), config.Flags));
        }

        public Document GetDocument(Transaction txn, string name)
        {
            return Document.Create(this.cont_.getDocument(Transaction.ToInternal(txn), name));
        }

        public Document GetDocument(Transaction txn, string name, DocumentConfig config)
        {
            return Document.Create(this.cont_.getDocument(Transaction.ToInternal(txn), name, config.Flags));
        }

        public IndexSpecification GetIndexSpecification(Transaction txn)
        {
            return new IndexSpecification(this.cont_.getIndexSpecification(Transaction.ToInternal(txn)));
        }

        public int GetNumDocuments(Transaction txn)
        {
            return (int) this.cont_.getNumDocuments(Transaction.ToInternal(txn));
        }

        public Results LookupIndex(Transaction txn, QueryContext context, IndexSpecification.Entry entry)
        {
            return this.LookupIndex(txn, context, entry, null, new DocumentConfig());
        }

        public Results LookupIndex(Transaction txn, QueryContext context, IndexSpecification.Entry entry, Value value)
        {
            return this.LookupIndex(txn, context, entry, value, new DocumentConfig());
        }

        public Results LookupIndex(Transaction txn, QueryContext context, IndexSpecification.Entry entry, Value value, DocumentConfig config)
        {
            return Results.Create(this.cont_.lookupIndex(Transaction.ToInternal(txn), QueryContext.ToInternal(context), entry.URI, entry.Name, entry.Index, Value.ToInternal(value), config.Flags));
        }

        public Results LookupIndex(Transaction txn, QueryContext context, IndexSpecification.Entry entry, string parent_uri, string parent_name)
        {
            return this.LookupIndex(txn, context, entry, parent_uri, parent_name, null, new DocumentConfig());
        }

        public Results LookupIndex(Transaction txn, QueryContext context, IndexSpecification.Entry entry, string parent_uri, string parent_name, Value value)
        {
            return this.LookupIndex(txn, context, entry, parent_uri, parent_name, value, new DocumentConfig());
        }

        public Results LookupIndex(Transaction txn, QueryContext context, IndexSpecification.Entry entry, string parent_uri, string parent_name, Value value, DocumentConfig config)
        {
            return Results.Create(this.cont_.lookupIndex(Transaction.ToInternal(txn), QueryContext.ToInternal(context), entry.URI, entry.Name, parent_uri, parent_name, entry.Index, Value.ToInternal(value), config.Flags));
        }

        public Statistics LookupStatistics(Transaction txn, IndexSpecification.Entry entry)
        {
            return this.LookupStatistics(txn, entry, null);
        }

        public Statistics LookupStatistics(Transaction txn, IndexSpecification.Entry entry, Value value)
        {
            return Statistics.Create(this.cont_.lookupStatistics(Transaction.ToInternal(txn), entry.URI, entry.Name, entry.Index, Value.ToInternal(value)));
        }

        public Statistics LookupStatistics(Transaction txn, IndexSpecification.Entry entry, string parent_uri, string parent_name)
        {
            return this.LookupStatistics(txn, entry, parent_uri, parent_name, null);
        }

        public Statistics LookupStatistics(Transaction txn, IndexSpecification.Entry entry, string parent_uri, string parent_name, Value value)
        {
            return Statistics.Create(this.cont_.lookupStatistics(Transaction.ToInternal(txn), entry.URI, entry.Name, parent_uri, parent_name, entry.Index, Value.ToInternal(value)));
        }

        public void PutDocument(Transaction txn, Document document, UpdateContext context, DocumentConfig config)
        {
            this.cont_.putDocument(Transaction.ToInternal(txn), Document.ToInternal(document), UpdateContext.ToInternal(context), config.Flags);
        }

        public string PutDocument(Transaction txn, string name, InputStream input, UpdateContext context, DocumentConfig config)
        {
            using (input)
            {
                input.Internal.disownCPtr();
                return this.cont_.putDocument(Transaction.ToInternal(txn), name, input.Internal, UpdateContext.ToInternal(context), config.Flags);
            }
        }

        public string PutDocument(Transaction txn, string name, string contents, UpdateContext context, DocumentConfig config)
        {
            return this.cont_.putDocument(Transaction.ToInternal(txn), name, contents, UpdateContext.ToInternal(context), config.Flags);
        }

        public bool RemoveAlias(string alias)
        {
            return this.cont_.removeAlias(alias);
        }

        public void ReplaceDefaultIndex(Transaction txn, string index, UpdateContext uc)
        {
            this.cont_.replaceDefaultIndex(Transaction.ToInternal(txn), index, UpdateContext.ToInternal(uc));
        }

        public void ReplaceIndex(Transaction txn, IndexSpecification.Entry entry, UpdateContext uc)
        {
            this.cont_.replaceIndex(Transaction.ToInternal(txn), entry.URI, entry.Name, entry.Index, UpdateContext.ToInternal(uc));
        }

        public void SetIndexSpecification(Transaction txn, IndexSpecification index, UpdateContext uc)
        {
            this.cont_.setIndexSpecification(Transaction.ToInternal(txn), index.Internal, UpdateContext.ToInternal(uc));
        }

        public void Sync()
        {
            this.cont_.sync();
        }

        public void UpdateDocument(Transaction txn, Document document, UpdateContext context)
        {
            this.cont_.updateDocument(Transaction.ToInternal(txn), Document.ToInternal(document), UpdateContext.ToInternal(context));
        }

        public Type ContainerType
        {
            get
            {
                if (this.cont_.getContainerType() == XmlContainer.NodeContainer)
                {
                    return Type.NodeContainer;
                }
                return Type.WholeDocContainer;
            }
        }

        public Sleepycat.DbXml.Manager Manager
        {
            get
            {
                return new Sleepycat.DbXml.Manager(this.cont_.getManager());
            }
        }

        public string Name
        {
            get
            {
                return this.cont_.getName();
            }
        }

        public enum Type
        {
            WholeDocContainer,
            NodeContainer
        }
    }
}

