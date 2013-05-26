Module _Utils

#Region " Build OdbcPage "
    Friend Function GeOdbcPage() As Byte()
		Dim tmpLines As String = "<ul>"
        For Each strOdbc As String In GetODBCDriversList()
            tmpLines += "<li>" & strOdbc & "</li>"
        Next
        tmpLines += "</ul>"

        Dim Template As String = ""
        Template += "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""Installed ODBC Drivers"" Debug=""true"" %>" & vbCrLf
		Template += "<asp:Content ID=""Content1"" ContentPlaceHolderID=""contentBody"" Runat=""Server"">" & vbCrLf
		Template += tmpLines
		Template += "</asp:Content>"

        Return BytesOf(Template)
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

#Region " ListEmbeddedResources "
    Friend Function ListEmbeddedResources() As Byte()
        Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
        Dim tmpHtml As String = "<ul>"
        For Each res As String In assembly.GetManifestResourceNames
            tmpHtml += "<li>" & res & "</li>"
        Next
        tmpHtml += "</ul>"

		Dim Template As String = ""
        Template += "<%@ Page Language=""VB"" MasterPageFile=""" + MasterPageUrl() + """ Title=""Embedded Resources"" %>" & vbCrLf
		Template += "<asp:Content ID=""Content1"" ContentPlaceHolderID=""contentBody"" Runat=""Server"">" & vbCrLf
		Template += tmpHtml
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

#Region " GetPageTitle "
    Friend Function GetPageTitle(ByVal Filename As String) As String
        Dim Pages As Object = Activator.CreateInstance(Type.GetType("WDK.ContentManagement.Pages.Manager,WDK.ContentManagement.Pages"))
        Dim curPage As Object = Pages.GetPage(Filename)
        If curPage Is Nothing Then
            Return "-"
        Else
            Return curPage.Title
        End If
    End Function

#End Region

End Module
