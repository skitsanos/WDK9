Imports System.Web.Configuration
Imports System.Configuration

Public Class Engine

#Region " Properties "
    Public ReadOnly Property Version() As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
        End Get
    End Property
#End Region

#Region " GetApplicationName "
    Public Function GetApplicationName() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Dim mSection As New MembershipSection
        mSection = conf.GetSection("system.web/membership")

        Dim appName As String = mSection.Providers(mSection.DefaultProvider).Parameters("applicationName")
        If appName = "" Then _
            appName = System.Web.HttpContext.Current.Request.Url.Host

        If System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath <> "/" Then _
        appName += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath

        Return appName
    End Function
#End Region

End Class
