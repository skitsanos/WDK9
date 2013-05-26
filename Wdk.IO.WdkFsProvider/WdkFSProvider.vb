Imports System.Data

Imports System.Security.Permissions

Imports System.Web
Imports System.Web.Hosting


<AspNetHostingPermission(SecurityAction.Demand, Level:=AspNetHostingPermissionLevel.Medium), _
   AspNetHostingPermission(SecurityAction.InheritanceDemand, level:=AspNetHostingPermissionLevel.High)> _
Public Class WdkFSProvider
    Inherits VirtualPathProvider

#Region " New "
    Public Sub New()
        'MyBase.New()

    End Sub
#End Region

#Region " Initialize "
    'Protected Overrides Sub Initialize()

    'End Sub
#End Region

#Region " IsPathVirtual "
    Public Function IsPathVirtual(ByVal virtualPath As String) As Boolean
        Dim cmdArr As String() = VirtualPathUtility.ToAppRelative(virtualPath).Split("/")
        If cmdArr.Length > 1 Then
            Dim cmd As String = cmdArr(1)

            'If Not System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("/" + cmd.ToLower)) AndAlso cmd.ToLower <> "default.aspx" Then
            '    Return True
            'Else
            '    Return False
            'End If

            Select Case cmd.ToLower
                Case "system", "content", "events", "faq", "controls", "applications", "files", "enews", "xmldrive", "profiles"
                    Return True
                Case Else
                    Return False
            End Select
        End If
    End Function
#End Region

#Region " FileExists "
    Public Overrides Function FileExists(ByVal virtualPath As String) As Boolean
        If (IsPathVirtual(virtualPath)) Then
            'Dim file As WdkVirtualFile
            'file = CType(GetFile(virtualPath), WdkVirtualFile)
            'Return file.Exists
            Return True
        Else
            Return Previous.FileExists(virtualPath)
        End If
    End Function
#End Region

#Region " DirectoryExists "
    Public Overrides Function DirectoryExists(ByVal virtualDir As String) As Boolean
        If (IsPathVirtual(virtualDir)) Then
            'Dim dir As WdkVirtualDirectory = CType(GetDirectory(virtualDir), WdkVirtualDirectory)
            'Return dir.Exists
            Return True
        Else
            'Return Previous.DirectoryExists(virtualDir)
            Return False
        End If

    End Function
#End Region

#Region " GetFile "
    Public Overrides Function GetFile(ByVal virtualPath As String) As VirtualFile
        If IsPathVirtual(virtualPath) Then
            Return New WdkVirtualFile(virtualPath)
        Else
            Return Previous.GetFile(virtualPath)
        End If
    End Function
#End Region

#Region " GetDirectory "
    Public Overrides Function GetDirectory(ByVal virtualDir As String) As VirtualDirectory
        Dim checkPath As String = VirtualPathUtility.ToAppRelative(virtualDir)

        If (IsPathVirtual(virtualDir)) Then
            Return New WdkVirtualDirectory(virtualDir, Me)
        Else
            Return Previous.GetDirectory(virtualDir)
        End If
    End Function
#End Region

#Region " GetCacheDependency "
    Public Overrides Function GetCacheDependency(ByVal virtualPath As String, ByVal virtualPathDependencies As IEnumerable, ByVal utcStart As Date) As Caching.CacheDependency
        'If (IsPathVirtual(virtualPath)) Then
        '    ' we dont cache anythign for this moment for virtual resources
        '    Return Nothing
        'Else
        '    Return Previous.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart)
        'End If
        Return Nothing
    End Function
#End Region

#Region " GetFileHash "
    Public Overrides Function GetFileHash(ByVal virtualPath As String, ByVal virtualPathDependencies As IEnumerable) As String
        Return Nothing
    End Function
#End Region

End Class