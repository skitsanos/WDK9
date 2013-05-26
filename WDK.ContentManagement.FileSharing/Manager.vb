Imports System.Data.SqlClient

Public Class Manager

#Region " SortOrder "
    Private _SortOrder As String = "Title ASC"
    Public Property SortBy() As String
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As String)
            _SortOrder = value
        End Set
    End Property
#End Region

#Region " .Add() "
    Public Function Add(ByVal FileName As String, ByVal ContentType As String, ByVal FileContent As Byte(), ByVal Description As String, ByVal Filesharingecurity As [Security]) As Boolean

        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "INSERT INTO Filesharing "
            strSQL += " (ApplicationName,Owner,FileName,ContentType,FileContent,Description,FileSecurity,Hits,CreatedOn,UpdatedOn) "
            strSQL += " VALUES (@ApplicationName,@Owner,@FileName,@ContentType,@FileContent,@Description,@FileSecurity,@Hits,@CreatedOn,@UpdatedOn)"

            Dim dbc As New SqlCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Owner", Web.HttpContext.Current.User.Identity.Name)
            dbc.Parameters.AddWithValue("@FileName", FileName)
            dbc.Parameters.AddWithValue("@ContentType", ContentType)
            dbc.Parameters.AddWithValue("@FileContent", FileContent)
            dbc.Parameters.AddWithValue("@Description", Description)
            dbc.Parameters.AddWithValue("@FileSecurity", WDK.XML.Utils.Serializer.ObjectToXmlString(Filesharingecurity))
            dbc.Parameters.AddWithValue("@Hits", 0)
            dbc.Parameters.AddWithValue("@CreatedOn", Now)
            dbc.Parameters.AddWithValue("@UpdatedOn", Now)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            res = True

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return res
    End Function
#End Region

#Region " .GetFile() "
    Public Function GetFile(ByVal Id As Integer) As File
        Dim db As SqlConnection = Nothing
        Dim res As File = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "SELECT * FROM Filesharing WHERE ApplicationName=@ApplicationName AND id=@Id"

            Dim dbc As New SqlCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Id", Id)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows = True Then
                res = New File
                res.Filename = reader("Filename").ToString
                res.Description = reader("Description").ToString
                res.ContentType = reader("ContentType").ToString
                res.Content = reader("FileContent")
                res.Owner = reader("Owner").ToString
                res.Hits = reader("Hits")
                res.CreatedOn = reader("CreatedOn")
                res.UpdatedOn = reader("UpdatedOn")
                res.Security = WDK.XML.Utils.Serializer.XmlStringToObject(Of Security)(reader("FileSecurity"))
            End If

        Catch ex As Exception
            Log(ex.ToString, True)
            res = Nothing
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return res
    End Function
#End Region

#Region " Update() "
    Public Function Update(ByVal Id As Integer, ByVal FileName As String, ByVal Description As String) As Boolean
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If IsNothing(Description) Then Description = ""

            Dim strSQL As String = "UPDATE Filesharing SET FileName=@FileName,Description=@Description,UpdatedOn=@UpdatedOn WHERE ApplicationName=@ApplicationName AND id=" & Id

            Dim dbc As New SqlCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@FileName", FileName)
            dbc.Parameters.AddWithValue("@Description", Description)
            dbc.Parameters.AddWithValue("@UpdatedOn", Now)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
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

    Public Function Update(ByVal Id As Integer, _
    ByVal FileName As String, _
    ByVal ContentType As String, _
    ByVal FileContent As Byte(), _
    ByVal Description As String) As Boolean
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSql As String = "UPDATE Filesharing SET FileName=@FileName, Description=@Description,ContentType=@ContentType, FileContent=@FileContent, UpdatedOn=@UpdatedOn WHERE ApplicationName=@ApplicationName AND id=" & Id

            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@FileName", FileName)
            dbc.Parameters.AddWithValue("@Description", Description)
            dbc.Parameters.AddWithValue("@ContentType", ContentType)
            dbc.Parameters.AddWithValue("@FileContent", FileContent)
            dbc.Parameters.AddWithValue("@UpdatedOn", Now)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            Log(dbc.Parameters.Count)

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

#Region " UpdateSecurity "
    Public Function UpdateSecurity(ByVal id As Integer, ByVal SecurityRules As WDK.ContentManagement.FileSharing.Security) As Boolean
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSQL As String = "UPDATE Filesharing SET FileSecurity=@FileSecurity WHERE ApplicationName=@ApplicationName AND id=" & id

            Dim dbc As New SqlCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@FileSecurity", WDK.XML.Utils.Serializer.ObjectToXmlString(SecurityRules))
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.ExecuteNonQuery()

            dbc.Dispose()

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
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("DELETE FROM Filesharing WHERE ApplicationName=@ApplicationName AND id=" & Id, db)
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

#Region " GetDatasource() "
    Public Function GetDatasource(Optional ByVal Criteria As String = "") As System.Data.DataSet
        Dim db As SqlConnection = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim strSQL As String = "SELECT * FROM Filesharing WHERE ApplicationName=@ApplicationName " & Criteria & " ORDER BY " & SortBy

            Dim dbc As New SqlCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Downloads")

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

#Region " UpdateHits "
    Public Sub UpdateHits(ByVal Id As Integer)
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim curValue As Integer = GetFile(Id).Hits
            Dim strSQL As String = "UPDATE Filesharing SET Hits=@Hits WHERE ApplicationName=@ApplicationName AND id=" & Id

            Dim dbc As New SqlCommand(strSQL, db)
            dbc.Parameters.AddWithValue("@Hits", curValue + 1)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.ExecuteNonQuery()

            dbc.Dispose()

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Sub
#End Region

End Class