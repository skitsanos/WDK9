Imports System.Configuration
Imports System.ComponentModel
Imports System.Web.UI
Imports System.Web
Imports System.Web.Configuration
Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Xml


<DefaultProperty("SqlConnection"), ToolboxData("<{0}:DbDocumenter runat=server ></{0}:DbDocumenter>")> Public Class DbDocumenter
    Inherits System.Web.UI.WebControls.WebControl

#Region " ConnectionString "
    Private Function ConnectionString() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.ConnectionStrings.ConnectionStrings("SiteAdmin").ConnectionString
    End Function
#End Region

#Region " Properties "
    Private _DatabaseName As String
#End Region

#Region " New "
    Sub New()
        MyBase.New("div")
    End Sub
#End Region

#Region " AddAttributesToRender "
    Protected Overrides Sub AddAttributesToRender(ByVal w As HtmlTextWriter)
        'w.AddAttribute("xmlns", "http://www.w3.org/1999/xhtml")
        'w.AddAttribute("xmlns:b", "http://www.backbase.com/b")
        'w.AddAttribute("xmlns:s", "http://www.backbase.com/s")

        w.AddAttribute("id", Me.ID)
    End Sub
#End Region

#Region " Render "
    Protected Overrides Sub RenderContents(ByVal output As System.Web.UI.HtmlTextWriter)
        Dim sql As String = "SELECT id, name, rtrim(xtype) as xtype " & _
              "FROM sysobjects SO " & _
              "WHERE SO.status>-1 AND SO.xtype in ('U','V','P','TR') " & _
              "ORDER BY name;" & _
              "SELECT SC.id, SC.name, SC.isnullable, ST.name AS typename, " & _
             "CASE WHEN SC.xtype = 231 THEN SC.length/2 ELSE SC.length END AS length " & _
             "FROM systypes ST, syscolumns SC, sysobjects SO " & _
             "WHERE SC.xtype=ST.xtype " & _
             " AND SC.id=SO.id" & _
             " AND SO.status>-1 AND SO.xtype in ('U','V','P') " & _
             " AND ST.length<>256 " & _
             "ORDER BY SO.name, SC.colid;" & _
              "SELECT SC.id, SC.text AS sql " & _
             "FROM syscomments SC, sysobjects SO " & _
             "WHERE SO.id=SC.id" & _
             " AND SO.status>-1 AND SO.xtype in ('TR','V','P'); "

        Dim conn As OdbcConnection = Nothing

        Try
            conn = New OdbcConnection(ConnectionString)
            Dim dba As New OdbcDataAdapter(sql, conn)
            Dim ds As New DataSet

            dba.Fill(ds, sql)

            Dim xType As String, xName As String, aFieldName As String, RowOdd As Boolean = True

            ds.Tables(0).TableName = "entity"
            ds.Tables(1).TableName = "column"
            ds.Tables(2).TableName = "definition"

            For Each dbt As Data.DataTable In ds.Tables
                With dbt
                    For Each dbCol As Data.DataColumn In .Columns
                        dbCol.ColumnName = dbCol.ColumnName
                        dbCol.ColumnMapping = MappingType.Attribute
                    Next
                    .Columns("id").ColumnMapping = MappingType.Hidden
                End With
            Next

            With ds
                Dim rel1 As DataRelation = .Relations.Add("entity-column", ds.Tables("entity").Columns("id"), ds.Tables("column").Columns("id"))
                rel1.Nested = True
                Dim rel2 As DataRelation = .Relations.Add("entity-definition", ds.Tables("entity").Columns("id"), ds.Tables("definition").Columns("id"))
                rel2.Nested = True
            End With

            Dim xmlDoc As New XmlDocument
            xmlDoc.LoadXml(ds.GetXml)

            Dim HTMLTableBegin As String = "<table class=""" & Me.ID & "-Grid"">"
            Dim HTMLTableEnd As String = "</table>"

            Dim xmlList As XmlNodeList = xmlDoc.DocumentElement.ChildNodes
            For Each aNode As XmlNode In xmlList
                With aNode
                    xName = .Attributes("name").Value
                    xType = .Attributes("xtype").Value
                    'object name
                    '~/resources/get.aspx?logo-siteadmin.gif

                    Dim img As New UI.WebControls.Image
                    img.ImageUrl = "~/resources/get.aspx?db" & xType.ToLower & ".gif"
                    img.Style.Add("padding-right", "10px")
                    img.ImageAlign = WebControls.ImageAlign.AbsMiddle
                    img.RenderControl(output)

                    output.Write("<b>" & xName & " <br /></b>")

                    Dim aNodeList2 As XmlNodeList = Nothing
                    If xType <> "TR" Then
                        'List of columns or parameters
                        aNodeList2 = .SelectNodes("./column")
                        output.Write(HTMLTableBegin)
                        If xType = "P" Then
                            aFieldName = "Parameter"
                        Else
                            aFieldName = "Column"
                        End If

                        output.Write("<tr class=""" & Me.ID & "-Header""><th width=""50%"">")
                        output.Write(aFieldName)
                        output.Write("</th><th width=""30%"">Type</th><th width=""10%"">Size</th><th width=""10%"">Nulls</th></tr>")

                        For Each curNode As XmlNode In aNodeList2
                            With curNode
                                RowOdd = Not RowOdd
                                If RowOdd Then
                                    output.Write("<tr class=""roweven""><td>")
                                Else
                                    output.Write("<tr class=""rowodd""><td>")
                                End If
                                output.Write(.Attributes("name").Value)
                                output.Write("</td><td>")
                                output.Write(.Attributes("typename").Value)
                                output.Write("</td><td>")
                                output.Write(.Attributes("length").Value)
                                output.Write("</td><td>")

                                If CInt(.Attributes("isnullable").Value) = 1 Then
                                    Dim img2 As New UI.WebControls.Image
                                    img2.ImageUrl = "~/resources/get.aspx?check.gif"
                                    img2.Style.Add("padding-right", "10px")
                                    img.Attributes.Add("text-align", "center")
                                    img.ImageAlign = WebControls.ImageAlign.AbsMiddle
                                    img2.RenderControl(output)
                                Else
                                    output.Write("&nbsp;")
                                End If
                                output.Write("</td></tr>")
                            End With
                        Next
                        output.Write(HTMLTableEnd)
                    End If
                End With

                If xType <> "U" Then
                    'SQL scripts (for views, triggers, and stored procedures)
                    Dim aNode2 As XmlNode = aNode.SelectSingleNode("./definition")
                    output.Write(HTMLTableBegin)
                    output.Write("<tr class=""" & Me.ID & "-Header""><th>SQL</th></tr><tr><td class=""" & Me.ID & "-Sql"">")
                    Try
                        Dim sqlHtml As String = aNode2.Attributes("sql").Value.Replace(">", "&gt;").Replace("<", "&lt;").Replace(vbCrLf, "<br>")
                        sqlHtml = sqlHtml.Replace(vbTab, "&nbsp;")
                        output.Write(sqlHtml)
                    Catch : End Try

                    output.Write("</td></tr>")
                    output.Write(HTMLTableEnd)
                End If
            Next

        Catch ex As Exception
            If Not Site Is Nothing AndAlso Site.DesignMode Then
                output.Write("<font size=""2"" color=""navy"" face=""arial"">[CMS DB Documenter: """ & Me.ID & """ ]</font><br>")
                output.Write("<font size=""1""  color=""SeaGreen"" face=""arial"">&nbsp;Documenter not visible in VS.NET designer!</font><br>")
            Else
                output.Write(ex.Message)
            End If
        Finally
            If conn IsNot Nothing Then
                conn.Close()
            End If
        End Try
    End Sub
#End Region

End Class
