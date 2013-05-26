<%@ Page Language="VB" %>
<%@ Import Namespace="System.Xml" %>

<script runat="server">    
    Public Function urlHome() As String
        Dim Request As Web.HttpRequest = Web.HttpContext.Current.Request

        Dim url As String = "http://" & Request.ServerVariables("HTTP_HOST") & _
                                     IO.Path.GetDirectoryName(Request.ServerVariables("URL")).Replace("\", "/")
        If url.EndsWith("/") = False Then url += "/"
        Return url
    End Function
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Clear()
        Response.ContentType = "text/xml"
        
        Try
            '- Generate RSS
            Dim xmlDoc As New XmlDocument
            Dim xmlPi As XmlProcessingInstruction = xmlDoc.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
            xmlDoc.AppendChild(xmlPi)
        
            Dim rss As XmlElement = xmlDoc.CreateElement("rss")
            rss.SetAttribute("version", "2.0")
            xmlDoc.AppendChild(rss)
        
            Dim channel As XmlElement = xmlDoc.CreateElement("channel")
            rss.AppendChild(channel)
        
            Dim title As XmlElement = xmlDoc.CreateElement("title")
            title.InnerText = "SiteAdmin CMS -- Images RSS Feed"
            channel.AppendChild(title)
        
            Dim link As XmlElement = xmlDoc.CreateElement("link")
            link.InnerText = urlHome()
            channel.AppendChild(link)
        
            Dim description As XmlElement = xmlDoc.CreateElement("description")
            description.InnerText = "Imeages RSS feed"
            channel.AppendChild(description)
        
            Dim language As XmlElement = xmlDoc.CreateElement("language")
            language.InnerText = "en-us"
            channel.AppendChild(language)
        
            Dim copyright As XmlElement = xmlDoc.CreateElement("copyright")
            copyright.InnerText = "&copy; 1996 - " & Year(Now) & ", Skitsanos.com."
            channel.AppendChild(copyright)
        
            Dim lastBuildDate As XmlElement = xmlDoc.CreateElement("lastBuildDate")
            Dim w3date As New WDK.Utilities.W3CDateTime(Now)
            lastBuildDate.InnerText = w3date.ToString("R")
            channel.AppendChild(lastBuildDate)
        
            Dim ttl As XmlElement = xmlDoc.CreateElement("ttl")
            ttl.InnerText = "5"
            channel.AppendChild(ttl)
            
            Dim rssData As New WDK.ContentManagement.Galleries.Images
            rssData.SortBy = "id DESC"
            
            Dim row As Integer = 0
            Dim rows As Integer = 10
            If IsNumeric(Request("rows")) Then rows = Request("rows")
            
            For Each dbr As Data.DataRow In rssData.GetDataset("WHERE Folder=6").Tables(0).Rows
                Dim item As XmlElement = xmlDoc.CreateElement("item")
                Dim itemTitle As XmlElement = xmlDoc.CreateElement("title")
                Dim titleCDATA As XmlCDataSection = xmlDoc.CreateCDataSection("Project #" & dbr("id"))
                itemTitle.AppendChild(titleCDATA)
                item.AppendChild(itemTitle)
                
                Dim itemLink As XmlElement = xmlDoc.CreateElement("link")
                itemLink.InnerText = urlHome() & "images_dl.aspx?id=" & dbr("id")
                item.AppendChild(itemLink)
                
                Dim itempubDate As XmlElement = xmlDoc.CreateElement("pubDate")
                w3date = New WDK.Utilities.W3CDateTime(dbr("CreatedOn"))
                itempubDate.InnerText = w3date.ToString("R")
                item.AppendChild(itempubDate)
                
                Dim itemDescription As XmlElement = xmlDoc.CreateElement("description")
                Dim descriptionCDATA As XmlCDataSection = xmlDoc.CreateCDataSection(dbr("Description"))
                itemDescription.AppendChild(descriptionCDATA)
                item.AppendChild(itemDescription)
                                
                channel.AppendChild(item)
                
                row += 1
                If row > rows - 1 Then Exit For
            Next
                       
            Response.Write(xmlDoc.OuterXml)
            
        Catch ex As Exception
            Response.Write("<error><![CDATA[" & ex.ToString & "]]></error>")
        End Try
    End Sub
</script>