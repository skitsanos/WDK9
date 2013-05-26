Imports System.Web

''' <summary>
''' IIS Path Rewrite HttpHandler
''' </summary>
''' <remarks></remarks>


Public Class PathRewrite
    Implements IHttpHandler

#Region " Properties "
    Public ReadOnly Property IsReusable() As Boolean Implements System.Web.IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
#End Region

#Region " ProcessRequest "
    Public Sub ProcessRequest(ByVal context As System.Web.HttpContext) Implements System.Web.IHttpHandler.ProcessRequest
        Dim Request As HttpRequest = context.Request
        Dim Response As HttpResponse = context.Response

        Response.Clear()

        Response.Expires = 0
        Response.ExpiresAbsolute = Date.Today
        Response.AddHeader("pragma", "no-cache")
        Response.AddHeader("cache-control", "private")
        Response.CacheControl = "no-cache"

        Dim appHome As String = AppDomain.CurrentDomain.BaseDirectory
        Dim urlHome As String = "http://" & Request.ServerVariables("HTTP_HOST") & IO.Path.GetDirectoryName(Request.Path).Replace("\", "/")

        Dim Page As String = GetPage(Request.Path)
        urlHome = urlHome.Replace(Page, "")

        'Dim Http As New WDK.Utilities.Http
        'Dim PageContent As String = Http.FetchURL(urlHome & "?o=" & Page)

        'PageContent = PageContent.Replace(Page & "/", "")

        'Response.Write(PageContent)

        context.Response.Redirect(urlHome & "?o=" & Page, True)

        'Response.Write(urlHome & "?o=" & Page)

    End Sub
#End Region

#Region " GetPage "
    Private Function GetPage(ByVal Path As String) As String
        Dim arr As String() = IO.Path.GetDirectoryName(Path).Split("\")
        Return arr(UBound(arr))
    End Function
#End Region

End Class
