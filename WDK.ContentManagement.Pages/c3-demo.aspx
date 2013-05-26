<%@ Page Language="VB" Debug="true" %>
<%@ Import Namespace="System.Xml" %>

<script runat="server">
    Dim c3 As New Wdk.ContentManagement.Pages.ContentTree
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        
        c3.CreateNode("/", "Products")
        c3.SetProperty("/Products/", "masterPage", "~/default.master")
        
        c3.CreateNode("/Products", "WDK")
        c3.CreateNode("/WDK", "Knowledge Base")
        
        c3.CreateNode("/", "Settings")
        
        c3.CreateNode("/", "My Drives")
        c3.CreateNode("/My Drives", "WDK VFS")
        
        c3.CreateNode("/WDK VFS", "Content")
        c3.CreateNode("/WDK VFS", "Resources")
        c3.CreateNode("/WDK VFS", "System")
        
        c3.CreateNode("/My Drives", "Box.Net")
        
        PushBoxNet()
                
        '- bind data tree into component
        Dim xmlDs As New XmlDataSource
        xmlDs.Data = c3.Document.OuterXml
        
        Dim nodeBinding As New TreeNodeBinding
        nodeBinding.DataMember = "Content"
        nodeBinding.TextField = "title"
        nodeBinding.ValueField = "title"
        
        treeMenu.DataSource = xmlDs
        treeMenu.DataBindings.Add(nodeBinding)
        treeMenu.DataBind()
        
        'Response.Clear()
        'Response.ContentType = "text/xml"
        'Response.Write(c3.Document.OuterXml)
    End Sub
    
    Sub PushBoxNet()
        c3.CreateNode("/Box.Net", "Signup")
        Dim bm As Box.net.API.BoxManager = Box.net.API.BoxManager.Current
        bm.Login("skitsanos@gmail.com", "beshamaim")
        
        If bm.User.IsAuthenticated Then
            Dim bi As New Box.net.API.BoxFolder
            bm.LoadFolderList(bi)
            'Dim newFolder As New Box.net.API.BoxFolder
            
            'bm.CreateFolder(Nothing, "Sample")
            
            Response.Write("found " & bi.Folders.Count)
        
            For Each fld As Box.net.API.BoxFolder In bi.Folders
                Response.Write("folder: " & fld.Name)
            Next
        End If
        
        bm.Logout()
    End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>contentTree Demo</title>
    <style type="text/css">
        *
        {
            font-family:Calibri;
            font-size:x-small;
        }
        body
            {
                margin:0 0 0 0;
                background-color:#F7F6F3;
            }
        .menu
            {
                display:block;
                font-size:x-small;
            }
    </style>
</head>
<body>
    <form id="c3Form" runat="server">
        <table width="100%">
            <tr>
                <td>
                    <asp:Menu ID="Menu1" runat="server" Orientation="Horizontal" CssClass="menu">
                        <Items>
                            <asp:MenuItem Text="File" Value="File">
                                <asp:MenuItem Text="Exit" Value="Exit"></asp:MenuItem>
                            </asp:MenuItem>
                            <asp:MenuItem Text="Help" Value="Help">
                                <asp:MenuItem Text="About" Value="About"></asp:MenuItem>
                            </asp:MenuItem>
                        </Items>
                        <StaticMenuStyle BackColor="WhiteSmoke" BorderColor="White" BorderWidth="1px" HorizontalPadding="3px"
                            VerticalPadding="3px" />
                        <StaticMenuItemStyle BorderColor="White" BorderWidth="1px" Font-Size="X-Small" ForeColor="#404040"
                            HorizontalPadding="3px" VerticalPadding="3px" />
                        <StaticSelectedStyle BackColor="AliceBlue" />
                        <StaticHoverStyle BackColor="AliceBlue" BorderColor="White" BorderWidth="1px" ForeColor="SteelBlue" />
                        <StaticItemTemplate>
                            <%# Eval("Text") %>
                        </StaticItemTemplate>
                    </asp:Menu>
                </td>
                <td style="text-align:right;">
                    <asp:LoginStatus ID="LoginStatus1" runat="server" />
                </td>
            </tr>
        </table>
        <br />
        <asp:TreeView ID="treeMenu" runat="server" BackColor="#F7F6F3" ShowLines="True" Width="200px"
            ExpandDepth="1">
        </asp:TreeView>
        
        <hr />
        
    </form>
</body>
</html>
