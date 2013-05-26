// Decompiled by Salamander version 1.0.6
// Copyright 2002 Remotesoft Inc. All rights reserved.
// http://www.remotesoft.com/salamander

using System;
using System.DirectoryServices;

namespace WDK.Network.IIS
{
    public class IISWebDirectory
    {
        private readonly IISWebDirectoryCollection wdc;
        internal bool _isApplication;

        internal int _iWebServerID;
        internal string _sName;

        internal string _sPath;

        internal IISWebDirectory()
        {
            _sName = "";
            _sPath = "";
            _isApplication = false;
            _iWebServerID = -1;
            wdc = new IISWebDirectoryCollection();
        }


        public IISWebDirectoryCollection NestedWebDirectories
        {
            get { return wdc; }
        }

        public string Name
        {
            get { return _sName; }
        }

        public string Path
        {
            get { return _sPath; }
        }

        public bool IsApplication
        {
            get { return _isApplication; }
        }

        public void CreateApplication()
        {
            var locals = new object[] {"IIS://localhost/W3SVC/", _iWebServerID, "/ROOT", _sPath, "/", _sName};
            var directoryEntry = new DirectoryEntry(String.Concat(locals));
            directoryEntry.Properties["AppIsolated"][0] = 2;
            locals = new object[] {"/LM/W3SVC/", _iWebServerID, "/ROOT", _sPath, "/", _sName};
            directoryEntry.Properties["AppRoot"][0] = String.Concat(locals);
            locals = new object[] {2};
            directoryEntry.Invoke("AppCreate", locals);
            directoryEntry.Properties["AppFriendlyName"][0] = _sName;
            directoryEntry.CommitChanges();
        }

        public void DeleteApplication()
        {
            var directoryEntry =
                new DirectoryEntry(
                    String.Concat(new object[] {"IIS://localhost/W3SVC/", _iWebServerID, "/ROOT", _sPath, "/", _sName}));
            directoryEntry.Invoke("AppDelete", new object[0]);
            directoryEntry.CommitChanges();
        }

        public void RestartApplication()
        {
            var directoryEntry =
                new DirectoryEntry(
                    String.Concat(new object[] {"IIS://localhost/W3SVC/", _iWebServerID, "/ROOT", _sPath, "/", _sName}));
            directoryEntry.Invoke("AspAppRestart", new object[0]);
            directoryEntry.CommitChanges();
        }
    }
}