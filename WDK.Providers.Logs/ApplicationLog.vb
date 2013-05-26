Imports System.Configuration
Imports System.Web.Configuration
Imports System.Data.SqlClient


Public Class ApplicationLog

#Region " getDatasource() "
	Public Function getDatasource(Optional ByVal query As String = "") As List(Of LogEntry)
		Dim ret As New List(Of LogEntry)
        Dim db As SqlConnection = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim criteria As String = ""
            Dim cb As New WDK.Utilities.SQL.CriteriaBuilder
            If query <> "" Then
                cb.Add("notes", "Notes", WDK.Utilities.SQL.CriteriaBuilder.CriteriaOperation.LIKE, query)
                criteria = cb.ToString
            End If

            If criteria <> "" Then criteria = " AND  (" & criteria & ")"

            Dim dbc As New SqlCommand("SELECT * FROM Logs WHERE ApplicationName=@ApplicationName " + criteria + " ORDER BY id DESC", db)
            dbc.Parameters.AddWithValue("@ApplicationName", getApplicationName)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim ds As New DataSet
            ds.Clear()
            dba.Fill(ds, "Logs")

            Dim dt As Data.DataTable = ds.Tables(0)

            For Each dbr As Data.DataRow In dt.Rows
                Dim entry As New LogEntry
                entry.uid = dbr("id")
                entry.createdOn = dbr("createdOn")
                entry.content = dbr("notes")
                entry.isError = dbr("status")

                ret.Add(entry)
            Next

            ds.Dispose()
            dba.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return ret
    End Function
#End Region

#Region " Write() "
    Public Sub write(ByVal Data As String, Optional ByVal IsError As Boolean = False)
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("INSERT INTO Logs (ApplicationName, Notes, Status, CreatedOn) VALUES (@ApplicationName, @Notes, @Status, @CreatedOn)", db)
            dbc.Parameters.AddWithValue("@ApplicationName", getApplicationName)
            dbc.Parameters.AddWithValue("@Notes", Data)
            dbc.Parameters.AddWithValue("@Status", IsError)
            dbc.Parameters.AddWithValue("@CreatedOn", Now)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

        Catch ex As Exception
            Throw New Exception(ex.ToString)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Sub
#End Region

#Region " remove() "
    Public Function remove(ByVal Id As Integer) As Boolean
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSql As String = "DELETE FROM Logs WHERE ApplicationName=@ApplicationName AND id=" & Id
            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", getApplicationName)
            dbc.ExecuteNonQuery()
            dbc.Dispose()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " removeAll() "
    Public Function removeAll() As Boolean
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSql As String = "DELETE FROM Logs WHERE ApplicationName=@ApplicationName"

            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", getApplicationName)

            dbc.ExecuteNonQuery()
            dbc.Dispose()

            Return True

        Catch ex As Exception
            Throw ex
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

End Class