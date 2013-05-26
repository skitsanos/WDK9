Public Class Manager

#Region " Properties "
    Private _SortBy As String = "Title"
    Public Property Sorting() As String
        Get
            Return _SortBy
        End Get
        Set(ByVal Value As String)
            _SortBy = Value
        End Set
    End Property

    Private _ApplicationName As String = GetApplicationName()
    Public Property ApplicationName() As String
        Get
            Return _ApplicationName
        End Get
        Set(ByVal value As String)
            _ApplicationName = value
        End Set
    End Property

#End Region

#Region " .Add() "
    ''' <summary>
    ''' Inserts new scheduled event into database
    ''' </summary>
    ''' <param name="Title"></param>
    ''' <param name="Description"></param>
    ''' <param name="StartsOn"></param>
    ''' <param name="EndsOn"></param>
    ''' <param name="Username"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Add(ByVal Title As String, ByVal Description As String, ByVal StartsOn As DateTime, ByVal EndsOn As DateTime, Optional ByVal Username As String = "") As Boolean
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = Nothing
            strSQL += "INSERT INTO Scheduler (ApplicationName, Title, Description, StartsOn, EndsOn, AccountUsername)"
            strSQL += "VALUES (?,?,?,?,?,?)"

            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            dbc.Parameters.AddWithValue("@Title", Title)
            dbc.Parameters.AddWithValue("@Description", Description)
            dbc.Parameters.AddWithValue("@StartsOn", StartsOn)
            dbc.Parameters.AddWithValue("@EndsOn", EndsOn)
            dbc.Parameters.AddWithValue("@AccountUsername", Username)
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
    ''' <summary>
    ''' Updates selected event details
    ''' </summary>
    ''' <param name="Id"></param>
    ''' <param name="Title"></param>
    ''' <param name="Description"></param>
    ''' <param name="StartsOn"></param>
    ''' <param name="EndsOn"></param>
    ''' <param name="Username"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Update(ByVal Id As Integer, ByVal Title As String, ByVal Description As String, ByVal StartsOn As DateTime, ByVal EndsOn As DateTime, Optional ByVal Username As String = "") As Boolean
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = Nothing
            strSQL += "UPDATE Scheduler SET Title=?, Description=?, StartsOn=?, EndsOn=?, AccountUsername=? WHERE ApplicationName=? AND id=" & Id

            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@Title", Title)
            dbc.Parameters.AddWithValue("@Description", Description)
            dbc.Parameters.AddWithValue("@StartsOn", StartsOn)
            dbc.Parameters.AddWithValue("@EndsOn", EndsOn)
            dbc.Parameters.AddWithValue("@AccountUsername", Username)
            dbc.Parameters.AddWithValue("@ApplicationName", ApplicationName)

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
    ''' <summary>
    ''' Deletes scheduled event from the database
    ''' </summary>
    ''' <param name="Id">Event Id</param>
    ''' <returns>Boolean if there is no error occured</returns>
    ''' <remarks></remarks>
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New Data.Odbc.OdbcCommand
            dbc.CommandText = "DELETE FROM Scheduler WHERE id=" & Id
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
    Public Function GetDatasource(Optional ByVal Criteria As String = "") As System.Data.DataSet
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

			Dim strSQL As String = "SELECT * FROM Scheduler WHERE ApplicationName=? " & Criteria & " ORDER BY " & Sorting
            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", ApplicationName)

            Dim dba As New Data.Odbc.OdbcDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet()
            dbs.Clear()
            dba.Fill(dbs, "Scheduler")

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
#End Region

#Region " .GetEvent() "
    Public Function GetEvent(ByVal Id As Integer) As ScheduledEvent
        Dim db As Data.Odbc.OdbcConnection = Nothing
        Dim thisEvent As ScheduledEvent = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM Scheduler WHERE id=" & Id
            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)

            Dim dba As New Data.Odbc.OdbcDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Scheduler")

            If dbs.Tables(0).Rows.Count > 0 Then
                thisEvent = New ScheduledEvent
                thisEvent.Title = dbs.Tables(0).Rows(0).Item("Title")
                thisEvent.Description = dbs.Tables(0).Rows(0).Item("Description")
                thisEvent.StartsOn = dbs.Tables(0).Rows(0).Item("StartsOn")
                thisEvent.EndsOn = dbs.Tables(0).Rows(0).Item("EndsOn")
                thisEvent.Username = dbs.Tables(0).Rows(0).Item("AccountUsername")
            End If

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return thisEvent
    End Function
#End Region

#Region " Count() "
    Public Function Count() As Integer
        Dim cnt As Integer = 0

        Dim db As Data.Odbc.OdbcConnection = Nothing
        Try
            db = New Data.Odbc.OdbcConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM Events WHERE ApplicationName=?"
            Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            cnt = CInt(dbc.ExecuteScalar())
            dbc.Dispose()

            dbc.Dispose()

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return cnt
    End Function
#End Region

End Class
