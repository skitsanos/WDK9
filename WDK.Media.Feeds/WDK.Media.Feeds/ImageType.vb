Imports System.Xml.Serialization

<XmlRoot("image")> _
Public Class ImageType
    <XmlElement("url")> Public Url As String
    <XmlElement("title")> Public Title As String
    <XmlElement("link")> Public Link As String

    Public Function DocumentElement() As Xml.XmlElement
        Return GetXml(Me).DocumentElement
    End Function
End Class
