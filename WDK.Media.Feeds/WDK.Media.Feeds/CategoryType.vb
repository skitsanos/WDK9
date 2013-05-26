Imports System.Xml.Serialization

<XmlRoot("category")> _
Public Class CategoryType
    <XmlAttribute("domain")> Public Domain As String
    <XmlText()> Public Name As String
    <XmlElement("category")> Public Categories As New CategoryCollectionType

    Public Function DocumentElement() As Xml.XmlElement
        Return GetXml(Me).DocumentElement
    End Function
End Class
