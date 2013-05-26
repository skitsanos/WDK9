<%@ Register TagPrefix="cc1" Namespace="Evolve.Portals.Framework" Assembly="Evolve.Portals.TemplateEngine" %>
<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="templatedemo._default" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>default</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="../styles/evolve.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <asp:Image id="Image1" runat="server" ImageUrl="../img/company.jpg"></asp:Image>
      <cc1:HelperPanel id="HelperPanel1" runat="server" Height="34px" Width="368px">
        <P>Certainly not common but still possible: This sample triggers the template 
          engine manually as the template is determined at runtime.</P>
        <P>This is a&nbsp;quick and dirty solution - there are certainly more elegant 
          ways to do this (e.g. base classes that&nbsp;trigger the engine for all your 
          web forms)</P>
        <P>
          <asp:Button id="Button1" runat="server" Text="switch template" CssClass="button"></asp:Button></P>
      </cc1:HelperPanel>
    </form>
  </body>
</HTML>
