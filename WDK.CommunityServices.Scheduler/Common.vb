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
        Dim logs As Object = Activator.CreateInstance(Type.GetType("WDK.Providers.Logs.AppLog,WDK.Providers.Logs"))
        If logs IsNot Nothing Then
            logs.Write(Data, IsError)
        End If
    End Sub
#End Region

#Region " Xml Serialization "
    Public Function GetXml(ByVal obj As Object) As Xml.XmlDocument
        Dim xmldoc As Xml.XmlDocument = Nothing
        Try
            Dim sb As New Text.StringBuilder
            Dim tw As IO.TextWriter = New IO.StringWriter(sb)

            Dim ser As New XmlSerializer(obj.GetType)
            ser.Serialize(tw, obj)

            xmldoc = New Xml.XmlDocument
            xmldoc.LoadXml(sb.ToString)

        Catch ex As Exception
            Log("{Xml Serializer Error within Pages Manager} " & ex.ToString, True)
        End Try

        Return xmldoc
    End Function

    Public Function SetXml(ByVal Type As System.Type, ByVal XmlData As String) As Object
        Dim obj As Object = Nothing

        Try
            Dim ser As New XmlSerializer(Type)
            Dim tr As IO.TextReader = New IO.StringReader(XmlData)
            obj = ser.Deserialize(tr)

        Catch ex As Exception
            Log("{Xml Deserializer Error within Pages Manager} " & ex.ToString)
        End Try

        Return obj
    End Function
#End Region

End Module
