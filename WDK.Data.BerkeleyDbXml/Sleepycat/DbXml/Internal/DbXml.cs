namespace Sleepycat.DbXml.Internal
{
    using System;

    internal class DbXml
    {
        public static readonly int CATEGORY_ALL = DbXmlPINVOKE.get_CATEGORY_ALL();
        public static readonly int CATEGORY_CONTAINER = DbXmlPINVOKE.get_CATEGORY_CONTAINER();
        public static readonly int CATEGORY_DICTIONARY = DbXmlPINVOKE.get_CATEGORY_DICTIONARY();
        public static readonly int CATEGORY_INDEXER = DbXmlPINVOKE.get_CATEGORY_INDEXER();
        public static readonly int CATEGORY_MANAGER = DbXmlPINVOKE.get_CATEGORY_MANAGER();
        public static readonly int CATEGORY_NODESTORE = DbXmlPINVOKE.get_CATEGORY_NODESTORE();
        public static readonly int CATEGORY_NONE = DbXmlPINVOKE.get_CATEGORY_NONE();
        public static readonly int CATEGORY_OPTIMIZER = DbXmlPINVOKE.get_CATEGORY_OPTIMIZER();
        public static readonly int CATEGORY_QUERY = DbXmlPINVOKE.get_CATEGORY_QUERY();
        public static readonly int DB_AGGRESSIVE = DbXmlPINVOKE.get_DB_AGGRESSIVE();
        public static readonly int DB_CHKSUM = DbXmlPINVOKE.get_DB_CHKSUM();
        public static readonly int DB_CREATE = DbXmlPINVOKE.get_DB_CREATE();
        public static readonly int DB_DEGREE_2 = DbXmlPINVOKE.get_DB_DEGREE_2();
        public static readonly int DB_DIRTY_READ = DbXmlPINVOKE.get_DB_DIRTY_READ();
        public static readonly int DB_ENCRYPT = DbXmlPINVOKE.get_DB_ENCRYPT();
        public static readonly int DB_EXCL = DbXmlPINVOKE.get_DB_EXCL();
        public static readonly int DB_FORCE = DbXmlPINVOKE.get_DB_FORCE();
        public static readonly int DB_INIT_CDB = DbXmlPINVOKE.get_DB_INIT_CDB();
        public static readonly int DB_INIT_LOCK = DbXmlPINVOKE.get_DB_INIT_LOCK();
        public static readonly int DB_INIT_LOG = DbXmlPINVOKE.get_DB_INIT_LOG();
        public static readonly int DB_INIT_MPOOL = DbXmlPINVOKE.get_DB_INIT_MPOOL();
        public static readonly int DB_INIT_REP = DbXmlPINVOKE.get_DB_INIT_REP();
        public static readonly int DB_INIT_TXN = DbXmlPINVOKE.get_DB_INIT_TXN();
        public static readonly int DB_JOINENV = DbXmlPINVOKE.get_DB_JOINENV();
        public static readonly int DB_LOCK_DEFAULT = DbXmlPINVOKE.get_DB_LOCK_DEFAULT();
        public static readonly int DB_LOCK_EXPIRE = DbXmlPINVOKE.get_DB_LOCK_EXPIRE();
        public static readonly int DB_LOCK_MAXLOCKS = DbXmlPINVOKE.get_DB_LOCK_MAXLOCKS();
        public static readonly int DB_LOCK_MAXWRITE = DbXmlPINVOKE.get_DB_LOCK_MAXWRITE();
        public static readonly int DB_LOCK_MINLOCKS = DbXmlPINVOKE.get_DB_LOCK_MINLOCKS();
        public static readonly int DB_LOCK_MINWRITE = DbXmlPINVOKE.get_DB_LOCK_MINWRITE();
        public static readonly int DB_LOCK_OLDEST = DbXmlPINVOKE.get_DB_LOCK_OLDEST();
        public static readonly int DB_LOCK_RANDOM = DbXmlPINVOKE.get_DB_LOCK_RANDOM();
        public static readonly int DB_LOCK_YOUNGEST = DbXmlPINVOKE.get_DB_LOCK_YOUNGEST();
        public static readonly int DB_LOCKDOWN = DbXmlPINVOKE.get_DB_LOCKDOWN();
        public static readonly int DB_NOMMAP = DbXmlPINVOKE.get_DB_NOMMAP();
        public static readonly int DB_PRIVATE = DbXmlPINVOKE.get_DB_PRIVATE();
        public static readonly int DB_RDONLY = DbXmlPINVOKE.get_DB_RDONLY();
        public static readonly int DB_RECOVER = DbXmlPINVOKE.get_DB_RECOVER();
        public static readonly int DB_RECOVER_FATAL = DbXmlPINVOKE.get_DB_RECOVER_FATAL();
        public static readonly int DB_RMW = DbXmlPINVOKE.get_DB_RMW();
        public static readonly int DB_SALVAGE = DbXmlPINVOKE.get_DB_SALVAGE();
        public static readonly int DB_SYSTEM_MEM = DbXmlPINVOKE.get_DB_SYSTEM_MEM();
        public static readonly int DB_THREAD = DbXmlPINVOKE.get_DB_THREAD();
        public static readonly int DB_TXN_NOSYNC = DbXmlPINVOKE.get_DB_TXN_NOSYNC();
        public static readonly int DB_TXN_NOWAIT = DbXmlPINVOKE.get_DB_TXN_NOWAIT();
        public static readonly int DB_TXN_SYNC = DbXmlPINVOKE.get_DB_TXN_SYNC();
        public static readonly int DB_USE_ENVIRON = DbXmlPINVOKE.get_DB_USE_ENVIRON();
        public static readonly int DB_USE_ENVIRON_ROOT = DbXmlPINVOKE.get_DB_USE_ENVIRON_ROOT();
        public static readonly int DB_XA_CREATE = DbXmlPINVOKE.get_DB_XA_CREATE();
        public static readonly int DBXML_ADOPT_DBENV = DbXmlPINVOKE.get_DBXML_ADOPT_DBENV();
        public static readonly int DBXML_ALLOW_AUTO_OPEN = DbXmlPINVOKE.get_DBXML_ALLOW_AUTO_OPEN();
        public static readonly int DBXML_ALLOW_EXTERNAL_ACCESS = DbXmlPINVOKE.get_DBXML_ALLOW_EXTERNAL_ACCESS();
        public static readonly int DBXML_ALLOW_VALIDATION = DbXmlPINVOKE.get_DBXML_ALLOW_VALIDATION();
        public static readonly int DBXML_CHKSUM = DbXmlPINVOKE.get_DBXML_CHKSUM();
        public static readonly int DBXML_ENCRYPT = DbXmlPINVOKE.get_DBXML_ENCRYPT();
        public static readonly int DBXML_GEN_NAME = DbXmlPINVOKE.get_DBXML_GEN_NAME();
        public static readonly int DBXML_INDEX_NODES = DbXmlPINVOKE.get_DBXML_INDEX_NODES();
        public static readonly int DBXML_LAZY_DOCS = DbXmlPINVOKE.get_DBXML_LAZY_DOCS();
        public static readonly int DBXML_TRANSACTIONAL = DbXmlPINVOKE.get_DBXML_TRANSACTIONAL();
        public static readonly int LEVEL_ALL = DbXmlPINVOKE.get_LEVEL_ALL();
        public static readonly int LEVEL_DEBUG = DbXmlPINVOKE.get_LEVEL_DEBUG();
        public static readonly int LEVEL_ERROR = DbXmlPINVOKE.get_LEVEL_ERROR();
        public static readonly int LEVEL_INFO = DbXmlPINVOKE.get_LEVEL_INFO();
        public static readonly int LEVEL_NONE = DbXmlPINVOKE.get_LEVEL_NONE();
        public static readonly int LEVEL_WARNING = DbXmlPINVOKE.get_LEVEL_WARNING();
        public static readonly string metaDataName_name = DbXmlPINVOKE.get_metaDataName_name();
        public static readonly string metaDataName_root = DbXmlPINVOKE.get_metaDataName_root();
        public static readonly string metaDataNamespace_prefix = DbXmlPINVOKE.get_metaDataNamespace_prefix();
        public static readonly string metaDataNamespace_uri = DbXmlPINVOKE.get_metaDataNamespace_uri();
    }
}

