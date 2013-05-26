Imports System.Data.SqlClient

Public Class Manager

#Region " table Structure "
    ' id                - Integer
    ' ApplicationName   - Varchar(255)
    ' XmlData           - Xml
#End Region

#Region " Sorting "
    Private _SortOrder As String = "id DESC"
    Public Property sortOrder() As String
        Get
            Return _SortOrder
        End Get
        Set(ByVal value As String)
            _SortOrder = value
        End Set
    End Property
#End Region

#Region " UpdateHits "
    Public Sub updateHits(ByVal Id As Integer)
        Dim db As SqlConnection = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim strSql As String = "UPDATE Events SET XmlData.modify('replace value of (/Event/@Hits)[1] with (/Page/@Hits)[1]+1') WHERE ApplicationName=@ApplicationName AND XmlData.value('/Page[1]/@Id','nvarchar(max)')=@id"
            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@id", Id)
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

#Region " create "
    Public Function create(ByVal obj As EventType) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("INSERT INTO Events (ApplicationName, XmlData) VALUES (@ApplicationName, @XmlData)", db)

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

#Region " create {non-object}"
    Public Function create(ByVal Title As String, ByVal Content As String, Optional ByVal AllowComments As Boolean = False) As Boolean
        Dim res As Boolean = False

        Try
            Dim webEvent As New EventType
            webEvent.title = Title
            webEvent.content = Content
            webEvent.allowComments = AllowComments
            res = create(webEvent)

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try
        Return res
    End Function
#End Region

#Region " Update "
    Public Function update(ByVal id As Integer, ByVal obj As EventType) As Boolean
        Dim ret As Boolean = False

        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim oldEvent As EventType = getItem(id)
            oldEvent.title = obj.title
            oldEvent.description = obj.description
            oldEvent.allowComments = obj.allowComments
            oldEvent.Comments = obj.Comments
            oldEvent.content = obj.content
            oldEvent.updatedOn = Now

            Dim dbc As New SqlCommand("UPDATE Events SET XmlData=@XmlData WHERE ApplicationName=@ApplicationName AND id=@id", db)
            dbc.Parameters.AddWithValue("@XmlData", WDK.XML.Utils.Serializer.ObjectToXmlString(oldEvent))
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
    Public Function remove(ByVal Id As String) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("DELETE FROM Events WHERE ApplicationName=@ApplicationName AND id=@id", db)
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
    Public Function getDatasource(Optional ByVal Criteria As String = "") As List(Of EventType)
        Dim db As SqlConnection = Nothing
        Dim ret As New List(Of EventType)

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

            Dim strSql As String = "SELECT id, CONVERT(nvarchar(max), XmlData) As XmlData FROM Events WHERE ApplicationName=@ApplicationName " & Criteria & " ORDER BY " & sortOrder
            Dim dbc As New SqlCommand(strSql, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim reader As SqlDataReader = dbc.ExecuteReader
            If reader.HasRows Then
                While reader.Read
                    Dim entry As EventType = WDK.XML.Utils.Serializer.XmlStringToObject(Of EventType)(reader("XmlData"))
                    entry.uid = reader("id")
                    ret.Add(entry)
                End While
            End If


        Catch ex As Exception
            Log("[Wdk.Events][GetDatasource] Error: " & ex.ToString, True)
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
    Public Function getItem(ByVal id As Integer) As EventType
        Dim db As SqlConnection = Nothing
        Dim ret As EventType = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT id, CONVERT(nvarchar(max), XmlData) As XmlData From Events WHERE ApplicationName=@ApplicationName AND id=" + id.ToString, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim reader As SqlDataReader = dbc.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows Then
                Dim entry As EventType = WDK.XML.Utils.Serializer.XmlStringToObject(Of EventType)(reader("xmldata"))
                entry.uid = reader("id")

                ret = entry
            End If

        Catch ex As Exception
            Log("[Wdk.Events][GetDatasource] Error: " & ex.ToString, True)
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