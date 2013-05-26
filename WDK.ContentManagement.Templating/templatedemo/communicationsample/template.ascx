<%@ Register TagPrefix="uc1" TagName="MessageTable" Src="MessageTable.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="template.ascx.cs" Inherits="Evolve.TemplateEngine.CommSample.SampleTemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="epf" Namespace="Evolve.Portals.Framework" Assembly="Evolve.Portals.TemplateEngine" %>
<table class="maintable" height="100%" cellSpacing="0" cellPadding="0" width="100%" border="0"
  id="Table1">
  <tr class="header" height="20">
    <td colspan="2"><b> Page /&nbsp;Control Communication Sample</b></td>
  </tr>
  <tr valign="top">
    <td class="contentcell" style="padding-right:10px;">
      <P>
        <epf:RegionPlaceHolder id="RegionPlaceHolder1" runat="server" RegionId="Content"></epf:RegionPlaceHolder></P>
    </td>
    <td width="200" style="PADDING-LEFT: 10px; BORDER-LEFT: 1px solid #CBD0D4">
      <P>
        <uc1:MessageTable id="MessageTable1" runat="server"></uc1:MessageTable></P>
      <P>
        <uc1:MessageTable id="MessageTable2" runat="server"></uc1:MessageTable></P>
      <P>
        <uc1:MessageTable id="MessageTable3" runat="server"></uc1:MessageTable></P>
    </td>
  </tr>
  <tr height="20" class="footer">
    <td colspan="2">My master page told me:
      <asp:Label id="Message" runat="server" ForeColor="Crimson">(nothing sent yet)</asp:Label></td>
  </tr>
</table>
