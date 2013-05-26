Public Class ExtensionCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As Object
        Get
            Return CType(MyBase.List.Item(Index), Object)
        End Get
    End Property
#End Region

#Region " Add (object) "
    Public Sub Add(ByVal s As Object)
        List.Add(s)
    End Sub
#End Region
End Class
