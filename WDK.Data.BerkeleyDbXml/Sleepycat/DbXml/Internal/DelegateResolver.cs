namespace Sleepycat.DbXml.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class DelegateResolver : XmlResolver
    {
        private IntPtr swigCPtr;

        protected DelegateResolver() : this(IntPtr.Zero, false)
        {
        }

        internal DelegateResolver(IntPtr cPtr, bool cMemoryOwn) : base(DbXmlPINVOKE.DelegateResolverUpcast(cPtr), cMemoryOwn)
        {
            this.swigCPtr = cPtr;
        }

        public DelegateResolver(ResolveDocumentDelegate d, ResolveCollectionDelegate c, ResolveSchemaDelegate s, ResolveEntityDelegate e) : this(DbXmlPINVOKE.new_DelegateResolver(d, c, s, e), true)
        {
        }

        public override void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && base.swigCMemOwn)
            {
                base.swigCMemOwn = false;
                DbXmlPINVOKE.delete_DelegateResolver(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
            base.Dispose();
        }

        ~DelegateResolver()
        {
            this.Dispose();
        }

        internal static IntPtr getCPtr(DelegateResolver obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public delegate bool ResolveCollectionDelegate(IntPtr txn, IntPtr mgr, string uri, IntPtr res);

        public delegate bool ResolveDocumentDelegate(IntPtr txn, IntPtr mgr, string uri, IntPtr val);

        public delegate IntPtr ResolveEntityDelegate(IntPtr txn, IntPtr mgr, string systemId, string publicId);

        public delegate IntPtr ResolveSchemaDelegate(IntPtr txn, IntPtr mgr, string schemaLocation, string nameSpace);
    }
}

