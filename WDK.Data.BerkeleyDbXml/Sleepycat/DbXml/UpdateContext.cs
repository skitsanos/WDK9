namespace Sleepycat.DbXml
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class UpdateContext : IDisposable
    {
        private XmlUpdateContext uc_;

        private UpdateContext(XmlUpdateContext u)
        {
            this.uc_ = u;
        }

        internal static UpdateContext Create(XmlUpdateContext v)
        {
            if (v == null)
            {
                return null;
            }
            return new UpdateContext(v);
        }

        public void Dispose()
        {
            this.uc_.Dispose();
            GC.SuppressFinalize(this);
        }

        ~UpdateContext()
        {
            this.Dispose();
        }

        internal static XmlUpdateContext ToInternal(UpdateContext v)
        {
            if (v == null)
            {
                return null;
            }
            return v.Internal;
        }

        public bool ApplyChangesToContainer
        {
            get
            {
                return this.uc_.getApplyChangesToContainers();
            }
            set
            {
                this.uc_.setApplyChangesToContainers(value);
            }
        }

        private XmlUpdateContext Internal
        {
            get
            {
                return this.uc_;
            }
        }
    }
}

