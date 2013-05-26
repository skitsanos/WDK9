namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class XmlValue : IDisposable
    {
        public static readonly int ANY_SIMPLE_TYPE = DbXmlPINVOKE.get_XmlValue_ANY_SIMPLE_TYPE();
        public static readonly int ANY_URI = DbXmlPINVOKE.get_XmlValue_ANY_URI();
        public static readonly int ATTRIBUTE_NODE = DbXmlPINVOKE.get_XmlValue_ATTRIBUTE_NODE();
        public static readonly int BASE_64_BINARY = DbXmlPINVOKE.get_XmlValue_BASE_64_BINARY();
        public static readonly int BOOLEAN = DbXmlPINVOKE.get_XmlValue_BOOLEAN();
        public static readonly int CDATA_SECTION_NODE = DbXmlPINVOKE.get_XmlValue_CDATA_SECTION_NODE();
        public static readonly int COMMENT_NODE = DbXmlPINVOKE.get_XmlValue_COMMENT_NODE();
        public static readonly int DATE = DbXmlPINVOKE.get_XmlValue_DATE();
        public static readonly int DATE_TIME = DbXmlPINVOKE.get_XmlValue_DATE_TIME();
        public static readonly int DAY_TIME_DURATION = DbXmlPINVOKE.get_XmlValue_DAY_TIME_DURATION();
        public static readonly int DECIMAL = DbXmlPINVOKE.get_XmlValue_DECIMAL();
        public static readonly int DOCUMENT_FRAGMENT_NODE = DbXmlPINVOKE.get_XmlValue_DOCUMENT_FRAGMENT_NODE();
        public static readonly int DOCUMENT_NODE = DbXmlPINVOKE.get_XmlValue_DOCUMENT_NODE();
        public static readonly int DOCUMENT_TYPE_NODE = DbXmlPINVOKE.get_XmlValue_DOCUMENT_TYPE_NODE();
        public static readonly int DOUBLE = DbXmlPINVOKE.get_XmlValue_DOUBLE();
        public static readonly int DURATION = DbXmlPINVOKE.get_XmlValue_DURATION();
        public static readonly int ELEMENT_NODE = DbXmlPINVOKE.get_XmlValue_ELEMENT_NODE();
        public static readonly int ENTITY_NODE = DbXmlPINVOKE.get_XmlValue_ENTITY_NODE();
        public static readonly int ENTITY_REFERENCE_NODE = DbXmlPINVOKE.get_XmlValue_ENTITY_REFERENCE_NODE();
        public static readonly int FLOAT = DbXmlPINVOKE.get_XmlValue_FLOAT();
        public static readonly int G_DAY = DbXmlPINVOKE.get_XmlValue_G_DAY();
        public static readonly int G_MONTH = DbXmlPINVOKE.get_XmlValue_G_MONTH();
        public static readonly int G_MONTH_DAY = DbXmlPINVOKE.get_XmlValue_G_MONTH_DAY();
        public static readonly int G_YEAR = DbXmlPINVOKE.get_XmlValue_G_YEAR();
        public static readonly int G_YEAR_MONTH = DbXmlPINVOKE.get_XmlValue_G_YEAR_MONTH();
        public static readonly int HEX_BINARY = DbXmlPINVOKE.get_XmlValue_HEX_BINARY();
        public static readonly int NODE = DbXmlPINVOKE.get_XmlValue_NODE();
        public static readonly int NONE = DbXmlPINVOKE.get_XmlValue_NONE();
        public static readonly int NOTATION = DbXmlPINVOKE.get_XmlValue_NOTATION();
        public static readonly int NOTATION_NODE = DbXmlPINVOKE.get_XmlValue_NOTATION_NODE();
        public static readonly int PROCESSING_INSTRUCTION_NODE = DbXmlPINVOKE.get_XmlValue_PROCESSING_INSTRUCTION_NODE();
        public static readonly int QNAME = DbXmlPINVOKE.get_XmlValue_QNAME();
        public static readonly int STRING = DbXmlPINVOKE.get_XmlValue_STRING();
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;
        public static readonly int TEXT_NODE = DbXmlPINVOKE.get_XmlValue_TEXT_NODE();
        public static readonly int TIME = DbXmlPINVOKE.get_XmlValue_TIME();
        public static readonly int UNTYPED_ATOMIC = DbXmlPINVOKE.get_XmlValue_UNTYPED_ATOMIC();
        public static readonly int YEAR_MONTH_DURATION = DbXmlPINVOKE.get_XmlValue_YEAR_MONTH_DURATION();

        protected XmlValue() : this(IntPtr.Zero, false)
        {
        }

        public XmlValue(XmlDocument document) : this(DbXmlPINVOKE.new_XmlValue__SWIG_4(XmlDocument.getCPtrOrThrow(document)), true)
        {
        }

        internal XmlValue(XmlValue other) : this(DbXmlPINVOKE.new_XmlValue__SWIG_0(getCPtr(other)), true)
        {
        }

        public XmlValue(bool v) : this(DbXmlPINVOKE.new_XmlValue__SWIG_3(v), true)
        {
        }

        public XmlValue(double v) : this(DbXmlPINVOKE.new_XmlValue__SWIG_2(v), true)
        {
        }

        public XmlValue(string v) : this(DbXmlPINVOKE.new_XmlValue__SWIG_1(v), true)
        {
        }

        public XmlValue(int type, XmlData dbt) : this(DbXmlPINVOKE.new_XmlValue__SWIG_6(type, XmlData.getCPtr(dbt)), true)
        {
        }

        public XmlValue(int type, string v) : this(DbXmlPINVOKE.new_XmlValue__SWIG_5(type, v), true)
        {
        }

        internal XmlValue(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public bool asBoolean()
        {
            return DbXmlPINVOKE.XmlValue_asBoolean(this.swigCPtr);
        }

        public XmlDocument asDocument()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_asDocument(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlDocument(cPtr, true);
            }
            return null;
        }

        public double asNumber()
        {
            return DbXmlPINVOKE.XmlValue_asNumber(this.swigCPtr);
        }

        public string asString()
        {
            return DbXmlPINVOKE.XmlValue_asString__SWIG_0(this.swigCPtr);
        }

        public string asString(string encoding)
        {
            return DbXmlPINVOKE.XmlValue_asString__SWIG_1(this.swigCPtr, encoding);
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_XmlValue(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        public bool equals(XmlValue value)
        {
            return DbXmlPINVOKE.XmlValue_equals(this.swigCPtr, getCPtr(value));
        }

        ~XmlValue()
        {
            this.Dispose();
        }

        public XmlResults getAttributes()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_getAttributes(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlResults(cPtr, true);
            }
            return null;
        }

        internal static IntPtr getCPtr(XmlValue obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        internal static IntPtr getCPtrOrThrow(XmlValue obj)
        {
            return obj.swigCPtr;
        }

        public XmlValue getFirstChild()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_getFirstChild(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public XmlValue getLastChild()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_getLastChild(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public string getLocalName()
        {
            return DbXmlPINVOKE.XmlValue_getLocalName(this.swigCPtr);
        }

        public string getNamespaceURI()
        {
            return DbXmlPINVOKE.XmlValue_getNamespaceURI(this.swigCPtr);
        }

        public XmlValue getNextSibling()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_getNextSibling(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public string getNodeName()
        {
            return DbXmlPINVOKE.XmlValue_getNodeName(this.swigCPtr);
        }

        public short getNodeType()
        {
            return DbXmlPINVOKE.XmlValue_getNodeType(this.swigCPtr);
        }

        public string getNodeValue()
        {
            return DbXmlPINVOKE.XmlValue_getNodeValue(this.swigCPtr);
        }

        public XmlValue getOwnerElement()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_getOwnerElement(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public XmlValue getParentNode()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_getParentNode(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public string getPrefix()
        {
            return DbXmlPINVOKE.XmlValue_getPrefix(this.swigCPtr);
        }

        public XmlValue getPreviousSibling()
        {
            IntPtr cPtr = DbXmlPINVOKE.XmlValue_getPreviousSibling(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new XmlValue(cPtr, true);
            }
            return null;
        }

        public int getType()
        {
            return DbXmlPINVOKE.XmlValue_getType(this.swigCPtr);
        }

        public bool isBoolean()
        {
            return DbXmlPINVOKE.XmlValue_isBoolean(this.swigCPtr);
        }

        public bool isNode()
        {
            return DbXmlPINVOKE.XmlValue_isNode(this.swigCPtr);
        }

        public bool isNumber()
        {
            return DbXmlPINVOKE.XmlValue_isNumber(this.swigCPtr);
        }

        public bool isString()
        {
            return DbXmlPINVOKE.XmlValue_isString(this.swigCPtr);
        }

        public bool isType(int type)
        {
            return DbXmlPINVOKE.XmlValue_isType(this.swigCPtr, type);
        }

        public static void setValue(XmlValue to, XmlValue from)
        {
            DbXmlPINVOKE.XmlValue_setValue(getCPtr(to), getCPtr(from));
        }
    }
}

