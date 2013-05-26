Imports System.Xml.Serialization

Public Structure JobDetailsPageType
    <XmlAttribute("homePage")> Public HomePage As String
    <XmlAttribute("jobsPage")> Public JobsPage As String
End Structure
