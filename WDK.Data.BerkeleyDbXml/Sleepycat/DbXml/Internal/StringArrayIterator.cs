namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class StringArrayIterator : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected StringArrayIterator() : this(IntPtr.Zero, false)
        {
        }

        internal StringArrayIterator(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_StringArrayIterator(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~StringArrayIterator()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(StringArrayIterator obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public string next()
        {
            return DbXmlPINVOKE.StringArrayIterator_next(this.swigCPtr);
        }

        public void reset()
        {
            DbXmlPINVOKE.StringArrayIterator_reset(this.swigCPtr);
        }
    }
}

