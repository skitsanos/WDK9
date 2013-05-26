Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Web.Configuration

Public Module SkitsanosCommon

#Region " appHome "
    Public Function appHome() As String
        Return AppDomain.CurrentDomain.BaseDirectory
    End Function

#Region " GetApplicationName "
    Public Function GetApplicationName() As String
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

#End Region

#Region " urlHome "
    Public Function urlHome() As String
        Dim Request As Web.HttpRequest = Web.HttpContext.Current.Request

        Dim url As String = "http://" & Request.ServerVariables("HTTP_HOST") & _
                                     IO.Path.GetDirectoryName(Request.ServerVariables("URL")).Replace("\", "/")
        If url.EndsWith("/") = False Then url += "/"
        Return url
    End Function
#End Region

#Region " Log "
    Public Sub Log(ByVal Data As String, Optional ByVal IsError As Boolean = False)
        Dim logs As New Providers.Logs.ApplicationLog
        logs.write(Data, IsError)
    End Sub
#End Region

#Region " diskLog "
    Public Sub diskLog(ByVal data As String)
        If Not IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "App_Data") Then IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "UserFiles")
        If Not IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "App_Data\Logs") Then IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "UserFiles\Logs")

        Dim sw As New IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "App_Data\Logs\" + Now.Month.ToString + Now.Day.ToString + Now.Year.ToString + ".log")
        sw.WriteLine(Now + vbTab + data)
        sw.Close()
    End Sub
#End Region

#Region " getAppSettings "
    Public Function getAppSettings(ByVal key As String) As String
        Return WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath).AppSettings.Settings(key).Value
    End Function
#End Region


End Module