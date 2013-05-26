Imports System.Xml.Serialization

''' <summary>
''' Employment Record Details
''' </summary>
''' <remarks></remarks>
Public Structure EmploymentType
    <XmlAttribute("position")> Public Position As String
    <XmlAttribute("startedOn")> Public StartedOn As String
    <XmlAttribute("completedOn")> Public CompletedOn As String
    <XmlAttribute("type")> Public Type As JobKindType
    <XmlAttribute("comments")> Public Comments As String

    <XmlElement("Company")> Public Company As CompanyDataType
End Structure