Imports System.Xml
Imports System.Data.SqlClient

Namespace PdfFolders
    Public Class Manager

#Region " Properties "
        Private _SortOrder As String = "Title ASC"
        Public Property SortBy() As String
            Get
                Return _SortOrder
            End Get
            Set(ByVal Value As String)
                _SortOrder = Value
            End Set
        End Property
#End Region

#Region " Add() "
        Public Function Add(ByVal Title As String, ByVal Description As String) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("INSERT INTO PdfFolders (ApplicationName, Title, Description) VALUES (@ApplicationName, @Title, @Description)", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@Description", Description)

                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("PdfFolders.Add: " & ex.ToString, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " Update() "
        Public Function Update(ByVal Id As Integer, ByVal Title As String, ByVal Description As String) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("UPDATE PdfFolders SET Title=@Title, Description=@Description WHERE ApplicationName=@ApplicationName AND id=" & Id, db)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@Description", Description)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("PdfFolders.Update: " & ex.ToString, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " .Delete() "
        Public Function Delete(ByVal Id As Integer) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("DELETE FROM PdfFolders WHERE ApplicationName=@ApplicationName AND id=" & Id, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("PdfFolders.Delete: " & ex.ToString, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " IsExists() "
        Public Function IsExists(ByVal Title As String) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("SELECT * FROM PdfFolders WHERE ApplicationName=@ApplicationName AND Title=@Title", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Title", Title)

                Dim dba As New SqlDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "PdfFolders")

                If dbs.Tables("PdfFolders").Rows.Count > 0 Then ret = True

            Catch ex As Exception
                Log("PdfFolders.IsExists: " & ex.ToString, True)
            Finally
                If db IsNot Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " GetDatasource() "
        Public Function GetDatasource(Optional ByVal Criteria As String = "") As System.Data.DataSet
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

                Dim strSQL As String = "SELECT * FROM PdfFolders WHERE ApplicationName=@ApplicationName " & Criteria & " ORDER BY " & SortBy
                Dim dbc As New SqlCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                Dim dba As New SqlDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "PdfFolders")

                Return dbs

            Catch ex As Exception
                Log("PdfFolders.GetDataset: " & ex.ToString, True)
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
            Dim db As SqlConnection = Nothing
            Dim ret As XmlDocument = Nothing

            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("SELECT * FROM PdfFolders WHERE ApplicationName=@ApplicationName AND id=@id for xml raw", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@id", id)

                Dim reader As SqlDataReader = dbc.ExecuteReader(Data.CommandBehavior.SingleRow)
                If reader.HasRows Then
                    reader.Read()
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

