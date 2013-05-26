<%@ Page language="c#" Codebehind="parent.aspx.cs" AutoEventWireup="false" Inherits="Evolve.TemplateEngine.CommSample.ParentClass" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <HEAD>
    <title>communicationsample</title>
    <meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" Content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="../styles/evolve.css" type="text/css" rel="stylesheet">
  </HEAD>
  <body>
    <form id="Form1" method="post" runat="server">
      <P>This sample demonstrates how to communicate with nested controls that are 
        being added at runtime. Communication is mastered through interfaces: The page 
        implements <EM>IMessageMaster</EM>, the child controls implement <EM>IMessageClient</EM>
        and register themselves with the page during initialization. You can find all 
        sources in the <EM>communicationsample</EM> folder.</P>
      <P>&nbsp;</P>
      <P>Send message:</P>
      <P>
        <asp:TextBox id="MessageText" runat="server" CssClass="textbox" Width="200px"></asp:TextBox>
        <asp:DropDownList id="ClientList" runat="server" CssClass="textbox" Width="150px">
          <asp:ListItem Value="-1">All</asp:ListItem>
        </asp:DropDownList>
        <asp:Button id="SendMessage" runat="server" CssClass="button" Text="Send" Width="80px"></asp:Button></P>
      <P>&nbsp;</P>
      <P>Points of Interest:</P>
      <UL>
        <LI>
          The&nbsp;template overrides&nbsp;one of the new initialization methods that are 
          being available for all templates (<EM>BeforeTemplating</EM> and <EM>AfterTemplating</EM>). 
          Use these rather than the <EM>Page_Load</EM> method of the VS.NET designer and 
          access the <EM>Page</EM> object that is submitted by the template engine.</LI>
        <LI>
          As the button event handler is fired pretty early, the <EM>RegionProvider</EM> of 
          this sample works with templating time <EM>OnInit</EM> rather than the default <EM>
            OnLoad</EM>. This is because the whole object tree must be available when the 
          button event handler kicks in - <EM>OnLoad</EM> would be too late. You can 
          observe a runtime exception if you set the templating time back to <EM>OnLoad</EM>.</LI>
        <LI>
          <FONT color="#cc0000">VB.NET Users</FONT>: Unfortunately, <EM>OnInit</EM> does 
          not work with VB.NET - C# and VB.NET applications work differently (somehow 
          strange). To render the template during <EM>OnInit</EM> in VB.NET, the 
          templating time needs to be set to <EM>Manual</EM>. Then, the template can be 
          triggered from within during the <EM>Init</EM> method in the code behind.</LI></UL>
    </form>
  </body>
</HTML>
