Imports System.Data
Imports System.Data.SqlClient

Imports System.Security.Permissions

Imports System.Web
Imports System.Web.Caching
Imports System.Web.Hosting

<AspNetHostingPermission(SecurityAction.Demand, Level:=AspNetHostingPermissionLevel.Minimal), _
   AspNetHostingPermission(SecurityAction.InheritanceDemand, level:=AspNetHostingPermissionLevel.Minimal)> _
  Public Class WdkVirtualDirectory
    Inherits VirtualDirectory

#Region " Properties"
    Private spp As WdkFSProvider

    ' Declare the variable the property uses.
    Private existsValue As Boolean = False

    Public ReadOnly Property Exists() As Boolean
        Get
            Return existsValue
        End Get
    End Property

    Public Sub New(ByVal virtualDir As String, ByVal provider As WdkFSProvider)
        MyBase.New(virtualDir)
        spp = provider
    End Sub

    Private childrenValue As ArrayList
    Public Overrides ReadOnly Property Children() As System.Collections.IEnumerable
        Get
            Return childrenValue
        End Get
    End Property

    Private directoriesValue As ArrayList
    Public Overrides ReadOnly Property Directories() As System.Collections.IEnumerable
        Get
            Return directoriesValue
        End Get
    End Property

    Private filesValue As ArrayList
    Public Overrides ReadOnly Property Files() As System.Collections.IEnumerable
        Get
            Return filesValue
        End Get
    End Property
#End Region

End Class

