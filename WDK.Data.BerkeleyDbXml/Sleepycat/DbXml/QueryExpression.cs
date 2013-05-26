namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class QueryExpression : IDisposable
    {
        private XmlQueryExpression qe_;

        private QueryExpression(XmlQueryExpression q)
        {
            this.qe_ = q;
        }

        internal static QueryExpression Create(XmlQueryExpression v)
        {
            if (v == null)
            {
                return null;
            }
            return new QueryExpression(v);
        }

        public void Dispose()
        {
            this.qe_.Dispose();
            GC.SuppressFinalize(this);
        }

        public Results Execute(Transaction txn, QueryContext context, DocumentConfig config)
        {
            return Results.Create(this.qe_.execute(Transaction.ToInternal(txn), QueryContext.ToInternal(context), config.Flags));
        }

        public Results Execute(Transaction txn, Value contextItem, QueryContext context, DocumentConfig config)
        {
            return Results.Create(this.qe_.execute(Transaction.ToInternal(txn), Value.ToInternal(contextItem), QueryContext.ToInternal(context), config.Flags));
        }

        ~QueryExpression()
        {
            this.Dispose();
        }

        internal static XmlQueryExpression ToInternal(QueryExpression v)
        {
            if (v == null)
            {
                return null;
            }
            return v.Internal;
        }

        private XmlQueryExpression Internal
        {
            get
            {
                return this.qe_;
            }
        }

        public string Query
        {
            get
            {
                return this.qe_.getQuery();
            }
        }

        public string QueryPlan
        {
            get
            {
                return this.qe_.getQueryPlan();
            }
        }
    }
}

