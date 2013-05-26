Imports System
Imports System.Data

'http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpcontemplateparametervb.asp

Public Class WdkParameter
    Implements IDataParameter

    Public Property DbType() As DbType Implements IDataParameter.DbType
        Get

        End Get
        Set(ByVal value As DbType)

        End Set
    End Property

    Public Property Direction() As ParameterDirection Implements IDataParameter.Direction
        Get

        End Get
        Set(ByVal value As ParameterDirection)

        End Set
    End Property

    Public ReadOnly Property IsNullable() As Boolean Implements IDataParameter.IsNullable
        Get

        End Get
    End Property

    Public Property ParameterName() As String Implements IDataParameter.ParameterName
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property SourceColumn() As String Implements IDataParameter.SourceColumn
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property SourceVersion() As DataRowVersion Implements IDataParameter.SourceVersion
        Get

        End Get
        Set(ByVal value As DataRowVersion)

        End Set
    End Property

    Public Property Value() As Object Implements IDataParameter.Value
        Get

        End Get
        Set(ByVal value As Object)

        End Set
    End Property
End Class
