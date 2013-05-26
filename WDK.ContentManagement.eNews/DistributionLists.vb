Imports System.Data.Odbc
Imports System.Xml

Namespace DistributionLists
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

        Public Property Template(ByVal Id As Integer) As String
            Get
                Return GetTemplate(Id)
            End Get
            Set(ByVal value As String)
                SetTemplate(Id, value)
            End Set
        End Property
#End Region

#Region " GetTemplate() "
        Private Function GetTemplate(ByVal Id As Integer) As String
            Return GetRecordAsXml(Id).DocumentElement.GetAttribute("Template")
        End Function
#End Region

#Region " SetTemplate "
        Private Sub SetTemplate(ByVal Id As Integer, ByVal Data As String)
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "UPDATE DistributionLists SET Template=? WHERE ApplicationName=? AND id=" & Id

                Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@Template", Data)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                dbc.ExecuteNonQuery()

                dbc.Dispose()

            Catch ex As Exception
                Log(ex.ToString, True)
            Finally
                If db IsNot Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        End Sub
#End Region

#Region " .Add() "
        Public Function Add(ByVal Title As String, ByVal Description As String) As Boolean
            If Title = "" Then
                Log("Distribution list should have a Title.", True)
                Return False
            End If

            If Exists(Title) Then
                Log("Distribution list titled {" & Title & "} already exists.", True)
                Return False
            End If

            Dim ret As Boolean = False

            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim dbc As New Data.Odbc.OdbcCommand("INSERT INTO DistributionLists (Title, Description, ApplicationName, Template) VALUES (?,?,?,?)", db)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@Description", Description)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Template", "%%content%%")

                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log(ex.Message, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " .Update() "
        Public Function Update(ByVal Id As Integer, ByVal Title As String, ByVal Description As String) As Boolean
            If Title = "" Then
                Log("Distribution list should have a Title.", True)
                Return False
            End If

            Dim ret As Boolean = False

            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "UPDATE DistributionLists SET Title=?, Description=? WHERE ApplicationName=? AND id=" & Id

                Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@Description", Description)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log(ex.Message, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " .Exists() "
        Public Function Exists(ByVal Title As String) As Boolean
            Dim res As Boolean = False
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim dbc As New Data.Odbc.OdbcCommand("SELECT * FROM DistributionLists WHERE ApplicationName=? AND Title=?", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Title", Title)

                Dim dba As New Data.Odbc.OdbcDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "DistributionLists")


                If dbs.Tables("DistributionLists").Rows.Count > 0 Then res = True
       
                dbs.Dispose()
                dba.Dispose()
                dbc.Dispose()

            Catch ex As Exception
                Log(ex.Message, True)
                Throw New Exception(ex.Message)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return res
        End Function
#End Region

#Region " FindIdByTitle "
        Public Function FindIdByTitle(ByVal Title As String) As Integer
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Dim res As Integer = 0

            Try
                db = New OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM DistributionLists WHERE ApplicationName=? AND Title=?"
                Dim dbc As New OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Title", Title)

                Dim reader As OdbcDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
                If reader.HasRows Then res = reader("id")

                dbc.Dispose()

            Catch ex As Exception
                Log("[enews.dll][DistributionLists.FindIdByTitle] " & ex.Message, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return res
        End Function
#End Region

#Region " GetDataset() "
        Public Function GetDataset(Optional ByVal Criteria As String = "") As System.Data.DataSet
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

                Dim strSQL As String = "SELECT * FROM DistributionLists WHERE ApplicationName=? " & Criteria & " ORDER BY " & SortOrder
                Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                Dim dba As New Data.Odbc.OdbcDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "DistributionLists")

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

#Region " .Delete() "
        Public Function Delete(ByVal Id As Integer) As Boolean
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Dim res As Boolean = False

            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim Title As String = GetRecordAsXml(Id).DocumentElement.GetAttribute("Title")

                Dim dbc As New Data.Odbc.OdbcCommand("DELETE FROM DistributionLists WHERE ApplicationName=? AND id=" & Id, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.ExecuteNonQuery()
                dbc.Dispose()

                res = True

            Catch ex As Exception
                Log(ex.Message, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
            Return res
        End Function
#End Region

#Region " GetRecordAsXml "
        Public Function GetRecordAsXml(ByVal id As Integer) As XmlDocument
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Dim ret As XmlDocument = Nothing

            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM DistributionLists WHERE ApplicationName=? AND id=? for xml raw"
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

    End Class
End Namespace


