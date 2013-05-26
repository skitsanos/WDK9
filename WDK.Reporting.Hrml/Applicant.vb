Imports System.Xml.Serialization

Public Structure ApplicantType
    <XmlAttribute("fullName")> Public FullName As String
    <XmlAttribute("birthDate", Datatype:="date")> Public BirthDate As Date

    <XmlElement("Contact")> Public Contact As ContactDetailsType
End Structure
