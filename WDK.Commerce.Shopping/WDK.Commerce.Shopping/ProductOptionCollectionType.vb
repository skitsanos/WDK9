Imports System.Xml.Serialization

''' <summary>
''' Product Item Options Collection Type defenition
''' </summary>
''' <remarks></remarks>

Public Class ProductOptionCollectionType
    Inherits CollectionBase

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As ProductOptionType
        Get
            Return CType(MyBase.List.Item(Index), ProductOptionType)
        End Get
    End Property
#End Region

#Region " Add (object) "
    Public Sub Add(ByVal s As ProductOptionType)
        List.Add(s)
    End Sub
#End Region

#Region " Add (named) "
    Public Sub Add(ByVal key As String, ByVal value As String)
        Dim obj As New ProductOptionType
        obj.Key = key
        obj.Value = value

        List.Add(obj)
    End Sub
#End Region

#Region " Exists "
    Public Function Exists(ByVal key As String) As Boolean
        Dim ret As Boolean = False

        For Each obj As ProductOptionType In MyBase.List
            If obj.Key = key Then
                ret = True
                Exit For
            End If
        Next

        Return ret
    End Function
#End Region

#Region " Remove "
    Public Sub Remove(ByVal key As String)
        For Each obj As ProductOptionType In MyBase.List
            If obj.Key = key Then
                MyBase.List.Remove(obj)
            End If
        Next
    End Sub
#End Region

End Class