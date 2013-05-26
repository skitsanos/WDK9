Imports System.Xml.Serialization

Public Class CommentType
    <XmlAttribute("createdOn")> Public CreatedOn As DateTime = Now
    <XmlAttribute("username")> Public Username As String
    <XmlAttribute("author")> Public Author As String
    <XmlAttribute("email")> Public Email As String
    <XmlAttribute("website")> Public Website As String
    <XmlAttribute("content")> Public Content As String
End Class
