Imports System.Xml.Serialization

Public Enum PaymentFrequencyType
    <XmlEnum("daily")> Daily
    <XmlEnum("weekly")> Weekly
    <XmlEnum("biweekly")> Biweekly
    <XmlEnum("monthly")> Monthly
    <XmlEnum("other")> Other
    <XmlEnum("perProject")> PerProject
    <XmlEnum("perMilestone")> PerMilestone
End Enum