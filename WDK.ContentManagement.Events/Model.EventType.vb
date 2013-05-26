Imports System.Xml.Serialization

<XmlRoot("Event")> _
Public Class EventType
    <XmlAttribute("uid")> Public uid As String = Guid.NewGuid.ToString
    <XmlAttribute("title")> Public title As String
    <XmlAttribute("description")> Public description As String
    <XmlAttribute("content")> Public content As String
    <XmlAttribute("allowComments")> Public allowComments As Boolean = True
    <XmlAttribute("hits")> Public hits As Integer = 0
    <XmlAttribute("createdOn")> Public createdOn As DateTime = Now
    <XmlAttribute("updatedOn")> Public updatedOn As DateTime = Now
    <XmlArray(), XmlArrayItem("Comment")> Public Comments As List(Of CommentType)
End Class