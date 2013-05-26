Imports System.Data.Odbc

Public Class BlogManager

#Region " Odbc Structure "
    ' id                - id
    ' ApplicationName   - Varchar(255)
    ' XmlData           - Xml
#End Region

#Region " Odbc Connection "
    'Data Source=192.168.0.2;Initial Catalog=skitsanos.com;User Id=myUsername;Password=myPassword;"
#End Region

#Region " SortOrder "
    Private _SortOrder As String = "Title ASC"
    Public Property SortOrder() As String
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As String)
            _SortOrder = value
        End Set
    End Property
#End Region

#Region " Version "
    Public ReadOnly Property Version() As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
        End Get
    End Property
#End Region

#Region " UpdateHits "
    '- folowing could be removed?
    Public Sub UpdateHits(ByVal Id As Integer)
        Dim db As OdbcConnection = Nothing

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New OdbcCommand("UPDATE Blog SET XmlData.modify('replace value of (/Article/@hits)[1] with (/Article/@hits)[1]+1') WHERE ApplicationName=? AND XmlData.value('/Article[1]/@Id','nvarchar(max)')=?", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@id", Id)
            dbc.ExecuteNonQuery()

            dbc.Dispose()

        Catch ex As Exception
            Log("Pages.UpdateHits -- " & ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Sub

    Public Sub UpdateHits(ByVal Filename As String)
        Dim db As OdbcConnection = Nothing

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New OdbcCommand("UPDATE Blog SET XmlData.modify('replace value of (/Article/@hits)[1] with (/Page/@hits)[1]+1') WHERE ApplicationName=? AND XmlData.value('/Article[1]/@file','nvarchar(max)')=?", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Filename", Filename)
            dbc.ExecuteNonQuery()

            dbc.Dispose()

        Catch ex As Exception
            Log("Pages.UpdateHits -- " & ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Sub
#End Region

#Region " Lock Comments "
    Public Function LockComments(ByVal id As Integer) As Boolean
        Dim db As OdbcConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New OdbcCommand("exec BlogArticleLockComments ?, ?", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@ArticleId", id)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Blogs.LockComments -- " & ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return res
    End Function
#End Region

#Region " Unlock Comments "
    Public Function UnlockComments(ByVal id As Integer) As Boolean
        Dim db As OdbcConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New OdbcCommand("exec BlogArticleUnlockComments ?, ?", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@ArticleId", id)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Blogs.UnlockComments -- " & ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return res
    End Function
#End Region

#Region " Add "
    Public Function Add(ByVal obj As ArticleType) As Boolean
        obj.Hits = 0
        obj.CreatedOn = Now
        obj.UpdatedOn = Now

        obj.Filename = NormalizeFileName(obj.Title.ToLower) + ".aspx"

        Dim db As OdbcConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New OdbcCommand("exec CreateBlogArticle ?, ?", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@XmlData", GetXml(obj).DocumentElement.OuterXml)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Blogs.Add -- " & ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return res
    End Function
#End Region

#Region " Add {non-object}"
    Public Function Add(ByVal Title As String, ByVal Keywords As String, ByVal Content As String) As Boolean
        Dim res As Boolean = False

        Try
            Dim webPage As New ArticleType
            webPage.Filename = NormalizeFileName(webPage.Title.ToLower) + ".aspx"
            webPage.CreatedOn = Now
            webPage.UpdatedOn = Now
            webPage.Keywords = Keywords
            webPage.Title = Title
            webPage.Hits = 0
            webPage.Content = Content

            res = Add(webPage)

        Catch ex As Exception
            Log("Blogs.Add -- " & ex.ToString, True)
        End Try
        Return res
    End Function
#End Region

#Region " Update {non-object} "
    Public Function Update(ByVal id As Integer, ByVal Title As String, ByVal Keywords As String, ByVal Content As String) As Boolean
        Dim res As Boolean = False
        Dim db As OdbcConnection = Nothing

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim filename As String = NormalizeFileName(Title.ToLower) + ".aspx"

            Dim dbc As New OdbcCommand("EXEC UpdateBlogArticle ?, ?, ?, ?, ?, ?, ?" & vbCrLf, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", id)
            dbc.Parameters.AddWithValue("@Filename", filename)
            dbc.Parameters.AddWithValue("@Title", Title)
            dbc.Parameters.AddWithValue("@Content", Content)
            dbc.Parameters.AddWithValue("@Keywords", Keywords)
            dbc.Parameters.AddWithValue("@UpdatedOn", Now.ToString("s"))

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Blog.Update -- " & ex.ToString, True)
        End Try

        Return res
    End Function
#End Region

#Region " .Delete() "
    Public Function Delete(ByVal Id As String) As Boolean
        Dim db As OdbcConnection = Nothing
        Dim res As Boolean = False
        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim dbc As New OdbcCommand("DELETE FROM Blog WHERE ApplicationName=? AND id=?", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@id", Id)
            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Blog.Delete -- " & ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
        Return res
    End Function
#End Region

#Region " GetDatasource() "
    Public Function GetDatasource(Optional ByVal Criteria As String = "") As DataSet
        Dim db As OdbcConnection = Nothing
        Dim dbs As DataSet = Nothing

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim strSql As String = "SELECT * FROM viewBlog WHERE ApplicationName=? " & Criteria & " ORDER BY " & SortOrder

            Dim dbc As New OdbcCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            dbs = New DataSet("Blog")
            dbs.Clear()

            Dim dba As New OdbcDataAdapter
            dba.SelectCommand = dbc

            dba.Fill(dbs)

        Catch ex As Exception
            Log("Blog.GetDatasource -- " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return dbs
    End Function
#End Region

#Region " GetDataset(Paged) "
    Public Function GetDatasource(ByVal startRecord As Integer, ByVal maxRecords As Integer, Optional ByVal Criteria As String = "") As DataSet
        Dim db As OdbcConnection = Nothing
        Dim dbs As DataSet = Nothing

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim strSql As String = "SELECT * FROM viewBlog WHERE ApplicationName=? " & Criteria & " ORDER BY " & SortOrder

            Dim dbc As New OdbcCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            dbs = New DataSet("Pages")
            dbs.Clear()

            Dim dba As New OdbcDataAdapter
            dba.SelectCommand = dbc

            dba.Fill(dbs, startRecord, maxRecords, "viewPages")

        Catch ex As Exception
            Log("Blog.GetDatasource -- " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return dbs
    End Function
#End Region

#Region " GetArticle "
    Public Function GetArticle(ByVal id As Integer) As ArticleType
        Dim db As OdbcConnection = Nothing
        Dim dbs As DataSet = Nothing
        Dim pg As New ArticleType

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim strOdbc As String = "EXEC GetBlogArticleById ?, ?"

            Dim dbc As New OdbcCommand(strOdbc, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", id)

            Dim reader As OdbcDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows = False Then
                pg.Filename = id
                pg.Title = "Page Not Found"
                pg.CreatedOn = Now
                pg.UpdatedOn = Now
                pg.Hits = 0
                pg.Content = "Requested page not found"
            Else
                pg = SetXml(GetType(ArticleType), reader("XmlData"))
            End If

        Catch ex As Exception
            Log("[Wdk.Pages][GetPage] Error: " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return pg
    End Function

    Public Function GetArticle(ByVal Filename As String) As ArticleType
        Dim db As OdbcConnection = Nothing
        Dim dbs As DataSet = Nothing
        Dim pg As New ArticleType

        Try
            db = New OdbcConnection(ConnectionString)
            db.Open()

            Dim strOdbc As String = "EXEC GetPageByFilename ?, ?"

            Dim dbc As New OdbcCommand(strOdbc, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", Filename)

            Dim reader As OdbcDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows = False Then
                pg.Filename = Filename
                pg.Title = "Page Not Found"
                pg.CreatedOn = Now
                pg.UpdatedOn = Now
                pg.Hits = 0
                pg.Content = "Requested page {" & Filename & "} not found"
            Else
                pg = SetXml(GetType(ArticleType), reader("XmlData"))
            End If

        Catch ex As Exception
            Log("[Wdk.Blog][GetBlogArtilce] Error: " & ex.ToString & vbCrLf & " Page Requested: " & Filename, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return pg
    End Function
#End Region

End Class
