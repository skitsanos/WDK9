// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System;
using System.DirectoryServices;

namespace WDK.Network.IIS
{
    public class IISFTPServer
    {
        internal bool bAllowAnonymous;
        internal int iID;
        internal int iMaxConnections;

        internal string sExitMessage;
        internal string sGreetingMessage;

        internal string sMaxClientsMessage;
        internal string sRootPath;
        internal string sServerName;

        internal IISFTPServer()
        {
            iID = -1;
            sServerName = "";
            sRootPath = "";
        }


        public int MaxConnections
        {
            get { return iMaxConnections; }

            set
            {
                if (iID == -1)
                {
                    throw new Exception("IISFTPServer variable not initialized");
                }
                if (value < 0 || value > int.MaxValue)
                {
                    value = int.MaxValue;
                }
                var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID));
                directoryEntry.Invoke("Put", new object[] {"MaxConnections", value});
                directoryEntry.Invoke("SetInfo", new object[0]);
                iMaxConnections = value;
                directoryEntry.Dispose();
            }
        }

        public string MaxClientsMessage
        {
            get { return sMaxClientsMessage; }

            set
            {
                if (iID == -1)
                {
                    throw new Exception("IISFTPServer variable not initialized");
                }
                var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID));
                directoryEntry.Invoke("Put", new object[] {"MaxClientsMessage", value});
                directoryEntry.Invoke("SetInfo", new object[0]);
                sMaxClientsMessage = value;
                directoryEntry.Dispose();
            }
        }

        public string ExitMessage
        {
            get { return sExitMessage; }

            set
            {
                if (iID == -1)
                {
                    throw new Exception("IISFTPServer variable not initialized");
                }
                var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID));
                directoryEntry.Invoke("Put", new object[] {"ExitMessage", value});
                directoryEntry.Invoke("SetInfo", new object[0]);
                sExitMessage = value;
                directoryEntry.Dispose();
            }
        }

        public string GreetingMessage
        {
            get { return sGreetingMessage; }

            set
            {
                if (iID == -1)
                {
                    throw new Exception("IISFTPServer variable not initialized");
                }
                var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID));
                directoryEntry.Invoke("Put", new object[] {"GreetingMessage", value});
                directoryEntry.Invoke("SetInfo", new object[0]);
                sGreetingMessage = value;
                directoryEntry.Dispose();
            }
        }

        public bool AllowAnonymous
        {
            get { return bAllowAnonymous; }

            set
            {
                if (iID == -1)
                {
                    throw new Exception("IISFTPServer variable not initialized");
                }
                var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID));
                directoryEntry.Invoke("Put", new object[] {"AllowAnonymous", value});
                directoryEntry.Invoke("SetInfo", new object[0]);
                bAllowAnonymous = value;
                directoryEntry.Dispose();
            }
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
                    throw new Exception("IISFTPServer variable not initialized");
                }
                var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID));
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

        public void Start()
        {
            if (iID == -1)
            {
                throw new Exception("IISFTPServer variable not initialized");
            }
            new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID)).Invoke("Start", new object[0]);
        }

        public void Stop()
        {
            if (iID == -1)
            {
                throw new Exception("IISFTPServer variable not initialized");
            }
            new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID)).Invoke("Stop", new object[0]);
        }

        public ServerState Status()
        {
            if (iID != -1)
            {
                return
                    (ServerState)
                    new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID)).Properties["ServerState"][0];
            }
            throw new Exception("IISFTPServer variable not initialized");
        }

        public void Pause()
        {
            if (iID == -1)
            {
                throw new Exception("IISFTPServer variable not initialized");
            }
            new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iID)).Invoke("Pause", new object[0]);
        }

        public void Delete()
        {
            if (iID == -1)
            {
                throw new Exception("IISFtpServer variable not initialized");
            }
            new DirectoryEntry("IIS://localhost/MSFTPSVC").Invoke("Delete", new object[] {"IIsFtpServer", iID});
        }

        public void DeleteVirtualDirectory(string sVirtualDirectoryName)
        {
            if (iID == -1)
            {
                throw new Exception("IISFtpServer variable not initialized");
            }
            var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", ID, "/ROOT"));
            directoryEntry.Invoke("Delete", new object[] {"IISFtpVirtualDir", sVirtualDirectoryName});
            directoryEntry.CommitChanges();
            directoryEntry.Dispose();
        }

        public void CreateVirtualDirectory(string sVirtualDirectoryName, string sPath, bool bCanRead, bool bCanWrite)
        {
            if (iID == -1)
            {
                throw new Exception("IISFTPServer variable not initialized");
            }
            var directoryEntry1 = new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", ID, "/ROOT"));
            var directoryEntry2 =
                (DirectoryEntry)
                directoryEntry1.Invoke("Create", new object[] {"IISFTPVirtualDir", sVirtualDirectoryName});
            directoryEntry2.Properties["Path"][0] = sPath;
            int i = 0;
            if (bCanRead)
            {
                i++;
            }
            if (bCanWrite)
            {
                i += 2;
            }
            directoryEntry2.Properties["AccessFlags"][0] = i;
            directoryEntry2.CommitChanges();
            directoryEntry1.Invoke("SetInfo", new object[0]);
            directoryEntry1.CommitChanges();
            directoryEntry1.Dispose();
        }
    }
}