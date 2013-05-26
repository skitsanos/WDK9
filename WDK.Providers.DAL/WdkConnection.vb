Imports System
Imports System.Data

Public Class WdkConnection
    Implements IDbConnection

#Region " Provider "
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

    Private conn As IDbConnection = Nothing

#Region "  New "
    Public Sub New()
        MyBase.New()

        Select Case Provider
            Case ProviderTypes.SQLClient
                conn = New SqlClient.SqlConnection

            Case ProviderTypes.OLEDB
                conn = New OleDb.OleDbConnection

            Case Else
                conn = New Odbc.OdbcConnection

        End Select
    End Sub

    Public Sub New(ByVal connString As String)
        MyBase.New()

        Select Case Provider
            Case ProviderTypes.SQLClient
                conn = New SqlClient.SqlConnection(connString)

            Case ProviderTypes.OLEDB
                conn = New OleDb.OleDbConnection(connString)

            Case Else
                conn = New Odbc.OdbcConnection(connString)

        End Select
    End Sub
#End Region

#Region " BeginTransaction "
    Public Function BeginTransaction() As System.Data.IDbTransaction Implements System.Data.IDbConnection.BeginTransaction
        Return conn.BeginTransaction()
    End Function

    Public Function BeginTransaction(ByVal il As System.Data.IsolationLevel) As System.Data.IDbTransaction Implements System.Data.IDbConnection.BeginTransaction
        Return conn.BeginTransaction(il)
    End Function
#End Region

#Region " ChangeDatabase "
    Public Sub ChangeDatabase(ByVal databaseName As String) Implements System.Data.IDbConnection.ChangeDatabase
        conn.ChangeDatabase(databaseName)
    End Sub
#End Region

#Region " Close "
    Public Sub Close() Implements System.Data.IDbConnection.Close
        conn.Close()
    End Sub
#End Region

#Region " ConnectionString "
    Public Property ConnectionString() As String Implements System.Data.IDbConnection.ConnectionString
        Get
            Return conn.ConnectionString
        End Get
        Set(ByVal value As String)
            conn.ConnectionString = value
        End Set
    End Property
#End Region

#Region " ConnectionTimeout "
    Public ReadOnly Property ConnectionTimeout() As Integer Implements System.Data.IDbConnection.ConnectionTimeout
        Get
            Return conn.ConnectionTimeout
        End Get
    End Property
#End Region

#Region " CreateCommand "
    Public Function CreateCommand() As System.Data.IDbCommand Implements System.Data.IDbConnection.CreateCommand
        Return conn.CreateCommand
    End Function
#End Region

#Region " Database "
    Public ReadOnly Property Database() As String Implements System.Data.IDbConnection.Database
        Get
            Return conn.Database
        End Get
    End Property
#End Region

#Region " Open "
    Public Sub Open() Implements System.Data.IDbConnection.Open
        conn.Open()
    End Sub
#End Region

#Region " State "
    Public ReadOnly Property State() As System.Data.ConnectionState Implements System.Data.IDbConnection.State
        Get
            Return conn.State
        End Get
    End Property
#End Region

#Region " IDisposable "
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If

            Me.Close()
            Me.Dispose(disposing)

        End If
        Me.disposedValue = True
    End Sub
#End Region

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
