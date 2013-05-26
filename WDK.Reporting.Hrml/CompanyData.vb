Imports System.Xml.Serialization

Public Structure CompanyDataType
    <XmlAttribute("name")> Public Name As String
    <XmlAttribute("dba")> Public DBA As String
    <XmlElement("Contact")> Public Contact As ContactDetailsType
    <XmlElement("CorporateData")> Public CorporateData As CorporateDataType
    <XmlElement("InternetData")> Public InternetData As JobDetailsPageType
End Structure
