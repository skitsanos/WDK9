Imports System.Xml.Serialization

<XmlRoot("ForumGroup")> _
Public Class ForumGroupType
    <XmlAttribute("title")> Public Title As String
    ''<XmlArray("Forums"), XmlArrayItem("Forum")> Public Forums As New ForumCollectionType
End Class
