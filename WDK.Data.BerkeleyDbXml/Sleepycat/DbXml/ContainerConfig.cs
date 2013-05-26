namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class ContainerConfig
    {
        private uint flags_;
        private int mode_;
        private int type_;

        public ContainerConfig()
        {
            this.flags_ = 0;
            this.mode_ = 0;
            this.type_ = XmlContainer.NodeContainer;
        }

        internal ContainerConfig(uint flags, int type, int mode)
        {
            this.flags_ = flags;
            this.type_ = type;
            this.mode_ = mode;
        }

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

        public bool AllowValidation
        {
            get
            {
                return ((this.flags_ & DbXml.DBXML_ALLOW_VALIDATION) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_ALLOW_VALIDATION);
            }
        }

        public bool Checksum
        {
            get
            {
                return ((this.flags_ & DbXml.DBXML_CHKSUM) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_CHKSUM);
            }
        }

        public Container.Type ContainerType
        {
            get
            {
                if (this.type_ == XmlContainer.WholedocContainer)
                {
                    return Container.Type.WholeDocContainer;
                }
                return Container.Type.NodeContainer;
            }
            set
            {
                if (value == Container.Type.WholeDocContainer)
                {
                    this.type_ = XmlContainer.WholedocContainer;
                }
                else
                {
                    this.type_ = XmlContainer.NodeContainer;
                }
            }
        }

        public bool Create
        {
            get
            {
                return ((this.flags_ & DbXml.DB_CREATE) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_CREATE);
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

        public bool Encrypt
        {
            get
            {
                return ((this.flags_ & DbXml.DBXML_ENCRYPT) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_ENCRYPT);
            }
        }

        public bool Exclusive
        {
            get
            {
                return ((this.flags_ & DbXml.DB_EXCL) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_EXCL);
            }
        }

        internal uint Flags
        {
            get
            {
                return this.flags_;
            }
        }

        public bool IndexNodes
        {
            get
            {
                return ((this.flags_ & DbXml.DBXML_INDEX_NODES) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_INDEX_NODES);
            }
        }

        public int Mode
        {
            get
            {
                return this.mode_;
            }
            set
            {
                this.mode_ = value;
            }
        }

        public bool NoMMap
        {
            get
            {
                return ((this.flags_ & DbXml.DB_NOMMAP) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_NOMMAP);
            }
        }

        internal int RawType
        {
            get
            {
                return this.type_;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return ((this.flags_ & DbXml.DB_RDONLY) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_RDONLY);
            }
        }

        public bool Threaded
        {
            get
            {
                return ((this.flags_ & DbXml.DB_THREAD) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_THREAD);
            }
        }

        public bool Transactional
        {
            get
            {
                return ((this.flags_ & DbXml.DBXML_TRANSACTIONAL) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DBXML_TRANSACTIONAL);
            }
        }

        public bool XACreate
        {
            get
            {
                return ((this.flags_ & DbXml.DB_XA_CREATE) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_XA_CREATE);
            }
        }
    }
}

