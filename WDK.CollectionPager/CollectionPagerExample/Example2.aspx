<%@ Register TagPrefix="UC" TagName="GridUserControl" Src="WebUserControl1.ascx" %>
<%@ Page language="c#" Codebehind="Example2.aspx.cs" AutoEventWireup="false" Inherits="CollectionPagerExample.Example2" %>
<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<TITLE>Collection Repeater Example</TITLE>
		<META content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<META content="C#" name="CODE_LANGUAGE">
		<META content="JavaScript" name="vs_defaultClientScript">
		<META content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<BODY bgColor="lightblue">
		<H3 id="DIV1" style="TEXT-ALIGN: left" runat="server">Repeater in WebUserControl 
			and Pager this page. [<A href="Default.aspx">back to menu</A>]</H3>
		<P style="TEXT-ALIGN: left" runat="server">QueryString mode (not postback)</P>
		<FORM id="Form1" method="post" runat="server">
			<P>
				<UC:GridUserControl id='MyUserControl' runat='server'></UC:GridUserControl></P>
			<P>
				<asp:button id="Button1" runat="server" Text="Button"></asp:button></P>
			<P><CC1:COLLECTIONPAGER id="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Right"
					BackText="« Back" ShowFirstLast="True" ResultsLocation="Top" PageSize="10"></CC1:COLLECTIONPAGER></P>
			<P>
			<P></P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
		</FORM>
	</BODY>
</HTML>
