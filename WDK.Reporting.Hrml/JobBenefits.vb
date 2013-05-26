Imports System.Xml.Serialization

Public Structure JobBenefitsType
    <XmlAttribute("relocation")> Public Relocation As Boolean
    <XmlAttribute("bonus")> Public Bonus As Boolean
    <XmlAttribute("iso")> Public ISO As Boolean
    <XmlAttribute("otherEquity")> Public OtherEquity As Boolean
    <XmlAttribute("benefitsNotes")> Public Notes As String
End Structure