<%@ Control Language="c#" AutoEventWireup="false" Codebehind="initializationtemplate.ascx.cs" Inherits="templatedemo.templateinitialization.initializationtemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="epf" Namespace="Evolve.Portals.Framework" Assembly="Evolve.Portals.TemplateEngine" %>
<div class="contentcell">
  <P><epf:regionplaceholder id="RegionPlaceHolder1" runat="server" RegionId="Content"></epf:regionplaceholder></P>
  <P><U>Before Templating:</U></P>
  <P>
    <table cellSpacing="0" cellPadding="0" border="0">
      <tr>
        <td width="200">Page property:</td>
        <td width="200"><asp:label id="PageBeforeTemplating" runat="server"></asp:label></td>
      </tr>
      <tr>
        <td>Submitted page parameter:</td>
        <td><asp:label id="ParameterBefore" runat="server"></asp:label></td>
      </tr>
      <tr>
        <td>Controls:</td>
        <td><asp:label id="ChildsBeforeTemplating" runat="server"></asp:label></td>
      </tr>
    </table>
  </P>
  <P><U><BR>
      After Templating:</U></P>
  <P>
    <table border="0" cellpadding="0" cellspacing="0">
      <tr>
        <td width="200">Page property:</td>
        <td width="200"><asp:label id="PageAfterTemplating" runat="server"></asp:label></td>
      </tr>
      <tr>
        <td>Submitted page parameter:</td>
        <td><asp:label id="ParameterAfter" runat="server"></asp:label></td>
      </tr>
      <tr>
        <td>Controls:</td>
        <td><asp:label id="ChildsAfterTemplating" runat="server"></asp:label></td>
      </tr>
    </table>
  </P>
  <P><U><BR>
      Page_Load</U></P>
  <P>
    <asp:Label id="PageLoadStatus" runat="server">NOT CALLED</asp:Label></P>
</div>
