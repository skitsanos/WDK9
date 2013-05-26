namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class MetaData : IDisposable
    {
        private string name_;
        private string uri_;
        private Sleepycat.DbXml.Value value_;
        private bool valueOwned_;

        internal MetaData(XmlMetaData md)
        {
            this.uri_ = md.get_uri();
            this.name_ = md.get_name();
            this.value_ = Sleepycat.DbXml.Value.Create(md.get_value());
            this.valueOwned_ = true;
            md.Dispose();
        }

        internal MetaData(string uri, string name, XmlValue value)
        {
            this.uri_ = uri;
            this.name_ = name;
            this.value_ = Sleepycat.DbXml.Value.Create(value);
            this.valueOwned_ = true;
        }

        public MetaData(string uri, string name, Sleepycat.DbXml.Value value)
        {
            this.uri_ = uri;
            this.name_ = name;
            this.value_ = value;
            this.valueOwned_ = false;
        }

        public void Dispose()
        {
            if (this.valueOwned_)
            {
                this.value_.Dispose();
            }
            this.valueOwned_ = false;
            GC.SuppressFinalize(this);
        }

        ~MetaData()
        {
            this.Dispose();
        }

        public string Name
        {
            get
            {
                return this.name_;
            }
        }

        public string URI
        {
            get
            {
                return this.uri_;
            }
        }

        public Sleepycat.DbXml.Value Value
        {
            get
            {
                return this.value_;
            }
        }
    }
}

