Imports System.Xml.Serialization

Public Structure EducationType
    <XmlAttribute("institution")> Public Institution As String
    <XmlAttribute("field")> Public Field As String
    <XmlAttribute("degree")> Public Degree As String
    <XmlAttribute("enroledOn")> Public EnrolledOn As String
    <XmlAttribute("completedOn")> Public CompletedOn As String
    <XmlAttribute("comments")> Public Comments As String

    <XmlElement("Contact")> Public Location As ContactDetailsType
End Structure