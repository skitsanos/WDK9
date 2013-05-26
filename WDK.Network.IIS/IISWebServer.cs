// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System;
using System.DirectoryServices;

namespace WDK.Network.IIS
{
    public class IISWebServer
    {
        private readonly IISWebVirtualDirectoryCollection vdcDirectories;

        private readonly IISWebDirectoryCollection wdcDirectories;
        internal int iID;
        internal string sRootPath;
        internal string sServerName;

        internal IISWebServer()
        {
            iID = -1;
            sServerName = "";
            sRootPath = "";
            vdcDirectories = new IISWebVirtualDirectoryCollection();
            wdcDirectories = new IISWebDirectoryCollection();
        }


        public int ID
        {
            get { return iID; }
        }

        public string ServerName
        {
            get { return sServerName; }

            set
            {
                if (iID == -1)
                {
                    throw new Exception("IISWebServer variable not initialized");
                }
                var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iID));
                directoryEntry.Invoke("Put", new object[] {"ServerComment", value});
                directoryEntry.Invoke("SetInfo", new object[0]);
                sServerName = value;
                directoryEntry.Dispose();
            }
        }

        public string RootPath
        {
            get { return sRootPath; }
        }

        public IISWebVirtualDirectoryCollection VirtualDirectories
        {
            get { return vdcDirectories; }
        }

        public IISWebDirectoryCollection WebDirectories
        {
            get { return wdcDirectories; }
        }

        public void Start()
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iID)).Invoke("Start", new object[0]);
        }

        public void Stop()
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iID)).Invoke("Stop", new object[0]);
        }

        public ServerState Status()
        {
            if (iID != -1)
            {
                return
                    (ServerState)
                    new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iID)).Properties["ServerState"][0];
            }
            else
            {
                throw new Exception("IISWebServer variable not initialized");
            }
        }

        public void Pause()
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iID)).Invoke("Pause", new object[0]);
        }

        public void Delete()
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            new DirectoryEntry("IIS://localhost/W3SVC").Invoke("Delete", new object[] {"IIsWebServer", iID});
        }

        public void CreateApplication(string sVirtualDirectoryName)
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            var locals = new object[] {"IIS://localhost/W3SVC/", ID, "/ROOT/", sVirtualDirectoryName};
            var directoryEntry = new DirectoryEntry(String.Concat(locals));
            directoryEntry.Properties["AppIsolated"][0] = 2;
            locals = new object[] {"/LM/W3SVC/", ID, "/ROOT/", sVirtualDirectoryName};
            directoryEntry.Properties["AppRoot"][0] = String.Concat(locals);
            locals = new object[] {2};
            directoryEntry.Invoke("AppCreate", locals);
            directoryEntry.Properties["AppFriendlyName"][0] = sVirtualDirectoryName;
            directoryEntry.CommitChanges();
        }

        public void DeleteApplication(string sVirtualDirectoryName)
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            var directoryEntry =
                new DirectoryEntry(
                    String.Concat(new object[] {"IIS://localhost/W3SVC/", ID, "/ROOT/", sVirtualDirectoryName}));
            directoryEntry.Invoke("AppDelete", new object[0]);
            directoryEntry.CommitChanges();
        }

        public void CreateVirtualDirectory(string sVirtualDirectoryName, string sPath, bool isApplication)
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            var directoryEntry1 = new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", ID, "/ROOT"));
            var directoryEntry2 =
                (DirectoryEntry)
                directoryEntry1.Invoke("Create", new object[] {"IISWebVirtualDir", sVirtualDirectoryName});
            directoryEntry2.Properties["Path"][0] = sPath;
            directoryEntry2.Properties["AccessFlags"][0] = 513;
            directoryEntry2.Properties["FrontPageWeb"][0] = 1;
            directoryEntry2.CommitChanges();
            directoryEntry1.Invoke("SetInfo", new object[0]);
            directoryEntry1.CommitChanges();
            if (isApplication)
            {
                CreateApplication(sVirtualDirectoryName);
            }
            directoryEntry1.Dispose();
        }

        public void DeleteVirtualDirectory(string sVirtualDirectoryName)
        {
            if (iID == -1)
            {
                throw new Exception("IISWebServer variable not initialized");
            }
            var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", ID, "/ROOT"));
            directoryEntry.Invoke("Delete", new object[] {"IISWebVirtualDir", sVirtualDirectoryName});
            directoryEntry.CommitChanges();
        }

        public IISWebVirtualDirectory GetVirtualDirectory(string sVirtualDirectoryName)
        {
            IISWebVirtualDirectory iISWebVirtualDirectory = null;
            for (int i = 0; i < VirtualDirectories.Count; i++)
            {
                if (VirtualDirectories[i].Name.Equals(sVirtualDirectoryName.Trim()))
                {
                    iISWebVirtualDirectory = VirtualDirectories[i];
                    break;
                }
            }
            return iISWebVirtualDirectory;
        }
    }
}