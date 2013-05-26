<%@ Register TagPrefix="uc1" TagName="nestedtemplate" Src="nestedtemplate.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="maintemplate.ascx.cs" Inherits="templatedemo.nestedtemplates.maintemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="epf" Namespace="Evolve.Portals.Framework" Assembly="Evolve.Portals.TemplateEngine" %>
<div class="maintable">
  <div class="header">
    <epf:RegionPlaceHolder id="HeaderPlaceHolder" runat="server" RegionId="Top"></epf:RegionPlaceHolder></div>
  <P>This text block (white background)&nbsp;belongs to the main template.</P>
  <uc1:nestedtemplate id="Nestedtemplate1" runat="server"></uc1:nestedtemplate>
</div>
