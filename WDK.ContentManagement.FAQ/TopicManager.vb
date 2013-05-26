Imports System.Data.SqlClient

Public Class TopicManager

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
#End Region

#Region " .Add() "
    Public Function Add(ByVal Question As String, ByVal Answer As String) As Boolean
        If IsNothing(Answer) Then Answer = ""
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim topic As New TopicType
            topic.Question = Question
            topic.Answer = Answer

            Dim dbc As New SqlCommand("INSERT INTO Faq (ApplicationName, XmlData) VALUES (@ApplicationName, @XmlData)", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@XmlData", GetXml(topic).DocumentElement.OuterXml)

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

#Region " Update() "
    Public Function Update(ByVal Id As Integer, ByVal Question As String, ByVal Answer As String) As Boolean
        Dim db As SqlConnection = Nothing
        Dim res As Boolean = False

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim topic As New TopicType
            topic.Question = Question
            topic.Answer = Answer

            Dim dbc As New SqlCommand("UPDATE Faq SET XmlData=@XmlData WHERE ApplicationName=@ApplicationName AND id=" + Id.ToString, db)
            dbc.Parameters.AddWithValue("@XmlData", GetXml(topic).DocumentElement.OuterXml)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

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

#Region " .Delete() "
    Public Function Delete(ByVal Id As Integer) As Boolean
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("DELETE FROM Faq WHERE ApplicationName=@ApplicationName AND id=" + Id.ToString, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.ExecuteNonQuery()

            dbc.Dispose()

            Return True

        Catch ex As Exception
            Log(ex.Message, True)
            Return False
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " GetDataset() "
    Public Function GetDatasource(Optional ByVal Criteria As String = "") As System.Data.DataSet
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" + Criteria + ")"

            Dim dbc As New SqlCommand("SELECT * FROM viewFaq WHERE ApplicationName=@ApplicationName " + Criteria + " ORDER BY " + SortOrder, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "FAQ")

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

#Region " GetDataset(Paged) "
    Public Function GetDatasource(ByVal startRecord As Integer, ByVal maxRecords As Integer, Optional ByVal Criteria As String = "") As DataSet
        Dim db As SqlConnection = Nothing
        Dim dbs As DataSet = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            If Criteria <> "" Then Criteria = "AND (" + Criteria + ")"

            Dim dbc As New SqlCommand("SELECT * FROM Faq WHERE ApplicationName=@ApplicationName " + Criteria + " ORDER BY " + SortOrder, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            dbs = New DataSet("Faq")
            dbs.Clear()

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            dba.Fill(dbs, startRecord, maxRecords, "viewFaq")

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

#Region " GetXmlDataSource "
    Public Function GetXmlDataSource(ByVal query As String) As Xml.XmlDocument
        Dim xmldoc As New Xml.XmlDocument
        Dim root As Xml.XmlElement = xmldoc.CreateElement("XmlDataSet")
        xmldoc.AppendChild(root)

        Dim ds As Data.DataSet = GetDatasource(query)
        If ds IsNot Nothing Then
            Dim dt As Data.DataTable = ds.Tables(0)

            For Each dbr As Data.DataRow In dt.Rows
                Dim el As Xml.XmlElement = xmldoc.CreateElement("Row")

                For Each dbCol As Data.DataColumn In dt.Columns
                    If dbCol.ColumnName.ToLower <> "applicationname" Then el.AppendChild(newElement(CamelCase(dbCol.ColumnName), dbr(dbCol.ColumnName).ToString, xmldoc))
                Next

                root.AppendChild(el)
            Next
        End If

        Return xmldoc
    End Function
#End Region

#Region " GetXmlDataSource "
    Public Function GetXmlDataSource(ByVal startRecord As Integer, ByVal maxRecords As Integer, ByVal query As String) As Xml.XmlDocument
        Dim xmldoc As New Xml.XmlDocument
        Dim root As Xml.XmlElement = xmldoc.CreateElement("XmlDataSet")
        xmldoc.AppendChild(root)

        Dim ds As Data.DataSet = GetDatasource(startRecord, maxRecords, query)
        If ds IsNot Nothing Then
            Dim dt As Data.DataTable = ds.Tables(0)

            For Each dbr As Data.DataRow In dt.Rows
                Dim el As Xml.XmlElement = xmldoc.CreateElement("Row")

                For Each dbCol As Data.DataColumn In dt.Columns
                    If dbCol.ColumnName.ToLower <> "applicationname" Then el.AppendChild(newElement(CamelCase(dbCol.ColumnName), dbr(dbCol.ColumnName).ToString, xmldoc))
                Next

                root.AppendChild(el)
            Next
        End If

        Return xmldoc
    End Function
#End Region

#Region " GetTopic "
    Public Function GetFaqTopic(ByVal id As Integer) As TopicType
        Dim db As SqlConnection = Nothing
        Dim res As TopicType = Nothing

        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT * FROM viewFaq WHERE ApplicationName=@ApplicationName AND id=" + id.ToString, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "viewFaq")

            Dim dbr As Data.DataRow = dbs.Tables(0).Rows(0)

            res = New TopicType
            res.Answer = dbr("Answer").ToString
            res.Question = dbr("Question").ToString

            dba.Dispose()
            dbc.Dispose()

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

#Region " Count() "
    Public Function Count() As Integer
        Dim cnt As Integer = 0

        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT COUNT(*) FROM Faq WHERE ApplicationName=@ApplicationName", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            cnt = CInt(dbc.ExecuteScalar())
            dbc.Dispose()

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return cnt
    End Function
#End Region

End Class