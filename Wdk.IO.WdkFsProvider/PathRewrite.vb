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

        Dim prv As New WdkFSProvider

        Response.Write(RenderWaml(Request.Path))

    End Sub
#End Region

#Region " GetPage "
    Private Function GetPage(ByVal Path As String) As String
        Dim arr As String() = System.IO.Path.GetDirectoryName(Path).Split("\")
        Return arr(UBound(arr))
    End Function
#End Region

End Class
