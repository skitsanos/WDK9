namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class ManagerConfig
    {
        private uint mFlags_ = 0;

        private void setFlag(bool value, uint flag)
        {
            if (value)
            {
                this.mFlags_ |= flag;
            }
            else
            {
                this.mFlags_ &= ~flag;
            }
        }

        public bool AdoptEnvironment
        {
            get
            {
                return ((this.mFlags_ & DbXml.DBXML_ADOPT_DBENV) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_ADOPT_DBENV);
            }
        }

        public bool AllowAutoOpen
        {
            get
            {
                return ((this.mFlags_ & DbXml.DBXML_ALLOW_AUTO_OPEN) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_ALLOW_AUTO_OPEN);
            }
        }

        public bool AllowExternalAccess
        {
            get
            {
                return ((this.mFlags_ & DbXml.DBXML_ALLOW_EXTERNAL_ACCESS) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_ALLOW_EXTERNAL_ACCESS);
            }
        }

        internal uint Flags
        {
            get
            {
                return this.mFlags_;
            }
        }
    }
}

