namespace Sleepycat.DbXml.Internal
{
    using System;
    using System.Runtime.CompilerServices;

    internal class DbEnv : IDisposable
    {
        protected bool swigCMemOwn;
        private IntPtr swigCPtr;

        protected DbEnv() : this(IntPtr.Zero, false)
        {
        }

        public DbEnv(uint flags) : this(DbXmlPINVOKE.new_DbEnv(flags), true)
        {
        }

        internal DbEnv(IntPtr cPtr, bool cMemoryOwn)
        {
            this.swigCMemOwn = cMemoryOwn;
            this.swigCPtr = cPtr;
        }

        public virtual int close(uint flags)
        {
            return DbXmlPINVOKE.DbEnv_close(this.swigCPtr, flags);
        }

        internal void disownCPtr()
        {
            this.swigCMemOwn = false;
        }

        public virtual void Dispose()
        {
            if ((this.swigCPtr != IntPtr.Zero) && this.swigCMemOwn)
            {
                this.swigCMemOwn = false;
                DbXmlPINVOKE.delete_DbEnv(this.swigCPtr);
            }
            this.swigCPtr = IntPtr.Zero;
            GC.SuppressFinalize(this);
        }

        ~DbEnv()
        {
            this.Dispose();
        }

        public uint get_cachesize_bytes()
        {
            return DbXmlPINVOKE.DbEnv_get_cachesize_bytes(this.swigCPtr);
        }

        public uint get_cachesize_gbytes()
        {
            return DbXmlPINVOKE.DbEnv_get_cachesize_gbytes(this.swigCPtr);
        }

        public uint get_cachesize_ncache()
        {
            return DbXmlPINVOKE.DbEnv_get_cachesize_ncache(this.swigCPtr);
        }

        public StringArrayIterator get_data_dirs()
        {
            IntPtr cPtr = DbXmlPINVOKE.DbEnv_get_data_dirs(this.swigCPtr);
            if (!(cPtr == IntPtr.Zero))
            {
                return new StringArrayIterator(cPtr, true);
            }
            return null;
        }

        public string get_errpfx()
        {
            return DbXmlPINVOKE.DbEnv_get_errpfx(this.swigCPtr);
        }

        public string get_home()
        {
            return DbXmlPINVOKE.DbEnv_get_home(this.swigCPtr);
        }

        public uint get_open_flags()
        {
            return DbXmlPINVOKE.DbEnv_get_open_flags(this.swigCPtr);
        }

        internal static IntPtr getCPtr(DbEnv obj)
        {
            if (obj != null)
            {
                return obj.swigCPtr;
            }
            return IntPtr.Zero;
        }

        public int lock_detect(uint flags, uint atype)
        {
            return DbXmlPINVOKE.DbEnv_lock_detect(this.swigCPtr, flags, atype);
        }

        public virtual int open(string db_home, uint flags, int mode)
        {
            return DbXmlPINVOKE.DbEnv_open(this.swigCPtr, db_home, flags, mode);
        }

        public virtual int remove(string db_home, uint flags)
        {
            return DbXmlPINVOKE.DbEnv_remove(this.swigCPtr, db_home, flags);
        }

        public virtual int set_cachesize(uint gbytes, uint bytes, int ncache)
        {
            return DbXmlPINVOKE.DbEnv_set_cachesize(this.swigCPtr, gbytes, bytes, ncache);
        }

        public virtual int set_data_dir(string arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_data_dir(this.swigCPtr, arg0);
        }

        public virtual int set_encrypt(string arg0, uint arg1)
        {
            return DbXmlPINVOKE.DbEnv_set_encrypt(this.swigCPtr, arg0, arg1);
        }

        public void set_errcall(ErrorCallbackDelegate callback)
        {
            DbXmlPINVOKE.DbEnv_set_errcall(this.swigCPtr, callback);
        }

        public void set_errpfx(string pfx)
        {
            DbXmlPINVOKE.DbEnv_set_errpfx(this.swigCPtr, pfx);
        }

        public virtual int set_flags(uint arg0, int arg1)
        {
            return DbXmlPINVOKE.DbEnv_set_flags(this.swigCPtr, arg0, arg1);
        }

        public virtual int set_lg_bsize(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lg_bsize(this.swigCPtr, arg0);
        }

        public virtual int set_lg_dir(string arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lg_dir(this.swigCPtr, arg0);
        }

        public virtual int set_lg_max(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lg_max(this.swigCPtr, arg0);
        }

        public virtual int set_lg_regionmax(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lg_regionmax(this.swigCPtr, arg0);
        }

        public virtual int set_lk_detect(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lk_detect(this.swigCPtr, arg0);
        }

        public virtual int set_lk_max_lockers(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lk_max_lockers(this.swigCPtr, arg0);
        }

        public virtual int set_lk_max_locks(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lk_max_locks(this.swigCPtr, arg0);
        }

        public virtual int set_lk_max_objects(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_lk_max_objects(this.swigCPtr, arg0);
        }

        public virtual int set_mp_mmapsize(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_mp_mmapsize(this.swigCPtr, arg0);
        }

        public virtual int set_shm_key(int arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_shm_key(this.swigCPtr, arg0);
        }

        public virtual int set_tas_spins(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_tas_spins(this.swigCPtr, arg0);
        }

        public virtual int set_timeout(int arg0, uint arg1)
        {
            return DbXmlPINVOKE.DbEnv_set_timeout(this.swigCPtr, arg0, arg1);
        }

        public virtual int set_tmp_dir(string arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_tmp_dir(this.swigCPtr, arg0);
        }

        public virtual int set_tx_max(uint arg0)
        {
            return DbXmlPINVOKE.DbEnv_set_tx_max(this.swigCPtr, arg0);
        }

        public virtual int set_verbose(uint which, int arg1)
        {
            return DbXmlPINVOKE.DbEnv_set_verbose(this.swigCPtr, which, arg1);
        }

        public static string strerror(int arg0)
        {
            return DbXmlPINVOKE.DbEnv_strerror(arg0);
        }

        public virtual int txn_checkpoint(uint kbyte, uint min, uint flags)
        {
            return DbXmlPINVOKE.DbEnv_txn_checkpoint(this.swigCPtr, kbyte, min, flags);
        }

        public static int version_major()
        {
            return DbXmlPINVOKE.DbEnv_version_major();
        }

        public static int version_minor()
        {
            return DbXmlPINVOKE.DbEnv_version_minor();
        }

        public static int version_patch()
        {
            return DbXmlPINVOKE.DbEnv_version_patch();
        }

        public static string version_string()
        {
            return DbXmlPINVOKE.DbEnv_version_string();
        }

        public delegate void ErrorCallbackDelegate(IntPtr dbenv, string errpfx, string msg);
    }
}

