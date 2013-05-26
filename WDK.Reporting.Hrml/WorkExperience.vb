Public Class WorkExperienceCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As EmploymentType
        Get
            Return CType(MyBase.List.Item(Index), EmploymentType)
        End Get
    End Property
#End Region

#Region " Add "
    Public Sub Add(ByVal employment As EmploymentType)
        List.Add(employment)
    End Sub

    Public Sub Add(ByVal companyName As String, ByVal position As String, ByVal type As JobKindType, ByVal startedOn As String, ByVal completedOn As String)
        Dim obj As New EmploymentType
        obj.Company.Name = companyName
        obj.Type = type
        obj.StartedOn = startedOn
        obj.CompletedOn = completedOn
        obj.Position = position

        List.Add(obj)
    End Sub
#End Region

End Class