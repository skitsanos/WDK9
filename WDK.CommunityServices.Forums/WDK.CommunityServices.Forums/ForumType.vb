Imports System.Xml.Serialization

Public Class ForumType
    <XmlAttribute("title")> Public Title As String
    <XmlArray("Rules"), XmlArrayItem("Rule")> Public Rules As New ForumAccessRolesCollectionType
    <XmlArray("Messages"), XmlArrayItem("Message")> Public Messages As New ForumMessageCollectionType
End Class