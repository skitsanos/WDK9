Imports System.Xml.Serialization

Public Structure PaymentTermsType
    <XmlAttribute("currency")> Public Currency As String
    <XmlAttribute("freq")> Public Frequency As PaymentFrequencyType
    <XmlAttribute("amount")> Public Amount As Double
End Structure
