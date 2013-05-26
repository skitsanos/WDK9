Imports System.Data.SqlClient

#Region " SQL Post Comment "
'CREATE PROCEDURE PostPageComment 
'	-- Add the parameters for the stored procedure here
'	@ApplicationName varchar(255) = NULL, 
'	@PageId int,
'	@XmlData xml = '<Comment />'
'AS
'BEGIN

'	INSERT INTO PageComments (ApplicationName, PageId, XmlData) VALUES (@ApplicationName, @PageId, @XmlData)
'END
#End Region

Public Class CommentsManager

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

#Region " Sql Connection "
    'Data Source=192.168.0.2;Initial Catalog=skitsanos.com;User Id=myUsername;Password=myPassword;"
#End Region

#Region " Version "
    Public ReadOnly Property Version() As String
        Get
            Return System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString
        End Get
    End Property
#End Region

#Region " Add "
    Public Function Add(ByVal PageId As Integer, ByVal comment As PageCommentType) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("exec PostPageComment @ApplicationName, @PageId, @XmlData", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@PageId", PageId)
            dbc.Parameters.AddWithValue("@XmlData", WDK.XML.Utils.Serializer.ObjectToXmlString(comment))

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Pages.Add -- " & ex.ToString, True)
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
    Public Function Add(ByVal PageId As Integer, ByVal author As String, ByVal message As String) As Boolean
        Dim res As Boolean = False

        Try
            Dim comment As New PageCommentType
            comment.Author = author
            comment.Message = message
            res = Add(PageId, comment)

        Catch ex As Exception
            Log("Pages.Add -- " & ex.ToString, True)
        End Try
        Return res
    End Function
#End Region

#Region " Update {non-object} "
    Private Function Update(ByVal id As Integer, ByVal Filename As String, ByVal Title As String, ByVal Keywords As String, ByVal Description As String, ByVal Content As String) As Boolean
        Dim res As Boolean = False
        Dim db As SqlConnection = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("EXEC UpdatePage @ApplicationName, @Id, @Filename, @Title, @Keywords, @Description, @Content" & vbCrLf, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", id)
            dbc.Parameters.AddWithValue("@Filename", Filename)
            dbc.Parameters.AddWithValue("@Title", Title)
            dbc.Parameters.AddWithValue("@Keywords", Keywords)
            dbc.Parameters.AddWithValue("@Description", Description)
            dbc.Parameters.AddWithValue("@Content", Content)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Pages.Update -- " & ex.ToString, True)
        End Try

        Return res
    End Function
#End Region

#Region " .Delete() "
    Public Function Delete(ByVal Id As String) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("DELETE FROM PageComments WHERE ApplicationName=@ApplicationName AND id=@id", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@id", Id)
            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Pages.Delete -- " & ex.ToString, True)
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
    Public Function GetDatasource(Optional ByVal Criteria As String = "") As DataSet
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim strSql As String = "SELECT * FROM viewPages WHERE ApplicationName=@ApplicationName " & Criteria & " ORDER BY " & SortOrder

            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            dbs = New DataSet("Pages")
            dbs.Clear()

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            dba.Fill(dbs)

        Catch ex As Exception
            Log("Pages.GetDatasource -- " & ex.ToString, True)
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
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim strSql As String = "SELECT * FROM viewPages WHERE ApplicationName=@ApplicationName " & Criteria & " ORDER BY " & SortOrder

            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            dbs = New DataSet("Pages")
            dbs.Clear()

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            dba.Fill(dbs, startRecord, maxRecords, "viewPages")

        Catch ex As Exception
            Log("Pages.GetDatasource -- " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return dbs
    End Function
#End Region

#Region " GetPage "
    Public Function GetPage(ByVal id As Integer) As PageType
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing
        Dim pg As New PageType

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSql As String = "EXEC GetPageById @ApplicationName, @Id"

            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", id)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows = False Then
                pg.Filename = id
                pg.Title = "Page Not Found"
                pg.CreatedOn = Now
                pg.UpdatedOn = Now
                pg.Hits = 0
                pg.Content = "Requested page not found"
            Else
                pg.Filename = reader("Filename")
                pg.Title = reader("Title")
                pg.Metas.Keywords = reader("Keywords")
                pg.Metas.Description = reader("Description")
                pg.Content = reader("PageContent")
                pg.CreatedOn = DateTime.Parse(reader("CreatedOn"))
                pg.UpdatedOn = DateTime.Parse(reader("UpdatedOn"))
                pg.Hits = reader("Hits")
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

    Public Function GetPage(ByVal Filename As String) As PageType
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing
        Dim pg As New PageType

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSql As String = "EXEC GetPageByFilename @ApplicationName, @Id"

            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", Filename)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows = False Then
                pg.Filename = Filename
                pg.Title = "Page Not Found"
                pg.CreatedOn = Now
                pg.UpdatedOn = Now
                pg.Hits = 0
                pg.Content = "Requested page {" & Filename & "} not found"
            Else
                pg.Filename = reader("Filename")
                pg.Title = reader("Title")
                pg.Metas.Keywords = reader("Keywords")
                pg.Metas.Description = reader("Description")
                pg.Content = reader("PageContent")
                pg.CreatedOn = DateTime.Parse(reader("CreatedOn"))
                pg.UpdatedOn = DateTime.Parse(reader("UpdatedOn"))
                pg.Hits = reader("Hits")
            End If

        Catch ex As Exception
            Log("[Wdk.Pages][GetPage] Error: " & ex.ToString & vbCrLf & " Page Requested: " & Filename, True)
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
