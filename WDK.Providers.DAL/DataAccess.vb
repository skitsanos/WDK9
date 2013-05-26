#Region " NameSpaces "
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Data.OleDb
Imports System.Configuration
Imports System.Text
Imports System.Collections
#End Region

Public Class DataAccess

    Private _trans As IDbTransaction
    Private _isolationLevel As IsolationLevel
    Private _conn As IDbConnection
    Private _cmdTimeout As Int32
    Private _connString As String
    Private _provider As ProviderTypes
    Private Const COMMAND_TIMEOUT = 100
    Private _commandBehavior As CommandBehavior


    ' Getting the config settings and set the default isolation level and
    ' DataReader command behavior
    Public Sub New()
        _isolationLevel = IsolationLevel.ReadCommitted
        _commandBehavior = CommandBehavior.CloseConnection
        _provider = ProviderFactory.Provider
    End Sub

    ' This method is used by ExecDataSet, ExecScalar, ExecReader and ExecNonQuery. This is a common piece of 
    ' code called in these methods
    Private Sub PrepareAll(ByRef cmd As IDbCommand, ByRef conn As IDbConnection, ByVal strSQL As String, ByVal cmdType As CommandType, ByVal parameterArray As ParamStruct())
        ' If transaction has already been started
        If Not IsInTransaction() Then
            conn = ProviderFactory.GetConnection(ConnectionString, Provider)
            cmd = ProviderFactory.GetCommand(strSQL, cmdType, CmdTimeout, parameterArray, Provider)
            cmd.Connection = conn
            conn.Open()
        Else
            cmd = ProviderFactory.GetCommand(strSQL, cmdType, CmdTimeout, parameterArray, Provider)
            cmd.Transaction = _trans
            cmd.Connection = _conn
        End If
    End Sub


#Region "Public Properties"
    Public Property Provider() As Providers
        Get
            Return _provider
        End Get
        Set(ByVal Value As Providers)
            _provider = Value
        End Set
    End Property

    Public Property ConnectionString() As String
        Get
            Return _connString
        End Get
        Set(ByVal Value As String)
            _connString = Value
        End Set
    End Property

    Public Property TransIsolationLevel() As IsolationLevel
        Get
            Return _isolationLevel
        End Get
        Set(ByVal Value As IsolationLevel)
            _isolationLevel = Value
        End Set
    End Property

    Public Property CmdTimeout() As Int32
        Get
            If _cmdTimeout = 0 Then
                Return COMMAND_TIMEOUT
            End If
            Return _cmdTimeout
        End Get
        Set(ByVal Value As Int32)
            _cmdTimeout = Value
        End Set
    End Property

    ' To be used exclusively by the Datareader
    Public Property ReaderCommandBehavior() As CommandBehavior
        Get
            Return _commandBehavior
        End Get
        Set(ByVal Value As CommandBehavior)
            _commandBehavior = Value
        End Set
    End Property



#End Region

#Region "Transactions"
    Public Sub BeginTrans(ByVal connString As String, ByVal transisolationLevel As IsolationLevel)
        _conn = ProviderFactory.GetConnection(connString, Provider)
        _conn.Open()
        _trans = ProviderFactory.GetTransaction(_conn, transisolationLevel)
    End Sub

    Public Sub BeginTrans(ByVal transisolationLevel As IsolationLevel)
        _conn = ProviderFactory.GetConnection(_connString, Provider)
        _conn.Open()
        _trans = ProviderFactory.GetTransaction(_conn, transisolationLevel)
    End Sub

    Public Sub CommitTrans()
        CommitTrans(True)
    End Sub

    ' This is for DataReader usage only. The caller has to pass false here so that
    ' the connection is not closed before the DR is closed
    Public Sub CommitTrans(ByVal CloseConnection As Boolean)
        _trans.Commit()
        DisposeTrans(CloseConnection)
    End Sub

    Public Sub AbortTrans()
        If IsInTransaction() Then
            _trans.Rollback()
            DisposeTrans(True)
        End If
    End Sub

    Private Sub DisposeTrans(ByVal CloseConnection As Boolean)
        If CloseConnection Then
            If Not _conn Is Nothing Then
                _conn.Close()
                _conn.Dispose()
            End If
        End If
        _trans.Dispose()
    End Sub

    Public Function IsInTransaction() As Boolean
        Return (Not _trans Is Nothing)
    End Function

#End Region

    'To return a DataSet after running a SQL Statement
#Region "ExecDataSet"

    Public Sub ExecDataSet( _
    ByVal ds As DataSet, _
        ByVal strSQL As String, _
        ByVal cmdtype As CommandType)

        ExecDataSet(ds, strSQL, cmdtype, Nothing)
    End Sub

    Public Function ExecDataSet( _
        ByVal strSQL As String, _
        ByVal cmdtype As CommandType) As DataSet

        Return ExecDataSet(strSQL, cmdtype, Nothing)
    End Function

    Public Function ExecDataSet( _
ByVal strSQL As String, _
ByVal cmdtype As CommandType, _
ByVal parameterArray As ParamStruct()) As DataSet

        Dim ds As New DataSet("DataSet")
        ExecDataSet(ds, strSQL, cmdtype, parameterArray)
        Return ds

    End Function

    Public Sub ExecDataSet( _
    ByVal ds As DataSet, _
ByVal strSQL As String, _
ByVal cmdtype As CommandType, _
ByVal parameterArray As ParamStruct())


        Dim da As IDbDataAdapter
        Dim cmd As IDbCommand
        Dim conn As IDbConnection
        Try
            da = ProviderFactory.GetAdapter(Provider)
            PrepareAll(cmd, conn, strSQL, cmdtype, parameterArray)
            da.SelectCommand = cmd
            da.Fill(ds)

        Catch ex As Exception
            GenericExceptionHandler(ex)
        Finally
            If Not IsInTransaction() Then
                conn.Close()
                conn.Dispose()
            End If
            cmd.Dispose()
            CType(da, IDisposable).Dispose()
        End Try


    End Sub


#End Region

    ' To run SQL Statements to return DataReader.
#Region "ExecDataReader"
    Public Function ExecDataReader _
       (ByVal strSQL As String, _
        ByVal cmdtype As CommandType, _
ByVal parameterArray As ParamStruct()) As IDataReader

        Dim conn As IDbConnection
        Dim cmd As IDbCommand
        Try

            PrepareAll(cmd, conn, strSQL, cmdtype, parameterArray)
            Return cmd.ExecuteReader(ReaderCommandBehavior)

        Catch ex As Exception
            If Not IsInTransaction() Then
                conn.Close()
                conn.Dispose()
            End If
            GenericExceptionHandler(ex)
        Finally
            cmd.Dispose()
        End Try

    End Function

    Public Function ExecDataReader _
       (ByVal strSQL As String, _
        ByVal cmdtype As CommandType) As IDataReader

        Return ExecDataReader(strSQL, cmdtype, Nothing)

    End Function

#End Region

    ' TO run simple SQL statements W/O returning anything(records) back
#Region "ExecNonQuery"

    Public Function ExecNonQuery _
    (ByVal strSQL As String, _
    ByVal cmdType As CommandType) As Integer
        Return ExecNonQuery(strSQL, cmdType, Nothing)
    End Function

    Public Function ExecNonQuery _
    (ByVal strSQL As String, _
    ByVal cmdType As CommandType, _
       ByVal parameterArray As ParamStruct()) As Integer

        Dim cmd As IDbCommand
        Dim conn As IDbConnection
        Try
            PrepareAll(cmd, conn, strSQL, cmdType, parameterArray)
            Return cmd.ExecuteNonQuery()

        Catch ex As Exception
            GenericExceptionHandler(ex)
        Finally
            If Not IsInTransaction() Then
                conn.Close()
                conn.Dispose()
            End If
            cmd.Dispose()
        End Try
    End Function


#End Region

#Region "SaveDataSet"

    ' This method saves data in a dataset with a single table and mandates the table name to be "Table".
    ' Operations on a single table are batched.
    Public Sub SaveDataSet _
        (ByVal ds As DataSet, _
            ByVal insertSQL As String, _
        ByVal deleteSQL As String, _
            ByVal updateSQL As String, _
        ByVal InsertparameterArray As ParamStruct(), _
        ByVal DeleteparameterArray As ParamStruct(), _
        ByVal UpdateparameterArray As ParamStruct() _
    )

        Dim cn As IDbConnection
        Dim da As IDbDataAdapter
        Dim cmd As IDbCommand
        Try
            da = ProviderFactory.GetAdapter(Provider)
            If Not IsInTransaction() Then
                cn = ProviderFactory.GetConnection(ConnectionString, Provider)
                If insertSQL <> "" Then
                    da.InsertCommand = ProviderFactory.GetCommand(insertSQL, CommandType.StoredProcedure, CmdTimeout, InsertparameterArray, Provider)
                    da.InsertCommand.Connection = cn
                End If
                If updateSQL <> "" Then
                    da.UpdateCommand = ProviderFactory.GetCommand(updateSQL, CommandType.StoredProcedure, CmdTimeout, UpdateparameterArray, Provider)
                    da.UpdateCommand.Connection = cn
                End If
                If deleteSQL <> "" Then
                    da.DeleteCommand = ProviderFactory.GetCommand(deleteSQL, CommandType.StoredProcedure, CmdTimeout, DeleteparameterArray, Provider)
                    da.DeleteCommand.Connection = cn
                End If
                cn.Open()
            Else
                If insertSQL <> "" Then
                    da.InsertCommand = ProviderFactory.GetCommand(insertSQL, CommandType.StoredProcedure, CmdTimeout, InsertparameterArray, Provider)
                    da.InsertCommand.Connection = _conn
                    da.InsertCommand.Transaction = _trans
                End If
                If updateSQL <> "" Then
                    da.UpdateCommand = ProviderFactory.GetCommand(updateSQL, CommandType.StoredProcedure, CmdTimeout, UpdateparameterArray, Provider)
                    da.UpdateCommand.Connection = _conn
                    da.UpdateCommand.Transaction = _trans
                End If
                If deleteSQL <> "" Then
                    da.DeleteCommand = ProviderFactory.GetCommand(deleteSQL, CommandType.StoredProcedure, CmdTimeout, DeleteparameterArray, Provider)
                    da.DeleteCommand.Connection = _conn
                    da.DeleteCommand.Transaction = _trans
                End If
            End If
            da.Update(ds)


        Catch ex As Exception
            GenericExceptionHandler(ex)
        Finally
            If Not IsInTransaction() Then
                cn.Close()
                cn.Dispose()
            End If
            If insertSQL <> "" Then
                da.InsertCommand.Parameters.Clear()
                da.InsertCommand.Dispose()
            End If
            If updateSQL <> "" Then
                da.UpdateCommand.Parameters.Clear()
                da.UpdateCommand.Dispose()
            End If
            If deleteSQL <> "" Then
                da.DeleteCommand.Parameters.Clear()
                da.DeleteCommand.Dispose()
            End If
            CType(da, IDisposable).Dispose()
        End Try
    End Sub

#End Region

    ' To be used for getting single values. like Average, Sum etc from the DB
#Region "ExecScalar"

    Public Function ExecScalar(ByVal strSQL As String, _
        ByVal cmdtype As CommandType, _
    ByVal parameterArray() As ParamStruct _
    ) As Object

        Dim conn As IDbConnection
        Dim cmd As IDbCommand
        Try

            PrepareAll(cmd, conn, strSQL, cmdtype, parameterArray)
            Return cmd.ExecuteScalar

        Catch ex As Exception
            GenericExceptionHandler(ex)
        Finally
            If Not IsInTransaction() Then
                conn.Close()
                conn.Dispose()
            End If
            cmd.Dispose()
        End Try

    End Function

    Public Function ExecScalar(ByVal strSQL As String, _
        ByVal cmdtype As CommandType) As Object
        Return ExecScalar(strSQL, cmdtype, Nothing)
    End Function


#End Region

    ' This can be used to execute an SP and get an array of output params from it
#Region "Prepared SQL"
    Public Function ExecPreparedSQL _
   (ByVal strSQL As String, _
   ByVal cmdtype As CommandType, _
         ByVal parameterArray As ParamStruct()) As ArrayList

        Dim cmd As IDbCommand
        Dim conn As IDbConnection
        Dim alParams As New ArrayList
        Try
            PrepareAll(cmd, conn, strSQL, cmdtype, parameterArray)
            cmd.ExecuteNonQuery()
            Dim iParam As IDbDataParameter
            For Each iParam In cmd.Parameters
                If iParam.Direction = ParameterDirection.Output Or iParam.Direction = ParameterDirection.InputOutput Then
                    alParams.Add(iParam.Value)
                End If
            Next
            Return alParams
        Catch ex As Exception
            GenericExceptionHandler(ex)
        Finally
            If Not IsInTransaction() Then
                conn.Close()
                conn.Dispose()
            End If
            cmd.Dispose()
        End Try
    End Function

#End Region

    ' There should be one hanlder for each supported provider.
    ' This is a template and more error handling code should come into place
#Region "Exception handlers"

    Private Sub GenericExceptionHandler(ByVal ex As Exception)

        If TypeOf ex Is SqlException Then
            SQLExceptionHandler(ex)
        ElseIf TypeOf ex Is OleDbException Then
            OLEDBExceptionHandler(ex)
        ElseIf TypeOf ex Is OdbcException Then
            ODBCExceptionHandler(ex)
        Else
            Throw ex
        End If

    End Sub

    Private Sub SQLExceptionHandler(ByVal ex As SqlException)
        Dim sqlerr As SqlError
        Dim sb As New StringBuilder
        For Each sqlerr In ex.Errors
            sb.AppendFormat("Error: {0}{1}", sqlerr.Message, Environment.NewLine)
            sb.AppendFormat("Server: {0}{1}", sqlerr.Server, Environment.NewLine)
            sb.AppendFormat("Source: {0}{1}", sqlerr.Source, Environment.NewLine)
            sb.Append("-----------------------------------------------")
        Next
        'TODO For each custom sql server error have an entry
        Throw New Exception(sb.ToString, ex)
    End Sub

    Private Sub OLEDBExceptionHandler(ByVal ex As OleDbException)
        Dim oledberr As OleDbError
        Dim sb As New StringBuilder
        For Each oledberr In ex.Errors
            sb.AppendFormat("Error: {0}{1}", oledberr.Message, Environment.NewLine)
            sb.AppendFormat("Source: {0}{1}", oledberr.Source, Environment.NewLine)
            sb.Append("-----------------------------------------------")
        Next
        'TODO For each custom sql server error have an entry
        Throw New Exception(sb.ToString, ex)
    End Sub

    Private Sub ODBCExceptionHandler(ByVal ex As OdbcException)
        Dim odbcerr As OdbcError
        Dim sb As New StringBuilder
        For Each odbcerr In ex.Errors
            sb.AppendFormat("Error: {0}{1}", odbcerr.Message, Environment.NewLine)
            sb.AppendFormat("Source: {0}{1}", odbcerr.Source, Environment.NewLine)
            sb.Append("-----------------------------------------------")
        Next
        'TODO For each custom sql server error have an entry
        Throw New Exception(sb.ToString, ex)
    End Sub

#End Region

End Class