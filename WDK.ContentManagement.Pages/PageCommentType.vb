Imports System.Xml.Serialization

#Region " SQL Create "
'CREATE TABLE [dbo].[Pages](
'	[id] [int] IDENTITY(1,1) NOT NULL,
'	[ApplicationName] [varchar](255) NOT NULL,
'	[PageId] [int] NOT NULL,
'	[XmlData] [xml] NOT NULL,
' CONSTRAINT [PK_Pages] PRIMARY KEY CLUSTERED 
'(
'	[id] ASC
')WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
') ON [PRIMARY]
#End Region

<XmlRoot("Comment")> _
Public Class PageCommentType
    <XmlAttribute("createdOn")> Public CreatedOn As DateTime = Now
    <XmlAttribute("author")> Public Author As String
    <XmlAttribute("url")> Public Url As String
    <XmlAttribute("message")> Public Message As String
End Class
