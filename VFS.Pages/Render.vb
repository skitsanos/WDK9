Imports WDK
Imports System.Web
Imports System.Security.Permissions

<AspNetHostingPermission(SecurityAction.Demand, Level:=AspNetHostingPermissionLevel.Minimal), _
   AspNetHostingPermission(SecurityAction.InheritanceDemand, level:=AspNetHostingPermissionLevel.Minimal)> _
Public Class Manager
	Public Function Render(ByVal filename As String) As Byte()
		Dim fileContent As Byte() = Nothing

		If System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "bin\WDK.ContentManagement.Pages.dll") = False Then
			fileContent = BytesOf("<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""WDK.VFS Error"" Debug=""true"" %>" & vbCrLf & _
			  "<asp:Content ID=""Content1"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & _
			  filename & " can't be served. WDK.ContentManagement.Pages.dll is missing from /bin" & _
			  "</asp:Content>")

		Else
			Select Case filename.ToLower
				Case "search.aspx"
					Dim pageInstruction As String = "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""Content Search"" Debug=""true"" %>" & vbCrLf
					pageInstruction += "<%@ Register Src=""~/siteadmin/_Controls/Pages/ContentSearch.ascx"" TagName=""ContentSearch"" TagPrefix=""cms"" %>" & vbCrLf
					pageInstruction += "<asp:Content ID=""page_search"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
					pageInstruction += "    <cms:ContentSearch runat=""server"" ID=""ctlSearchPages"" />" & vbCrLf
					pageInstruction += "</asp:Content>"
					fileContent = BytesOf(pageInstruction)

				Case "contentmap.aspx"
					Dim pageInstruction As String = "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""Content Map"" Debug=""true"" %>" & vbCrLf
					pageInstruction += "<%@ Register Src=""~/siteadmin/_Controls/Pages/ContentMap.ascx"" TagName=""ContentMap"" TagPrefix=""cms"" %>" & vbCrLf
					pageInstruction += "<asp:Content ID=""page_search"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
					pageInstruction += "    <cms:ContentMap runat=""server"" ID=""ctlSearchPages"" />" & vbCrLf
					pageInstruction += "</asp:Content>"
					fileContent = BytesOf(pageInstruction)

				Case Else
					Dim Pages As New WDK.ContentManagement.Pages.Manager
					If Pages IsNot Nothing Then
						Dim thisPage As WDK.ContentManagement.Pages.PageType = Pages.getPageByFilename(filename.ToLower)
						If thisPage IsNot Nothing Then
							Pages.UpdateHits(filename.ToLower)

							If HttpContext.Current.Request("mode") <> "" AndAlso HttpContext.Current.Request("mode").ToLower = "plain" Then
								fileContent = BytesOf(thisPage.content.ToString)
							Else
								'- Render page based on Master Page selected
								Dim pageInstruction As String = "<%@ Page Language=""VB"" MasterPageFile="""

								If thisPage.masterPage.ToString <> "" Then
									If System.IO.File.Exists(HttpContext.Current.Request.MapPath(thisPage.masterPage.Replace("~", ""))) Then
										pageInstruction += thisPage.masterPage.ToString
									Else
										pageInstruction += MasterPageUrl()
									End If
								Else

								End If

                                pageInstruction += """ Title=""Untitled Page"" Debug=""true"" %>" + vbCrLf

                                pageInstruction += "<script runat=""server"">" + vbCrLf
                                pageInstruction += "    'Generated with Skitsanos VFS.Pages plugin for SiteAdmin CMS" + vbCrLf + vbCrLf
                                pageInstruction += "    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)" + vbCrLf
                                pageInstruction += "        Page.Title=""" + thisPage.Title.ToString + """" + vbCrLf

                                pageInstruction += "        Dim metaKeywords As New HtmlMeta" + vbCrLf
                                pageInstruction += "        metaKeywords.Name = ""keywords""" + vbCrLf
                                pageInstruction += "        metaKeywords.Content = """ + thisPage.Metas.Keywords + """" + vbCrLf

                                pageInstruction += "        Dim metaDescription As New HtmlMeta" + vbCrLf
                                pageInstruction += "        metaDescription.Name = ""description""" + vbCrLf
                                pageInstruction += "        metaDescription.Content =""" + thisPage.Metas.Description + """" + vbCrLf

                                pageInstruction += "        Page.Header.Controls.Add(metaKeywords)" + vbCrLf
                                pageInstruction += "        Page.Header.Controls.Add(metaDescription)" + vbCrLf
                                pageInstruction += "    End Sub" + vbCrLf
                                pageInstruction += "</script>" + vbCrLf

								'dump(pageInstruction)

								Select Case thisPage.type
									Case ContentManagement.Pages.PageContentType.RSS
										pageInstruction += "<%@ Register Src=""~/siteadmin/_Controls/pages-rsspage.ascx"" TagName=""pagesRssPage"" TagPrefix=""cms"" %>" & vbCrLf
										pageInstruction += "<asp:Content ID=""pageRssContentBody"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
										pageInstruction += "    <cms:pagesRssPage runat=""server"" ID=""ctlRssPage"" Url=""" & thisPage.content.ToString.Replace("{RSS:}", "") & """ />" & vbCrLf
										pageInstruction += "</asp:Content>"

									Case ContentManagement.Pages.PageContentType.HTML
                                        pageInstruction += "<asp:Content ID=""siteadminContent1"" ContentPlaceHolderID=""contentBody"" runat=""server"">"
										pageInstruction += thisPage.content
										pageInstruction += "</asp:Content>"

									Case ContentManagement.Pages.PageContentType.Text
                                        pageInstruction += "<asp:Content ID=""siteadminContent1"" ContentPlaceHolderID=""contentBody"" runat=""Server"">"
										pageInstruction += thisPage.content.ToString.Replace(Chr(10), "<br />")
										pageInstruction += "</asp:Content>"
								End Select


								fileContent = BytesOf(pageInstruction)
							End If
						Else
							fileContent = ResourceNotFound(IO.Path.GetFileNameWithoutExtension(filename).ToUpper)
						End If
					End If
			End Select
		End If

		Return fileContent
	End Function
End Class