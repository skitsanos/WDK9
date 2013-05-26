namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class TransactionConfig
    {
        private uint flags_ = 0;

        private void setFlag(bool value, uint flag)
        {
            if (value)
            {
                this.flags_ |= flag;
            }
            else
            {
                this.flags_ &= ~flag;
            }
        }

        public bool Degree2
        {
            get
            {
                return ((this.flags_ & DbXml.DB_DEGREE_2) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_DEGREE_2);
            }
        }

        public bool DirtyRead
        {
            get
            {
                return ((this.flags_ & DbXml.DB_DIRTY_READ) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_DIRTY_READ);
            }
        }

        internal uint Flags
        {
            get
            {
                return this.flags_;
            }
        }

        public bool NoSync
        {
            get
            {
                return ((this.flags_ & DbXml.DB_TXN_NOSYNC) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_TXN_NOSYNC);
            }
        }

        public bool NoWait
        {
            get
            {
                return ((this.flags_ & DbXml.DB_TXN_NOWAIT) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_TXN_NOWAIT);
            }
        }

        public bool Sync
        {
            get
            {
                return ((this.flags_ & DbXml.DB_TXN_SYNC) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_TXN_SYNC);
            }
        }
    }
}

