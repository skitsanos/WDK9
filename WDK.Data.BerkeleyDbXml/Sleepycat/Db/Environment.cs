namespace Sleepycat.Db
{
    using Sleepycat.DbXml.Internal;
    using System;

    public class Environment : IDisposable
    {
        private EnvironmentConfig config_;
        private DbEnv env_;
        private bool envClosed_;

        public Environment() : this(null, new EnvironmentConfig())
        {
        }

        public Environment(EnvironmentConfig config) : this(null, config)
        {
        }

        internal Environment(DbEnv e)
        {
            this.env_ = e;
            this.envClosed_ = true;
            this.config_ = null;
        }

        public Environment(string home, EnvironmentConfig config)
        {
            this.env_ = null;
            this.envClosed_ = true;
            this.env_ = config.openDbEnv(home);
            this.envClosed_ = false;
            this.config_ = config;
        }

        public void Checkpoint(int kbyte, int min, bool force)
        {
            uint flags = 0;
            if (force)
            {
                flags |= (uint) DbXml.DB_FORCE;
            }
            this.env_.txn_checkpoint((uint) kbyte, (uint) min, flags);
        }

        public int DetectDeadlocks(LockDetectMode mode)
        {
            return this.env_.lock_detect(0, LockDetectModeToInt(mode));
        }

        internal void Disown()
        {
            this.envClosed_ = true;
            this.env_.disownCPtr();
            GC.SuppressFinalize(this);
        }

        public void Dispose()
        {
            if (!this.envClosed_)
            {
                this.envClosed_ = true;
                if (DbEnv.getCPtr(this.env_) != IntPtr.Zero)
                {
                    this.env_.close(0);
                }
            }
            if (this.env_ != null)
            {
                this.env_.Dispose();
            }
            GC.SuppressFinalize(this);
        }

        ~Environment()
        {
            this.Dispose();
        }

        private static uint LockDetectModeToInt(LockDetectMode mode)
        {
            switch (mode)
            {
                case LockDetectMode.DEFAULT:
                    return (uint) DbXml.DB_LOCK_DEFAULT;

                case LockDetectMode.EXPIRE:
                    return (uint) DbXml.DB_LOCK_EXPIRE;

                case LockDetectMode.MAXLOCKS:
                    return (uint) DbXml.DB_LOCK_MAXLOCKS;

                case LockDetectMode.MAXWRITE:
                    return (uint) DbXml.DB_LOCK_MAXWRITE;

                case LockDetectMode.MINLOCKS:
                    return (uint) DbXml.DB_LOCK_MINLOCKS;

                case LockDetectMode.MINWRITE:
                    return (uint) DbXml.DB_LOCK_MINWRITE;

                case LockDetectMode.OLDEST:
                    return (uint) DbXml.DB_LOCK_OLDEST;

                case LockDetectMode.RANDOM:
                    return (uint) DbXml.DB_LOCK_RANDOM;

                case LockDetectMode.YOUNGEST:
                    return (uint) DbXml.DB_LOCK_YOUNGEST;
            }
            throw new Exception("Enumeration not handled in switch: " + mode);
        }

        public static void Remove(string home, bool force, EnvironmentConfig config)
        {
            DbEnv env = config.createDbEnv();
            uint flags = 0;
            if (force)
            {
                flags |= (uint) DbXml.DB_FORCE;
            }
            if (config.UseEnvironment)
            {
                flags |= (uint) DbXml.DB_USE_ENVIRON;
            }
            if (config.UseEnvironmentRoot)
            {
                flags |= (uint) DbXml.DB_USE_ENVIRON_ROOT;
            }
            env.remove(home, flags);
        }

        public EnvironmentConfig Configuration
        {
            get
            {
                if (this.config_ == null)
                {
                    this.config_ = new EnvironmentConfig(this.env_);
                }
                else
                {
                    this.config_.SetFromDbEnv(this.env_);
                }
                return this.config_;
            }
            set
            {
                this.config_ = value;
                value.configureDbEnv(this.env_, true);
            }
        }

        public string Home
        {
            get
            {
                return this.env_.get_home();
            }
        }

        internal DbEnv Internal
        {
            get
            {
                return this.env_;
            }
        }

        public static string Version
        {
            get
            {
                return DbEnv.version_string();
            }
        }

        public static int VersionMajor
        {
            get
            {
                return DbEnv.version_major();
            }
        }

        public static int VersionMinor
        {
            get
            {
                return DbEnv.version_minor();
            }
        }

        public static int VersionPatch
        {
            get
            {
                return DbEnv.version_patch();
            }
        }

        public enum LockDetectMode
        {
            DEFAULT,
            EXPIRE,
            MAXLOCKS,
            MAXWRITE,
            MINLOCKS,
            MINWRITE,
            OLDEST,
            RANDOM,
            YOUNGEST
        }
    }
}

