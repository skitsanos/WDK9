Imports System.Configuration
Imports System.Web.Configuration


Friend Module Common

#Region " ConnectionString "
    Friend Function ConnectionString() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)

        Dim mSection As New MembershipSection
        mSection = conf.GetSection("system.web/membership")

        Dim csName As String = mSection.Providers(mSection.DefaultProvider).Parameters("connectionStringName")
        Return conf.ConnectionStrings.ConnectionStrings(csName).ConnectionString
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

#Region " Log "
    Friend Sub Log(ByVal Data As String, ByVal IsError As Boolean)
		Dim logs As New WDK.Providers.Logs.ApplicationLog
		logs.write(Data, IsError)
    End Sub
#End Region

End Module
