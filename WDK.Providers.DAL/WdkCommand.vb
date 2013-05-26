Imports System
Imports System.Data

Public Class WdkCommand
    Implements IDbCommand

#Region " Properties "
    Private m_connection As WdkConnection
    Private m_txn As WdkTransaction
    Private m_sCmdText As String
    Private m_updatedRowSource As UpdateRowSource = UpdateRowSource.None
    Private m_parameters As New WdkParameterCollection


    Private iCmd As IDbCommand
#End Region

#Region " New "
    Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal cmdText As String)
        MyBase.New()

        Dim prv As WdkConnection = iCmd.Connection

        Select Case prv.Provider
            Case ProviderTypes.SQLClient
                iCmd = New SqlClient.SqlCommand
                iCmd.CommandText = cmdText

            Case ProviderTypes.OLEDB
                iCmd = New OleDb.OleDbCommand
                iCmd.CommandText = cmdText

            Case Else
                iCmd = New Odbc.OdbcCommand
                iCmd.CommandText = cmdText
        End Select
    End Sub

    Public Sub New(ByVal cmdText As String, ByVal connection As WdkConnection)
        Select Case connection.Provider
            Case ProviderTypes.SQLClient
                iCmd = New SqlClient.SqlCommand
                iCmd.Connection = connection
                iCmd.CommandText = cmdText

            Case ProviderTypes.OLEDB
                iCmd = New OleDb.OleDbCommand
                iCmd.Connection = connection
                iCmd.CommandText = cmdText

            Case Else
                iCmd = New Odbc.OdbcCommand
                iCmd.Connection = connection
                iCmd.CommandText = cmdText
        End Select
    End Sub
#End Region

#Region " Cancel "
    Public Sub Cancel() Implements IDbCommand.Cancel
        iCmd.Cancel()
    End Sub
#End Region

#Region " CommandText "
    Public Property CommandText() As String Implements IDbCommand.CommandText
        Get
            Return iCmd.CommandText
        End Get
        Set(ByVal value As String)
            iCmd.CommandText = value
        End Set
    End Property
#End Region

#Region " CommandTimeout "
    Public Property CommandTimeout() As Integer Implements IDbCommand.CommandTimeout
        Get
            Return iCmd.CommandTimeout
        End Get
        Set(ByVal value As Integer)
            iCmd.CommandTimeout = value
        End Set
    End Property
#End Region

#Region " CommandType "
    Public Property CommandType() As CommandType Implements IDbCommand.CommandType
        Get
            Return iCmd.CommandType
        End Get
        Set(ByVal value As CommandType)
            iCmd.CommandType = value
        End Set
    End Property
#End Region

#Region " Connection "
    Public Property Connection() As IDbConnection Implements IDbCommand.Connection
        Get
            Return iCmd.Connection
        End Get
        Set(ByVal value As IDbConnection)
            iCmd.Connection = value
        End Set
    End Property
#End Region

#Region " CreateParameter "
    Public Function CreateParameter() As IDbDataParameter Implements IDbCommand.CreateParameter
        Return iCmd.CreateParameter
    End Function
#End Region

#Region " ExecuteNonQuery "
    Public Function ExecuteNonQuery() As Integer Implements IDbCommand.ExecuteNonQuery
        Return iCmd.ExecuteNonQuery
    End Function
#End Region

#Region " ExecuteReader "
    Public Function ExecuteReader() As IDataReader Implements IDbCommand.ExecuteReader
        Return iCmd.ExecuteReader
    End Function
#End Region

#Region " ExecuteReader {behavior} "
    Public Function ExecuteReader(ByVal behavior As CommandBehavior) As IDataReader Implements IDbCommand.ExecuteReader
        Return iCmd.ExecuteReader(behavior)
    End Function
#End Region

#Region " ExecuteScalar "
    Public Function ExecuteScalar() As Object Implements IDbCommand.ExecuteScalar
        Return iCmd.ExecuteScalar
    End Function
#End Region

#Region " Parameters "
    Public ReadOnly Property Parameters() As IDataParameterCollection Implements IDbCommand.Parameters
        Get
            Return iCmd.Parameters
        End Get
    End Property
#End Region

#Region " Prepare "
    Public Sub Prepare() Implements IDbCommand.Prepare
        iCmd.Prepare()
    End Sub
#End Region

#Region " Transaction "
    Public Property Transaction() As IDbTransaction Implements IDbCommand.Transaction
        Get
            Return iCmd.Transaction
        End Get
        Set(ByVal value As IDbTransaction)
            iCmd.Transaction = value
        End Set
    End Property
#End Region

#Region " UpdatedRowSource "
    Public Property UpdatedRowSource() As UpdateRowSource Implements IDbCommand.UpdatedRowSource
        Get
            Return iCmd.UpdatedRowSource
        End Get
        Set(ByVal value As UpdateRowSource)
            iCmd.UpdatedRowSource = value
        End Set
    End Property
#End Region

#Region " Dispose "
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If

        iCmd.Dispose()

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
