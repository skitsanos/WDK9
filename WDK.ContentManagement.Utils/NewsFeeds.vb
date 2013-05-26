Imports System.Xml
Imports System.IO

Public Class NewsFeeds
#Region " Properties "
    Private _Template As String = ""
    Public Property Template() As String
        Get
            Return _Template
        End Get
        Set(ByVal Value As String)
            _Template = Value
        End Set
    End Property

#End Region

#Region " GetFeed "
    Public Function GetFeed(ByVal URL As String) As String
        If URL = "" Then
            Return "[NewsFeeds].GetFeed method failed. URL is empty."
        End If

        Try
            Dim HTTP As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.Http,wdk.utilities"))
            Dim xmlDocument As New Xml.XmlDocument

            Dim xmlContent As String = HTTP.FetchURL(URL)
            If xmlContent <> "" Then
                xmlDocument.LoadXml(xmlContent)

                If Template = "" Then
                    Return "{.GetFeed} Error Can't find RSS Schema for displaying news."
                Else
                    Return TransformXML(xmlDocument, Template)
                End If
            Else
                Return "There is no XML data was received from server"
            End If

        Catch e As Exception
            Return "{Error}: " & e.ToString & " {URL=" & URL & "}"
        End Try
    End Function
#End Region

#Region " TransformXml "
    Private Function TransformXML(ByVal Source As Xml.XmlDocument, ByVal Template As String) As String
        Try
            Dim sw As New IO.StringWriter

            Dim XSLT As New Xml.Xsl.XslCompiledTransform
            XSLT.Load(Template)

            Dim tr As New StringReader(Source.OuterXml)
            Dim xr As New XmlTextReader(tr)

            XSLT.Transform(xr, Nothing, sw)

            Return sw.ToString

        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function
#End Region

End Class


