'- arachnoWare.Storages.WebxmlDb
'
'  Web based XML DB Storage, Lite Edition (Former XMLDBelt)
'  Created: April 14, 2004
'  Updated: November 15, 2004

Imports System.Web
Imports System.Text.RegularExpressions

Namespace Data
    Public Class XmlDbHttp
        Implements IHttpHandler

#Region " Properties "
        Private xmlResponse As Xml.XmlDocument

        Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
            Get
                Return False
            End Get
        End Property

        Public Function Version() As String
            Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
        End Function
#End Region

#Region " New() "
        Public Sub New()
            xmlResponse = New Xml.XmlDocument
        End Sub
#End Region

        Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
            Dim Query As String = ""

            Dim URL As String = context.Request.RawUrl
            If InStrRev(context.Request.RawUrl, "?") <> 0 Then
                Query = Mid(URL, InStrRev(URL, "?"))
            End If

            '- <?xml version="1.0" ?>
            Dim xmlPI As Xml.XmlProcessingInstruction = xmlResponse.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""UTF-8""")
            xmlResponse.AppendChild(xmlPI)

            Dim xmlRoot As Xml.XmlElement = xmlResponse.CreateElement("response")
            xmlResponse.AppendChild(xmlRoot)

            Try
                If context.Request.RequestType = "GET" Then
                    '- ('storage=xmlcs:doc.xml;username=admin;password=nothing;')//node[@id='222']
                    Dim regex As Regex
                    Dim rxQuery As String = "'(?<document>.*)'\)(?<path>.*)"
                    regex = New Regex(rxQuery, RegexOptions.IgnoreCase Or RegexOptions.Compiled)

                    Dim matchCollection As MatchCollection = regex.Matches(Query)

                    If matchCollection.Count > 0 Then
                        Dim xmlRequest As Xml.XmlElement = xmlResponse.CreateElement("request")
                        xmlRoot.AppendChild(xmlRequest)

                        Dim txtDocument As String = matchCollection(0).Result("${document}")
                        Dim txtXPath As String = matchCollection(0).Result("${path}")

                        xmlRequest.SetAttribute("connection", txtDocument)
                        xmlRequest.SetAttribute("xpath", txtXPath)

                        '- Get username
                        Dim txtUsername As String = ExtractValue("username", txtDocument, ";")
                        If txtUsername = "" Then
                            xmlRequest.SetAttribute("username", "{NONE}")
                        Else
                            xmlRequest.SetAttribute("username", txtUsername)
                        End If

                        '- Get storage path
                        Dim txtStorage As String = ExtractValue("storage", txtDocument, ";")
                        If txtStorage = "" Then
                            xmlRequest.SetAttribute("storage", "{NONE}")
                        Else
                            xmlRequest.SetAttribute("storage", txtStorage)
                        End If

                        'PrepareDb("db")
                        If txtStorage.StartsWith("xmlcs:") Then
                        Else
                            Execute(xmlRoot, txtStorage, txtXPath)
                        End If

                    Else
                        Response(xmlRoot, , "Web based XML DB Storage v." & Version())
                    End If

                    matchCollection = Nothing
                    regex = Nothing

                Else

                End If

            Catch e As Exception
                Response(xmlRoot, 500, e.Message)
            End Try

            context.Response.StatusCode = 200
            context.Response.ContentType = "text/xml"
            context.Response.Clear()
            context.Response.Write(xmlResponse.OuterXml)
            context.Response.End()

            xmlResponse = Nothing
        End Sub

#Region " Report "
        Sub Response(ByVal xmlElement As Xml.XmlElement, Optional ByVal Status As Integer = 200, Optional ByVal StatusText As String = "")
            xmlElement.SetAttribute("status", Status)

            If StatusText <> "" Then
                Dim xmlCDataSection As Xml.XmlCDataSection = xmlResponse.CreateCDataSection(StatusText)
                xmlElement.AppendChild(xmlCDataSection)
            End If
        End Sub
#End Region

#Region " Value Parser "
        Public Function ExtractValue(ByVal VariableName As String, ByVal QueryString As String, Optional ByVal Splitter As String = "&") As String
            If QueryString = "" Then Return ""

            Dim myVar As String() = Split(QueryString, Splitter)
            Dim maxVar As Integer = UBound(myVar)

            Dim x As Integer = 0
            Dim extVar As String = ""

            For x = 0 To maxVar
                extVar = Mid(myVar(x), 1, InStr(1, myVar(x), "=") - 1)
                If VariableName = extVar Then
                    Return Mid(myVar(x), InStr(1, myVar(x), "=") + 1)
                End If
            Next
        End Function
#End Region

#Region " Execute Query "
        Public Sub Execute(ByVal xmlContext As Xml.XmlElement, ByVal Document As String, ByVal Query As String)
            Document = Strings.Trim(Document)
            If LCase(Document) = "version" Then
                Response(xmlContext, 200, "Web based XML DB Storage v." & Version())

            ElseIf Not IO.File.Exists(Document) Then
                xmlContext.SetAttribute("status", 404)
                Response(xmlContext, 404, "Document (" & Document & ") not found.")

            Else
                Dim xmlDocument As New Xml.XmlDocument
                xmlDocument.Load(Document)

                If xmlDocument.OuterXml <> "" Then

                    If Query = "" Then
                        Query = "//*"
                    End If

                    Dim xmlNodeList As Xml.XmlNodeList = xmlDocument.SelectNodes(Query)
                    Dim xmlFoundNode As Xml.XmlElement

                    Try
                        For Each xmlFoundNode In xmlNodeList
                            Dim xmlNode As Xml.XmlNode = xmlResponse.ImportNode(xmlFoundNode, True)
                            xmlContext.AppendChild(xmlNode)
                        Next

                        Response(xmlContext, 200)

                    Catch e As Exception
                        Response(xmlContext, 500, e.Message)
                    End Try
                Else
                    Response(xmlContext, 404, "Not found")
                End If
            End If
        End Sub
#End Region


    End Class
End Namespace


