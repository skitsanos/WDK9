Imports System.Web

Public Class HttpModule
    Implements IHttpModule

#Region " Properties "
    Private Application As HttpApplication
#End Region

#Region " Dispose "
    Public Sub Dispose() Implements System.Web.IHttpModule.Dispose

    End Sub
#End Region

#Region " Init "
    Public Sub Init(ByVal Application As HttpApplication) Implements IHttpModule.Init
        AddHandler Application.BeginRequest, AddressOf Me.Application_BeginRequest
    End Sub
#End Region

#Region " Application_BeginRequest "
    Public Sub Application_BeginRequest(ByVal sender As Object, ByVal events As System.EventArgs)
        Application = CType(sender, HttpApplication)

        Dim Request As HttpRequest = Application.Context.Request
        Dim Response As HttpResponse = Application.Context.Response

        Response.Write(Request.Path)

        Application.CompleteRequest()
    End Sub
#End Region

End Class
