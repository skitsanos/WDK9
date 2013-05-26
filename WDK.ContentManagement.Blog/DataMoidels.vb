Imports System.Xml.Serialization

<XmlRoot("Article")> _
Public Class ArticleType
    <XmlAttribute("file")> Public Filename As String = "untitled.aspx"
    <XmlAttribute("title")> Public Title As String = "[Untitled]"
    <XmlAttribute("keywords")> Public Keywords As String
    <XmlAttribute("content")> Public Content As String
    <XmlAttribute("allowComments")> Public AllowComments As Boolean = True
    <XmlAttribute("hits")> Public Hits As Integer = 0
    <XmlAttribute("createdOn")> Public CreatedOn As DateTime = Now
    <XmlAttribute("updatedOn")> Public UpdatedOn As DateTime = Now
    <XmlArray("Comments"), XmlArrayItem("Comment")> Public Comments As New CommentsCollectionType
End Class

<XmlRoot("Comment")> _
Public Structure CommentType
    <XmlAttribute("createdOn")> Public CreatedOn As DateTime
    <XmlAttribute("username")> Public Username As String
    <XmlAttribute("name")> Public Fullname As String
    <XmlAttribute("email")> Public Email As String
    <XmlAttribute("url")> Public Website As String
    <XmlAttribute("content")> Public Content As String
End Structure

Public Class CommentsCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As CommentType
        Get
            Return CType(MyBase.List.Item(Index), CommentType)
        End Get
    End Property
#End Region

#Region " Add (object) "
    Public Sub Add(ByVal s As CommentType)
        List.Add(s)
    End Sub
#End Region

End Class