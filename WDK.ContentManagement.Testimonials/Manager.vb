Imports System.Data.SqlClient
Imports System.Xml

Public Class Manager

#Region " Sql Structure "
    ' id                - Integer
    ' ApplicationName   - Varchar(255)
    ' XmlData           - Xml
#End Region

#Region " Sorting "
    Private _SortOrder As String = "id DESC"
    Public Property SortOrder() As String
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As String)
            _SortOrder = value
        End Set
    End Property
#End Region

#Region " Add "
    Public Function Add(ByVal obj As TestimonialType) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False

        'Dim str As String = GetXml(obj)

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("INSERT INTO Testimonials (ApplicationName, XmlData) VALUES (@ApplicationName, @XmlData)", db)

            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@XmlData", WDK.XML.Utils.Serializer.ObjectToXmlString(obj))

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

#Region " Add {non-object}"
    Public Function Add(ByVal author As String, ByVal Content As String) As Boolean
        Dim res As Boolean = False

        Try
            Dim webEvent As New TestimonialType
            webEvent.author = author
            webEvent.content = Content
            webEvent.createdOn = Now
            res = Add(webEvent)

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try
        Return res
    End Function
#End Region

#Region " SQL -- UpdateTestimonial "
    'CREATE PROCEDURE [dbo].[UpdateTestimonial]
    '	@ApplicationName varchar(255) = NULL, 
    '	@id int = NULL, 
    '	@Author nvarchar(max) = NULL, 
    '	@Email nvarchar(max) = NULL, 
    '	@Content nvarchar(max) = NULL, 
    '	@UpdatedOn nvarchar(max) = NULL
    'AS
    'BEGIN
    '	UPDATE Events SET XmlData.modify('replace value of (/Testimonial/@author)[1] with sql:variable("@Author")') WHERE ApplicationName=@ApplicationName AND id=@id;
    '	UPDATE Events SET XmlData.modify('replace value of (/Testimonial/@email)[1] with sql:variable("@Email")') WHERE ApplicationName=@ApplicationName AND id=@id;
    '	UPDATE Events SET XmlData.modify('replace value of (/Testimonial/@content)[1] with sql:variable("@Content")') WHERE ApplicationName=@ApplicationName AND id=@id;
    '	UPDATE Events SET XmlData.modify('replace value of (/Testimonial/@updatedOn)[1] with sql:variable("@UpdatedOn")') WHERE ApplicationName=@ApplicationName AND id=@id;
    'END 
#End Region

#Region " Update {non-object} "
    Public Function Update(ByVal id As Integer, ByVal Author As String, ByVal Email As String, ByVal Content As String) As Boolean
        Dim ret As Boolean = False

        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim entry As TestimonialType = getItem(id)
            entry.author = Author
            entry.email = Email
            entry.content = Content
            entry.updatedOn = Now

            Dim dbc As New SqlCommand("UPDATE Testimonials SET XmlData=@XmlData WHERE ApplicationName=@ApplicationName AND id=@id", db)
            dbc.Parameters.AddWithValue("@XmlData", WDK.XML.Utils.Serializer.ObjectToXmlString(entry))
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@id", id)

            dbc.ExecuteNonQuery()

            dbc.Dispose()

            ret = True

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try

        Return ret
    End Function
#End Region

#Region " .Delete() "
    Public Function Delete(ByVal Id As String) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("DELETE FROM Testimonials WHERE ApplicationName=@ApplicationName AND id=@id", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@id", Id)
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

#Region " GetDatasource() "
    Public Function GetDatasource(Optional ByVal Criteria As String = "") As List(Of TestimonialType)
        Dim db As SqlConnection = Nothing
        Dim ret As New List(Of TestimonialType)

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim dbc As New SqlCommand("SELECT id, CONVERT(nvarchar(max), XmlData) As XmlData From Testimonials WHERE ApplicationName=@ApplicationName " + Criteria + " ORDER BY id DESC", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim reader As SqlDataReader = dbc.ExecuteReader
            If reader.HasRows Then
                While reader.Read
                    Dim entry As TestimonialType = WDK.XML.Utils.Serializer.XmlStringToObject(Of TestimonialType)(reader("XmlData"))
                    entry.uid = reader("id")
                    ret.Add(entry)
                End While
            End If

        Catch ex As Exception
            Log("[Wdk.Testimonials][GetDatasource] Error: " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return ret
    End Function
#End Region

#Region " getRandomItem "
    Public Function getRandomItem(Optional ByVal Criteria As String = "") As TestimonialType
        Dim db As SqlConnection = Nothing
        Dim ret As TestimonialType = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT TOP 1 id, CONVERT(nvarchar(max), XmlData) As XmlData FROM Testimonials WHERE ApplicationName=@ApplicationName ORDER BY NEWID()", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()
            If reader.HasRows Then
                Dim entry As TestimonialType = WDK.XML.Utils.Serializer.XmlStringToObject(Of TestimonialType)(reader("XmlData"))
                entry.uid = reader("id")

                ret = entry
            End If

        Catch ex As Exception
            Log("[Wdk.Testimonials][GetDatasource] Error: " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return ret
    End Function
#End Region

#Region " getItem "
    Public Function getItem(ByVal id As Integer) As TestimonialType
        Dim db As SqlConnection = Nothing
        Dim ret As TestimonialType = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT TOP 1 id, CONVERT(nvarchar(max), XmlData) As XmlData From Testimonials WHERE ApplicationName=@ApplicationName AND id=" + id.ToString, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows Then
                Dim entry As TestimonialType = WDK.XML.Utils.Serializer.XmlStringToObject(Of TestimonialType)(reader("XmlData"))
                entry.uid = reader("id")

                ret = entry
            End If

        Catch ex As Exception
            Log("[Wdk.Testimonials][GetDatasource] Error: " & ex.ToString, True)
        Finally
            If db IsNot Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return ret
    End Function
#End Region

#Region " getLastItem "
    Public Function getLastItem() As TestimonialType
        Dim db As SqlConnection = Nothing
        Dim ret As TestimonialType = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT TOP 1 id, CONVERT(nvarchar(max), XmlData) As XmlData FROM Testimonials WHERE ApplicationName=@ApplicationName ORDER BY id DESC", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows Then
                Dim entry As TestimonialType = WDK.XML.Utils.Serializer.XmlStringToObject(Of TestimonialType)(reader("XmlData"))
                entry.uid = reader("id")

                ret = entry
            End If

        Catch ex As Exception
            Log("[Wdk.Testimonials][GetDatasource] Error: " & ex.ToString, True)
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