Imports System.Xml.Serialization

Public Class JobType
    <XmlAttribute("refCode")> Public ReferenceCode As String
    <XmlAttribute("title")> Public Title As String
    <XmlAttribute("description")> Public Description As String
    <XmlAttribute("keywords")> Public Keywords As String
    <XmlAttribute("closeDate", Datatype:="date")> Public CloseDate As Date
    <XmlAttribute("degree")> Public Degree As String
    <XmlAttribute("jobType")> Public JobType As JobKindType
    <XmlAttribute("misc")> Public Miscellaneous As String

    <XmlArray("Skills"), XmlArrayItem("Skill")> Public Skills As New SkillsCollectionType
    <XmlElement("PaymentTerms")> Public PaymentTerms As PaymentTermsType
    <XmlElement("Location")> Public Location As ContactDetailsType
End Class
