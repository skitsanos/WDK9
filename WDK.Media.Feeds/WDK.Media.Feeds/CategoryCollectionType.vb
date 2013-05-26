Public Class CategoryCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As CategoryType
        Get
            Return CType(MyBase.List.Item(Index), CategoryType)
        End Get
    End Property
#End Region

#Region " Add (object) "
    Public Sub Add(ByVal s As CategoryType)
        List.Add(s)
    End Sub
#End Region

#Region " Add (nonobject) "
    Public Sub Add(ByVal name As String, Optional ByVal domain As String = "")
        Dim cat As New CategoryType
        cat.Domain = domain
        cat.Name = name
        List.Add(cat)
    End Sub
#End Region
End Class