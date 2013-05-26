<%@ Page Language="VB" Debug="false" %>

<script runat="server">

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        '- Catch the resource
        Dim url As String = Request.Url.AbsoluteUri.ToLower.Remove(Request.Url.AbsoluteUri.IndexOf("?"))
        url = url.Replace("resources/", "resources_bin/")
        url = url.Replace("/get", "/" & Request.ServerVariables("QUERY_STRING"))
        
        Try
            Dim resBase64 As String = FetchURL(url)
            Dim resByte As Byte() = Convert.FromBase64String(resBase64)

            Response.BinaryWrite(resByte)
        Catch ex As Exception
            Dim urlErr As String = Request.Url.AbsoluteUri.ToLower.Remove(Request.Url.AbsoluteUri.IndexOf("?"))
            urlErr = url.Replace("resources/", "system/")
        
            Dim Template As String = FetchURL(urlErr)
            Template = Template.Replace("{@@title/}", "Error")
            Template = Template.Replace("{@@description/}", ex.Message)
            Template = Template.Replace("{@@content/}", ex.ToString)
            Template = Template.Replace("{@@related/}", "")
            
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

