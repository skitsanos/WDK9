Imports System.Xml.Serialization

<XmlRoot("Page")> _
Public Class PageType
    <XmlAttribute("uid")> Public uid As String = Guid.NewGuid.ToString
    <XmlAttribute("file")> Public Filename As String = "untitled.aspx"
    <XmlAttribute("type")> Public Type As PageContentType = PageContentType.HTML
    <XmlAttribute("masterPage")> Public MasterPage As String = "~/default.master"
    <XmlAttribute("title")> Public Title As String = "untitled page"
    <XmlAttribute("createdOn")> Public CreatedOn As String = Now
    <XmlAttribute("updatedOn")> Public UpdatedOn As String = Now
    <XmlAttribute("hits")> Public Hits As Integer = 0
    <XmlAttribute("allowComments")> Public AllowComments As Boolean = False
    <XmlAttribute("content")> Public Content As String = ""
    <XmlElement("Meta")> Public Metas As New MetaDataType
End Class

<XmlRoot("Meta")> _
Public Structure MetaDataType
    <XmlAttribute("keywords")> Public Keywords As String
    <XmlAttribute("description")> Public Description As String
    <XmlAttribute("author")> Public Author As String
End Structure