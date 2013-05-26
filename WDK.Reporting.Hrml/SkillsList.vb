Imports System.Xml.Serialization

Public Class SkillsCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As SkillType
        Get
            Return CType(MyBase.List.Item(Index), SkillType)
        End Get
    End Property
#End Region

#Region " Add (object) "
    Public Sub Add(ByVal s As SkillType)
        List.Add(s)
    End Sub
#End Region

#Region " Add "
    Public Sub Add(<XmlAnyAttribute()> ByVal Title As String, <XmlAnyAttribute()> ByVal YearsOfExperience As Integer, <XmlAnyAttribute()> ByVal Level As ExpertiseLevelType)
        Dim item As New SkillType
        item.Level = Level
        item.Title = Title
        item.YearsOfExperience = YearsOfExperience

        List.Add(item)
    End Sub
#End Region

End Class