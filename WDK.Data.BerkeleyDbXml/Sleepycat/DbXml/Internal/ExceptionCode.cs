namespace Sleepycat.DbXml.Internal
{
    using System;

    internal sealed class ExceptionCode
    {
        public static readonly int CONTAINER_CLOSED = DbXmlPINVOKE.get_CONTAINER_CLOSED();
        public static readonly int CONTAINER_EXISTS = DbXmlPINVOKE.get_CONTAINER_EXISTS();
        public static readonly int CONTAINER_NOT_FOUND = DbXmlPINVOKE.get_CONTAINER_NOT_FOUND();
        public static readonly int CONTAINER_OPEN = DbXmlPINVOKE.get_CONTAINER_OPEN();
        public static readonly int DATABASE_ERROR = DbXmlPINVOKE.get_DATABASE_ERROR();
        public static readonly int DOCUMENT_NOT_FOUND = DbXmlPINVOKE.get_DOCUMENT_NOT_FOUND();
        public static readonly int DOM_PARSER_ERROR = DbXmlPINVOKE.get_DOM_PARSER_ERROR();
        public static readonly int INDEXER_PARSER_ERROR = DbXmlPINVOKE.get_INDEXER_PARSER_ERROR();
        public static readonly int INTERNAL_ERROR = DbXmlPINVOKE.get_INTERNAL_ERROR();
        public static readonly int INVALID_VALUE = DbXmlPINVOKE.get_INVALID_VALUE();
        public static readonly int LAZY_EVALUATION = DbXmlPINVOKE.get_LAZY_EVALUATION();
        public static readonly int NO_MEMORY_ERROR = DbXmlPINVOKE.get_NO_MEMORY_ERROR();
        public static readonly int NO_VARIABLE_BINDING = DbXmlPINVOKE.get_NO_VARIABLE_BINDING();
        public static readonly int NULL_POINTER = DbXmlPINVOKE.get_NULL_POINTER();
        public static readonly int TRANSACTION_ERROR = DbXmlPINVOKE.get_TRANSACTION_ERROR();
        public static readonly int UNIQUE_ERROR = DbXmlPINVOKE.get_UNIQUE_ERROR();
        public static readonly int UNKNOWN_INDEX = DbXmlPINVOKE.get_UNKNOWN_INDEX();
        public static readonly int VERSION_MISMATCH = DbXmlPINVOKE.get_VERSION_MISMATCH();
        public static readonly int XPATH_EVALUATION_ERROR = DbXmlPINVOKE.get_XPATH_EVALUATION_ERROR();
        public static readonly int XPATH_PARSER_ERROR = DbXmlPINVOKE.get_XPATH_PARSER_ERROR();
    }
}

