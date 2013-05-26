Imports System.Xml.Serialization

<XmlRoot("Testimonial")> _
Public Class TestimonialType
    <XmlAttribute("uid")> Public uid As String = Guid.NewGuid.ToString
    <XmlAttribute("author")> Public author As String
    <XmlAttribute("email")> Public email As String
    <XmlAttribute("content")> Public content As String
    <XmlAttribute("createdOn")> Public createdOn As DateTime = Now
    <XmlAttribute("updatedOn")> Public updatedOn As DateTime = Now
End Class