namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public abstract class Node : IDisposable
    {
        protected Node()
        {
        }

        internal static Node CreateNode(XmlValue v)
        {
            if (v == null)
            {
                return null;
            }
            if (v.getType() != XmlValue.NODE)
            {
                throw new Exception("XmlValue is not of type XmlValue.NODE");
            }
            if (v.getNodeType() == XmlValue.DOCUMENT_NODE)
            {
                Document document = Document.Create(v.asDocument());
                v.Dispose();
                return document;
            }
            return new NodeImpl(v);
        }

        internal abstract XmlValue CreateXmlValue();
        internal static XmlValue CreateXmlValue(Node n)
        {
            if (n == null)
            {
                return null;
            }
            return n.CreateXmlValue();
        }

        public abstract void Dispose();
        public abstract override bool Equals(object o);
        public abstract override int GetHashCode();
        internal static NodeType IntToNodeType(int type)
        {
            if (type == XmlValue.ELEMENT_NODE)
            {
                return NodeType.ELEMENT;
            }
            if (type == XmlValue.ATTRIBUTE_NODE)
            {
                return NodeType.ATTRIBUTE;
            }
            if (type == XmlValue.TEXT_NODE)
            {
                return NodeType.TEXT;
            }
            if (type == XmlValue.CDATA_SECTION_NODE)
            {
                return NodeType.CDATA_SECTION;
            }
            if (type == XmlValue.ENTITY_REFERENCE_NODE)
            {
                return NodeType.ENTITY_REFERENCE;
            }
            if (type == XmlValue.ENTITY_NODE)
            {
                return NodeType.ENTITY;
            }
            if (type == XmlValue.PROCESSING_INSTRUCTION_NODE)
            {
                return NodeType.PROCESSING_INSTRUCTION;
            }
            if (type == XmlValue.COMMENT_NODE)
            {
                return NodeType.COMMENT;
            }
            if (type == XmlValue.DOCUMENT_NODE)
            {
                return NodeType.DOCUMENT;
            }
            if (type == XmlValue.DOCUMENT_TYPE_NODE)
            {
                return NodeType.DOCUMENT_TYPE;
            }
            if (type == XmlValue.DOCUMENT_FRAGMENT_NODE)
            {
                return NodeType.DOCUMENT_FRAGMENT;
            }
            if (type != XmlValue.NOTATION_NODE)
            {
                throw new Exception("Node type integer not handled in nested if statements: " + type);
            }
            return NodeType.NOTATION;
        }

        public abstract Results Attributes { get; }

        public abstract Node FirstChild { get; }

        public abstract Node LastChild { get; }

        public abstract string LocalName { get; }

        public abstract string NamespaceURI { get; }

        public abstract Node NextSibling { get; }

        public abstract string NodeName { get; }

        public abstract string NodeValue { get; }

        public abstract Document OwnerDocument { get; }

        public abstract Node OwnerElement { get; }

        public abstract Node ParentNode { get; }

        public abstract string Prefix { get; }

        public abstract Node PreviousSibling { get; }

        public abstract NodeType Type { get; }

        public enum NodeType
        {
            ELEMENT,
            ATTRIBUTE,
            TEXT,
            CDATA_SECTION,
            ENTITY_REFERENCE,
            ENTITY,
            PROCESSING_INSTRUCTION,
            COMMENT,
            DOCUMENT,
            DOCUMENT_TYPE,
            DOCUMENT_FRAGMENT,
            NOTATION
        }
    }
}

