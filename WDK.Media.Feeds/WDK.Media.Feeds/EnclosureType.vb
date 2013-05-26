Imports System.Xml.Serialization

<XmlRoot("enclosure")> _
Public Class EnclosureType
    <XmlAttribute("url")> Public Url As String
    <XmlAttribute("length")> Public Length As String
    <XmlAttribute("type")> Public MimeType As String

    Public Function DocumentElement() As Xml.XmlElement
        Return GetXml(Me).DocumentElement
    End Function
End Class