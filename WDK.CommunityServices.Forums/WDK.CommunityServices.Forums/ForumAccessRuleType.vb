Imports System.Xml.Serialization

Public Class ForumAccessRuleType
    <XmlAttribute("allowRead")> Public allowRead As Boolean = True
    <XmlAttribute("allowWrite")> Public allowWrite As Boolean = True
    <XmlAttribute("accountUsername")> Public accountUsername As String
End Class
