Imports System.Xml.Serialization

Public Structure RecruiterType
    <XmlAttribute("name")> Public Name As String
    <XmlAttribute("title")> Public Title As String
    <XmlElement("Contact", isnullable:=False)> Public Contact As ContactDetailsType
End Structure