Imports System.Xml
Imports System.Data.SqlClient

Namespace Files

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
        Public Function Add(ByVal Folder As Integer, ByVal Filename As String, ByVal Description As String, ByVal ContentType As String, ByVal FileContent As Byte()) As Boolean
            Dim ret As Boolean = True
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("INSERT INTO ImageGalleryItems (ApplicationName, ImageGalleryFolder, Filename, Description, ContentType, FileContent, CreatedOn, Orientation) VALUES (@ApplicationName, @ImageGalleryFolder, @Filename, @Description, @ContentType, @FileContent, @CreatedOn, @Orientation)", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@ImageGalleryFolder", Folder)
                dbc.Parameters.AddWithValue("@Filename", Filename)
                dbc.Parameters.AddWithValue("@Description", Description)
                dbc.Parameters.AddWithValue("@ContentType", ContentType)
                dbc.Parameters.AddWithValue("@FileContent", FileContent)
                dbc.Parameters.AddWithValue("@CreatedOn", Now)
                dbc.Parameters.AddWithValue("@Orientation", 0)

                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("ImageGalleryItems.Add: " & ex.ToString, True)
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
        Public Function IsExists(ByVal filename As String) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim dbc As New SqlCommand("SELECT * FROM ImageGalleryItems WHERE ApplicationName=@ApplicationName AND Filename=@Filename", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@Filename", filename)

                Dim dba As New SqlDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "ImageGalleryItems")

                If dbs.Tables("ImageGalleryItems").Rows.Count > 0 Then ret = True

            Catch ex As Exception
                Log("ImageGalleryItems.IsExists: " & ex.ToString, True)
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
        Public Function Update(ByVal Id As Integer, ByVal Folder As Integer, ByVal Description As String, ByVal ContentType As String, ByVal FileContent As Byte()) As Boolean
            Dim ret As Boolean = False
            Dim db As SqlConnection = Nothing
            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = ""
                If IsNothing(FileContent) = False Then
                    strSQL += "UPDATE ImageGalleryItems SET ImageGalleryFolder=@ImageGalleryFolder, Description=@Description, ContentType=@ContentType, FileContent=@FileContent WHERE ApplicationName=@ApplicationName AND id=" & Id
                Else
                    strSQL += "UPDATE ImageGalleryItems SET ImageGalleryFolder=@ImageGalleryFolder, Description=@ WHERE ApplicationName=@ApplicationName AND id=" & Id
                End If

                Dim dbc As New SqlCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ImageGalleryFolder", Folder)
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
                Log("ImageGalleryItems.Update: " & ex.ToString, True)
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

                Dim dbc As New SqlCommand("DELETE FROM ImageGalleryItems WHERE ApplicationName=@ApplicationName AND id=" & Id, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log("ImageGalleryItems.Delete: " & ex.Message, True)
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

                Dim strSQL As String = "SELECT * FROM viewImageGalleryItems WHERE ApplicationName=@ApplicationName " & Criteria & " ORDER BY " & SortBy
                Dim dbc As New SqlCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                Dim dba As New SqlDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "ImageGalleryItems")

                res = dbs

            Catch ex As Exception
                Log("ImageGalleryItems.GetDataset: " & ex.ToString, True)
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

                Dim dbc As New SqlCommand("SELECT * FROM viewImageGalleryItems WHERE ApplicationName=@ApplicationName AND id=@id for xml raw", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@id", id)

                Dim reader As SqlDataReader = dbc.ExecuteReader(Data.CommandBehavior.SingleRow)
                reader.Read()

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

#Region " GetItem "
        Public Function GetItem(ByVal id As Integer) As FileItemType
            Dim db As SqlConnection = Nothing
            Dim ret As FileItemType = Nothing

            Try
                db = New SqlConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM ImageGalleryItems WHERE ApplicationName=@ApplicationName AND id=@id"
                Dim dbc As New SqlCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@id", id)

                Dim reader As SqlDataReader = dbc.ExecuteReader(Data.CommandBehavior.SingleRow)
                reader.Read()

                If reader.HasRows Then
                    ret = New FileItemType
                    ret.Filename = reader("Filename")
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