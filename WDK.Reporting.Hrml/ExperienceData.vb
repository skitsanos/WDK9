Imports System.Xml.Serialization

Public Class ExperienceType
    <XmlElement("Skills")> Public Skills As New SkillsCollectionType
    <XmlArray("WorkingExperience"), XmlArrayItem("Employment")> Public WorkExperience As New WorkExperienceCollectionType
End Class
