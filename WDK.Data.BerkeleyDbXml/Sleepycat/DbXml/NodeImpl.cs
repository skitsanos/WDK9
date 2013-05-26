namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    internal class NodeImpl : Node, IDisposable
    {
        private XmlValue v_;

        internal NodeImpl(XmlValue v)
        {
            this.v_ = v;
        }

        internal override XmlValue CreateXmlValue()
        {
            return new XmlValue(this.v_);
        }

        public override void Dispose()
        {
            this.v_.Dispose();
            GC.SuppressFinalize(this);
        }

        private static string emptyToNull(string val)
        {
            if ((val != null) && val.Equals(""))
            {
                return null;
            }
            return val;
        }

        public override bool Equals(object o)
        {
            if (o is Value)
            {
                return this.v_.equals(Value.ToInternal((Value) o));
            }
            if (o is NodeImpl)
            {
                return this.v_.equals(((NodeImpl) o).Internal);
            }
            if (o is Node)
            {
                using (XmlValue value2 = Node.CreateXmlValue((Node) o))
                {
                    return this.v_.equals(value2);
                }
            }
            return false;
        }

        ~NodeImpl()
        {
            this.Dispose();
        }

        public override int GetHashCode()
        {
            return (this.ToString().GetHashCode() ^ Value.ValueType.NODE.GetHashCode());
        }

        public override string ToString()
        {
            return this.v_.asString();
        }

        public override Results Attributes
        {
            get
            {
                if (this.Type == Node.NodeType.ELEMENT)
                {
                    return Results.Create(this.v_.getAttributes());
                }
                return null;
            }
        }

        public override Node FirstChild
        {
            get
            {
                return Node.CreateNode(this.v_.getFirstChild());
            }
        }

        internal XmlValue Internal
        {
            get
            {
                return this.v_;
            }
        }

        public override Node LastChild
        {
            get
            {
                return Node.CreateNode(this.v_.getLastChild());
            }
        }

        public override string LocalName
        {
            get
            {
                return emptyToNull(this.v_.getLocalName());
            }
        }

        public override string NamespaceURI
        {
            get
            {
                return emptyToNull(this.v_.getNamespaceURI());
            }
        }

        public override Node NextSibling
        {
            get
            {
                return Node.CreateNode(this.v_.getNextSibling());
            }
        }

        public override string NodeName
        {
            get
            {
                return emptyToNull(this.v_.getNodeName());
            }
        }

        public override string NodeValue
        {
            get
            {
                return emptyToNull(this.v_.getNodeValue());
            }
        }

        public override Document OwnerDocument
        {
            get
            {
                return Document.Create(this.v_.asDocument());
            }
        }

        public override Node OwnerElement
        {
            get
            {
                return Node.CreateNode(this.v_.getOwnerElement());
            }
        }

        public override Node ParentNode
        {
            get
            {
                return Node.CreateNode(this.v_.getParentNode());
            }
        }

        public override string Prefix
        {
            get
            {
                return emptyToNull(this.v_.getPrefix());
            }
        }

        public override Node PreviousSibling
        {
            get
            {
                return Node.CreateNode(this.v_.getPreviousSibling());
            }
        }

        public override Node.NodeType Type
        {
            get
            {
                return Node.IntToNodeType(this.v_.getNodeType());
            }
        }
    }
}

