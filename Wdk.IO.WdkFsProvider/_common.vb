Imports System.IO

Imports System.Web
Imports System.Web.Configuration

Imports System.Configuration
Imports System.Xml
Imports System.ServiceModel

Module _common

    Friend Settings As New Wdk.Providers.Settings

#Region " ConnectionString "
    Friend Function ConnectionString() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.ConnectionStrings.ConnectionStrings("SiteAdmin").ConnectionString
    End Function
#End Region

#Region " GetWebConfigValue "
    Friend Function GetWebConfigValue(ByVal Key As String)
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        If conf.AppSettings.Settings(Key) Is Nothing Then
            Return Nothing
        Else
            Return conf.AppSettings.Settings(Key).Value
        End If
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

#Region " GetEmbeddedContent "
    Friend Function GetEmbeddedContent(ByVal Resource As String) As Byte()
        Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim ns As String = "Wdk.IO"

        Try
            Dim resStream As System.IO.Stream = assembly.GetManifestResourceStream(ns & "." & Resource)
            If resStream IsNot Nothing Then
                Dim myBuffer(resStream.Length - 1) As Byte
                resStream.Read(myBuffer, 0, resStream.Length)
                Return myBuffer
            Else
                Return BytesOf("<!--- Resource {" & Resource.ToUpper & "} not found under " & ns & " -->")
            End If

        Catch ex As Exception
            Return BytesOf(ex.ToString)
        End Try
    End Function
#End Region

#Region " BytesOf "
    Friend Function BytesOf(ByVal Data As String) As Byte()
        Return Text.Encoding.UTF8.GetBytes(Data)
    End Function
#End Region

#Region " GetODBCDriversList() "
    Friend Function GetODBCDriversList() As String()
        'HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI\ODBC Drivers
        Dim regKey As Microsoft.Win32.RegistryKey = _
        Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE", False).OpenSubKey("ODBC", False).OpenSubKey("ODBCINST.INI", False).OpenSubKey("ODBC Drivers", False)

        Return regKey.GetValueNames
    End Function
#End Region

#Region " FetchURL "
    Friend Function FetchURL(ByVal URL As String) As String
        If URL = "" Then
            Return ""
        Else
            Try
                Dim webClient As Net.WebClient = New Net.WebClient
                webClient.Headers.Add("pragma", "no-cache")
                webClient.Headers.Add("cache-control", "private")
                Dim streamReader As StreamReader = New StreamReader(webClient.OpenRead(URL))
                Dim str As String = streamReader.ReadToEnd()
                streamReader.Close()
                streamReader = Nothing
                webClient.Dispose()
                webClient = Nothing
                Return str
            Catch e As Exception
                Return e.Message
            End Try
        End If
    End Function
#End Region

#Region " UrlHome "
    Friend Function UrlHome() As String
        Dim Request As System.Web.HttpRequest = System.Web.HttpContext.Current.Request

        Dim url As String = "http://" & System.Web.HttpContext.Current.Request.Url.Host 'Request.ServerVariables("SERVER_NAME").ToLower
        If System.Web.HttpContext.Current.Request.Url.Port <> 80 Then url += ":" & System.Web.HttpContext.Current.Request.Url.Port
        url += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
        Return url
    End Function
#End Region

#Region " Log "
    Friend Sub Log(ByVal Data As String, Optional ByVal IsError As Boolean = False)
        If System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "bin\WDK.Providers.Logs.dll") = False Then
            Throw New Exception(Data & " (WDK.Providers.Logs.dll is missing as well, so your errors can't be logged and seen in SiteAdmin CMS Logs)")
        Else
            Dim logs As New Wdk.Providers.Logs.ApplicationLog
            logs.Write(Data, IsError)
        End If
    End Sub
#End Region


#Region " Render WAML "
    Friend Function RenderWaml(ByVal Path As String) As String
        Dim amlRenderTemplate As String = UrlHome() & "waml/waml.xslt"

        Dim res As String = ""

        Try
            Dim sw As New System.IO.StringWriter

			Dim XSLT As New Xsl.XslCompiledTransform
            XSLT.Load(amlRenderTemplate)

			Dim xslProc As New Xsl.XsltArgumentList

			Dim xmlDoc As New XmlDocument

            Dim prv As New WdkFSProvider
            If prv.IsPathVirtual(Path) = False Then
                Path = HttpContext.Current.Request.PhysicalPath
            End If

            xmlDoc.Load(Path)

            Dim tr As New System.IO.StringReader(xmlDoc.OuterXml)
			Dim xr As New XmlTextReader(tr)

            XSLT.Transform(xr, xslProc, sw)
            XSLT = Nothing
            res = sw.ToString

        Catch ex As Exception
            res = ex.ToString & " <p/>{Path: " & Path & "}"
        End Try

        Return res

    End Function
#End Region

#Region " SupportTarget "
    Friend Function SupportTarget() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.AppSettings.Settings("wdk_support_target").Value
    End Function
#End Region

#Region " SupportMethod "
    ''' <summary>
    ''' Support method (email|edx)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SupportMethod() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.AppSettings.Settings("wdk_support_method").Value
    End Function
#End Region

#Region " GoogleAnalyticsCode "
    Friend Function GoogleAnalyticsCode(ByVal analyticsId As String) As String
        Dim uid As String = "WDK" + analyticsId.Replace("-", "")
        Dim js As String = " <script type=""text/javascript"">"
        js += "var gaJsHost" + uid + " = ((""https:"" == document.location.protocol) ? ""https://ssl."" : ""http://www."");"
        js += "document.write(unescape(""%3Cscript src='"" + gaJsHost" + uid + " + ""google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E""));"
        js += "</script>"

        js += "<script type=""text/javascript"">"
        js += "var pageTracker" + uid + " = _gat._getTracker(""" + analyticsId + """);"
        js += "pageTracker" + uid + "._initData();"
        js += "pageTracker" + uid + "._trackPageview();"
        js += "</script>"

        Return js
    End Function
#End Region

    Friend Sub dump(ByVal data As String)
        Dim sw As New System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "log.txt", True)
        sw.WriteLine(data)
        sw.Close()
    End Sub
End Module
