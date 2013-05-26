Imports System.Xml.Serialization

Public Class ForumMessageType
    <XmlAttribute("postedOn")> Public PostedOn As DateTime = Now
    <XmlAttribute("author")> Public Author As String
    <XmlAttribute("subject")> Public Subject As String
    <XmlAttribute("message")> Public Message As String
    <XmlArray("Replies"), XmlArrayItem("Message")> Public Messages As New ForumMessageCollectionType
End Class