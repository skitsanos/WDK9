namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class Statistics
    {
        private double indexedKeys_;
        private double sumKeyValueSize_;
        private double uniqueKeys_;

        private Statistics(XmlStatistics s)
        {
            using (s)
            {
                this.indexedKeys_ = s.getNumberOfIndexedKeys();
                this.uniqueKeys_ = s.getNumberOfUniqueKeys();
                this.sumKeyValueSize_ = s.getSumKeyValueSize();
            }
        }

        internal static Statistics Create(XmlStatistics v)
        {
            if (v == null)
            {
                return null;
            }
            return new Statistics(v);
        }

        public double NumberOfIndexedKeys
        {
            get
            {
                return this.indexedKeys_;
            }
        }

        public double NumberOfUniqueKeys
        {
            get
            {
                return this.uniqueKeys_;
            }
        }

        public double SumKeyValueSize
        {
            get
            {
                return this.sumKeyValueSize_;
            }
        }
    }
}

