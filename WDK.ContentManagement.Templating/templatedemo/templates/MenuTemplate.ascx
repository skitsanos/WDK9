<%@ Control Language="c#" AutoEventWireup="false" Codebehind="MenuTemplate.ascx.cs" Inherits="Evolve.Site.templates.MenuTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="cc1" Namespace="Evolve.Portals.Framework" Assembly="Evolve.Portals.TemplateEngine" %>
<table class="maintable" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
  id="Table1">
  <tr>
    <td class="header"><table width="100%" cellpadding="0" cellspacing="0" border="0" id="Table2">
        <tr>
          <td><asp:Image id="EvolveLogo" runat="server" ImageUrl="~/img/ci_gradient.gif" BorderStyle="None"></asp:Image></td>
          <td align="right" valign="top"></td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td height="20">
      <table class="mainmenu" cellSpacing="0" cellPadding="0" width="100%" border="0" id="Table4">
        <tr>
          <td>&nbsp;</td>
          <td class="menuitem"><asp:HyperLink id="HomeLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Home</asp:HyperLink></td>
          <td class="menuitem"><asp:HyperLink id="CompanyLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Company</asp:HyperLink></td>
          <td class="menuitem"><asp:HyperLink id="ProjectLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Projects</asp:HyperLink></td>
          <td class="menuitem"><asp:HyperLink id="TechnologyLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Technology</asp:HyperLink></td>
          <td class="menuitem"><asp:HyperLink id="DownloadLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Downloads</asp:HyperLink></td>
          <td class="menuitem"><asp:HyperLink id="LoginLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Login</asp:HyperLink></td>
        </tr>
      </table>
    </td>
  <tr>
    <td valign="top">
      <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" id="Table5">
        <tr>
          <td vAlign="top" class="menupane" width="150"><cc1:RegionPlaceHolder id="MenuPlaceHolder" runat="server" RegionId="Left1"></cc1:RegionPlaceHolder></td>
          <td vAlign="top" class="contentcell"><cc1:RegionPlaceHolder id="ContentPlaceHolder" runat="server" RegionId="Content"></cc1:RegionPlaceHolder></td>
        </tr>
      </table>
    </td>
  </tr>
  <tr>
    <td class="footer" vAlign="middle" height="20"><table border="0" cellpadding="0" cellspacing="0" width="100%" id="Table3">
        <tr>
          <td>Copyright (c) 2004 Evolve Software Technologies</td>
          <td align="right">
            <asp:Label id="UserName" runat="server">Guest</asp:Label></td>
        </tr>
      </table>
    </td>
  </tr>
</table>
