Imports System.Web

''' <summary>
''' Content Router for SiteAdmin CMS
''' </summary>
''' <remarks></remarks>

Public Class ContentRouter
    Implements IHttpModule

#Region " Properties "
    Private Application As HttpApplication
#End Region

#Region " Dispose "
    Public Sub Dispose() Implements System.Web.IHttpModule.Dispose
        'Application.Dispose()
    End Sub
#End Region

#Region " Init "
    Public Sub Init(ByVal context As System.Web.HttpApplication) Implements System.Web.IHttpModule.Init
        Application = context
        AddHandler Application.BeginRequest, AddressOf Application_BeginRequest
    End Sub
#End Region

#Region " Application_BeginRequest "
    Public Sub Application_BeginRequest(ByVal sender As Object, ByVal events As System.EventArgs)
        Application = CType(sender, HttpApplication)

        'Dim Request As HttpRequest = Application.Context.Request
        'Dim Response As HttpResponse = Application.Context.Response
        With Application.Context
            'If .Request.Path.ToLower.StartsWith("/content/") Then
            '    .RewritePath(.Request.Path + ".aspx")
            '    '.Server.Execute(.Request.Path + ".aspx")
            'Else
            '    .Response.Write(.Request.Path + " not found")
            '    '.Server.Execute(.Request.Path)
            'End If
            .Response.Write(.Request.Path + " not found")
        End With

        Application.CompleteRequest()
    End Sub
#End Region


End Class
