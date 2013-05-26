namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class Document : Node, IDisposable
    {
        private XmlDocument doc_;

        private Document(XmlDocument d)
        {
            this.doc_ = d;
        }

        internal static Document Create(XmlDocument v)
        {
            if (v == null)
            {
                return null;
            }
            return new Document(v);
        }

        internal override XmlValue CreateXmlValue()
        {
            return new XmlValue(this.doc_);
        }

        public override void Dispose()
        {
            this.doc_.Dispose();
            GC.SuppressFinalize(this);
        }

        public override bool Equals(object o)
        {
            if (o is Value)
            {
                using (XmlValue value2 = this.CreateXmlValue())
                {
                    return value2.equals(Value.ToInternal((Value) o));
                }
            }
            if (o is NodeImpl)
            {
                using (XmlValue value3 = this.CreateXmlValue())
                {
                    return value3.equals(((NodeImpl) o).Internal);
                }
            }
            if (o is Node)
            {
                using (XmlValue value4 = this.CreateXmlValue())
                {
                    using (XmlValue value5 = Node.CreateXmlValue((Node) o))
                    {
                        return value4.equals(value5);
                    }
                }
            }
            return false;
        }

        public void FetchAllData()
        {
            this.doc_.fetchAllData();
        }

        ~Document()
        {
            this.Dispose();
        }

        public override int GetHashCode()
        {
            return (this.ToString().GetHashCode() ^ Value.ValueType.NODE.GetHashCode());
        }

        public MetaData GetMetaData(string uri, string name)
        {
            XmlValue value2 = this.doc_.getMetaData(uri, name);
            if (value2 == null)
            {
                return null;
            }
            return new MetaData(uri, name, value2);
        }

        public MetaDataEnumerator GetMetaDataEnumerator()
        {
            return MetaDataEnumerator.Create(this.doc_.getMetaDataIterator());
        }

        public void RemoveMetaData(string uri, string name)
        {
            this.doc_.removeMetaData(uri, name);
        }

        public void SetMetaData(MetaData md)
        {
            this.doc_.setMetaData(md.URI, md.Name, Value.ToInternal(md.Value));
        }

        internal static XmlDocument ToInternal(Document v)
        {
            if (v == null)
            {
                return null;
            }
            return v.Internal;
        }

        public override string ToString()
        {
            return this.StringContent;
        }

        public override Results Attributes
        {
            get
            {
                return null;
            }
        }

        public Node DocumentElement
        {
            get
            {
                Node node = null;
                Node node2 = null;
                using (node2 = this.FirstChild)
                {
                    while (node2 != null)
                    {
                        if (node2.Type == Node.NodeType.ELEMENT)
                        {
                            node = node2;
                            node2 = null;
                            return node;
                        }
                        using (var node3 = node2.NextSibling)
                        {
                            node2.Dispose();
                            node2 = node3;
                            //node3 = null;
                            continue;
                        }
                    }
                }
                return node;
            }
        }

        public override Node FirstChild
        {
            get
            {
                using (XmlValue value2 = this.CreateXmlValue())
                {
                    return Node.CreateNode(value2.getFirstChild());
                }
            }
        }

        private XmlDocument Internal
        {
            get
            {
                return this.doc_;
            }
        }

        public override Node LastChild
        {
            get
            {
                using (XmlValue value2 = this.CreateXmlValue())
                {
                    return Node.CreateNode(value2.getLastChild());
                }
            }
        }

        public override string LocalName
        {
            get
            {
                return null;
            }
        }

        public string Name
        {
            get
            {
                return this.doc_.getName();
            }
            set
            {
                this.doc_.setName(value);
            }
        }

        public override string NamespaceURI
        {
            get
            {
                return null;
            }
        }

        public override Node NextSibling
        {
            get
            {
                return null;
            }
        }

        public override string NodeName
        {
            get
            {
                return "#document";
            }
        }

        public override string NodeValue
        {
            get
            {
                return null;
            }
        }

        public override Document OwnerDocument
        {
            get
            {
                return null;
            }
        }

        public override Node OwnerElement
        {
            get
            {
                return null;
            }
        }

        public override Node ParentNode
        {
            get
            {
                return null;
            }
        }

        public override string Prefix
        {
            get
            {
                return null;
            }
        }

        public override Node PreviousSibling
        {
            get
            {
                return null;
            }
        }

        public Data RawContent
        {
            get
            {
                return new Data(this.doc_.getContent());
            }
            set
            {
                this.doc_.setContent(value.Internal);
            }
        }

        public InputStream StreamContent
        {
            get
            {
                return new BuiltInInputStream(this.doc_.getContentAsXmlInputStream());
            }
            set
            {
                using (value)
                {
                    value.Internal.disownCPtr();
                    this.doc_.setContentAsXmlInputStream(value.Internal);
                }
            }
        }

        public string StringContent
        {
            get
            {
                return this.doc_.getContentAsString();
            }
            set
            {
                this.doc_.setContent(value);
            }
        }

        public override Node.NodeType Type
        {
            get
            {
                return Node.NodeType.DOCUMENT;
            }
        }
    }
}

