Imports System.Xml.Serialization

<XmlRoot("LogEntry")> _
Public Class LogEntry
    <XmlAttribute("uid")> Public uid As Integer
    <XmlAttribute("createdOn")> Public createdOn As DateTime
    <XmlAttribute("content")> Public content As String
    <XmlAttribute("isError")> Public isError As Boolean
End Class