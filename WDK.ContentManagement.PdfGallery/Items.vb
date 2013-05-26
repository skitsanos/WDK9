Imports System.Xml
Imports System.Data.SqlClient

Namespace PdfItems

    Public Class Manager

#Region " Comments "
        '- Orientation: [sql::int] 0 - Landscape, 1 - Portrait, 2 - Square
#End Region

#Region " Properties "
        Private _SortOrder As String = "id DESC"
        Public Property SortBy() As String
            Get
                Return _SortOrder
            End Get
            Set(ByVal Value As String)
                _SortOrder = Value
            End Set
        End Property
#End Region

#Region " .Add() "
        Public Function Add(ByVal Folder As Integer, ByVal Filename As String, ByVal Title As String, ByVal Description As String, ByVal ContentType As String, ByVal FileContent As Byte()) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("INSERT INTO PdfItems (ApplicationName, Folder, Filename, Title, Description, ContentType, FileContent, CreatedOn) VALUES (@ApplicationName, @Folder, @Filename, @Title, @Description, @ContentType, @FileContent, @CreatedOn)", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Folder", Folder)
                dbc.Parameters.AddWithValue("@Filename", Filename)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@Description", Description)
                dbc.Parameters.AddWithValue("@ContentType", ContentType)
                dbc.Parameters.AddWithValue("@FileContent", FileContent)
                dbc.Parameters.AddWithValue("@CreatedOn", Now)

                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("PdfItems.Add: " & ex.ToString, True)
            Finally
                If db IsNot Nothing Then
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

                Dim dbc As New SqlCommand("SELECT * FROM PdfItems WHERE ApplicationName=@ApplicationName AND Title=@Title", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Title", Title)

                Dim dba As New SqlDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "PdfItems")

                If dbs.Tables("PdfItems").Rows.Count > 0 Then ret = True

            Catch ex As Exception
                Log("PdfItems.IsExists: " & ex.ToString, True)
            Finally
                If db IsNot Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " Update() "
        Public Function Update(ByVal Id As Integer, ByVal Folder As Integer, ByVal Title As String, ByVal Description As String, ByVal ContentType As String, ByVal FileContent As Byte()) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = ""
                If IsNothing(FileContent) = False Then
                    strSQL += "UPDATE PdfItems SET Folder=@Folder, Title=@Title, Description=@Description, ContentType, FileContent=@FileContent WHERE ApplicationName=@ApplicationName AND id=" & Id
                Else
                    strSQL += "UPDATE PdfItems SET Folder=@Folder, Title=@Title, Description=@Description WHERE ApplicationName=@ApplicationName AND id=" & Id
                End If

                Dim dbc As New SqlCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@Folder", Folder)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@Description", Description)

                If IsNothing(FileContent) = False Then
                    dbc.Parameters.AddWithValue("@ContentType", ContentType)
                    dbc.Parameters.AddWithValue("@FileContent", FileContent)
                End If

                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                dbc.ExecuteNonQuery()
                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("PdfItems.Update: " & ex.ToString, True)
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
            Dim db As SqlConnection = Nothing
            Dim ret As Boolean = False

            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("DELETE FROM PdfItems WHERE ApplicationName=@ApplicationName AND id=" & Id, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("PdfItems.Delete: " & ex.Message, True)
            Finally
                If Not db Is Nothing Then
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
            Dim res As Data.DataSet = Nothing

            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

                Dim dbc As New SqlCommand("SELECT * FROM viewPdfItems WHERE ApplicationName=@ApplicationName " & Criteria & " ORDER BY " & SortBy, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                Dim dba As New SqlDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "PdfItems")

                res = dbs

            Catch ex As Exception
                Log("PdfItems.GetDataset: " & ex.ToString, True)
            Finally
                If db IsNot Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return res
        End Function
#End Region

#Region " GetRecordAsXml "
        Public Function GetRecordAsXml(ByVal id As Integer) As XmlDocument
            Dim db As SqlConnection = Nothing
            Dim ret As XmlDocument = Nothing

            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM viewPdfItems WHERE ApplicationName=@ApplicationName AND id=@id for xml raw"
                Dim dbc As New SqlCommand(strSQL, db)
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

#Region " GetItem "
        Public Function GetItem(ByVal id As Integer) As FileItemType
            Dim db As SqlConnection = Nothing
            Dim ret As FileItemType = Nothing

            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM PdfItems WHERE ApplicationName=@ApplicationName AND id=@id"
                Dim dbc As New SqlCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@id", id)

                Dim reader As SqlDataReader = dbc.ExecuteReader(Data.CommandBehavior.SingleRow)
                If reader.HasRows Then
                    reader.Read()
                    ret = New FileItemType
                    ret.ContentType = reader("ContentType")
                    ret.Content = reader("FileContent")
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