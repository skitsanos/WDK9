Public Class ChannelItemCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As ChannelItemType
        Get
            Return CType(MyBase.List.Item(Index), ChannelItemType)
        End Get
    End Property
#End Region

#Region " Add (object) "
    Public Sub Add(ByVal s As ChannelItemType)
        List.Add(s)
    End Sub
#End Region

End Class