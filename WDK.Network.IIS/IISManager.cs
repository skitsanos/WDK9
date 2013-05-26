// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System;
using System.Collections;
using System.DirectoryServices;
using System.IO;

namespace WDK.Network.IIS
{
    public class IISManager
    {
        public void DeleteWebServer(int iWebSiteID)
        {
            new DirectoryEntry("IIS://localhost/W3SVC").Invoke("Delete", new object[] {"IIsWebServer", iWebSiteID});
        }

        public void DeleteWebServer(IISWebServer ws)
        {
            DeleteWebServer(ws.ID);
        }

        public void StartWebServer(int iWebSiteID)
        {
            new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iWebSiteID)).Invoke("Start", new object[0]);
        }

        public void StartWebServer(IISWebServer ws)
        {
            StartWebServer(ws.ID);
        }

        public void StopWebServer(int iWebSiteID)
        {
            new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iWebSiteID)).Invoke("Stop", new object[0]);
        }

        public void StopWebServer(IISWebServer ws)
        {
            StopWebServer(ws.ID);
        }

        public void PauseWebServer(int iWebSiteID)
        {
            new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iWebSiteID)).Invoke("Pause", new object[0]);
        }

        public void PauseWebServer(IISWebServer ws)
        {
            PauseWebServer(ws.ID);
        }

        public ServerState WebServerStatus(int iWebSiteID)
        {
            return
                (ServerState)
                new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iWebSiteID)).Properties["ServerState"][0];
        }

        public ServerState WebServerStatus(IISWebServer ws)
        {
            return
                (ServerState)
                new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", ws.ID)).Properties["ServerState"][0];
        }

        public void CreateWebServer(string sServerName, string sRootPath)
        {
            CreateWebServer(sServerName, sRootPath, "", "", -1, new[] {"Default.html"});
        }

        public void CreateWebServer(string sServerName, string sRootPath, int iPort)
        {
            CreateWebServer(sServerName, sRootPath, "", "", iPort, new[] {"Default.html"});
        }

        public void CreateWebServer(string sServerName, string sRootPath, int iPort, string[] DefaultDocs)
        {
            CreateWebServer(sServerName, sRootPath, "", "", iPort, DefaultDocs);
        }

        public void CreateWebServer(string sServerName, string sRootPath, int iPort, string[] DefaultDocs, string sIP)
        {
            CreateWebServer(sServerName, sRootPath, sIP, "", iPort, DefaultDocs);
        }

        public void CreateWebServer(string sServerName, string sRootPath, string sIP, string sHostName, int iPort,
                                    string[] DefaultDocs)
        {
            int i = 1;
            string str = "";
            if (iPort < 1 || iPort > 65535)
            {
                iPort = 80;
            }
            var directoryEntry1 = new DirectoryEntry("IIS://localhost/W3SVC");
            IEnumerator iEnumerator = directoryEntry1.Children.GetEnumerator();
            try
            {
                while (iEnumerator.MoveNext())
                {
                    var directoryEntry2 = (DirectoryEntry) iEnumerator.Current;
                    if (directoryEntry2.SchemaClassName == "IIsWebServer")
                    {
                        int j = Convert.ToInt32(directoryEntry2.Name);
                        if (j >= i)
                        {
                            i = j + 1;
                        }
                    }
                }
            }
            finally
            {
                var iDisposable = iEnumerator as IDisposable;
                if (iDisposable != null)
                {
                    iDisposable.Dispose();
                }
            }
            var locals = new object[] {"IIsWebServer", i};
            var directoryEntry3 = (DirectoryEntry) directoryEntry1.Invoke("Create", locals);
            locals = new object[] {"ServerComment", sServerName};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"KeyType", "IIsWebServer"};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerBindings", String.Concat(new[] {sIP, ":", iPort.ToString(), ":", sHostName})};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerState", 2};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"FrontPageWeb", 1};
            directoryEntry3.Invoke("Put", locals);
            for (int k = 0; k < DefaultDocs.Length; k++)
            {
                str = String.Concat(str, DefaultDocs[k], ",");
            }
            locals = new object[] {"DefaultDoc", str};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerAutoStart", 1};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerSize", 1};
            directoryEntry3.Invoke("Put", locals);
            directoryEntry3.Invoke("SetInfo", new object[0]);
            CreateWebServerVirtualDirectory(i, sServerName, sRootPath, true, true);
        }

        public void CreateWebServerVirtualDirectory(IISWebServer ws, string sVirtualDirectoryName, string sPath,
                                                    bool isApplication)
        {
            CreateWebServerVirtualDirectory(ws.ID, sVirtualDirectoryName, sPath, isApplication, false);
        }

        public void CreateWebServerVirtualDirectory(int iWebSiteID, string sVirtualDirectoryName, string sPath,
                                                    bool isApplication)
        {
            CreateWebServerVirtualDirectory(iWebSiteID, sVirtualDirectoryName, sPath, isApplication, false);
        }

        public void CreateWebServerVirtualDirectory(int iWebSiteID, string sVirtualDirectoryName, string sPath,
                                                    bool isApplication, bool isRoot)
        {
            DirectoryEntry directoryEntry1;

            DirectoryEntry directoryEntry2;

            if (!isRoot)
            {
                directoryEntry1 = new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iWebSiteID, "/ROOT"));
                var locals = new object[] {"IISWebVirtualDir", sVirtualDirectoryName};
                directoryEntry2 = (DirectoryEntry) directoryEntry1.Invoke("Create", locals);
            }
            else
            {
                directoryEntry1 = new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iWebSiteID));
                var locals = new object[] {"IISWebVirtualDir", "ROOT"};
                directoryEntry2 = (DirectoryEntry) directoryEntry1.Invoke("Create", locals);
            }
            directoryEntry2.Properties["Path"][0] = sPath;
            directoryEntry2.Properties["AccessFlags"][0] = 513;
            directoryEntry2.Properties["FrontPageWeb"][0] = 1;
            directoryEntry2.CommitChanges();
            directoryEntry1.Invoke("SetInfo", new object[0]);
            directoryEntry1.CommitChanges();
            if (isApplication)
            {
                CreateWebSiteApplication(iWebSiteID, sVirtualDirectoryName, isRoot);
            }
        }

        public void DeleteWebServerVirtualDirectory(int iWebSiteID, string sVirtualDirectoryName)
        {
            var directoryEntry = new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iWebSiteID, "/ROOT"));
            directoryEntry.Invoke("Delete", new object[] {"IISWebVirtualDir", sVirtualDirectoryName});
            directoryEntry.CommitChanges();
        }

        public void DeleteWebServerVirtualDirectory(IISWebServer ws, string sVirtualDirectoryName)
        {
            DeleteWebServerVirtualDirectory(ws.ID, sVirtualDirectoryName);
        }

        public void DeleteWebSiteApplication(int iWebSiteID, string sVirtualDirectoryName)
        {
            DeleteWebSiteApplication(iWebSiteID, sVirtualDirectoryName, false);
        }

        public void DeleteWebSiteApplication(int iWebSiteID, string sVirtualDirectoryName, bool isRoot)
        {
            string str = String.Concat("IIS://localhost/W3SVC/", iWebSiteID, "/ROOT");
            if (!isRoot)
            {
                str = String.Concat(str, "/", sVirtualDirectoryName);
            }
            var directoryEntry = new DirectoryEntry(str);
            directoryEntry.Invoke("AppDelete", new object[0]);
        }

        public void DeleteWebSiteApplication(IISWebServer ws, string sVirtualDirectoryName)
        {
            DeleteWebSiteApplication(ws.ID, sVirtualDirectoryName);
        }

        public void CreateWebSiteApplication(IISWebServer ws, string sVirtualDirectoryName)
        {
            CreateWebSiteApplication(ws.ID, sVirtualDirectoryName, false);
        }

        public void CreateWebSiteApplication(int iWebSiteID, string sVirtualDirectoryName)
        {
            CreateWebSiteApplication(iWebSiteID, sVirtualDirectoryName, false);
        }

        public void CreateWebSiteApplication(int iWebSiteID, string sVirtualDirectoryName, bool isRoot)
        {
            
            var str = String.Concat("IIS://localhost/W3SVC/", iWebSiteID, "/ROOT");
            if (!isRoot)
            {
                str = String.Concat(str, "/", sVirtualDirectoryName);
            }
            var directoryEntry = new DirectoryEntry(str);
            directoryEntry.Properties["AppIsolated"][0] = 2;
            directoryEntry.Properties["AppRoot"][0] = String.Concat("/LM/W3SVC/", iWebSiteID, "/ROOT");
            if (!isRoot)
            {
                PropertyValueCollection propertyValueCollection;
                (propertyValueCollection = directoryEntry.Properties["AppRoot"])[0] =
                    String.Concat(propertyValueCollection[0], "/", sVirtualDirectoryName);
            }
            directoryEntry.Invoke("AppCreate", new object[] {2});
            directoryEntry.Properties["AppFriendlyName"][0] = sVirtualDirectoryName;
            directoryEntry.CommitChanges();
        }

        public IISWebServer GetWebServer(string sWerServerName)
        {
            DirectoryEntry directoryEntry3;

            IISWebServer iISWebServer = null;
            IEnumerator iEnumerator1 = new DirectoryEntry("IIS://localhost/W3SVC").Children.GetEnumerator();
            try
            {
                while (iEnumerator1.MoveNext())
                {
                    var directoryEntry2 = (DirectoryEntry) iEnumerator1.Current;
                    if (directoryEntry2.SchemaClassName == "IIsWebServer" &&
                        ((String) directoryEntry2.Properties["ServerComment"].Value).Equals(sWerServerName))
                    {
                        iISWebServer = new IISWebServer
                                           {
                                               iID = Convert.ToInt32(directoryEntry2.Name),
                                               sServerName = (String) directoryEntry2.Properties["ServerComment"].Value
                                           };
                        directoryEntry3 =
                            new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iISWebServer.ID, "/Root"));
                        iISWebServer.sRootPath = directoryEntry3.Properties["Path"][0] as String;
                        var iEnumerator2 = directoryEntry3.Children.GetEnumerator();
                        try
                        {
                            while (iEnumerator2.MoveNext())
                            {
                                var directoryEntry4 = (DirectoryEntry) iEnumerator2.Current;
                                if (directoryEntry4.SchemaClassName.ToUpper() != "IIsWebVirtualDir".ToUpper()) continue;
                                var iISWebVirtualDirectory = new IISWebVirtualDirectory
                                                                 {
                                                                     _sPath =
                                                                         (String)
                                                                         directoryEntry4.Properties["Path"][0],
                                                                     _sName = directoryEntry4.Name,
                                                                     _isApplication =
                                                                         (String)
                                                                         directoryEntry4.Properties["AppRoot"][0] !=
                                                                         String.Concat("/LM/W3SVC/", iISWebServer.ID,
                                                                                       "/ROOT"),
                                                                     _iWebServerID = iISWebServer.ID
                                                                 };
                                AddWebDirectories(directoryEntry4, String.Concat("/", iISWebVirtualDirectory._sName),
                                                  iISWebVirtualDirectory.WebDirectories, iISWebServer.ID,
                                                  iISWebVirtualDirectory._sPath);
                                iISWebServer.VirtualDirectories.Add(iISWebVirtualDirectory);
                            }
                        }
                        finally
                        {
                            var iDisposable = iEnumerator2 as IDisposable;
                            if (iDisposable != null)
                            {
                                iDisposable.Dispose();
                            }
                        }
                        AddWebDirectories(directoryEntry3, "", iISWebServer.WebDirectories, iISWebServer.ID,
                                          iISWebServer.RootPath);
                        directoryEntry3.Dispose();
                        break;
                    }
                }
            }
            finally
            {
                var iDisposable = iEnumerator1 as IDisposable;
                if (iDisposable != null)
                {
                    iDisposable.Dispose();
                }
            }
            return iISWebServer;
        }

        public IISWebServerCollection GetWebServers()
        {
            var iISWebServerCollection = new IISWebServerCollection();
            IEnumerator iEnumerator1 = new DirectoryEntry("IIS://localhost/W3SVC").Children.GetEnumerator();
            try
            {
                while (iEnumerator1.MoveNext())
                {
                    var directoryEntry2 = (DirectoryEntry) iEnumerator1.Current;
                    if (directoryEntry2.SchemaClassName != "IIsWebServer") continue;
                    var iISWebServer = new IISWebServer
                                           {
                                               iID = Convert.ToInt32(directoryEntry2.Name),
                                               sServerName =
                                                   (String) directoryEntry2.Properties["ServerComment"].Value
                                           };
                    var directoryEntry3 =
                        new DirectoryEntry(String.Concat("IIS://localhost/W3SVC/", iISWebServer.ID, "/Root"));
                    iISWebServer.sRootPath = directoryEntry3.Properties["Path"][0] as String;
                    IEnumerator iEnumerator2 = directoryEntry3.Children.GetEnumerator();
                    try
                    {
                        while (iEnumerator2.MoveNext())
                        {
                            var directoryEntry4 = (DirectoryEntry) iEnumerator2.Current;
                            if (directoryEntry4.SchemaClassName.ToUpper() == "IIsWebVirtualDir".ToUpper())
                            {
                                var iISWebVirtualDirectory = new IISWebVirtualDirectory
                                                                 {
                                                                     _sPath =
                                                                         (String) directoryEntry4.Properties["Path"][0],
                                                                     _sName = directoryEntry4.Name,
                                                                     _isApplication =
                                                                         (String)
                                                                         directoryEntry4.Properties["AppRoot"][0] !=
                                                                         String.Concat("/LM/W3SVC/", iISWebServer.ID,
                                                                                       "/ROOT"),
                                                                     _iWebServerID = iISWebServer.ID
                                                                 };
                                iISWebServer.VirtualDirectories.Add(iISWebVirtualDirectory);
                            }
                        }
                    }
                    finally
                    {
                        var iDisposable = iEnumerator2 as IDisposable;
                        if (iDisposable != null)
                        {
                            iDisposable.Dispose();
                        }
                    }
                    iISWebServerCollection.Add(iISWebServer);
                }
            }
            finally
            {
                var iDisposable = iEnumerator1 as IDisposable;
                if (iDisposable != null)
                {
                    iDisposable.Dispose();
                }
            }
            return iISWebServerCollection;
        }

        private static void AddWebDirectories(DirectoryEntry o, string sParentPath, IISWebDirectoryCollection wdcParent,
                                       int iServerID, string sPhisicalPath)
        {
            IEnumerator iEnumerator = o.Children.GetEnumerator();
            try
            {
                while (iEnumerator.MoveNext())
                {
                    var directoryEntry = (DirectoryEntry) iEnumerator.Current;
                    if (directoryEntry.SchemaClassName.ToUpper() == "IIsWebDirectory".ToUpper() &&
                        Directory.Exists(String.Concat(sPhisicalPath, "\\", directoryEntry.Name)))
                    {
                        var iISWebDirectory = new IISWebDirectory
                                                  {
                                                      _sPath = String.Concat(sParentPath, "/", directoryEntry.Name),
                                                      _sName = directoryEntry.Name
                                                  };
                        if (directoryEntry.Properties["AppRoot"][0].ToString().ToUpper() ==
                            String.Concat("/LM/W3SVC/", iServerID, "/ROOT") ||
                            directoryEntry.Properties["AppRoot"][0].ToString().ToUpper() ==
                            String.Concat(new object[] {"/LM/W3SVC/", iServerID, "/ROOT", sParentPath.ToUpper()}))
                        {
                            iISWebDirectory._isApplication = false;
                        }
                        else
                        {
                            iISWebDirectory._isApplication = true;
                        }
                        iISWebDirectory._iWebServerID = iServerID;
                        AddWebDirectories(new DirectoryEntry(String.Concat(o.Path, "/", directoryEntry.Name)),
                                          iISWebDirectory._sPath, iISWebDirectory.NestedWebDirectories, iServerID,
                                          String.Concat(sPhisicalPath, "\\", directoryEntry.Name));
                        wdcParent.Add(iISWebDirectory);
                    }
                }
            }
            finally
            {
                var iDisposable = iEnumerator as IDisposable;
                if (iDisposable != null)
                {
                    iDisposable.Dispose();
                }
            }
        }

        public IISFTPServerCollection GetFTPServers()
        {
            var iISFTPServerCollection = new IISFTPServerCollection();
            IEnumerator iEnumerator = new DirectoryEntry("IIS://localhost/MSFTPSVC").Children.GetEnumerator();
            try
            {
                while (iEnumerator.MoveNext())
                {
                    var directoryEntry2 = (DirectoryEntry) iEnumerator.Current;
                    if (directoryEntry2.SchemaClassName == "IIsFtpServer")
                    {
                        var iISFTPServer = new IISFTPServer
                                               {
                                                   iID = Convert.ToInt32(directoryEntry2.Name),
                                                   sServerName =
                                                       (String) directoryEntry2.Properties["ServerComment"].Value,
                                                   bAllowAnonymous =
                                                       (bool) directoryEntry2.Properties["AllowAnonymous"].Value,
                                                   sGreetingMessage =
                                                       (String) directoryEntry2.Properties["GreetingMessage"].Value,
                                                   sExitMessage =
                                                       (String) directoryEntry2.Properties["ExitMessage"].Value,
                                                   sMaxClientsMessage =
                                                       (String) directoryEntry2.Properties["MaxClientsMessage"].Value,
                                                   iMaxConnections =
                                                       (int) directoryEntry2.Properties["MaxConnections"].Value
                                               };
                        var directoryEntry3 =
                            new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iISFTPServer.ID, "/Root"));
                        iISFTPServer.sRootPath = directoryEntry3.Properties["Path"][0] as String;
                        iISFTPServerCollection.Add(iISFTPServer);
                    }
                }
            }
            finally
            {
                var iDisposable = iEnumerator as IDisposable;
                if (iDisposable != null)
                {
                    iDisposable.Dispose();
                }
            }
            return iISFTPServerCollection;
        }

        public void CreateFtpServer(string sServerName, string sRootPath, bool bAllowAnonymous, string sIP,
                                    string sHostName, int iPort, bool bCanRead, bool bCanWrite)
        {
            int i = 1;
            if (iPort < 1 || iPort > 65535)
            {
                iPort = 21;
            }
            var directoryEntry1 = new DirectoryEntry("IIS://localhost/MSFTPSVC");
            IEnumerator iEnumerator = directoryEntry1.Children.GetEnumerator();
            try
            {
                while (iEnumerator.MoveNext())
                {
                    var directoryEntry2 = (DirectoryEntry) iEnumerator.Current;
                    if (directoryEntry2.SchemaClassName == "IIsFtpServer")
                    {
                        int j = Convert.ToInt32(directoryEntry2.Name);
                        if (j >= i)
                        {
                            i = j + 1;
                        }
                    }
                }
            }
            finally
            {
                var iDisposable = iEnumerator as IDisposable;
                if (iDisposable != null)
                {
                    iDisposable.Dispose();
                }
            }
            var locals = new object[] {"IIsFtpServer", i};
            var directoryEntry3 = (DirectoryEntry) directoryEntry1.Invoke("Create", locals);
            locals = new object[] {"ServerComment", sServerName};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"AllowAnonymous", bAllowAnonymous};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"KeyType", "IIsFtpServer"};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerBindings", String.Concat(new[] {sIP, ":", iPort.ToString(), ":", sHostName})};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerState", 2};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerAutoStart", 1};
            directoryEntry3.Invoke("Put", locals);
            locals = new object[] {"ServerSize", 1};
            directoryEntry3.Invoke("Put", locals);
            directoryEntry3.Invoke("SetInfo", new object[0]);
            CreateFtpServerVirtualDirectory(i, sServerName, sRootPath, bCanRead, bCanWrite, true);
            directoryEntry3.Dispose();
        }

        public void CreateFtpServerVirtualDirectory(int iFtpSiteID, string sVirtualDirectoryName, string sPath,
                                                    bool bCanRead, bool bCanWrite, bool isRoot)
        {
            DirectoryEntry directoryEntry1;

            DirectoryEntry directoryEntry2;

            if (!isRoot)
            {
                directoryEntry1 = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iFtpSiteID, "/ROOT"));
                var locals = new object[] {"IISFtpVirtualDir", sVirtualDirectoryName};
                directoryEntry2 = (DirectoryEntry) directoryEntry1.Invoke("Create", locals);
            }
            else
            {
                directoryEntry1 = new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iFtpSiteID));
                var locals = new object[] {"IISFtpVirtualDir", "ROOT"};
                directoryEntry2 = (DirectoryEntry) directoryEntry1.Invoke("Create", locals);
            }
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

        public IISFTPServer GetFTPServer(string sWerServerName)
        {
            DirectoryEntry directoryEntry2;

            DirectoryEntry directoryEntry3;

            IISFTPServer iISFTPServer = null;
            var iEnumerator = new DirectoryEntry("IIS://localhost/MSFTPSVC").Children.GetEnumerator();
            try
            {
                while (iEnumerator.MoveNext())
                {
                    directoryEntry2 = (DirectoryEntry) iEnumerator.Current;
                    if (directoryEntry2.SchemaClassName == "IIsFtpServer" &&
                        ((String) directoryEntry2.Properties["ServerComment"].Value).Equals(sWerServerName))
                    {
                        iISFTPServer = new IISFTPServer
                                           {
                                               iID = Convert.ToInt32(directoryEntry2.Name),
                                               sServerName = (String) directoryEntry2.Properties["ServerComment"].Value,
                                               bAllowAnonymous =
                                                   (bool) directoryEntry2.Properties["AllowAnonymous"].Value,
                                               sGreetingMessage =
                                                   (String) directoryEntry2.Properties["GreetingMessage"].Value,
                                               sExitMessage = (String) directoryEntry2.Properties["ExitMessage"].Value,
                                               sMaxClientsMessage =
                                                   (String) directoryEntry2.Properties["MaxClientsMessage"].Value,
                                               iMaxConnections =
                                                   (int) directoryEntry2.Properties["MaxConnections"].Value
                                           };
                        directoryEntry3 =
                            new DirectoryEntry(String.Concat("IIS://localhost/MSFTPSVC/", iISFTPServer.ID, "/Root"));
                        iISFTPServer.sRootPath = directoryEntry3.Properties["Path"][0] as String;
                        break;
                    }
                }
            }
            finally
            {
                var iDisposable = iEnumerator as IDisposable;
                if (iDisposable != null)
                {
                    iDisposable.Dispose();
                }
            }
            return iISFTPServer;
        }
    }
}