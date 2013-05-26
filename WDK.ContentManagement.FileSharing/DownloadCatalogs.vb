Imports System.Data.Odbc

Public Class Catalogs

#Region " Properties "
    Private _SortBy As String = "id DESC"
    Public Property SortBy() As String
        Get
            Return _SortBy
        End Get
        Set(ByVal Value As String)
            _SortBy = Value
        End Set
    End Property
#End Region

#Region " Add() "
    Public Function Add(ByVal Title As String, ByVal Description As String) As Boolean
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = Nothing
            strSQL += "INSERT INTO Folders (ApplicationName, Title, Description)"
            strSQL += "VALUES (?,?,?)"

            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Title", Title)
            dbc.Parameters.AddWithValue("@Description", Description)

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

#Region " Update() "
    Public Function Update(ByVal Id As Integer, ByVal Title As String, ByVal Description As String) As Boolean
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Dim res As Boolean = False
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = _
            "UPDATE Folders SET Title=?, Description=? WHERE ApplicationName=? AND id=" & Id

            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@Title", Title)
            dbc.Parameters.AddWithValue("@Description", Description)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return res
    End Function
#End Region

#Region " .Delete() "
    Public Function Delete(ByVal Id As Integer, Optional ByVal DeleteFiles As Boolean = True) As Boolean
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New Data.Odbc.OdbcCommand( _
            "DELETE FROM Folders WHERE ApplicationName=? AND id=" & Id, db)

            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.ExecuteNonQuery()

            If DeleteFiles = True Then
                dbc = New OdbcCommand( _
                "DELETE FROM Downloads WHERE ApplicationName=? AND DownloadCatalog=" & Id, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.ExecuteNonQuery()
            End If

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

#Region " GetDataset() "
    Public Function GetDataset(Optional ByVal Criteria As String = "") As System.Data.DataSet
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            If Criteria = "" Then
                Criteria = "WHERE "
            Else
                Criteria += " AND "
            End If

            Dim strSQL As String = "SELECT * FROM Folders " & Criteria & "  ApplicationName=? ORDER BY " & SortBy
            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New Data.Odbc.OdbcDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Folders")

            Return dbs

        Catch ex As Exception
            Log(ex.ToString, True)
            Return Nothing
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function

    Public Function GetDataset(ByVal startRecord As Integer, ByVal Rows As Integer, Optional ByVal Criteria As String = "") As System.Data.DataSet
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()
            If Criteria = "" Then
                Criteria = "WHERE "
            Else
                Criteria += " AND "
            End If

            Dim strSQL As String = "SELECT * FROM Folders " & Criteria & " ApplicationName=? ORDER BY " & SortBy

            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New Data.Odbc.OdbcDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, startRecord, Rows, "Folders")

            Return dbs

        Catch ex As Exception
            Log(ex.ToString, True)
            Throw New Exception(ex.ToString)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " .GetPropertyById() "
    Public Function GetPropertyById(ByVal Id As Integer, ByVal Key As String) As Object
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Dim res As Object = Nothing

        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM Folders WHERE ApplicationName=? AND id=" & Id
            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New Data.Odbc.OdbcDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Folders")


            If dbs.Tables("Folders").Rows.Count > 0 Then
                res = dbs.Tables("Folders").Rows(0).Item(Key)
            End If

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

        Catch ex As Exception
            Log(ex.ToString, True)
            Return Nothing
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return res
    End Function
#End Region

End Class
