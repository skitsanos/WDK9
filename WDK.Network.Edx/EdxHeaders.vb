Public Class EdxHeaders
    Inherits CollectionBase

#Region " Add() "
    Public Sub Add(ByVal Name As String, ByVal Value As String)
        Dim NewHeader As New EdxHeader
        NewHeader.Name = Name
        NewHeader.Value = Value
        MyBase.List.Add(NewHeader)
    End Sub
#End Region

#Region " Item "
    Default Public ReadOnly Property Item(ByVal index As Integer) As EdxHeader
        Get
            Return CType(MyBase.List.Item(index), EdxHeader)
        End Get
    End Property
#End Region

#Region " Remove() "
    Public Sub Remove(ByVal Name As String)
        For Each hdr As EdxHeader In MyBase.List
            If hdr.Name = Name Then
                MyBase.List.Remove(Name)
            End If
        Next
    End Sub

    Public Sub Remove(ByVal Header As EdxHeader)
        MyBase.List.Remove(Header)
    End Sub
#End Region

End Class