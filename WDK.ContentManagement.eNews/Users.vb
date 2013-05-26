Public Class Users
#Region " Properties "
    Private _SortBy As String = "ID DESC"
    Public Property SortBy() As String
        Get
            Return _SortBy
        End Get
        Set(ByVal Value As String)
            _SortBy = Value
        End Set
    End Property
#End Region

#Region " .Add() "
    Public Function Add(ByVal Email As String, ByVal Firstname As String, ByVal Lastname As String, Optional ByVal IsAdmin As Integer = 0) As Boolean
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = Nothing
            strSQL += "INSERT INTO users (email, acc_password, first_name, last_name, isadmin)"
            strSQL += "VALUES (?,?,?,?,?)"

            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)
            dbc.Parameters.Add("@email", OleDb.OleDbType.VarChar, 255).Value = LCase(Email)
            dbc.Parameters.Add("@acc_password", OleDb.OleDbType.LongVarWChar).Value = GeneratePassword(10)
            dbc.Parameters.Add("@first_name", OleDb.OleDbType.VarChar, 255).Value = Firstname
            dbc.Parameters.Add("@last_name", OleDb.OleDbType.VarChar, 255).Value = Lastname
            dbc.Parameters.Add("@isadmin", OleDb.OleDbType.Numeric).Value = IsAdmin

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            Return True

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " .GetPropertyById() "
    Public Function GetPropertyById(ByVal Id As Integer, ByVal Key As String)
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM users WHERE id=" & Id
            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

            Dim dba As New Data.OleDb.OleDbDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "users")

            If dbs.Tables("users").Rows.Count > 0 Then
                GetPropertyById = dbs.Tables("users").Rows(0).Item(Key)
            Else
                GetPropertyById = 0
            End If

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return GetPropertyById
        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " .GetIdByEmail() "
    Public Function GetIdByEmail(ByVal Email As String) As Integer
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM users WHERE email='" & LCase(Email) & "'"
           Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

            Dim dba As New Data.OleDb.OleDbDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "users")

            Dim res As Integer = 0
            If dbs.Tables("users").Rows.Count > 0 Then
                res = dbs.Tables("users").Rows(0).Item("id")
            End If

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return res

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " Update() "
    Public Function Update(ByVal Id As Integer, ByVal Email As String, ByVal FirstName As String, ByVal LastName As String, ByVal Password As String, Optional ByVal IsAdmin As Integer = 0) As Boolean
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = Nothing
            strSQL += "UPDATE users SET email=?, first_name=?, last_name=?, acc_password=?, isadmin=? WHERE id=" & Id

            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)
            dbc.Parameters.Add("@email", OleDb.OleDbType.VarChar, 255).Value = LCase(Email)
            dbc.Parameters.Add("@first_name", OleDb.OleDbType.VarChar, 255).Value = FirstName
            dbc.Parameters.Add("@last_name", OleDb.OleDbType.VarChar, 255).Value = LastName
            dbc.Parameters.Add("@acc_password", OleDb.OleDbType.LongVarWChar).Value = Password
            dbc.Parameters.Add("@isadmin", OleDb.OleDbType.Numeric).Value = IsAdmin

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            Return True

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " .Delete() "
    Public Function Delete(ByVal UserId As Integer) As Boolean
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim dbc As New Data.OleDb.OleDbCommand
            dbc.CommandText = "DELETE FROM users WHERE id=" & UserId
            dbc.Connection = db
            dbc.ExecuteNonQuery()

            dbc.CommandText = "DELETE FROM subscriptions WHERE [user]=" & UserId
            dbc.Connection = db
            dbc.ExecuteNonQuery()

            dbc.Dispose()

            Return True

        Catch ex As Exception
            Log(ex.Message, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " GetDataset() "
    Public Function GetDataset() As System.Data.DataSet
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM users ORDER BY " & SortBy
            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

            Dim dba As New Data.OleDb.OleDbDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "users")

            Return dbs

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function

    Public Function GetDataset(ByVal startRecord As Integer, ByVal Rows As Integer) As System.Data.DataSet
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM users"
            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

            Dim dba As New Data.OleDb.OleDbDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, startRecord, Rows, "users")

            Return dbs

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " Exists() "
    Public Function Exists(ByVal Email As String) As Boolean
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM users WHERE email='" & LCase(Email) & "'"
            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

            Dim dba As New Data.OleDb.OleDbDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "users")

            Dim res As Boolean = False

            If dbs.Tables("users").Rows.Count > 0 Then res = True

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return res
        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " IsAuthorized() "
    Public Function IsAuthorized(ByVal Email As String, ByVal Password As String) As Boolean
        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM users WHERE email='" & LCase(Email) & "' AND acc_password='" & Password & "'"
            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

            Dim dba As New Data.OleDb.OleDbDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "users")

            Dim res As Boolean = False

            If dbs.Tables("users").Rows.Count > 0 Then res = True

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return res
        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " Count() "
    Public Function Count() As Integer
        Dim cnt As Integer = 0

        Dim db As Data.OleDb.OleDbConnection
        Try
            db = New Data.OleDb.OleDbConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM users"
            Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

            Dim dba As New Data.OleDb.OleDbDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "users")

            cnt = dbs.Tables("users").Rows.Count

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return cnt
        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

End Class
