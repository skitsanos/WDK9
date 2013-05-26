Imports System.Xml
Imports System.Data.Odbc

Public Class XmlOdbcDataBridge
    Implements IXmlDataBridge

    Private _ConnectionString As String = ""

    Public Property ConnectionString() As String Implements IXmlDataBridge.ConnectionString
        Get
            Return _ConnectionString
        End Get
        Set(ByVal value As String)
            _ConnectionString = value
        End Set
    End Property

    Public Function Execute(ByVal xq As String) As System.Xml.XmlDocument Implements IXmlDataBridge.Execute
        Dim doc As New XmlDocument
        Dim root As XmlElement = doc.CreateElement("bridge:Response", "http://xmldb.info")
        root.SetAttribute("type", "XmlOdbcDataBridge")
        doc.AppendChild(root)

        Dim db As New OdbcConnection(Me.ConnectionString)
        Try
            db.Open()
            Dim result As Integer = New OdbcCommand(xq, db).ExecuteNonQuery

            Dim elResult As XmlElement = doc.CreateElement("Result")
            root.SetAttribute("status", "200")
            elResult.InnerText = result.ToString
            root.AppendChild(elResult)

        Catch ex As Exception
            root.SetAttribute("status", "500")

            Dim elError As XmlElement = doc.CreateElement("Error")
            root.AppendChild(elError)
            Dim cdataError As XmlCDataSection = doc.CreateCDataSection(ex.ToString)
            elError.AppendChild(cdataError)
        Finally
            If (Not db Is Nothing) Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return doc
    End Function

    Public Function Query(ByVal xq As String, Optional ByVal start As Integer = 0, Optional ByVal size As Integer = 0) As System.Xml.XmlDocument Implements IXmlDataBridge.Query
        Dim doc As New XmlDocument
        Dim root As XmlElement = doc.CreateElement("bridge:Response", "http://xmldb.info")
        root.SetAttribute("type", "XmlOdbcDataBridge")
        doc.AppendChild(root)

        Dim db As OdbcConnection = Nothing
        Try
            db = New OdbcConnection(Me.ConnectionString)
            db.Open()

            Dim dataTable As New DataTable
            Dim adapter As New OdbcDataAdapter(xq, db)
            If ((start <> 0) AndAlso (size <> 0)) Then
                adapter.Fill(start, size, New DataTable() {dataTable})
            Else
                adapter.Fill(dataTable)
            End If

            root.SetAttribute("total", dataTable.Rows.Count.ToString)

            For Each dbr As DataRow In dataTable.Rows
                Dim elRow As XmlElement = doc.CreateElement("Row")

                For Each column As DataColumn In dataTable.Columns
                    If (column.ColumnName.ToLower <> "applicationname") Then
                        elRow.AppendChild(newElement(column.ColumnName, dbr(column.ColumnName).ToString, doc))
                    End If
                Next
                root.AppendChild(elRow)
            Next

            root.SetAttribute("status", "200")

        Catch ex As Exception
            root.SetAttribute("status", "500")

            Dim elError As XmlElement = doc.CreateElement("Error")
            root.AppendChild(elError)
            Dim section As XmlCDataSection = doc.CreateCDataSection(ex.ToString)
            elError.AppendChild(section)
        Finally
            If (Not db Is Nothing) Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return doc
    End Function
End Class
