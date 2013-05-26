namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;
    using System.Runtime.InteropServices;

    public class Data : IDisposable
    {
        private XmlData data_;
        private bool freeHandle_;
        private GCHandle gcHandle_;

        public Data() : this(new XmlData())
        {
        }

        internal Data(XmlData d)
        {
            this.data_ = d;
            this.freeHandle_ = false;
        }

        public Data(byte[] data) : this()
        {
            this.ByteArrayData = data;
        }

        public Data(IntPtr data, int size) : this(new XmlData(data, (uint) size))
        {
        }

        public void Dispose()
        {
            this.data_.Dispose();
            if (this.freeHandle_)
            {
                this.gcHandle_.Free();
                this.freeHandle_ = false;
            }
            GC.SuppressFinalize(this);
        }

        ~Data()
        {
            this.Dispose();
        }

        public byte[] ByteArrayData
        {
            get
            {
                int size = this.Size;
                byte[] destination = new byte[size];
                Marshal.Copy(this.IntPtrData, destination, 0, size);
                return destination;
            }
            set
            {
                if (this.freeHandle_)
                {
                    this.gcHandle_.Free();
                    this.freeHandle_ = false;
                }
                this.gcHandle_ = GCHandle.Alloc(value, GCHandleType.Pinned);
                this.freeHandle_ = true;
                this.IntPtrData = this.gcHandle_.AddrOfPinnedObject();
                this.Size = value.Length;
            }
        }

        internal XmlData Internal
        {
            get
            {
                return this.data_;
            }
        }

        public IntPtr IntPtrData
        {
            get
            {
                return this.data_.get_data();
            }
            set
            {
                this.data_.set_data(value);
                if (this.freeHandle_)
                {
                    this.gcHandle_.Free();
                    this.freeHandle_ = false;
                }
            }
        }

        public int Size
        {
            get
            {
                return (int) this.data_.get_size();
            }
            set
            {
                this.data_.set_size((uint) value);
            }
        }
    }
}

