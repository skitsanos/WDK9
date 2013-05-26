<%@ Page language="c#" Codebehind="samplepage.aspx.cs" AutoEventWireup="false" Inherits="templatedemo.nestedtemplates.samplepage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>samplepage</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="../styles/evolve.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <P>The framework provides support for nested templates. If you look at this 
        sample in Visual Studio, you can observe the following:
      </P>
      <UL>
        <LI>
          The <EM>RegionProvider</EM> component is linked to the top-level template (<EM>maintemplate.ascx</EM>)
        <LI>
          The top-level template derives from the <EM>PortalTemplate</EM>
        class
        <LI>
          The top-level template contains a nested template (<EM>nestedtemplate.ascx</EM>)
        <LI>
          The nested template derives from <EM>System.Web.UI.UserControl</EM>
        <LI>
          The nested template contains the <EM>RegionPlaceHolder</EM> for the region <EM>Content</EM>
        </LI>
      </UL>
      <asp:Panel id="HeaderPanel" runat="server">
        <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
          <TR>
            <TD width="200">
              <asp:Image id="HeaderImage" runat="server" ImageUrl="../img/ci_gradient.gif"></asp:Image></TD>
            <TD>
              <asp:Label id="Label1" runat="server" CssClass="maintitle">Nested Templates Sample</asp:Label></TD>
          </TR>
        </TABLE>
      </asp:Panel>
    </form>
  </body>
</HTML>
