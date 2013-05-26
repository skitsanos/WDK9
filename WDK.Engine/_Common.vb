Imports System.Configuration
Imports System.Web.Configuration

Module _Common

#Region " SupportTarget "
    Friend Function SupportTarget() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.AppSettings.Settings("wdk_support_target").Value
    End Function
#End Region

#Region " SupportMethod "
    ''' <summary>
    ''' Support method (email|edx)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SupportMethod() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.AppSettings.Settings("wdk_support_method").Value
    End Function
#End Region

End Module
