Imports System.Web.Configuration
Imports System.Configuration

Friend Module _common

#Region " BytesOf "
	Friend Function BytesOf(ByVal Data As String) As Byte()
		Return Text.Encoding.UTF8.GetBytes(Data)
	End Function
#End Region

#Region " ResourceNotFound "
	Friend Function ResourceNotFound(ByVal Resource As String) As Byte()
		Dim Template As String = ""
		Template += "<%@ Page Language=""VB"" MasterPageFile=""" + MasterPageUrl() + """ Title=""Resource not found"" %>" & vbCrLf
		Template += "<asp:Content ID=""Content1"" ContentPlaceHolderID=""contentBody"" Runat=""Server"">" & vbCrLf
		Template += "The resource you requested {" & Resource & "} is not found on your server or not served by WDKFS" & vbCrLf
		Template += "</asp:Content>"


		Return BytesOf(Template)
	End Function
#End Region

#Region " MasterPageUrl "
	Friend Function MasterPageUrl() As String
		Dim defMasterPage As String = "~/siteadmin/DefaultSite/default.master"

		If System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "default.master") Then
			Return "~/default.master"
		Else
			Return defMasterPage
		End If
	End Function
#End Region

#Region " GetApplicationName "
	Friend Function GetApplicationName() As String
		Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
		Dim mSection As New MembershipSection
		mSection = conf.GetSection("system.web/membership")

		Dim appName As String = mSection.Providers(mSection.DefaultProvider).Parameters("applicationName")
		If appName = "" Then
			appName = System.Web.HttpContext.Current.Request.Url.Host

			If System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath <> "/" Then _
			appName += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
		End If

		Return appName
	End Function
#End Region

End Module
