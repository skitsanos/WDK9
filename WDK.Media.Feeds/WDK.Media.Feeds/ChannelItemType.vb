Imports System.Xml.Serialization

<XmlRoot("item")> _
Public Class ChannelItemType
    'Dim dt As New W3CDateTime(Now)

    <XmlElement("title")> Public Title As String
    <XmlElement("link")> Public Link As String
    <XmlElement("description")> Public Description As String
    <XmlElement("keywords")> Public Keywords As String
    <XmlElement("author")> Public Author As String

    <XmlElement("comments")> Public CommentsUrl As String
    <XmlElement("enclosure")> Public Enclosure As New EnclosureType
    <XmlElement("quid")> Public UniqueId As String = Guid.NewGuid.ToString
    <XmlElement("pubDate")> Public pubDate As String = (New W3CDateTime(Now)).ToString("R") 'dt.ToString("R")
    <XmlElement("source")> Public SourceUrl As New SourceUrlType

    <XmlElement("category")> Public Categories As New CategoryCollectionType

    Public Function DocumentElement() As Xml.XmlElement
        Return GetXml(Me).DocumentElement
    End Function
End Class
