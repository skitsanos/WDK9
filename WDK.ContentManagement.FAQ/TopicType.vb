Imports System.Xml.Serialization

<XmlRoot("Topic")> _
Public Class TopicType
    <XmlAttribute("question")> Public Question As String
    <XmlAttribute("answer")> Public Answer As String
End Class
