Imports System.Data
Imports System.io

Imports System.Security.Permissions

Imports System.Web
Imports System.Web.Caching
Imports System.Web.Hosting

<AspNetHostingPermission(SecurityAction.Demand, Level:=AspNetHostingPermissionLevel.Minimal), _
   AspNetHostingPermission(SecurityAction.InheritanceDemand, level:=AspNetHostingPermissionLevel.Minimal)> _
Public Class WdkVirtualFile
    Inherits VirtualFile

#Region " New "
    Public Sub New(ByVal virtualPath As String)
        MyBase.New(virtualPath)
    End Sub
#End Region

#Region " Open "
    Public Overrides Function Open() As System.IO.Stream
        Dim fileContent As Byte() = Nothing

        Try
            Dim checkPath As String = VirtualPathUtility.ToAppRelative(VirtualPath)

            Dim assembly As System.Reflection.Assembly = System.Reflection.Assembly.GetExecutingAssembly()
            Dim ns As String = "Wdk.IO"

            Dim cmdArr As String() = checkPath.Split("/")
            If cmdArr.Length > 1 Then
                Dim cmd As String = cmdArr(1)
                Select Case cmd.ToLower
                    Case "system"
                        Dim contentPath As String = Mid(Me.VirtualPath, Me.VirtualPath.IndexOf("/system/") + 9)

                        Select Case Me.Name.ToLower
                            Case "odbc.aspx"
                                fileContent = GeOdbcPage()

                            Case "resources.aspx"
                                fileContent = ListEmbeddedResources()

                            Case "template.aspx", "storexml.aspx"
                                Return assembly.GetManifestResourceStream(ns & "." & Me.Name.ToLower)

                            Case "login.aspx"
                                Dim pageInstruction As String = "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""Login"" Debug=""true"" %>" & vbCrLf
                                pageInstruction += "<%@ Register Src=""~/siteadmin/_Controls/netpass-login.ascx"" TagName=""netpassLogin"" TagPrefix=""cms"" %>" & vbCrLf
                                pageInstruction += "<asp:Content ID=""page_search"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
                                pageInstruction += "    <cms:netpassLogin runat=""server"" ID=""ctlnetpassLogin1"" />" & vbCrLf
                                pageInstruction += "</asp:Content>"
                                fileContent = BytesOf(pageInstruction)

                            Case Else
                                If System.IO.File.Exists(HttpContext.Current.Request.MapPath("/siteadmin/DefaultSite/" + Me.Name)) Then
                                    Return System.IO.File.OpenRead(HttpContext.Current.Request.MapPath("/siteadmin/DefaultSite/" + Me.Name))
                                Else
                                    fileContent = ResourceNotFound(Path.GetFileNameWithoutExtension(Me.Name).ToUpper)
                                End If
                                'Dim s As Stream = assembly.GetManifestResourceStream(ns & "." & Me.Name.ToLower)
                                'If s Is Nothing Then
                                '    
                                'Else
                                '    Return s
                                'End If
                        End Select
                        '
                        '   /eNews
                        '
                    Case "enews"
                        Select Case Me.Name.ToLower
                            Case "subscribe.aspx"
                                Dim pageInstruction As String = "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""Subscribe to newsletter"" Debug=""true"" %>" & vbCrLf
                                pageInstruction += "<%@ Register Src=""~/siteadmin/_Controls/enews-subscribe.ascx"" TagName=""enewsSubscribe"" TagPrefix=""cms"" %>" & vbCrLf
                                pageInstruction += "<asp:Content ID=""page_enews_subscribe"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
                                pageInstruction += "    <cms:enewsSubscribe runat=""server"" ID=""ctlenewsSubscribe"" />" & vbCrLf
                                pageInstruction += "</asp:Content>"
                                fileContent = BytesOf(pageInstruction)

                            Case "unsubscribe.aspx"
                                Dim pageInstruction As String = "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""Unsubscribe from newsletter"" Debug=""true"" %>" & vbCrLf
                                pageInstruction += "<%@ Register Src=""~/siteadmin/_Controls/enews-unsubscribe.ascx"" TagName=""enewsUnsubscribe"" TagPrefix=""cms"" %>" & vbCrLf
                                pageInstruction += "<asp:Content ID=""page_enews_unsubscribe"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
                                pageInstruction += "    <cms:enewsUnsubscribe runat=""server"" ID=""ctlenewsUnsubscribe"" />" & vbCrLf
                                pageInstruction += "</asp:Content>"
                                fileContent = BytesOf(pageInstruction)

                        End Select

                    Case "faq"
                        Dim content As String = ""
                        Dim faqMasterPage As String = Providers.Settings.Get("faqMasterPage")
                        If faqMasterPage Is Nothing Then
                            faqMasterPage = MasterPageUrl()
                        Else
                            faqMasterPage = "~/" + faqMasterPage + ".master"
                        End If

                        Dim pageInstruction As String = "<%@ Page Language=""VB"" MasterPageFile=""" & faqMasterPage & """ Title=""Questions and Answers"" Debug=""true"" %>" & vbCrLf
                        pageInstruction += "<%@ Register Src=""~/siteadmin/_Controls/Faq/topics.ascx"" TagName=""FaqTopics"" TagPrefix=""cms"" %>" & vbCrLf
                        pageInstruction += "<asp:Content ID=""topics"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
                        pageInstruction += " <cms:FaqTopics runat=""server"" ID=""ctlFaqTopicsContent"" />" & vbCrLf
                        pageInstruction += "</asp:Content>"
                        fileContent = BytesOf(pageInstruction)

                    Case "content"
                        If System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "bin\VFS.Content.dll") = False Then
                            fileContent = BytesOf("There is an error occured during content rendering. VFS.Content.dll is missing")
                        Else
                            'Dim pages As Object = Activator.CreateInstance("VFS.Content", "VFS.Pages.Manager")
                            Dim pages As Object = Activator.CreateInstance(Type.GetType("VFS.Pages.Manager, VFS.Content"))
                            If pages IsNot Nothing Then
                                fileContent = pages.Render(Me.Name)
                            Else
                                fileContent = BytesOf("There is an error occured during content rendering.")
                            End If
                        End If

                    Case "files"
                        Dim aspContent As String = "<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""File Download"" Debug=""true"" %>" & vbCrLf
                        aspContent += "<%@ Register Src=""~/siteadmin/_Controls/FileSharing/filesDownload.ascx"" TagName=""filesDownload"" TagPrefix=""cms"" %>" & vbCrLf
                        aspContent += "<asp:Content ID=""wdk_filesharing_download"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & vbCrLf
                        aspContent += "    <cms:filesDownload runat=""server"" ID=""ctlfilesDownload"" />" & vbCrLf
                        aspContent += "</asp:Content>"
                        fileContent = BytesOf(aspContent)

                    Case Else
                        If System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "bin\VFS." + cmd + ".dll") = False Then
                            fileContent = BytesOf("There is an error occured during content rendering. VFS." + cmd + ".dll is missing")
                        Else
                            Dim mgr As Object = Activator.CreateInstance(Type.GetType("VFS." + cmd + ".Manager, VFS." + cmd, True, True))
                            If mgr IsNot Nothing Then
                                fileContent = mgr.Render(Me.Name)
                            Else
                                fileContent = BytesOf("There is an error occured during content rendering.")
                            End If
                        End If

                        'fileContent = BytesOf("Serving " & Me.Name & " on " & VirtualPathUtility.GetDirectory(Me.VirtualPath))
                End Select
            End If

        Catch ex As Exception
            Log(HttpContext.Current.Request.RawUrl & vbCrLf & " " & ex.ToString, True)

            fileContent = BytesOf("<%@ Page Language=""VB"" MasterPageFile=""" & MasterPageUrl() & """ Title=""WDK.VFS Error"" Debug=""true"" %>" & vbCrLf & _
            "<asp:Content ID=""Content1"" ContentPlaceHolderID=""contentBody"" runat=""Server"">" & _
            ex.ToString & _
            "</asp:Content>")
        End Try

        Dim stream As New MemoryStream
        stream.SetLength(fileContent.Length)
        stream.Write(fileContent, 0, fileContent.Length)
        stream.Seek(0, SeekOrigin.Begin)

        Return stream
    End Function
#End Region

End Class