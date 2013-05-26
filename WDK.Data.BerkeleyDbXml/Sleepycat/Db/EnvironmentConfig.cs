namespace Sleepycat.Db
{
    using Sleepycat.DbXml.Internal;
    using System;
    using System.Collections;
    using System.IO;

    public class EnvironmentConfig
    {
        private int cachesize_;
        private string[] dataDirs_;
        private DbEnv.ErrorCallbackDelegate dbenvDelegate_;
        private string encryptPassword_;
        private Sleepycat.Db.ErrorCallback errCallback_;
        private string errPrefix_;
        private TextWriter errWriter_;
        private uint flags_;
        private int mode_;
        private int nCaches_;

        public EnvironmentConfig()
        {
            this.flags_ = 0;
            this.mode_ = 0;
            this.cachesize_ = 0x4000000;
            this.nCaches_ = 1;
            this.dataDirs_ = null;
            this.encryptPassword_ = null;
            this.errCallback_ = null;
            this.errWriter_ = null;
            this.dbenvDelegate_ = new DbEnv.ErrorCallbackDelegate(this.ErrorCallbackWrapper);
        }

        internal EnvironmentConfig(DbEnv env) : this()
        {
            this.SetFromDbEnv(env);
        }

        internal void configureDbEnv(DbEnv env, bool isOpen)
        {
            if (!isOpen)
            {
                env.set_cachesize(0, (uint) this.cachesize_, this.nCaches_);
            }
            if (this.dataDirs_ != null)
            {
                foreach (string str in this.dataDirs_)
                {
                    env.set_data_dir(str);
                }
            }
            if (this.encryptPassword_ != null)
            {
                env.set_encrypt(this.encryptPassword_, 0);
            }
            if (this.errPrefix_ != null)
            {
                env.set_errpfx(this.errPrefix_);
            }
            if ((this.errCallback_ != null) || (this.errWriter_ != null))
            {
                env.set_errcall(this.dbenvDelegate_);
            }
        }

        internal DbEnv createDbEnv()
        {
            DbEnv env = new DbEnv(0);
            this.configureDbEnv(env, false);
            return env;
        }

        private void ErrorCallbackWrapper(IntPtr dbenv, string errpfx, string msg)
        {
            if (this.errCallback_ != null)
            {
                using (Sleepycat.Db.Environment environment = (dbenv == IntPtr.Zero) ? null : new Sleepycat.Db.Environment(new DbEnv(dbenv, false)))
                {
                    this.errCallback_(environment, errpfx, msg);
                    return;
                }
            }
            if (this.errWriter_ != null)
            {
                if (errpfx != null)
                {
                    this.errWriter_.Write(errpfx + ": ");
                }
                this.errWriter_.WriteLine(msg);
            }
        }

        internal DbEnv openDbEnv(string home)
        {
            DbEnv env = this.createDbEnv();
            try
            {
                if (home == null)
                {
                    this.Private = true;
                    this.Create = true;
                    this.InitializeCache = true;
                }
                env.open(home, this.flags_, this.mode_);
            }
            catch (Exception exception)
            {
                try
                {
                    env.close(0);
                }
                catch (Exception)
                {
                }
                throw exception;
            }
            return env;
        }

        private void setFlag(bool value, uint flag)
        {
            if (value)
            {
                this.flags_ |= flag;
            }
            else
            {
                this.flags_ &= ~flag;
            }
        }

        internal void SetFromDbEnv(DbEnv env)
        {
            string str;
            this.flags_ = env.get_open_flags();
            this.cachesize_ = (int) ((0x40000000 * env.get_cachesize_gbytes()) + env.get_cachesize_bytes());
            this.nCaches_ = (int) env.get_cachesize_ncache();
            StringArrayIterator iterator = env.get_data_dirs();
            ArrayList list = new ArrayList();
            while ((str = iterator.next()) != null)
            {
                list.Add(str);
            }
            this.dataDirs_ = (string[]) list.ToArray(typeof(string));
            this.errPrefix_ = env.get_errpfx();
        }

        public int CacheCount
        {
            get
            {
                return this.nCaches_;
            }
            set
            {
                this.nCaches_ = value;
            }
        }

        public int CacheSize
        {
            get
            {
                return this.cachesize_;
            }
            set
            {
                this.cachesize_ = value;
            }
        }

        public bool Create
        {
            get
            {
                return ((this.flags_ & DbXml.DB_CREATE) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_CREATE);
            }
        }

        public string[] DataDirs
        {
            get
            {
                return this.dataDirs_;
            }
            set
            {
                this.dataDirs_ = value;
            }
        }

        public string EncryptedPassword
        {
            get
            {
                return this.encryptPassword_;
            }
            set
            {
                this.encryptPassword_ = value;
            }
        }

        public Sleepycat.Db.ErrorCallback ErrorCallback
        {
            get
            {
                return this.errCallback_;
            }
            set
            {
                this.errCallback_ = value;
                this.errWriter_ = null;
            }
        }

        public string ErrorPrefix
        {
            get
            {
                return this.errPrefix_;
            }
            set
            {
                this.errPrefix_ = value;
            }
        }

        public TextWriter ErrorWriter
        {
            get
            {
                return this.errWriter_;
            }
            set
            {
                this.errWriter_ = value;
                this.errCallback_ = null;
            }
        }

        public bool InitializeCache
        {
            get
            {
                return ((this.flags_ & DbXml.DB_INIT_MPOOL) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_INIT_MPOOL);
            }
        }

        public bool InitializeCDB
        {
            get
            {
                return ((this.flags_ & DbXml.DB_INIT_CDB) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_INIT_CDB);
            }
        }

        public bool InitializeLocking
        {
            get
            {
                return ((this.flags_ & DbXml.DB_INIT_LOCK) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_INIT_LOCK);
            }
        }

        public bool InitializeLogging
        {
            get
            {
                return ((this.flags_ & DbXml.DB_INIT_LOG) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_INIT_LOG);
            }
        }

        public bool InitializeReplication
        {
            get
            {
                return ((this.flags_ & DbXml.DB_INIT_REP) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_INIT_REP);
            }
        }

        public bool JoinEnvironment
        {
            get
            {
                return ((this.flags_ & DbXml.DB_JOINENV) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_JOINENV);
            }
        }

        public bool Lockdown
        {
            get
            {
                return ((this.flags_ & DbXml.DB_LOCKDOWN) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_LOCKDOWN);
            }
        }

        public int Mode
        {
            get
            {
                return this.mode_;
            }
            set
            {
                this.mode_ = value;
            }
        }

        public bool Private
        {
            get
            {
                return ((this.flags_ & DbXml.DB_PRIVATE) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_PRIVATE);
            }
        }

        public bool Recover
        {
            get
            {
                return ((this.flags_ & DbXml.DB_RECOVER) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_RECOVER);
            }
        }

        public bool RecoverFatal
        {
            get
            {
                return ((this.flags_ & DbXml.DB_RECOVER_FATAL) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_RECOVER_FATAL);
            }
        }

        public bool SystemMemory
        {
            get
            {
                return ((this.flags_ & DbXml.DB_SYSTEM_MEM) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_SYSTEM_MEM);
            }
        }

        public bool Threaded
        {
            get
            {
                return ((this.flags_ & DbXml.DB_THREAD) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_THREAD);
            }
        }

        public bool Transactional
        {
            get
            {
                return ((this.flags_ & DbXml.DB_INIT_TXN) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_INIT_TXN);
            }
        }

        public bool UseEnvironment
        {
            get
            {
                return ((this.flags_ & DbXml.DB_USE_ENVIRON) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_USE_ENVIRON);
            }
        }

        public bool UseEnvironmentRoot
        {
            get
            {
                return ((this.flags_ & DbXml.DB_USE_ENVIRON_ROOT) != 0);
            }
            set
            {
                this.setFlag(value, (uint) DbXml.DB_USE_ENVIRON_ROOT);
            }
        }
    }
}

