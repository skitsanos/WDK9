namespace Sleepycat.DbXml.Internal
{
    using Sleepycat.DbXml;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class DbXmlPINVOKE
    {
        private static SWIGExceptionHelper exceptionHelper = new SWIGExceptionHelper();
        private static DbXmlExceptionThrower exceptionThrower = new DbXmlExceptionThrower();
        private static SWIGStringHelper stringHelper = new SWIGStringHelper();

        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_close")]
        public static extern int DbEnv_close(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_get_cachesize_bytes")]
        public static extern uint DbEnv_get_cachesize_bytes(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_get_cachesize_gbytes")]
        public static extern uint DbEnv_get_cachesize_gbytes(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_get_cachesize_ncache")]
        public static extern uint DbEnv_get_cachesize_ncache(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_get_data_dirs")]
        public static extern IntPtr DbEnv_get_data_dirs(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_get_errpfx")]
        public static extern string DbEnv_get_errpfx(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_get_home")]
        public static extern string DbEnv_get_home(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_get_open_flags")]
        public static extern uint DbEnv_get_open_flags(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_lock_detect")]
        public static extern int DbEnv_lock_detect(IntPtr jarg1, uint jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_open")]
        public static extern int DbEnv_open(IntPtr jarg1, string jarg2, uint jarg3, int jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_remove")]
        public static extern int DbEnv_remove(IntPtr jarg1, string jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_cachesize")]
        public static extern int DbEnv_set_cachesize(IntPtr jarg1, uint jarg2, uint jarg3, int jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_data_dir")]
        public static extern int DbEnv_set_data_dir(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_encrypt")]
        public static extern int DbEnv_set_encrypt(IntPtr jarg1, string jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_errcall")]
        public static extern void DbEnv_set_errcall(IntPtr jarg1, DbEnv.ErrorCallbackDelegate jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_errpfx")]
        public static extern void DbEnv_set_errpfx(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_flags")]
        public static extern int DbEnv_set_flags(IntPtr jarg1, uint jarg2, int jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lg_bsize")]
        public static extern int DbEnv_set_lg_bsize(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lg_dir")]
        public static extern int DbEnv_set_lg_dir(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lg_max")]
        public static extern int DbEnv_set_lg_max(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lg_regionmax")]
        public static extern int DbEnv_set_lg_regionmax(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lk_detect")]
        public static extern int DbEnv_set_lk_detect(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lk_max_lockers")]
        public static extern int DbEnv_set_lk_max_lockers(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lk_max_locks")]
        public static extern int DbEnv_set_lk_max_locks(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_lk_max_objects")]
        public static extern int DbEnv_set_lk_max_objects(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_mp_mmapsize")]
        public static extern int DbEnv_set_mp_mmapsize(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_shm_key")]
        public static extern int DbEnv_set_shm_key(IntPtr jarg1, int jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_tas_spins")]
        public static extern int DbEnv_set_tas_spins(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_timeout")]
        public static extern int DbEnv_set_timeout(IntPtr jarg1, int jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_tmp_dir")]
        public static extern int DbEnv_set_tmp_dir(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_tx_max")]
        public static extern int DbEnv_set_tx_max(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_set_verbose")]
        public static extern int DbEnv_set_verbose(IntPtr jarg1, uint jarg2, int jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_strerror")]
        public static extern string DbEnv_strerror(int jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_txn_checkpoint")]
        public static extern int DbEnv_txn_checkpoint(IntPtr jarg1, uint jarg2, uint jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_version_major")]
        public static extern int DbEnv_version_major();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_version_minor")]
        public static extern int DbEnv_version_minor();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_version_patch")]
        public static extern int DbEnv_version_patch();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DbEnv_version_string")]
        public static extern string DbEnv_version_string();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DelegateInputStream_create")]
        public static extern IntPtr DelegateInputStream_create(DelegateInputStream.CurPosDelegate jarg1, DelegateInputStream.ReadBytesDelegate jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DelegateInputStreamUpcast")]
        public static extern IntPtr DelegateInputStreamUpcast(IntPtr objectRef);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_DelegateResolverUpcast")]
        public static extern IntPtr DelegateResolverUpcast(IntPtr objectRef);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_DbEnv")]
        public static extern void delete_DbEnv(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_DelegateInputStream")]
        public static extern void delete_DelegateInputStream(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_DelegateResolver")]
        public static extern void delete_DelegateResolver(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_StringArrayIterator")]
        public static extern void delete_StringArrayIterator(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlContainer")]
        public static extern void delete_XmlContainer(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlData")]
        public static extern void delete_XmlData(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlDocument")]
        public static extern void delete_XmlDocument(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlIndexDeclaration")]
        public static extern void delete_XmlIndexDeclaration(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlIndexSpecification")]
        public static extern void delete_XmlIndexSpecification(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlInputStream")]
        public static extern void delete_XmlInputStream(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlManager")]
        public static extern void delete_XmlManager(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlMetaData")]
        public static extern void delete_XmlMetaData(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlMetaDataIterator")]
        public static extern void delete_XmlMetaDataIterator(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlModify")]
        public static extern void delete_XmlModify(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlQueryContext")]
        public static extern void delete_XmlQueryContext(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlQueryExpression")]
        public static extern void delete_XmlQueryExpression(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlResolver")]
        public static extern void delete_XmlResolver(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlResults")]
        public static extern void delete_XmlResults(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlStatistics")]
        public static extern void delete_XmlStatistics(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlTransaction")]
        public static extern void delete_XmlTransaction(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlUpdateContext")]
        public static extern void delete_XmlUpdateContext(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_delete_XmlValue")]
        public static extern void delete_XmlValue(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_ALL")]
        public static extern int get_CATEGORY_ALL();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_CONTAINER")]
        public static extern int get_CATEGORY_CONTAINER();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_DICTIONARY")]
        public static extern int get_CATEGORY_DICTIONARY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_INDEXER")]
        public static extern int get_CATEGORY_INDEXER();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_MANAGER")]
        public static extern int get_CATEGORY_MANAGER();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_NODESTORE")]
        public static extern int get_CATEGORY_NODESTORE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_NONE")]
        public static extern int get_CATEGORY_NONE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_OPTIMIZER")]
        public static extern int get_CATEGORY_OPTIMIZER();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CATEGORY_QUERY")]
        public static extern int get_CATEGORY_QUERY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CONTAINER_CLOSED")]
        public static extern int get_CONTAINER_CLOSED();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CONTAINER_EXISTS")]
        public static extern int get_CONTAINER_EXISTS();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CONTAINER_NOT_FOUND")]
        public static extern int get_CONTAINER_NOT_FOUND();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_CONTAINER_OPEN")]
        public static extern int get_CONTAINER_OPEN();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DATABASE_ERROR")]
        public static extern int get_DATABASE_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_AGGRESSIVE")]
        public static extern int get_DB_AGGRESSIVE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_CHKSUM")]
        public static extern int get_DB_CHKSUM();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_CREATE")]
        public static extern int get_DB_CREATE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_DEGREE_2")]
        public static extern int get_DB_DEGREE_2();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_DIRTY_READ")]
        public static extern int get_DB_DIRTY_READ();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_ENCRYPT")]
        public static extern int get_DB_ENCRYPT();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_EXCL")]
        public static extern int get_DB_EXCL();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_FORCE")]
        public static extern int get_DB_FORCE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_INIT_CDB")]
        public static extern int get_DB_INIT_CDB();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_INIT_LOCK")]
        public static extern int get_DB_INIT_LOCK();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_INIT_LOG")]
        public static extern int get_DB_INIT_LOG();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_INIT_MPOOL")]
        public static extern int get_DB_INIT_MPOOL();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_INIT_REP")]
        public static extern int get_DB_INIT_REP();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_INIT_TXN")]
        public static extern int get_DB_INIT_TXN();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_JOINENV")]
        public static extern int get_DB_JOINENV();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_DEFAULT")]
        public static extern int get_DB_LOCK_DEFAULT();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_EXPIRE")]
        public static extern int get_DB_LOCK_EXPIRE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_MAXLOCKS")]
        public static extern int get_DB_LOCK_MAXLOCKS();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_MAXWRITE")]
        public static extern int get_DB_LOCK_MAXWRITE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_MINLOCKS")]
        public static extern int get_DB_LOCK_MINLOCKS();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_MINWRITE")]
        public static extern int get_DB_LOCK_MINWRITE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_OLDEST")]
        public static extern int get_DB_LOCK_OLDEST();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_RANDOM")]
        public static extern int get_DB_LOCK_RANDOM();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCK_YOUNGEST")]
        public static extern int get_DB_LOCK_YOUNGEST();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_LOCKDOWN")]
        public static extern int get_DB_LOCKDOWN();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_NOMMAP")]
        public static extern int get_DB_NOMMAP();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_PRIVATE")]
        public static extern int get_DB_PRIVATE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_RDONLY")]
        public static extern int get_DB_RDONLY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_RECOVER")]
        public static extern int get_DB_RECOVER();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_RECOVER_FATAL")]
        public static extern int get_DB_RECOVER_FATAL();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_RMW")]
        public static extern int get_DB_RMW();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_SALVAGE")]
        public static extern int get_DB_SALVAGE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_SYSTEM_MEM")]
        public static extern int get_DB_SYSTEM_MEM();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_THREAD")]
        public static extern int get_DB_THREAD();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_TXN_NOSYNC")]
        public static extern int get_DB_TXN_NOSYNC();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_TXN_NOWAIT")]
        public static extern int get_DB_TXN_NOWAIT();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_TXN_SYNC")]
        public static extern int get_DB_TXN_SYNC();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_USE_ENVIRON")]
        public static extern int get_DB_USE_ENVIRON();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_USE_ENVIRON_ROOT")]
        public static extern int get_DB_USE_ENVIRON_ROOT();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DB_XA_CREATE")]
        public static extern int get_DB_XA_CREATE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_ADOPT_DBENV")]
        public static extern int get_DBXML_ADOPT_DBENV();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_ALLOW_AUTO_OPEN")]
        public static extern int get_DBXML_ALLOW_AUTO_OPEN();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_ALLOW_EXTERNAL_ACCESS")]
        public static extern int get_DBXML_ALLOW_EXTERNAL_ACCESS();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_ALLOW_VALIDATION")]
        public static extern int get_DBXML_ALLOW_VALIDATION();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_CHKSUM")]
        public static extern int get_DBXML_CHKSUM();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_ENCRYPT")]
        public static extern int get_DBXML_ENCRYPT();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_GEN_NAME")]
        public static extern int get_DBXML_GEN_NAME();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_INDEX_NODES")]
        public static extern int get_DBXML_INDEX_NODES();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_LAZY_DOCS")]
        public static extern int get_DBXML_LAZY_DOCS();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DBXML_TRANSACTIONAL")]
        public static extern int get_DBXML_TRANSACTIONAL();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DOCUMENT_NOT_FOUND")]
        public static extern int get_DOCUMENT_NOT_FOUND();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_DOM_PARSER_ERROR")]
        public static extern int get_DOM_PARSER_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_INDEXER_PARSER_ERROR")]
        public static extern int get_INDEXER_PARSER_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_INTERNAL_ERROR")]
        public static extern int get_INTERNAL_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_INVALID_VALUE")]
        public static extern int get_INVALID_VALUE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_LAZY_EVALUATION")]
        public static extern int get_LAZY_EVALUATION();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_LEVEL_ALL")]
        public static extern int get_LEVEL_ALL();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_LEVEL_DEBUG")]
        public static extern int get_LEVEL_DEBUG();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_LEVEL_ERROR")]
        public static extern int get_LEVEL_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_LEVEL_INFO")]
        public static extern int get_LEVEL_INFO();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_LEVEL_NONE")]
        public static extern int get_LEVEL_NONE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_LEVEL_WARNING")]
        public static extern int get_LEVEL_WARNING();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_metaDataName_name")]
        public static extern string get_metaDataName_name();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_metaDataName_root")]
        public static extern string get_metaDataName_root();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_metaDataNamespace_prefix")]
        public static extern string get_metaDataNamespace_prefix();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_metaDataNamespace_uri")]
        public static extern string get_metaDataNamespace_uri();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_NO_MEMORY_ERROR")]
        public static extern int get_NO_MEMORY_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_NO_VARIABLE_BINDING")]
        public static extern int get_NO_VARIABLE_BINDING();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_NULL_POINTER")]
        public static extern int get_NULL_POINTER();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_TRANSACTION_ERROR")]
        public static extern int get_TRANSACTION_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_UNIQUE_ERROR")]
        public static extern int get_UNIQUE_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_UNKNOWN_INDEX")]
        public static extern int get_UNKNOWN_INDEX();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_VERSION_MISMATCH")]
        public static extern int get_VERSION_MISMATCH();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlContainer_NodeContainer")]
        public static extern int get_XmlContainer_NodeContainer();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlContainer_WholedocContainer")]
        public static extern int get_XmlContainer_WholedocContainer();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_KEY_EQUALITY")]
        public static extern int get_XmlIndexSpecification_KEY_EQUALITY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_KEY_NONE")]
        public static extern int get_XmlIndexSpecification_KEY_NONE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_KEY_PRESENCE")]
        public static extern int get_XmlIndexSpecification_KEY_PRESENCE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_KEY_SUBSTRING")]
        public static extern int get_XmlIndexSpecification_KEY_SUBSTRING();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_NODE_ATTRIBUTE")]
        public static extern int get_XmlIndexSpecification_NODE_ATTRIBUTE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_NODE_ELEMENT")]
        public static extern int get_XmlIndexSpecification_NODE_ELEMENT();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_NODE_METADATA")]
        public static extern int get_XmlIndexSpecification_NODE_METADATA();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_NODE_NONE")]
        public static extern int get_XmlIndexSpecification_NODE_NONE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_PATH_EDGE")]
        public static extern int get_XmlIndexSpecification_PATH_EDGE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_PATH_NODE")]
        public static extern int get_XmlIndexSpecification_PATH_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_PATH_NONE")]
        public static extern int get_XmlIndexSpecification_PATH_NONE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_UNIQUE_OFF")]
        public static extern int get_XmlIndexSpecification_UNIQUE_OFF();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlIndexSpecification_UNIQUE_ON")]
        public static extern int get_XmlIndexSpecification_UNIQUE_ON();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlModify_Attribute")]
        public static extern int get_XmlModify_Attribute();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlModify_Comment")]
        public static extern int get_XmlModify_Comment();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlModify_Element")]
        public static extern int get_XmlModify_Element();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlModify_ProcessingInstruction")]
        public static extern int get_XmlModify_ProcessingInstruction();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlModify_Text")]
        public static extern int get_XmlModify_Text();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlQueryContext_DeadValues")]
        public static extern int get_XmlQueryContext_DeadValues();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlQueryContext_Eager")]
        public static extern int get_XmlQueryContext_Eager();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlQueryContext_Lazy")]
        public static extern int get_XmlQueryContext_Lazy();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlQueryContext_LiveValues")]
        public static extern int get_XmlQueryContext_LiveValues();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_ANY_SIMPLE_TYPE")]
        public static extern int get_XmlValue_ANY_SIMPLE_TYPE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_ANY_URI")]
        public static extern int get_XmlValue_ANY_URI();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_ATTRIBUTE_NODE")]
        public static extern int get_XmlValue_ATTRIBUTE_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_BASE_64_BINARY")]
        public static extern int get_XmlValue_BASE_64_BINARY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_BOOLEAN")]
        public static extern int get_XmlValue_BOOLEAN();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_CDATA_SECTION_NODE")]
        public static extern int get_XmlValue_CDATA_SECTION_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_COMMENT_NODE")]
        public static extern int get_XmlValue_COMMENT_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DATE")]
        public static extern int get_XmlValue_DATE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DATE_TIME")]
        public static extern int get_XmlValue_DATE_TIME();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DAY_TIME_DURATION")]
        public static extern int get_XmlValue_DAY_TIME_DURATION();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DECIMAL")]
        public static extern int get_XmlValue_DECIMAL();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DOCUMENT_FRAGMENT_NODE")]
        public static extern int get_XmlValue_DOCUMENT_FRAGMENT_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DOCUMENT_NODE")]
        public static extern int get_XmlValue_DOCUMENT_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DOCUMENT_TYPE_NODE")]
        public static extern int get_XmlValue_DOCUMENT_TYPE_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DOUBLE")]
        public static extern int get_XmlValue_DOUBLE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_DURATION")]
        public static extern int get_XmlValue_DURATION();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_ELEMENT_NODE")]
        public static extern int get_XmlValue_ELEMENT_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_ENTITY_NODE")]
        public static extern int get_XmlValue_ENTITY_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_ENTITY_REFERENCE_NODE")]
        public static extern int get_XmlValue_ENTITY_REFERENCE_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_FLOAT")]
        public static extern int get_XmlValue_FLOAT();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_G_DAY")]
        public static extern int get_XmlValue_G_DAY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_G_MONTH")]
        public static extern int get_XmlValue_G_MONTH();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_G_MONTH_DAY")]
        public static extern int get_XmlValue_G_MONTH_DAY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_G_YEAR")]
        public static extern int get_XmlValue_G_YEAR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_G_YEAR_MONTH")]
        public static extern int get_XmlValue_G_YEAR_MONTH();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_HEX_BINARY")]
        public static extern int get_XmlValue_HEX_BINARY();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_NODE")]
        public static extern int get_XmlValue_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_NONE")]
        public static extern int get_XmlValue_NONE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_NOTATION")]
        public static extern int get_XmlValue_NOTATION();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_NOTATION_NODE")]
        public static extern int get_XmlValue_NOTATION_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_PROCESSING_INSTRUCTION_NODE")]
        public static extern int get_XmlValue_PROCESSING_INSTRUCTION_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_QNAME")]
        public static extern int get_XmlValue_QNAME();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_STRING")]
        public static extern int get_XmlValue_STRING();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_TEXT_NODE")]
        public static extern int get_XmlValue_TEXT_NODE();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_TIME")]
        public static extern int get_XmlValue_TIME();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_UNTYPED_ATOMIC")]
        public static extern int get_XmlValue_UNTYPED_ATOMIC();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XmlValue_YEAR_MONTH_DURATION")]
        public static extern int get_XmlValue_YEAR_MONTH_DURATION();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XPATH_EVALUATION_ERROR")]
        public static extern int get_XPATH_EVALUATION_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_get_XPATH_PARSER_ERROR")]
        public static extern int get_XPATH_PARSER_ERROR();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_DbEnv")]
        public static extern IntPtr new_DbEnv(uint jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_DelegateResolver")]
        public static extern IntPtr new_DelegateResolver(DelegateResolver.ResolveDocumentDelegate jarg1, DelegateResolver.ResolveCollectionDelegate jarg2, DelegateResolver.ResolveSchemaDelegate jarg3, DelegateResolver.ResolveEntityDelegate jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlData__SWIG_0")]
        public static extern IntPtr new_XmlData__SWIG_0();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlData__SWIG_1")]
        public static extern IntPtr new_XmlData__SWIG_1(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlIndexDeclaration")]
        public static extern IntPtr new_XmlIndexDeclaration();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlIndexSpecification")]
        public static extern IntPtr new_XmlIndexSpecification();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlManager__SWIG_0")]
        public static extern IntPtr new_XmlManager__SWIG_0();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlManager__SWIG_1")]
        public static extern IntPtr new_XmlManager__SWIG_1(uint jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlManager__SWIG_2")]
        public static extern IntPtr new_XmlManager__SWIG_2(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlMetaData")]
        public static extern IntPtr new_XmlMetaData();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlValue__SWIG_0")]
        public static extern IntPtr new_XmlValue__SWIG_0(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlValue__SWIG_1")]
        public static extern IntPtr new_XmlValue__SWIG_1(string jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlValue__SWIG_2")]
        public static extern IntPtr new_XmlValue__SWIG_2(double jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlValue__SWIG_3")]
        public static extern IntPtr new_XmlValue__SWIG_3(bool jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlValue__SWIG_4")]
        public static extern IntPtr new_XmlValue__SWIG_4(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlValue__SWIG_5")]
        public static extern IntPtr new_XmlValue__SWIG_5(int jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_new_XmlValue__SWIG_6")]
        public static extern IntPtr new_XmlValue__SWIG_6(int jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_StringArrayIterator_next")]
        public static extern string StringArrayIterator_next(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_StringArrayIterator_reset")]
        public static extern void StringArrayIterator_reset(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_addAlias")]
        public static extern bool XmlContainer_addAlias(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_addDefaultIndex__SWIG_0")]
        public static extern void XmlContainer_addDefaultIndex__SWIG_0(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_addDefaultIndex__SWIG_1")]
        public static extern void XmlContainer_addDefaultIndex__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_addIndex__SWIG_0")]
        public static extern void XmlContainer_addIndex__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, string jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_addIndex__SWIG_1")]
        public static extern void XmlContainer_addIndex__SWIG_1(IntPtr jarg1, string jarg2, string jarg3, int jarg4, int jarg5, IntPtr jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_addIndex__SWIG_2")]
        public static extern void XmlContainer_addIndex__SWIG_2(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, string jarg5, IntPtr jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_addIndex__SWIG_3")]
        public static extern void XmlContainer_addIndex__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, int jarg5, int jarg6, IntPtr jarg7);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_close")]
        public static extern void XmlContainer_close(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteDefaultIndex__SWIG_0")]
        public static extern void XmlContainer_deleteDefaultIndex__SWIG_0(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteDefaultIndex__SWIG_1")]
        public static extern void XmlContainer_deleteDefaultIndex__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteDocument__SWIG_0")]
        public static extern void XmlContainer_deleteDocument__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteDocument__SWIG_1")]
        public static extern void XmlContainer_deleteDocument__SWIG_1(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteDocument__SWIG_2")]
        public static extern void XmlContainer_deleteDocument__SWIG_2(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteDocument__SWIG_3")]
        public static extern void XmlContainer_deleteDocument__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteIndex__SWIG_0")]
        public static extern void XmlContainer_deleteIndex__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, string jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_deleteIndex__SWIG_1")]
        public static extern void XmlContainer_deleteIndex__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, string jarg5, IntPtr jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getAllDocuments__SWIG_0")]
        public static extern IntPtr XmlContainer_getAllDocuments__SWIG_0(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getAllDocuments__SWIG_1")]
        public static extern IntPtr XmlContainer_getAllDocuments__SWIG_1(IntPtr jarg1, IntPtr jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getContainerType")]
        public static extern int XmlContainer_getContainerType(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getDocument__SWIG_0")]
        public static extern IntPtr XmlContainer_getDocument__SWIG_0(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getDocument__SWIG_1")]
        public static extern IntPtr XmlContainer_getDocument__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getDocument__SWIG_2")]
        public static extern IntPtr XmlContainer_getDocument__SWIG_2(IntPtr jarg1, string jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getDocument__SWIG_3")]
        public static extern IntPtr XmlContainer_getDocument__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getIndexSpecification__SWIG_0")]
        public static extern IntPtr XmlContainer_getIndexSpecification__SWIG_0(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getIndexSpecification__SWIG_1")]
        public static extern IntPtr XmlContainer_getIndexSpecification__SWIG_1(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getIndexSpecification__SWIG_2")]
        public static extern IntPtr XmlContainer_getIndexSpecification__SWIG_2(IntPtr jarg1, IntPtr jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getManager")]
        public static extern IntPtr XmlContainer_getManager(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getName")]
        public static extern string XmlContainer_getName(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getNumDocuments__SWIG_0")]
        public static extern uint XmlContainer_getNumDocuments__SWIG_0(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_getNumDocuments__SWIG_1")]
        public static extern uint XmlContainer_getNumDocuments__SWIG_1(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupIndex__SWIG_0")]
        public static extern IntPtr XmlContainer_lookupIndex__SWIG_0(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, string jarg5, IntPtr jarg6, uint jarg7);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupIndex__SWIG_1")]
        public static extern IntPtr XmlContainer_lookupIndex__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, string jarg5, string jarg6, string jarg7, IntPtr jarg8, uint jarg9);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupIndex__SWIG_2")]
        public static extern IntPtr XmlContainer_lookupIndex__SWIG_2(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, string jarg4, string jarg5, string jarg6, IntPtr jarg7, uint jarg8);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupIndex__SWIG_3")]
        public static extern IntPtr XmlContainer_lookupIndex__SWIG_3(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, string jarg4, string jarg5, string jarg6, string jarg7, string jarg8, IntPtr jarg9, uint jarg10);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupStatistics__SWIG_0")]
        public static extern IntPtr XmlContainer_lookupStatistics__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, string jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupStatistics__SWIG_1")]
        public static extern IntPtr XmlContainer_lookupStatistics__SWIG_1(IntPtr jarg1, string jarg2, string jarg3, string jarg4, string jarg5, string jarg6, IntPtr jarg7);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupStatistics__SWIG_2")]
        public static extern IntPtr XmlContainer_lookupStatistics__SWIG_2(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, string jarg5, IntPtr jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_lookupStatistics__SWIG_3")]
        public static extern IntPtr XmlContainer_lookupStatistics__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, string jarg5, string jarg6, string jarg7, IntPtr jarg8);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_putDocument__SWIG_0")]
        public static extern void XmlContainer_putDocument__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_putDocument__SWIG_1")]
        public static extern string XmlContainer_putDocument__SWIG_1(IntPtr jarg1, string jarg2, string jarg3, IntPtr jarg4, uint jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_putDocument__SWIG_2")]
        public static extern string XmlContainer_putDocument__SWIG_2(IntPtr jarg1, string jarg2, IntPtr jarg3, IntPtr jarg4, uint jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_putDocument__SWIG_3")]
        public static extern string XmlContainer_putDocument__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4, IntPtr jarg5, uint jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_putDocument__SWIG_4")]
        public static extern void XmlContainer_putDocument__SWIG_4(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4, uint jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_putDocument__SWIG_5")]
        public static extern string XmlContainer_putDocument__SWIG_5(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, IntPtr jarg5, uint jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_removeAlias")]
        public static extern bool XmlContainer_removeAlias(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_replaceDefaultIndex__SWIG_0")]
        public static extern void XmlContainer_replaceDefaultIndex__SWIG_0(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_replaceDefaultIndex__SWIG_1")]
        public static extern void XmlContainer_replaceDefaultIndex__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_replaceIndex__SWIG_0")]
        public static extern void XmlContainer_replaceIndex__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, string jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_replaceIndex__SWIG_1")]
        public static extern void XmlContainer_replaceIndex__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4, string jarg5, IntPtr jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_setIndexSpecification__SWIG_0")]
        public static extern void XmlContainer_setIndexSpecification__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_setIndexSpecification__SWIG_1")]
        public static extern void XmlContainer_setIndexSpecification__SWIG_1(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_sync")]
        public static extern void XmlContainer_sync(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_updateDocument__SWIG_0")]
        public static extern void XmlContainer_updateDocument__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlContainer_updateDocument__SWIG_1")]
        public static extern void XmlContainer_updateDocument__SWIG_1(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlData_get_data")]
        public static extern IntPtr XmlData_get_data(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlData_get_size")]
        public static extern uint XmlData_get_size(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlData_set_data")]
        public static extern void XmlData_set_data(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlData_set_size")]
        public static extern void XmlData_set_size(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_fetchAllData")]
        public static extern void XmlDocument_fetchAllData(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_getContent")]
        public static extern IntPtr XmlDocument_getContent(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_getContentAsString")]
        public static extern string XmlDocument_getContentAsString(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_getContentAsXmlInputStream")]
        public static extern IntPtr XmlDocument_getContentAsXmlInputStream(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_getMetaData__SWIG_0")]
        public static extern bool XmlDocument_getMetaData__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_getMetaData__SWIG_1")]
        public static extern IntPtr XmlDocument_getMetaData__SWIG_1(IntPtr jarg1, string jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_getMetaDataIterator")]
        public static extern IntPtr XmlDocument_getMetaDataIterator(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_getName")]
        public static extern string XmlDocument_getName(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_removeMetaData")]
        public static extern void XmlDocument_removeMetaData(IntPtr jarg1, string jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_setContent__SWIG_0")]
        public static extern void XmlDocument_setContent__SWIG_0(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_setContent__SWIG_1")]
        public static extern void XmlDocument_setContent__SWIG_1(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_setContentAsXmlInputStream")]
        public static extern void XmlDocument_setContentAsXmlInputStream(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_setMetaData__SWIG_0")]
        public static extern void XmlDocument_setMetaData__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_setMetaData__SWIG_1")]
        public static extern void XmlDocument_setMetaData__SWIG_1(IntPtr jarg1, string jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlDocument_setName")]
        public static extern void XmlDocument_setName(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexDeclaration_get_index")]
        public static extern string XmlIndexDeclaration_get_index(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexDeclaration_get_name")]
        public static extern string XmlIndexDeclaration_get_name(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexDeclaration_get_uri")]
        public static extern string XmlIndexDeclaration_get_uri(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_addDefaultIndex__SWIG_0")]
        public static extern void XmlIndexSpecification_addDefaultIndex__SWIG_0(IntPtr jarg1, int jarg2, int jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_addDefaultIndex__SWIG_1")]
        public static extern void XmlIndexSpecification_addDefaultIndex__SWIG_1(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_addIndex__SWIG_0")]
        public static extern void XmlIndexSpecification_addIndex__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, int jarg4, int jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_addIndex__SWIG_1")]
        public static extern void XmlIndexSpecification_addIndex__SWIG_1(IntPtr jarg1, string jarg2, string jarg3, string jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_deleteDefaultIndex__SWIG_0")]
        public static extern void XmlIndexSpecification_deleteDefaultIndex__SWIG_0(IntPtr jarg1, int jarg2, int jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_deleteDefaultIndex__SWIG_1")]
        public static extern void XmlIndexSpecification_deleteDefaultIndex__SWIG_1(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_deleteIndex__SWIG_0")]
        public static extern void XmlIndexSpecification_deleteIndex__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, int jarg4, int jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_deleteIndex__SWIG_1")]
        public static extern void XmlIndexSpecification_deleteIndex__SWIG_1(IntPtr jarg1, string jarg2, string jarg3, string jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_find")]
        public static extern IntPtr XmlIndexSpecification_find(IntPtr jarg1, string jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_getDefaultIndex")]
        public static extern string XmlIndexSpecification_getDefaultIndex(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_getValueType")]
        public static extern int XmlIndexSpecification_getValueType(string jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_next")]
        public static extern IntPtr XmlIndexSpecification_next(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_replaceDefaultIndex__SWIG_0")]
        public static extern void XmlIndexSpecification_replaceDefaultIndex__SWIG_0(IntPtr jarg1, int jarg2, int jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_replaceDefaultIndex__SWIG_1")]
        public static extern void XmlIndexSpecification_replaceDefaultIndex__SWIG_1(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_replaceIndex__SWIG_0")]
        public static extern void XmlIndexSpecification_replaceIndex__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, int jarg4, int jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_replaceIndex__SWIG_1")]
        public static extern void XmlIndexSpecification_replaceIndex__SWIG_1(IntPtr jarg1, string jarg2, string jarg3, string jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlIndexSpecification_reset")]
        public static extern void XmlIndexSpecification_reset(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlInputStream_curPos")]
        public static extern uint XmlInputStream_curPos(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlInputStream_readBytes")]
        public static extern uint XmlInputStream_readBytes(IntPtr jarg1, IntPtr jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createContainer__SWIG_0")]
        public static extern IntPtr XmlManager_createContainer__SWIG_0(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createContainer__SWIG_1")]
        public static extern IntPtr XmlManager_createContainer__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createContainer__SWIG_2")]
        public static extern IntPtr XmlManager_createContainer__SWIG_2(IntPtr jarg1, string jarg2, uint jarg3, int jarg4, int jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createContainer__SWIG_3")]
        public static extern IntPtr XmlManager_createContainer__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, uint jarg4, int jarg5, int jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createDocument")]
        public static extern IntPtr XmlManager_createDocument(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createLocalFileInputStream")]
        public static extern IntPtr XmlManager_createLocalFileInputStream(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createMemBufInputStream__SWIG_0")]
        public static extern IntPtr XmlManager_createMemBufInputStream__SWIG_0(IntPtr jarg1, string jarg2, uint jarg3, string jarg4, bool jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createMemBufInputStream__SWIG_1")]
        public static extern IntPtr XmlManager_createMemBufInputStream__SWIG_1(IntPtr jarg1, string jarg2, uint jarg3, bool jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createModify")]
        public static extern IntPtr XmlManager_createModify(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createQueryContext__SWIG_0")]
        public static extern IntPtr XmlManager_createQueryContext__SWIG_0(IntPtr jarg1, int jarg2, int jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createQueryContext__SWIG_1")]
        public static extern IntPtr XmlManager_createQueryContext__SWIG_1(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createQueryContext__SWIG_2")]
        public static extern IntPtr XmlManager_createQueryContext__SWIG_2(IntPtr jarg1, int jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createResults")]
        public static extern IntPtr XmlManager_createResults(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createStdInInputStream")]
        public static extern IntPtr XmlManager_createStdInInputStream(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createTransaction__SWIG_0")]
        public static extern IntPtr XmlManager_createTransaction__SWIG_0(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createTransaction__SWIG_1")]
        public static extern IntPtr XmlManager_createTransaction__SWIG_1(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createUpdateContext")]
        public static extern IntPtr XmlManager_createUpdateContext(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createURLInputStream__SWIG_0")]
        public static extern IntPtr XmlManager_createURLInputStream__SWIG_0(IntPtr jarg1, string jarg2, string jarg3, string jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_createURLInputStream__SWIG_1")]
        public static extern IntPtr XmlManager_createURLInputStream__SWIG_1(IntPtr jarg1, string jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_dumpContainer")]
        public static extern void XmlManager_dumpContainer(IntPtr jarg1, string jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_get_version_major")]
        public static extern int XmlManager_get_version_major();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_get_version_minor")]
        public static extern int XmlManager_get_version_minor();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_get_version_patch")]
        public static extern int XmlManager_get_version_patch();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_get_version_string")]
        public static extern string XmlManager_get_version_string();
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_getDbEnv")]
        public static extern IntPtr XmlManager_getDbEnv(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_getDefaultContainerFlags")]
        public static extern uint XmlManager_getDefaultContainerFlags(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_getDefaultContainerType")]
        public static extern int XmlManager_getDefaultContainerType(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_getDefaultPageSize")]
        public static extern uint XmlManager_getDefaultPageSize(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_getHome")]
        public static extern string XmlManager_getHome(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_loadContainer")]
        public static extern void XmlManager_loadContainer(IntPtr jarg1, string jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_openContainer__SWIG_0")]
        public static extern IntPtr XmlManager_openContainer__SWIG_0(IntPtr jarg1, IntPtr jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_openContainer__SWIG_1")]
        public static extern IntPtr XmlManager_openContainer__SWIG_1(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_openContainer__SWIG_2")]
        public static extern IntPtr XmlManager_openContainer__SWIG_2(IntPtr jarg1, string jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_openContainer__SWIG_3")]
        public static extern IntPtr XmlManager_openContainer__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_prepare__SWIG_0")]
        public static extern IntPtr XmlManager_prepare__SWIG_0(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_prepare__SWIG_1")]
        public static extern IntPtr XmlManager_prepare__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_query__SWIG_0")]
        public static extern IntPtr XmlManager_query__SWIG_0(IntPtr jarg1, string jarg2, IntPtr jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_query__SWIG_1")]
        public static extern IntPtr XmlManager_query__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4, uint jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_query__SWIG_2")]
        public static extern IntPtr XmlManager_query__SWIG_2(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_query__SWIG_3")]
        public static extern IntPtr XmlManager_query__SWIG_3(IntPtr jarg1, IntPtr jarg2, string jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_registerResolver")]
        public static extern void XmlManager_registerResolver(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_removeContainer__SWIG_0")]
        public static extern void XmlManager_removeContainer__SWIG_0(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_removeContainer__SWIG_1")]
        public static extern void XmlManager_removeContainer__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_renameContainer__SWIG_0")]
        public static extern void XmlManager_renameContainer__SWIG_0(IntPtr jarg1, string jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_renameContainer__SWIG_1")]
        public static extern void XmlManager_renameContainer__SWIG_1(IntPtr jarg1, IntPtr jarg2, string jarg3, string jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_setDefaultContainerFlags")]
        public static extern void XmlManager_setDefaultContainerFlags(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_setDefaultContainerType")]
        public static extern void XmlManager_setDefaultContainerType(IntPtr jarg1, int jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_setDefaultPageSize")]
        public static extern void XmlManager_setDefaultPageSize(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_setLogCategory")]
        public static extern void XmlManager_setLogCategory(int jarg1, bool jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_setLogLevel")]
        public static extern void XmlManager_setLogLevel(int jarg1, bool jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_upgradeContainer")]
        public static extern void XmlManager_upgradeContainer(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlManager_verifyContainer")]
        public static extern void XmlManager_verifyContainer(IntPtr jarg1, string jarg2, string jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlMetaData_get_name")]
        public static extern string XmlMetaData_get_name(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlMetaData_get_uri")]
        public static extern string XmlMetaData_get_uri(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlMetaData_get_value")]
        public static extern IntPtr XmlMetaData_get_value(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlMetaDataIterator_next")]
        public static extern IntPtr XmlMetaDataIterator_next(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlMetaDataIterator_reset")]
        public static extern void XmlMetaDataIterator_reset(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_addAppendStep")]
        public static extern void XmlModify_addAppendStep(IntPtr jarg1, IntPtr jarg2, int jarg3, string jarg4, string jarg5, int jarg6);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_addInsertAfterStep")]
        public static extern void XmlModify_addInsertAfterStep(IntPtr jarg1, IntPtr jarg2, int jarg3, string jarg4, string jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_addInsertBeforeStep")]
        public static extern void XmlModify_addInsertBeforeStep(IntPtr jarg1, IntPtr jarg2, int jarg3, string jarg4, string jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_addRemoveStep")]
        public static extern void XmlModify_addRemoveStep(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_addRenameStep")]
        public static extern void XmlModify_addRenameStep(IntPtr jarg1, IntPtr jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_addUpdateStep")]
        public static extern void XmlModify_addUpdateStep(IntPtr jarg1, IntPtr jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_execute__SWIG_0")]
        public static extern uint XmlModify_execute__SWIG_0(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_execute__SWIG_1")]
        public static extern uint XmlModify_execute__SWIG_1(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_execute__SWIG_2")]
        public static extern uint XmlModify_execute__SWIG_2(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_execute__SWIG_3")]
        public static extern uint XmlModify_execute__SWIG_3(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlModify_setNewEncoding")]
        public static extern void XmlModify_setNewEncoding(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_clearNamespaces")]
        public static extern void XmlQueryContext_clearNamespaces(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_getBaseURI")]
        public static extern string XmlQueryContext_getBaseURI(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_getEvaluationType")]
        public static extern int XmlQueryContext_getEvaluationType(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_getNamespace")]
        public static extern string XmlQueryContext_getNamespace(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_getReturnType")]
        public static extern int XmlQueryContext_getReturnType(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_getVariableValue")]
        public static extern IntPtr XmlQueryContext_getVariableValue(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_removeNamespace")]
        public static extern void XmlQueryContext_removeNamespace(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_setBaseURI")]
        public static extern void XmlQueryContext_setBaseURI(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_setEvaluationType")]
        public static extern void XmlQueryContext_setEvaluationType(IntPtr jarg1, int jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_setNamespace")]
        public static extern void XmlQueryContext_setNamespace(IntPtr jarg1, string jarg2, string jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_setReturnType")]
        public static extern void XmlQueryContext_setReturnType(IntPtr jarg1, int jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryContext_setVariableValue")]
        public static extern void XmlQueryContext_setVariableValue(IntPtr jarg1, string jarg2, IntPtr jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryExpression_execute__SWIG_0")]
        public static extern IntPtr XmlQueryExpression_execute__SWIG_0(IntPtr jarg1, IntPtr jarg2, uint jarg3);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryExpression_execute__SWIG_1")]
        public static extern IntPtr XmlQueryExpression_execute__SWIG_1(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryExpression_execute__SWIG_2")]
        public static extern IntPtr XmlQueryExpression_execute__SWIG_2(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, uint jarg4);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryExpression_execute__SWIG_3")]
        public static extern IntPtr XmlQueryExpression_execute__SWIG_3(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, IntPtr jarg4, uint jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryExpression_getQuery")]
        public static extern string XmlQueryExpression_getQuery(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlQueryExpression_getQueryPlan")]
        public static extern string XmlQueryExpression_getQueryPlan(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResolver_resolveCollection")]
        public static extern bool XmlResolver_resolveCollection(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, string jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResolver_resolveDocument")]
        public static extern bool XmlResolver_resolveDocument(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, string jarg4, IntPtr jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResolver_resolveEntity")]
        public static extern IntPtr XmlResolver_resolveEntity(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, string jarg4, string jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResolver_resolveSchema")]
        public static extern IntPtr XmlResolver_resolveSchema(IntPtr jarg1, IntPtr jarg2, IntPtr jarg3, string jarg4, string jarg5);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_add")]
        public static extern void XmlResults_add(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_hasNext")]
        public static extern bool XmlResults_hasNext(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_hasPrevious")]
        public static extern bool XmlResults_hasPrevious(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_next")]
        public static extern IntPtr XmlResults_next(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_peek")]
        public static extern IntPtr XmlResults_peek(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_previous")]
        public static extern IntPtr XmlResults_previous(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_reset")]
        public static extern void XmlResults_reset(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlResults_size")]
        public static extern uint XmlResults_size(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlStatistics_getNumberOfIndexedKeys")]
        public static extern double XmlStatistics_getNumberOfIndexedKeys(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlStatistics_getNumberOfUniqueKeys")]
        public static extern double XmlStatistics_getNumberOfUniqueKeys(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlStatistics_getSumKeyValueSize")]
        public static extern double XmlStatistics_getSumKeyValueSize(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlTransaction_abort")]
        public static extern void XmlTransaction_abort(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlTransaction_commit__SWIG_0")]
        public static extern void XmlTransaction_commit__SWIG_0(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlTransaction_commit__SWIG_1")]
        public static extern void XmlTransaction_commit__SWIG_1(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlTransaction_createChild")]
        public static extern IntPtr XmlTransaction_createChild(IntPtr jarg1, uint jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlUpdateContext_getApplyChangesToContainers")]
        public static extern bool XmlUpdateContext_getApplyChangesToContainers(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlUpdateContext_setApplyChangesToContainers")]
        public static extern void XmlUpdateContext_setApplyChangesToContainers(IntPtr jarg1, bool jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_asBoolean")]
        public static extern bool XmlValue_asBoolean(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_asDocument")]
        public static extern IntPtr XmlValue_asDocument(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_asNumber")]
        public static extern double XmlValue_asNumber(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_asString__SWIG_0")]
        public static extern string XmlValue_asString__SWIG_0(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_asString__SWIG_1")]
        public static extern string XmlValue_asString__SWIG_1(IntPtr jarg1, string jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_equals")]
        public static extern bool XmlValue_equals(IntPtr jarg1, IntPtr jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getAttributes")]
        public static extern IntPtr XmlValue_getAttributes(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getFirstChild")]
        public static extern IntPtr XmlValue_getFirstChild(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getLastChild")]
        public static extern IntPtr XmlValue_getLastChild(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getLocalName")]
        public static extern string XmlValue_getLocalName(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getNamespaceURI")]
        public static extern string XmlValue_getNamespaceURI(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getNextSibling")]
        public static extern IntPtr XmlValue_getNextSibling(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getNodeName")]
        public static extern string XmlValue_getNodeName(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getNodeType")]
        public static extern short XmlValue_getNodeType(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getNodeValue")]
        public static extern string XmlValue_getNodeValue(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getOwnerElement")]
        public static extern IntPtr XmlValue_getOwnerElement(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getParentNode")]
        public static extern IntPtr XmlValue_getParentNode(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getPrefix")]
        public static extern string XmlValue_getPrefix(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getPreviousSibling")]
        public static extern IntPtr XmlValue_getPreviousSibling(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_getType")]
        public static extern int XmlValue_getType(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_isBoolean")]
        public static extern bool XmlValue_isBoolean(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_isNode")]
        public static extern bool XmlValue_isNode(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_isNumber")]
        public static extern bool XmlValue_isNumber(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_isString")]
        public static extern bool XmlValue_isString(IntPtr jarg1);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_isType")]
        public static extern bool XmlValue_isType(IntPtr jarg1, int jarg2);
        [DllImport("libdbxml_dotnet21.dll", EntryPoint="CSharp_XmlValue_setValue")]
        public static extern void XmlValue_setValue(IntPtr jarg1, IntPtr jarg2);

        private class DbXmlExceptionThrower
        {
            private static DbXmlExceptionDelegate exceptionDelegate = new DbXmlExceptionDelegate(DbXmlPINVOKE.DbXmlExceptionThrower.ThrowXmlException);

            static DbXmlExceptionThrower()
            {
                DbXmlRegisterExceptionCallback(exceptionDelegate);
            }

            [DllImport("libdbxml_dotnet21.dll")]
            public static extern void DbXmlRegisterExceptionCallback(DbXmlExceptionDelegate exceptionDelegate);
            private static void ThrowXmlException(int code, string message)
            {
                if (code == Sleepycat.DbXml.Internal.ExceptionCode.NULL_POINTER)
                {
                    throw new NullReferenceException(message);
                }
                throw new DbXmlException(code, message);
            }

            public delegate void DbXmlExceptionDelegate(int code, string message);
        }

        private class SWIGExceptionHelper
        {
            private static SWIGExceptionDelegate argumentOutOfRangeDelegate = new SWIGExceptionDelegate(DbXmlPINVOKE.SWIGExceptionHelper.ThrowArgumentOutOfRangeException);
            private static SWIGExceptionDelegate divideByZeroDelegate = new SWIGExceptionDelegate(DbXmlPINVOKE.SWIGExceptionHelper.ThrowDivideByZeroException);
            private static SWIGExceptionDelegate indexOutOfRangeDelegate = new SWIGExceptionDelegate(DbXmlPINVOKE.SWIGExceptionHelper.ThrowIndexOutOfRangeException);
            private static SWIGExceptionDelegate nullReferenceDelegate = new SWIGExceptionDelegate(DbXmlPINVOKE.SWIGExceptionHelper.ThrowNullReferenceException);
            private static SWIGExceptionDelegate outOfMemoryDelegate = new SWIGExceptionDelegate(DbXmlPINVOKE.SWIGExceptionHelper.ThrowOutOfMemoryException);
            private static SWIGExceptionDelegate systemDelegate = new SWIGExceptionDelegate(DbXmlPINVOKE.SWIGExceptionHelper.ThrowSystemException);

            static SWIGExceptionHelper()
            {
                SWIGRegisterExceptionCallbacks_DbXml(systemDelegate, outOfMemoryDelegate, indexOutOfRangeDelegate, divideByZeroDelegate, argumentOutOfRangeDelegate, nullReferenceDelegate);
            }

            [DllImport("libdbxml_dotnet21.dll")]
            public static extern void SWIGRegisterExceptionCallbacks_DbXml(SWIGExceptionDelegate systemExceptionDelegate, SWIGExceptionDelegate outOfMemoryDelegate, SWIGExceptionDelegate indexOutOfRangeDelegate, SWIGExceptionDelegate divideByZeroDelegate, SWIGExceptionDelegate argumentOutOfRangeDelegate, SWIGExceptionDelegate nullReferenceDelegate);
            private static void ThrowArgumentOutOfRangeException(string message)
            {
                throw new ArgumentOutOfRangeException(message);
            }

            private static void ThrowDivideByZeroException(string message)
            {
                throw new DivideByZeroException(message);
            }

            private static void ThrowIndexOutOfRangeException(string message)
            {
                throw new IndexOutOfRangeException(message);
            }

            private static void ThrowNullReferenceException(string message)
            {
                throw new NullReferenceException(message);
            }

            private static void ThrowOutOfMemoryException(string message)
            {
                throw new OutOfMemoryException(message);
            }

            private static void ThrowSystemException(string message)
            {
                throw new SystemException(message);
            }

            public delegate void SWIGExceptionDelegate(string message);
        }

        private class SWIGStringHelper
        {
            private static SWIGStringDelegate stringDelegate = new SWIGStringDelegate(DbXmlPINVOKE.SWIGStringHelper.CreateString);

            static SWIGStringHelper()
            {
                SWIGRegisterStringCallback_DbXml(stringDelegate);
            }

            private static string CreateString(string cString)
            {
                return cString;
            }

            [DllImport("libdbxml_dotnet21.dll")]
            public static extern void SWIGRegisterStringCallback_DbXml(SWIGStringDelegate stringDelegate);

            public delegate string SWIGStringDelegate(string message);
        }
    }
}