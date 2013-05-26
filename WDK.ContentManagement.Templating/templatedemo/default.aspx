<%@ Page language="c#" Codebehind="default.aspx.cs" AutoEventWireup="false" Inherits="templatedemo.MainPage" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>default</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="styles/evolve.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <P class="maintitle">Evolve Template Engine 1.0.4 - Demo Application</P>
      <P>This web application provides you with a few samples that show how to use the 
        template engine.</P>
      <P class="subtitle1">What is it?</P>
      <P>This template engine for ASP.NET 1.1 allows you to render the contents of your 
        ASP.NET web forms as parts of a Master Page (template). It's extremely easy to 
        use and provides full designer support. The engine takes a new (component 
        based) approach regarding the separation of ASP.NET templates and web forms and 
        makes it extremely easy to get your templates working. Unlike other solutions, 
        it prevents you from scattering additional HTML all over your web application 
        and enforces a clean separation of design and development. Source code is 
        included. Enjoy!</P>
      <P class="subtitle1">Samples:</P>
      <UL>
        <LI>
        Sample 1: Simple Rendering: That's what you are looking at right now :-)
        <LI>
          <A href="templateinitialization/samplepage.aspx">Sample 2</A>: Template 
          initialization: <EM>BeforeTemplating</EM> / <EM>AfterTemplating</EM>
        <LI>
          <A href="communicationsample/parent.aspx">Sample 3</A>: Handle communication 
        between&nbsp;master page, templates and and nested controls
        <LI>
          <A href="nestedtemplates/samplepage.aspx">Sample 4</A>: Nested templates: 
        Shows how to handle templates/regions that are contained in other templates.
        <LI>
          <A href="templateexchange/default.aspx">Sample 5</A>: Switch templates at 
          runtime and render manually.
          <BR>
        </LI>
      </UL>
      <P>
        <asp:Image id="Image1" runat="server" ImageUrl="img/company.jpg"></asp:Image></P>
    </form>
  </body>
</HTML>
