Imports System.Xml.Serialization

Public Structure SkillType
    <XmlAttribute("title")> Public Title As String
    <XmlAttribute("yearsOfExperience")> Public YearsOfExperience As Integer
    <XmlAttribute("level")> Public Level As ExpertiseLevelType
End Structure
