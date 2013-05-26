Imports System.Data.SqlClient

Public Class Manager

    'sql drivers:
    'http://www.microsoft.com/downloads/details.aspx?FamilyID=50b97994-8453-4998-8226-fa42ec403d17&DisplayLang=en#filelist

#Region " Sql Structure "
    ' id                - id
    ' ApplicationName   - Varchar(255)
    ' XmlData           - Xml
#End Region

#Region " Sql Connection "
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

#Region " UpdateHits "
    Public Sub UpdateHits(ByVal Id As Integer)
        Dim db As SqlConnection = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("UPDATE Pages SET XmlData.modify('replace value of (/Page/@hits)[1] with (/Page/@hits)[1]+1') WHERE ApplicationName=@ApplicationName AND XmlData.value('/Page[1]/@Id','nvarchar(max)')=@id", db)
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
        Dim db As SqlConnection = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("UPDATE Pages SET XmlData.modify('replace value of (/Page/@hits)[1] with (/Page/@hits)[1]+1') WHERE ApplicationName=@ApplicationName AND XmlData.value('/Page[1]/@file','nvarchar(max)')=@Filename", db)
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

#Region " Add "
    Public Function create(ByVal obj As PageType) As Boolean
        obj.Filename = obj.Filename.ToLower

        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("exec CreatePage @ApplicationName, @XmlData", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@XmlData", WDK.XML.Utils.Serializer.ObjectToXmlString(obj))

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
    Public Function create(ByVal Filename As String, ByVal MasterPage As String, ByVal Title As String, ByVal Keywords As String, ByVal Description As String, ByVal Content As String) As Boolean
        Dim res As Boolean = False

        Try
            Dim webPage As New PageType
            webPage.Filename = Filename.ToLower
            webPage.MasterPage = MasterPage
            webPage.CreatedOn = Now
            webPage.UpdatedOn = Now
            webPage.Title = Title
            webPage.Metas.Keywords = Keywords
            webPage.Metas.Description = Description
            webPage.Hits = 0
            webPage.Content = Content

            res = create(webPage)

        Catch ex As Exception
            Log("Pages.Add -- " & ex.ToString, True)
        End Try
        Return res
    End Function
#End Region

#Region " Update {non-object} "
    '@ApplicationName varchar(255)=NULL,
    '@Id varchar(255) = NULL,
    '@Filename varchar(255) = NULL,
    '@MasterPage varchar(255) = '~/default.master',
    '@Title nvarchar(255) = NULL,
    '@Keywords nvarchar(max) = '',
    '@Description nvarchar(max) = '',
    '@Content nvarchar(max) = '',
    '@UpdatedOn nvarchar(max) = ''
    Public Function update(ByVal id As Integer, ByVal entry As PageType) As Boolean
        Dim res As Boolean = False
        Dim db As SqlConnection = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("EXEC UpdatePage @ApplicationName, @Id, @Filename, @MasterPage, @Title, @Keywords, @Description, @Content, @UpdatedOn", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", id)
            dbc.Parameters.AddWithValue("@Filename", entry.Filename)
            dbc.Parameters.AddWithValue("@MasterPage", entry.MasterPage)
            dbc.Parameters.AddWithValue("@Title", entry.Title)
            dbc.Parameters.AddWithValue("@Keywords", entry.Metas.Keywords)
            dbc.Parameters.AddWithValue("@Description", entry.Metas.Description)
            dbc.Parameters.AddWithValue("@Content", entry.Content)
            dbc.Parameters.AddWithValue("@UpdatedOn", Now)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log("Pages.Update -- " & ex.ToString, True)
        End Try

        Return res
    End Function
#End Region

#Region " .remove() "
    Public Function remove(ByVal Id As String) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("DELETE FROM Pages WHERE ApplicationName=@ApplicationName AND id=@id", db)
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
    Public Function getDatasource(Optional ByVal Criteria As String = "") As List(Of PageType)
        Dim db As SqlConnection = Nothing
        Dim ret As New List(Of PageType)

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim dbc As New SqlCommand("SELECT id, 	CONVERT(nvarchar(max), XmlData) As XmlData FROM Pages WHERE ApplicationName=@ApplicationName " & Criteria, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim reader As SqlDataReader = dbc.ExecuteReader
            If reader.HasRows Then
                While reader.Read
                    Dim entry As PageType = WDK.XML.Utils.Serializer.XmlStringToObject(Of PageType)(reader("XmlData"))
                    entry.uid = reader("id")
                    ret.Add(entry)
                End While
            End If


        Catch ex As Exception
            Log("Pages.GetDatasource -- " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return ret
    End Function
#End Region

#Region " GetDataset(Paged) "

#End Region

#Region " GetPage "
    Public Function getPageById(ByVal id As Integer) As PageType
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing
        Dim pg As New PageType

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("EXEC GetPageById @ApplicationName, @id", db)
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
                pg.Type = PageContentType.HTML
                pg.Content = "Requested page not found"
                pg.MasterPage = MasterPageUrl()
            Else
                pg = WDK.XML.Utils.Serializer.XmlStringToObject(Of PageType)(reader("XmlData"))
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

    Public Function getPageByFilename(ByVal Filename As String) As PageType
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing
        Dim pg As New PageType

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("EXEC GetPageByFilename @ApplicationName, @id", db)
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
                pg.Type = PageContentType.HTML
                pg.Content = "Requested page {" & Filename & "} not found"
				pg.MasterPage = MasterPageUrl()
				Log("Page not found {" + Filename + "}")
            Else
				pg = WDK.XML.Utils.Serializer.XmlStringToObject(Of PageType)(reader("XmlData"))
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

#Region " GetPageIdByFilename "
    Public Function getPageIdByFilename(ByVal filename As String) As Integer
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing
        Dim ret As Integer = 0

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("EXEC [GetPageByFilename] @ApplicationName, @Filename", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Filename", filename)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows = True Then ret = reader("id")

        Catch ex As Exception
            Log("[Wdk.Pages][GetPageIdByFilename] Error: " & ex.ToString & vbCrLf & " Page Requested: " & filename, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return ret
    End Function
#End Region

End Class
