<%@ Page Language="VB" Debug="false"  %>
<%@ Import Namespace="System.Xml" %>

<script runat="server">
       
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        Response.Expires = 0
        Response.ExpiresAbsolute = Date.Today
        Response.AddHeader("pragma", "no-cache")
        Response.AddHeader("cache-control", "private")
        Response.CacheControl = "no-cache"
        
        Dim xmlDoc As New XmlDocument
        Try
            Dim Catalogs As New Skitsanos.BusinessTools.Storages.StoreCatalogs
            Dim Products As New Skitsanos.BusinessTools.Storages.Products
        
            Dim xmlProducts As XmlElement = xmlDoc.CreateElement("products")
            xmlDoc.AppendChild(xmlProducts)
        
            '- Go through catalogs:        
            Dim dsCatalogs As Data.DataSet = Catalogs.GetDatasource
            If dsCatalogs.Tables.Count > 0 Then
                For Each dbr As Data.DataRow In dsCatalogs.Tables(0).Rows
                    Dim xmlCat As XmlElement = xmlDoc.CreateElement("cat")
                    xmlCat.SetAttribute("id", dbr("id"))
                    xmlCat.SetAttribute("title", dbr("title"))
                    xmlProducts.AppendChild(xmlCat)
            
                    '- Go through products in each catalog
                    Dim dsProducts As Data.DataSet = Products.GetDatasource("CatalogId=" & dbr("id"))
            
                    If dsProducts.Tables.Count > 0 Then
                        For Each dbrProd As Data.DataRow In dsProducts.Tables(0).Rows
                            Dim xmlPrd As XmlElement = xmlDoc.CreateElement("prd")
                            xmlPrd.SetAttribute("id", dbrProd("SKU"))
                            xmlPrd.SetAttribute("title", dbrProd("Title"))
                            xmlPrd.SetAttribute("desc", dbrProd("Description"))
                            xmlPrd.SetAttribute("price", dbrProd("Price"))
                
                            xmlCat.AppendChild(xmlPrd)
                        Next
                    End If
                Next
            End If
            
            Response.Clear()
            Response.ContentType = "text/xml"
            Response.Write(xmlDoc.OuterXml)
            
        Catch ex As Exception
            Dim url As String = Request.Url.AbsoluteUri.ToLower.Replace("storexml.aspx", "template.aspx")
            Dim Template As String = FetchURL(url)
            
            Template = Template.Replace("{@@title/}", "Error")
            Template = Template.Replace("{@@description/}", ex.ToString)
            Template = Template.Replace("{@@content/}", "Please make sure that you have installed into your /bin folder Skitsanos.BusinessTools library.")
            Template = Template.Replace("{@@related/}", "")
            
            Response.Clear()
            Response.ContentType = "text/html"
            Response.Write(Template)
        End Try
    End Sub
    
    Function FetchURL(ByVal URL As String) As String
        If URL = "" Then
            Return ""
        Else
            Try
                Dim webClient As Net.WebClient = New Net.WebClient
                webClient.Headers.Add("pragma", "no-cache")
                webClient.Headers.Add("cache-control", "private")
                Dim streamReader As IO.StreamReader = New IO.StreamReader(webClient.OpenRead(URL))
                Dim str As String = streamReader.ReadToEnd()
                streamReader.Close()
                streamReader = Nothing
                webClient.Dispose()
                webClient = Nothing
                Return str
            Catch e As Exception
                Return e.Message
            End Try
        End If
    End Function
</script>
