Imports System.Xml.Serialization

Public Class ForumAccessRolesCollectionType
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As ForumAccessRuleType) As ForumAccessRuleType
        MyBase.Add(obj)
        Return obj
    End Function

    Public Shadows Function Add(ByVal accountUsername As String, ByVal allowRead As Boolean, ByVal allowWrite As Boolean) As ForumAccessRuleType
        Dim obj As New ForumAccessRuleType
        obj.accountUsername = accountUsername
        obj.allowRead = allowRead
        obj.allowWrite = allowWrite
        MyBase.Add(obj)
        Return obj
    End Function

    Public Shadows Function Add() As ForumAccessRuleType
        Return Add(New ForumAccessRuleType)
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As ForumAccessRuleType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As ForumAccessRuleType)
        MyBase.Remove(obj)
    End Sub

    Public Shadows Sub Remove(ByVal index As Integer)
        MyBase.Remove(MyBase.Item(index))
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As ForumAccessRuleType
        Get
            Item = DirectCast(MyBase.Item(index), ForumAccessRuleType)
        End Get
        Set(ByVal Value As ForumAccessRuleType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class
