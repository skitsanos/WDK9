namespace Sleepycat.DbXml.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class DelegateInputStream : XmlInputStream
    {
        private IntPtr swigCPtr;

        protected DelegateInputStream() : this(IntPtr.Zero, false)
        {
        }

        internal DelegateInputStream(IntPtr cPtr, bool cMemoryOwn) : base(DbXmlPINVOKE.DelegateInputStreamUpcast(cPtr), cMemoryOwn)
        {
            this.swigCPtr = cPtr;
        }

        public static DelegateInputStream create(CurPosDelegate c, ReadBytesDelegate r)
        {
            IntPtr cPtr = DbXmlPINVOKE.DelegateInputStream_create(c, r);
            if (!(cPtr == IntPtr.Zero))
            {
                return new DelegateInputStream(cPtr, true);
            }
            return null;
        }

        public override void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && base.swigCMemOwn)
            {
                base.swigCMemOwn = false;
                DbXmlPINVOKE.delete_DelegateInputStream(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
            base.Dispose();
        }

        ~DelegateInputStream()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(DelegateInputStream obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public delegate uint CurPosDelegate();

        public delegate uint ReadBytesDelegate(IntPtr toFill, uint maxToRead);
    }
}

