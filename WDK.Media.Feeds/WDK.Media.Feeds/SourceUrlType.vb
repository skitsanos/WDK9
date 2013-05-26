Imports System.Xml.Serialization

<XmlRoot("source")> _
Public Class SourceUrlType
    <XmlAttribute("url")> Public Url As String
    <XmlText()> Public Title As String

    Public Function DocumentElement() As Xml.XmlElement
        Return GetXml(Me).DocumentElement
    End Function
End Class
