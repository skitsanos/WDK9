Imports System.Xml.Serialization

'- 
'   <Security>
'       <Access User="*|username" Enabled="true|false" />
'   </Security>

Public Class [Security]
    <XmlArray(), XmlArrayItem("Access")> Public Rules As New AccessRules
End Class

#Region " AccessRules "
Public Class AccessRules
    Inherits CollectionBase

    Default Public ReadOnly Property Item(ByVal Index As Integer) As AccessRule
        Get
            Return CType(MyBase.List.Item(Index), AccessRule)
        End Get
    End Property

    Public Sub Add(ByVal s As AccessRule)
        List.Add(s)
    End Sub

    Public Sub Add(<XmlAnyAttribute()> ByVal Name As String, <XmlAnyAttribute()> Optional ByVal Enabled As Boolean = True)
        Dim item As New AccessRule
        item.User = Name
        item.Enabled = Enabled
        List.Add(item)
    End Sub

    Public Sub Remove(ByVal Name As String)
        Try
            For Each opt As AccessRule In List
                If opt.User = Name Then List.Remove(opt)
            Next
        Catch ex As Exception
            'Log(ex.ToString)
        End Try
    End Sub
End Class
#End Region

#Region " AccessRule "
Public Structure AccessRule
    <XmlAttribute("user")> Public User As String
    <XmlAttribute("enabled")> Public Enabled As Boolean
End Structure
#End Region