namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class Transaction : IDisposable
    {
        private XmlTransaction txn_;

        private Transaction(XmlTransaction t)
        {
            this.txn_ = t;
        }

        public void Abort()
        {
            this.txn_.abort();
            this.Dispose();
        }

        public void Commit()
        {
            this.txn_.commit();
            this.Dispose();
        }

        public void CommitNoSync()
        {
            this.txn_.commit((uint) DbXml.DB_TXN_NOSYNC);
            this.Dispose();
        }

        public void CommitSync()
        {
            this.txn_.commit((uint) DbXml.DB_TXN_SYNC);
            this.Dispose();
        }

        internal static Transaction Create(XmlTransaction v)
        {
            if (v == null)
            {
                return null;
            }
            return new Transaction(v);
        }

        public Transaction CreateChild(TransactionConfig config)
        {
            return new Transaction(this.txn_.createChild(config.Flags));
        }

        public void Dispose()
        {
            this.txn_.Dispose();
        }

        ~Transaction()
        {
            this.Dispose();
        }

        internal static XmlTransaction ToInternal(Transaction txn)
        {
            if (txn == null)
            {
                return null;
            }
            return txn.txn_;
        }
    }
}

