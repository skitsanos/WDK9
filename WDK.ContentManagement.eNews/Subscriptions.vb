Imports System.Data.Odbc
Imports System.Xml

Namespace Subscriptions

    Public Class Manager

#Region " Properties "
        Private _SortOrder As String = "id DESC"
        Public Property SortOrder() As String
            Get
                Return _SortOrder
            End Get
            Set(ByVal Value As String)
                _SortOrder = Value
            End Set
        End Property
#End Region

#Region " IsSubscribed "
        Public Function IsSubscribed(ByVal Email As String, ByVal DistributionListId As Integer) As Boolean
            Email = NormalizeEmail(Email)

            Dim db As OdbcConnection = Nothing
            Try
                db = New OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM Subscriptions WHERE Email=? AND List=" & DistributionListId
                Dim dbc As New OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@Email", Email)

                Dim dba As New OdbcDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "Subscriptions")

                Dim res As Boolean = False
                If dbs.Tables("Subscriptions").Rows.Count > 0 Then
                    res = True
                End If

                dbs.Dispose()
                dba.Dispose()
                dbc.Dispose()

                Return res
            Catch ex As Exception
                Log(ex.ToString, True)
                Return False
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        End Function
#End Region

#Region " Subscribe() "
        Public Function Subscribe(ByVal Email As String, ByVal DistributionListId As Integer) As Boolean
            If IsSubscribed(Email, DistributionListId) Then
                Return False
            End If

            Email = NormalizeEmail(Email)

            Dim db As OdbcConnection = Nothing
            Try
                db = New OdbcConnection(ConnectionString)
                db.Open()

                Dim dbc As New OdbcCommand("INSERT INTO Subscriptions (ApplicationName, Email, List, CreatedOn) VALUES (?, ?, ?, ?)", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Email", Email)
                dbc.Parameters.AddWithValue("@List", DistributionListId)
                dbc.Parameters.AddWithValue("@CreatedOn", Now)

                dbc.ExecuteNonQuery()

                dbc.Dispose()

                Return True

            Catch ex As Exception
                Log(ex.ToString, True)
                Return False
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        End Function
#End Region

#Region " .Unsubscribe() "
        Public Function Unsubscribe(ByVal DistributionList As Integer, ByVal Email As String) As Boolean
            Email = NormalizeEmail(Email)
            Dim db As OdbcConnection = Nothing
            Try
                db = New OdbcConnection(ConnectionString)
                db.Open()

                Dim dbc As New OdbcCommand("DELETE FROM Subscriptions WHERE Email=? AND List=" & DistributionList, db)
                dbc.Parameters.AddWithValue("@Email", Email)

                dbc.ExecuteNonQuery()
                dbc.Dispose()

                Return True

            Catch ex As Exception
                Log(ex.ToString, True)
                Return False
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        End Function
#End Region

#Region " .FindSubscription() "
        Public Function FindSubscription(ByVal DistributionList As Integer, ByVal Email As String) As Integer
            Dim db As OdbcConnection = Nothing
            Try
                db = New OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM Subscriptions WHERE List=? AND Email=?"
                Dim dbc As New OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@List", DistributionList)
                dbc.Parameters.AddWithValue("@Email", Email)

                Dim dba As New OdbcDataAdapter(dbc)

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "Subscriptions")

                Dim res As Integer = 0
                If dbs.Tables("Subscriptions").Rows.Count > 0 Then
                    res = dbs.Tables("Subscriptions").Rows(0).Item("id")
                End If

                Return res

            Catch ex As Exception
                Log(ex.ToString, True)
                Return 0
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        End Function
#End Region

#Region " FindAllSubscriptions "
        Public Function FindAllSubscriptions(ByVal Email As String) As DataSet
            Dim db As OdbcConnection = Nothing

            Try
                db = New OdbcConnection(ConnectionString)
                db.Open()

                Dim dbc As New OdbcCommand("SELECT Subscriptions.id As id, DistributionLists.Title As Title, DistributionLists.Description As Description FROM Subscriptions, DistributionLists WHERE Subscriptions.List=DistributionLists.id AND Subscriptions.Email=? AND Subscriptions.ApplicationName=?", db)
                dbc.Parameters.AddWithValue("@Email", Email)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                Dim dba As New OdbcDataAdapter(dbc)
                Dim res As New DataSet
                dba.Fill(res, "Subscriptions")

                Return res

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

#Region " GetDataset() "
        Public Function GetDataset(Optional ByVal Criteria As String = "") As System.Data.DataSet
            Dim db As OdbcConnection = Nothing
            Try
                db = New OdbcConnection(ConnectionString)
                db.Open()

                If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

                Dim dbc As New OdbcCommand("SELECT * FROM viewSubscriptions WHERE ApplicationName=? " & Criteria & " ORDER BY " & SortOrder, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                Dim dba As New OdbcDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "Subscriptions")

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

#Region " GetRecordAsXml "
        Public Function GetRecordAsXml(ByVal id As Integer) As XmlDocument
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Dim ret As XmlDocument = Nothing

            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM viewSubscriptions WHERE ApplicationName=? AND id=? for xml raw"
                Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@id", id)

                Dim reader As Data.Odbc.OdbcDataReader = dbc.ExecuteReader(Data.CommandBehavior.SingleRow)
                If reader.HasRows Then
                    ret = New XmlDocument
                    ret.LoadXml(reader(0).ToString)
                End If

            Catch ex As Exception
                Log(ex.ToString, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " NormalizeEmail "
        Private Function NormalizeEmail(ByVal Email As String) As String
            Email = Trim(Email)
            Email = Email.ToLower
            Email = Email.Replace(vbCrLf, "")
            Email = Email.Replace(vbCr, "")
            Email = Email.Replace(vbLf, "")
            Return Email
        End Function
#End Region

    End Class
End Namespace