Imports System.Xml.Serialization

Public Class JobsCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As JobType
        Get
            Return CType(MyBase.List.Item(Index), JobType)
        End Get
    End Property
#End Region

#Region " Add "
    Public Sub Add(ByVal Job As JobType)
        List.Add(Job)
    End Sub
#End Region

End Class
