namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class VerifyConfig
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

        public bool Aggressive
        {
            get
            {
                return ((this.flags_ & DbXml.DB_AGGRESSIVE) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_AGGRESSIVE);
            }
        }

        internal uint Flags
        {
            get
            {
                return this.flags_;
            }
        }

        public bool Salvage
        {
            get
            {
                return ((this.flags_ & DbXml.DB_SALVAGE) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_SALVAGE);
            }
        }
    }
}

