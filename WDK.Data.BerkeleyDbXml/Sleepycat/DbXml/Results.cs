namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;
    using System.Collections;

    public class Results : IEnumerator, IDisposable
    {
        private Value current_;
        private XmlResults r_;

        private Results(XmlResults r)
        {
            this.r_ = r;
        }

        public void Add(Value v)
        {
            this.r_.add(Value.ToInternal(v));
        }

        internal static Results Create(XmlResults v)
        {
            if (v == null)
            {
                return null;
            }
            return new Results(v);
        }

        public void Dispose()
        {
            this.setCurrent(null);
            this.r_.Dispose();
            GC.SuppressFinalize(this);
        }

        ~Results()
        {
            this.Dispose();
        }

        public bool MoveNext()
        {
            XmlValue v = this.r_.next();
            if (v == null)
            {
                this.setCurrent(null);
                return false;
            }
            this.setCurrent(Value.Create(v));
            return true;
        }

        public bool MovePrevious()
        {
            XmlValue v = this.r_.previous();
            if (v == null)
            {
                this.setCurrent(null);
                return false;
            }
            this.setCurrent(Value.Create(v));
            return true;
        }

        public void Reset()
        {
            this.r_.reset();
            this.setCurrent(null);
        }

        private void setCurrent(Value val)
        {
            if (this.current_ != null)
            {
                this.current_.Dispose();
            }
            this.current_ = val;
        }

        internal static XmlResults ToInternal(Results v)
        {
            if (v == null)
            {
                return null;
            }
            return v.Internal;
        }

        public Value Current
        {
            get
            {
                return this.current_;
            }
        }

        private XmlResults Internal
        {
            get
            {
                return this.r_;
            }
        }

        public int Size
        {
            get
            {
                return (int) this.r_.size();
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

