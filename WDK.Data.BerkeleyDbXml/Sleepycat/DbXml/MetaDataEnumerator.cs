namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;
    using System.Collections;

    public class MetaDataEnumerator : IEnumerator, IDisposable
    {
        private MetaData current_ = null;
        private XmlMetaDataIterator mdi_;

        private MetaDataEnumerator(XmlMetaDataIterator i)
        {
            this.mdi_ = i;
        }

        internal static MetaDataEnumerator Create(XmlMetaDataIterator v)
        {
            if (v == null)
            {
                return null;
            }
            return new MetaDataEnumerator(v);
        }

        public void Dispose()
        {
            this.mdi_.Dispose();
            GC.SuppressFinalize(this);
        }

        ~MetaDataEnumerator()
        {
            this.Dispose();
        }

        public bool MoveNext()
        {
            XmlMetaData md = this.mdi_.next();
            if (md == null)
            {
                this.setCurrent(null);
                return false;
            }
            this.setCurrent(new MetaData(md));
            return true;
        }

        public void Reset()
        {
            this.mdi_.reset();
            this.setCurrent(null);
        }

        private void setCurrent(MetaData md)
        {
            if (this.current_ != null)
            {
                this.current_.Dispose();
            }
            this.current_ = md;
        }

        public MetaData Current
        {
            get
            {
                return this.current_;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.current_;
            }
        }
    }
}

