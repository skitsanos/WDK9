Imports System.Web
Imports System.Security.Permissions

<AspNetHostingPermission(SecurityAction.Demand, Level:=AspNetHostingPermissionLevel.Minimal), _
   AspNetHostingPermission(SecurityAction.InheritanceDemand, level:=AspNetHostingPermissionLevel.Minimal)> _
Public Class Manager
    Public Function Render(ByVal filename As String) As Byte()
        Dim fileContent As Byte() = Nothing

        Select Case filename.ToLower
            Case "rss.aspx"
                If System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "bin\WDK.ContentManagement.Events.dll") = False Then
                    fileContent = BytesOf(filename & " can't be served. WDK.ContentManagement.Events.dll is missing from /bin")
                Else
                    fileContent = BytesOf(RenderEventsFeed)
                End If

            Case Else
                If System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "bin\WDK.ContentManagement.Events.dll") = False Then
                    fileContent = BytesOf(filename & " can't be served. WDK.ContentManagement.Events.dll is missing from /bin")
                Else
                    Dim Events As New Wdk.ContentManagement.Events.Manager
                    Dim thisPage As WDK.ContentManagement.Events.EventType = Events.getItem(CInt(filename.ToLower.Replace(".aspx", "")))

                    If thisPage IsNot Nothing Then
                        Events.updateHits(CInt(filename.ToLower.Replace(".aspx", "")))

                        '- Render page based on Master Page selected
                        thisPage.title = thisPage.title.Replace(Chr(34), Chr(34) + Chr(34))

                        Dim aspx As String = "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""Untitled Page"" Debug=""true"" %>" & vbCrLf
                        aspx += "<script runat=""server"">" & vbCrLf
                        aspx += "Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)" & vbCrLf
                        aspx += "    Page.Title=""" & thisPage.title.ToString & """" & vbCrLf
                        aspx += "End Sub" & vbCrLf
                        aspx += "</script>" & vbCrLf

                        aspx += "<asp:Content ID=""Content1"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" + thisPage.content.ToString + "</asp:Content>"

                        fileContent = Text.Encoding.UTF8.GetBytes(aspx)

                    End If
                End If
        End Select

        Return fileContent
    End Function

End Class
