Imports System.Web.Configuration
Imports System.Configuration
Imports System.Xml.Serialization

Module Common

#Region " ConnectionString "
    Friend Function ConnectionString() As String
        Return WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath).ConnectionStrings.ConnectionStrings("SiteAdmin").ConnectionString
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

#Region " GetEmbeddedContent "
    Friend Function GetEmbeddedContent(ByVal Resource As String) As Byte()
        Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim ns As String = "WDK.ContentManagement.Pages"

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

#Region " newElement "
    Public Function newElement(ByVal name As String, ByVal value As String, ByVal doc As Xml.XmlDocument) As Xml.XmlElement
        Dim el As Xml.XmlElement = doc.CreateElement(name)
        el.InnerText = value
        Return el
    End Function
#End Region

#Region " CamelCase "
    Public Function CamelCase(ByVal source As String) As String
        If source.Split(" ").Length < 1 Then
            Return source.Replace(source.Substring(1), source.Substring(1).ToLower)
        Else
            Return Replace(Left(source, 1).ToLower & Mid(source, 2), " ", "")
        End If
    End Function
#End Region


End Module
