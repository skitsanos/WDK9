Imports System.Xml.Serialization

Public Class File
    <XmlAttribute("filename")> Public Filename As String
    <XmlAttribute("description")> Public Description As String
    <XmlAttribute("contentType")> Public ContentType As String
    <XmlAttribute("content")> Public Content As Byte()
    <XmlAttribute("owner")> Public Owner As String
    <XmlAttribute("createdOn")> Public CreatedOn As String
    <XmlAttribute("updatesOn")> Public UpdatedOn As String
    <XmlAttribute("hits")> Public Hits As Integer = 0
    Public Security As New [Security]
End Class
