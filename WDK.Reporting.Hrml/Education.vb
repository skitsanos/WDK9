Public Class EducationCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As EducationType
        Get
            Return CType(MyBase.List.Item(Index), EducationType)
        End Get
    End Property
#End Region

#Region " Add (object) "
    Public Sub Add(ByVal s As EducationType)
        List.Add(s)
    End Sub

    Public Sub Add(ByVal Institution As String, ByVal field As String, ByVal degree As String, ByVal enrolledOn As String, ByVal completedOn As String)
        Dim obj As New EducationType
        obj.EnrolledOn = enrolledOn
        obj.CompletedOn = completedOn
        obj.Field = field
        obj.Degree = degree

        List.Add(obj)
    End Sub
#End Region

End Class
