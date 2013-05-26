<%@ Control Language="c#" AutoEventWireup="false" Codebehind="AlternativeTemplate.ascx.cs" Inherits="templatedemo.templates.AlternativeTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="cc1" Namespace="Evolve.Portals.Framework" Assembly="Evolve.Portals.TemplateEngine" %>
<table class="maintable" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
  id="Table1">
  <tr>
    <td class="header" style="FONT-WEIGHT:bold;FONT-SIZE:16px;VERTICAL-ALIGN:middle;BACKGROUND-COLOR:white"><cc1:RegionPlaceHolder id="MenuPlaceHolder" runat="server" RegionId="Left1"></cc1:RegionPlaceHolder>Template 
      Engine</td>
  </tr>
  <tr>
    <td height="20">&nbsp;</td>
  </tr>
  <tr>
    <td valign="top">
      <table width="100%" height="100%" cellpadding="0" cellspacing="0" border="0" id="Table5">
        <tr>
          <td valign="top" width="150" class="menuitem" style="BACKGROUND-COLOR:#e6ebee">
            <asp:HyperLink id="HomeLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Home</asp:HyperLink><BR>
            <asp:HyperLink id="CompanyLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Company</asp:HyperLink><BR>
            <asp:HyperLink id="ProjectLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Projects</asp:HyperLink><BR>
            <asp:HyperLink id="TechnologyLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Technology</asp:HyperLink><BR>
            <asp:HyperLink id="DownloadLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Downloads</asp:HyperLink><BR>
            <asp:HyperLink id="LoginLink" runat="server" NavigateUrl="~/default.aspx" Target="_self">Login</asp:HyperLink>
          <td vAlign="top" class="contentcell" style="VERTICAL-ALIGN:top"><cc1:RegionPlaceHolder id="ContentPlaceHolder" runat="server" RegionId="Content"></cc1:RegionPlaceHolder></td>
          <td vAlign="top" class="menupane" width="150"></td>
        </tr>
      </table>
    </td>
  </tr>
</table>
