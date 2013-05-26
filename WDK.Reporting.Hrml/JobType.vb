Imports System.Xml.Serialization

Public Enum JobKindType
    <XmlEnum("partTime")> PartTime
    <XmlEnum("contract")> Contract
    <XmlEnum("fullTime")> FullTime
    <XmlEnum("remote")> Remote
End Enum