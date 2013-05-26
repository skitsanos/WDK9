'Option Strict Off

#Region " NameSpaces "
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports System.Collections
#End Region

#Region "  Enumerations "
' Each provider supported will be a part of this enum

#End Region


' The sctrucure is to hold parameter info. An Array of this structure
' is sent to the DAL bcos we should not bind to a 
' specific type of parameter like SQLParamter
<Serializable()> _
Public Structure ParamStruct
    Public ParamName As String
    Public DataType As DbType
    Public Value As Object
    Public Direction As ParameterDirection
    Public SourceColumn As String
    Public Size As Int16
End Structure


Public Class ProviderFactory

#Region " Properties "
    Private _ConnectionString As String
    Public Property ConnectionString() As String
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    Private _Provider As ProviderTypes = ProviderTypes.ODBC
    Public Property Provider() As ProviderTypes
        Get
            Return _Provider
        End Get
        Set(ByVal value As ProviderTypes)
            _Provider = value
        End Set
    End Property
#End Region

#Region " New "
    ' Should not be instantiated. So that is always shared
    Private Sub New()
    End Sub
#End Region


#Region " Command "
    Public Shared Function GetCommand(Optional ByVal Provider As ProviderTypes = ProviderTypes.ODBC) As IDbCommand
        Dim cmd As IDbCommand = Nothing

        Select Case Provider
            Case ProviderTypes.SQLClient
                cmd = New SqlCommand

            Case ProviderTypes.OLEDB
                cmd = New OleDbCommand

            Case Else
                cmd = New OdbcCommand

        End Select

        Return cmd
    End Function


    Public Shared Function GetCommand( _
ByVal strCmdText As String, _
ByVal cmdType As CommandType, _
ByVal cmdTimeout As Integer, _
ByVal ParameterArray As ParamStruct(), ByVal Provider As ProviderTypes) As IDbCommand
        Dim cmd As IDbCommand = GetCommand(Provider)
        Dim i As Int16

        If Not ParameterArray Is Nothing Then
            For i = 0 To ParameterArray.Length - 1
                Dim ps As ParamStruct = ParameterArray(i)
                Dim pm As IDbDataParameter = GetParameter(ps.ParamName, ps.Direction, ps.Value, ps.DataType, ps.SourceColumn, ps.Size, Provider)
                cmd.Parameters.Add(pm)
            Next i
        End If
        cmd.CommandType = cmdType
        cmd.CommandText = strCmdText
        Return cmd
    End Function

#End Region

#Region "Connection"
    Public Shared Function GetConnection(Optional ByVal prv As ProviderTypes = ProviderTypes.ODBC) As IDbConnection
        Dim conn As IDbConnection = Nothing

        Select Case prv
            Case ProviderTypes.SQLClient
                conn = New SqlConnection

            Case ProviderTypes.OLEDB
                conn = New OleDbConnection

            Case Else
                conn = New OdbcConnection
        End Select

        Return conn
    End Function

    Public Shared Function GetConnection(ByVal strConnString As String, ByVal provider As ProviderTypes) As IDbConnection

        Dim con As IDbConnection = GetConnection(provider)
        If strConnString = "" Then
            strConnString = ConnectionString
        End If
        strConnString = strConnString & ";App=" & provider.ToString & " Provider"
        con.ConnectionString = strConnString
        Return con

    End Function
#End Region

#Region "Data Adapter"
    Public Shared Function GetAdapter(ByVal provider As ProviderTypes) As IDbDataAdapter

        Select Case provider
            Case ProviderTypes.ODBC
                Return New OdbcDataAdapter
            Case ProviderTypes.SQLClient
                Return New SqlDataAdapter
            Case ProviderTypes.OLEDB
                Return New OleDbDataAdapter
        End Select

    End Function

#End Region

#Region "Parameters"

    Public Shared Function GetParameter(ByVal provider As ProviderTypes) As IDbDataParameter
        Select Case provider
            Case ProviderTypes.ODBC
                Return New OdbcParameter
            Case ProviderTypes.SQLClient
                Return New SqlParameter
            Case ProviderTypes.OLEDB
                Return New OleDbParameter
        End Select
    End Function

    Public Shared Function GetParameter( _
     ByVal paramName As String, _
     ByVal paramDirection As ParameterDirection, _
     ByVal paramValue As Object, _
    ByVal paramtype As DbType, _
    ByVal sourceColumn As String, _
    ByVal size As Int16, _
ByVal provider As ProviderTypes) As IDbDataParameter
        Dim param As IDbDataParameter = GetParameter(provider)
        param.ParameterName = paramName
        param.DbType = paramtype
        If size > 0 Then
            param.Size = size
        End If
        If Not paramValue Is Nothing Then
            param.Value = paramValue
        End If
        param.Direction = paramDirection
        If Not sourceColumn = "" Then
            param.SourceColumn = sourceColumn
        End If
        Return param
    End Function

#End Region

#Region "Transaction"

    Public Shared Function GetTransaction(ByVal conn As IDbConnection, ByVal transisolationLevel As IsolationLevel) As IDbTransaction
        Return conn.BeginTransaction(transisolationLevel)
    End Function


#End Region

#Region "CommandBuilder"
    Public Shared Function GetCommandBuilder(ByVal provider As ProviderTypes) As Object
        Select Case provider
            Case ProviderTypes.ODBC
                Return New OdbcCommandBuilder
            Case ProviderTypes.SQLClient
                Return New SqlCommandBuilder
            Case ProviderTypes.OLEDB
                Return New OleDbCommandBuilder
        End Select
    End Function


#End Region

End Class


