Imports System.Xml.Serialization

' http://cyber.law.harvard.edu/rss/rss.html

<XmlRoot("channel")> _
Public Class ChannelType
    <XmlElement("title")> Public Title As String
    <XmlElement("link")> Public Link As String = "http://localhost/"
    <XmlElement("description")> Public Description As String
    <XmlElement("keywords")> Public Keywords As String

    <XmlElement("language")> Public Language As String = "en-us"
    <XmlElement("copyright")> Public Copyright As String
    <XmlElement("managingEditor")> Public ManagingEditor As String
    <XmlElement("webMaster")> Public WebMaster As String
    <XmlElement("pubDate")> Public PubDate As String = (New W3CDateTime(Now)).ToString("R")
    <XmlElement("lastBuildDate")> Public LastBuildDate As String = (New W3CDateTime(Now)).ToString("R")
    <XmlElement("generator")> Public Generator As String = "Skitsanos WDK8 Media.Feeds"
    <XmlElement("image")> Public Image As New ImageType
    <XmlElement("ttl")> Public ttl As String

    <XmlElement("item")> Public Items As New ChannelItemCollectionType
    <XmlElement("category")> Public Categories As New CategoryCollectionType

    Public Function DocumentElement() As Xml.XmlElement
        Return GetXml(Me).DocumentElement
    End Function
End Class
