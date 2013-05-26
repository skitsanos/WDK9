Imports System.Web.Configuration
Imports System.Configuration
Imports System.Xml.Serialization

Module Common

#Region " ConnectionString "
    Friend Function ConnectionString() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.ConnectionStrings.ConnectionStrings("SiteAdmin").ConnectionString
    End Function
#End Region

#Region " GetApplicationName "
    Friend Function GetApplicationName() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Dim mSection As New MembershipSection
        mSection = conf.GetSection("system.web/membership")

        Dim appName As String = mSection.Providers(mSection.DefaultProvider).Parameters("applicationName")
        If appName = "" Then
            appName = System.Web.HttpContext.Current.Request.Url.Host

            If System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath <> "/" Then _
            appName += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
        End If

        Return appName
    End Function
#End Region

#Region " .Log() "
    Friend Sub Log(ByVal Data As String, Optional ByVal IsError As Boolean = False)
		Dim logs As Object = Activator.CreateInstance(Type.GetType("WDK.Providers.Logs.ApplicationLog,WDK.Providers.Logs"))
        If logs IsNot Nothing Then
            logs.Write(Data, IsError)
        End If
    End Sub
#End Region

End Module
