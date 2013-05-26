Imports System.Xml.Serialization

Public Class ForumMessageCollectionType
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As ForumMessageType) As ForumMessageType
        MyBase.Add(obj)
        Return obj
    End Function

    Public Shadows Function Add(ByVal author As String, ByVal subject As String, ByVal message As String) As ForumMessageType
        Dim obj As New ForumMessageType
        obj.Message = message
        obj.Subject = subject
        obj.Author = author
        MyBase.Add(obj)
        Return obj
    End Function

    Public Shadows Function Add() As ForumMessageType
        Return Add(New ForumMessageType)
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As ForumMessageType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As ForumMessageType)
        MyBase.Remove(obj)
    End Sub

    Public Shadows Sub Remove(ByVal index As Integer)
        MyBase.Remove(MyBase.Item(index))
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As ForumMessageType
        Get
            Item = DirectCast(MyBase.Item(index), ForumMessageType)
        End Get
        Set(ByVal Value As ForumMessageType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class