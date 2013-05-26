namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class DocumentConfig
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

        internal uint Flags
        {
            get
            {
                return this.flags_;
            }
        }

        public bool GenerateName
        {
            get
            {
                return ((this.flags_ & DbXml.DBXML_GEN_NAME) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_GEN_NAME);
            }
        }

        public bool LazyDocs
        {
            get
            {
                return ((this.flags_ & DbXml.DBXML_LAZY_DOCS) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_LAZY_DOCS);
            }
        }

        public Sleepycat.DbXml.LockMode LockMode
        {
            get
            {
                if ((this.flags_ & DbXml.DB_DIRTY_READ) != 0)
                {
                    return Sleepycat.DbXml.LockMode.DIRTY_READ;
                }
                if ((this.flags_ & DbXml.DB_DEGREE_2) != 0)
                {
                    return Sleepycat.DbXml.LockMode.DEGREE_2;
                }
                if ((this.flags_ & DbXml.DB_RMW) != 0)
                {
                    return Sleepycat.DbXml.LockMode.RMW;
                }
                return Sleepycat.DbXml.LockMode.DEFAULT;
            }
            set
            {
                switch (value)
                {
                    case Sleepycat.DbXml.LockMode.DEFAULT:
                        this.setFlag(false, (uint) DbXml.DB_DIRTY_READ);
                        this.setFlag(false, (uint) DbXml.DB_DEGREE_2);
                        this.setFlag(false, (uint) DbXml.DB_RMW);
                        return;

                    case Sleepycat.DbXml.LockMode.DEGREE_2:
                        this.setFlag(false, (uint) DbXml.DB_DIRTY_READ);
                        this.setFlag(true, (uint) DbXml.DB_DEGREE_2);
                        this.setFlag(false, (uint) DbXml.DB_RMW);
                        return;

                    case Sleepycat.DbXml.LockMode.DIRTY_READ:
                        this.setFlag(true, (uint) DbXml.DB_DIRTY_READ);
                        this.setFlag(false, (uint) DbXml.DB_DEGREE_2);
                        this.setFlag(false, (uint) DbXml.DB_RMW);
                        return;

                    case Sleepycat.DbXml.LockMode.RMW:
                        this.setFlag(false, (uint) DbXml.DB_DIRTY_READ);
                        this.setFlag(false, (uint) DbXml.DB_DEGREE_2);
                        this.setFlag(true, (uint) DbXml.DB_RMW);
                        return;
                }
            }
        }
    }
}

