namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class QueryContext : IDisposable
    {
        private XmlQueryContext qc_;

        private QueryContext(XmlQueryContext q)
        {
            this.qc_ = q;
        }

        public void ClearNamespaces()
        {
            this.qc_.clearNamespaces();
        }

        internal static QueryContext Create(XmlQueryContext v)
        {
            if (v == null)
            {
                return null;
            }
            return new QueryContext(v);
        }

        public void Dispose()
        {
            this.qc_.Dispose();
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

        internal static int EvaluationTypeToInt(Sleepycat.DbXml.EvaluationType et)
        {
            if (et == Sleepycat.DbXml.EvaluationType.Lazy)
            {
                return XmlQueryContext.Lazy;
            }
            return XmlQueryContext.Eager;
        }

        ~QueryContext()
        {
            this.Dispose();
        }

        public string GetNamespace(string prefix)
        {
            return emptyToNull(this.qc_.getNamespace(prefix));
        }

        public Value GetVariableValue(string name)
        {
            return Value.Create(this.qc_.getVariableValue(name));
        }

        internal static Sleepycat.DbXml.EvaluationType IntToEvaluationType(int et)
        {
            if (et == XmlQueryContext.Lazy)
            {
                return Sleepycat.DbXml.EvaluationType.Lazy;
            }
            return Sleepycat.DbXml.EvaluationType.Eager;
        }

        internal static Sleepycat.DbXml.ReturnType IntToReturnType(int rt)
        {
            if (rt == XmlQueryContext.LiveValues)
            {
                return Sleepycat.DbXml.ReturnType.LiveValues;
            }
            return Sleepycat.DbXml.ReturnType.DeadValues;
        }

        public void RemoveNamespace(string prefix)
        {
            this.qc_.removeNamespace(prefix);
        }

        internal static int ReturnTypeToInt(Sleepycat.DbXml.ReturnType rt)
        {
            if (rt == Sleepycat.DbXml.ReturnType.LiveValues)
            {
                return XmlQueryContext.LiveValues;
            }
            return XmlQueryContext.DeadValues;
        }

        public void SetNamespace(string prefix, string uri)
        {
            this.qc_.setNamespace(prefix, uri);
        }

        public void SetVariableValue(string name, Value value)
        {
            this.qc_.setVariableValue(name, Value.ToInternal(value));
        }

        internal static XmlQueryContext ToInternal(QueryContext v)
        {
            if (v == null)
            {
                return null;
            }
            return v.Internal;
        }

        public string BaseURI
        {
            get
            {
                return this.qc_.getBaseURI();
            }
            set
            {
                this.qc_.setBaseURI(value);
            }
        }

        public Sleepycat.DbXml.EvaluationType EvaluationType
        {
            get
            {
                return IntToEvaluationType(this.qc_.getEvaluationType());
            }
            set
            {
                this.qc_.setEvaluationType(EvaluationTypeToInt(value));
            }
        }

        private XmlQueryContext Internal
        {
            get
            {
                return this.qc_;
            }
        }

        public Sleepycat.DbXml.ReturnType ReturnType
        {
            get
            {
                return IntToReturnType(this.qc_.getReturnType());
            }
            set
            {
                this.qc_.setReturnType(ReturnTypeToInt(value));
            }
        }
    }
}

