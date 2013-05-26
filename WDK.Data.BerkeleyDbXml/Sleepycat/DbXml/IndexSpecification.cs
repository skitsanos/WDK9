namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;
    using System.Collections;

    public class IndexSpecification : IEnumerator, IDisposable
    {
        private Entry current_;
        private XmlIndexSpecification is_;

        public IndexSpecification() : this(new XmlIndexSpecification())
        {
        }

        internal IndexSpecification(XmlIndexSpecification i)
        {
            this.current_ = null;
            this.is_ = i;
        }

        public void AddDefaultIndex(string index)
        {
            this.is_.addDefaultIndex(index);
        }

        public void AddIndex(Entry index)
        {
            this.is_.addIndex(index.URI, index.Name, index.Index);
        }

        public void DeleteDefaultIndex(string index)
        {
            this.is_.deleteDefaultIndex(index);
        }

        public void DeleteIndex(Entry index)
        {
            this.is_.deleteIndex(index.URI, index.Name, index.Index);
        }

        public void Dispose()
        {
            this.is_.Dispose();
            GC.SuppressFinalize(this);
        }

        ~IndexSpecification()
        {
            this.Dispose();
        }

        public string GetDefaultIndex()
        {
            return this.is_.getDefaultIndex();
        }

        public Entry GetIndex(string uri, string name)
        {
            XmlIndexDeclaration id = this.is_.find(uri, name);
            if (id != null)
            {
                return new Entry(id);
            }
            return null;
        }

        public bool MoveNext()
        {
            XmlIndexDeclaration id = this.is_.next();
            if (id == null)
            {
                this.current_ = null;
                return false;
            }
            this.current_ = new Entry(id);
            return true;
        }

        public void ReplaceDefaultIndex(string index)
        {
            this.is_.replaceDefaultIndex(index);
        }

        public void ReplaceIndex(Entry index)
        {
            this.is_.replaceIndex(index.URI, index.Name, index.Index);
        }

        public void Reset()
        {
            this.is_.reset();
            this.current_ = null;
        }

        public Entry Current
        {
            get
            {
                return this.current_;
            }
        }

        internal XmlIndexSpecification Internal
        {
            get
            {
                return this.is_;
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return this.current_;
            }
        }

        public class Entry
        {
            private string index_;
            private string name_;
            private string uri_;

            internal Entry(XmlIndexDeclaration id)
            {
                using (id)
                {
                    this.uri_ = id.get_uri();
                    this.name_ = id.get_name();
                    this.index_ = id.get_index();
                }
            }

            public Entry(string uri, string name, string index)
            {
                this.uri_ = uri;
                this.name_ = name;
                this.index_ = index;
            }

            public string Index
            {
                get
                {
                    return this.index_;
                }
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
        }
    }
}

