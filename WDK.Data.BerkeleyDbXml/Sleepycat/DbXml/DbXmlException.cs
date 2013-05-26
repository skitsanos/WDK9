namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class DbXmlException : Exception
    {
        private ExceptionCode code_;

        public DbXmlException(ExceptionCode code)
        {
            this.code_ = code;
        }

        public DbXmlException(ExceptionCode code, string message) : base(message + ", exception code = " + code.ToString())
        {
            this.code_ = code;
        }

        internal DbXmlException(int code, string message) : this(IntToExceptionCode(code), message)
        {
        }

        internal static ExceptionCode IntToExceptionCode(int code)
        {
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.INTERNAL_ERROR)
            {
                return ExceptionCode.INTERNAL_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.CONTAINER_OPEN)
            {
                return ExceptionCode.CONTAINER_OPEN;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.CONTAINER_CLOSED)
            {
                return ExceptionCode.CONTAINER_CLOSED;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.INDEXER_PARSER_ERROR)
            {
                return ExceptionCode.INDEXER_PARSER_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.DATABASE_ERROR)
            {
                return ExceptionCode.DATABASE_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.XPATH_PARSER_ERROR)
            {
                return ExceptionCode.XPATH_PARSER_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.DOM_PARSER_ERROR)
            {
                return ExceptionCode.DOM_PARSER_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.XPATH_EVALUATION_ERROR)
            {
                return ExceptionCode.XPATH_EVALUATION_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.NO_VARIABLE_BINDING)
            {
                return ExceptionCode.NO_VARIABLE_BINDING;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.LAZY_EVALUATION)
            {
                return ExceptionCode.LAZY_EVALUATION;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.CONTAINER_EXISTS)
            {
                return ExceptionCode.CONTAINER_EXISTS;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.UNKNOWN_INDEX)
            {
                return ExceptionCode.UNKNOWN_INDEX;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.INVALID_VALUE)
            {
                return ExceptionCode.INVALID_VALUE;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.VERSION_MISMATCH)
            {
                return ExceptionCode.VERSION_MISMATCH;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.CONTAINER_NOT_FOUND)
            {
                return ExceptionCode.CONTAINER_NOT_FOUND;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.TRANSACTION_ERROR)
            {
                return ExceptionCode.TRANSACTION_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.UNIQUE_ERROR)
            {
                return ExceptionCode.UNIQUE_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.NO_MEMORY_ERROR)
            {
                return ExceptionCode.NO_MEMORY_ERROR;
            }
            if (code == Sleepycat.DbXml.Internal.ExceptionCode.DOCUMENT_NOT_FOUND)
            {
                return ExceptionCode.DOCUMENT_NOT_FOUND;
            }
            return ~ExceptionCode.INTERNAL_ERROR;
        }

        public ExceptionCode Code
        {
            get
            {
                return this.code_;
            }
            set
            {
                this.code_ = value;
            }
        }

        public enum ExceptionCode
        {
            INTERNAL_ERROR,
            CONTAINER_OPEN,
            CONTAINER_CLOSED,
            INDEXER_PARSER_ERROR,
            DATABASE_ERROR,
            XPATH_PARSER_ERROR,
            DOM_PARSER_ERROR,
            XPATH_EVALUATION_ERROR,
            NO_VARIABLE_BINDING,
            LAZY_EVALUATION,
            DOCUMENT_NOT_FOUND,
            CONTAINER_EXISTS,
            UNKNOWN_INDEX,
            INVALID_VALUE,
            VERSION_MISMATCH,
            CONTAINER_NOT_FOUND,
            TRANSACTION_ERROR,
            UNIQUE_ERROR,
            NO_MEMORY_ERROR
        }
    }
}

