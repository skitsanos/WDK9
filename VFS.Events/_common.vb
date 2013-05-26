Imports System.Web.Configuration
Imports System.Configuration

Friend Module _common

#Region " BytesOf "
    Friend Function BytesOf(ByVal data As String) As Byte()
        Return Text.Encoding.UTF8.GetBytes(data)
    End Function
#End Region

#Region " .Log() "
    Friend Sub Log(ByVal Data As String, Optional ByVal IsError As Boolean = False)
        Dim logs As Object = Activator.CreateInstance(Type.GetType("WDK.Providers.Logs.ApplicationLog,WDK.Providers.Logs"))
        If logs IsNot Nothing Then
            logs.Write(Data, IsError)
        End If
    End Sub
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

#Region " UrlHome "
    Friend Function UrlHome() As String
        Dim Request As System.Web.HttpRequest = System.Web.HttpContext.Current.Request

        Dim url As String = "http://" & System.Web.HttpContext.Current.Request.Url.Host 'Request.ServerVariables("SERVER_NAME").ToLower
        If System.Web.HttpContext.Current.Request.Url.Port <> 80 Then url += ":" & System.Web.HttpContext.Current.Request.Url.Port
        url += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
        Return url
    End Function
#End Region

#Region " RenderEventsFeed "
    Friend Function RenderEventsFeed(Optional ByVal format As String = "rss") As String
        Dim Events As New Wdk.ContentManagement.Events.Manager
     
        '- generate feed
        Dim feed As New Wdk.Media.Feeds.ChannelType
        feed.Copyright = WDK.Providers.Settings.Get("RssCopyright")
        feed.Generator = "SiteAdmin CMS VFS, by Skitsanos.com"
        feed.Title = WDK.Providers.Settings.Get("RssTitle")
        If feed.Title Is Nothing Then feed.Title = "[Untitled feed]"
        feed.Description = WDK.Providers.Settings.Get("RssDescription")
        If feed.Description Is Nothing Then feed.Description = ""
        feed.Link = UrlHome()
        feed.LastBuildDate = Now

        Dim row As Integer = 0
        Dim rows As Integer = 10

        For Each entry As WDK.ContentManagement.Events.EventType In Events.getDatasource
            Dim item As New WDK.Media.Feeds.ChannelItemType
            item.Title = entry.title
            item.Description = entry.content
            item.Link = UrlHome() + "/events/" & entry.uid & ".aspx"
            item.pubDate = New WDK.Utilities.W3CDateTime(Now, New TimeSpan(0, 0, 0)).ToString("R")
            feed.Items.Add(item)

            row += 1
            If row > rows - 1 Then Exit For
        Next

        Dim doc As New Xml.XmlDocument
        doc.LoadXml(feed.DocumentElement.OuterXml)
        Dim tr As New Wdk.XML.Utils.Transformation

        Return WDK.XML.Utils.Transformation.TransformXML(feed.DocumentElement, Web.HttpContext.Current.Request.MapPath("/SiteAdmin/_Controls/feed2" + format + ".xslt"))
    End Function

#End Region

End Module
