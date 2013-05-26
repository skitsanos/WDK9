<%@ Page language="c#" Codebehind="Example4.aspx.cs" AutoEventWireup="false" Inherits="CollectionPagerExample.Example4" %>
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
		<H3 id="DIV1" style="TEXT-ALIGN: left" runat="server">Repeater and Pager on Same 
			Page [<A href="Default.aspx">back to menu</A>]</H3>
		<P style="TEXT-ALIGN: left" runat="server"><STRONG><FONT color="#ff0033">SLIDER MODE!!!</FONT></STRONG>
			Watch the page numbers move as you move up the number range.&nbsp;Postback 
			mode.</P>
		<FORM id="Form1" method="post" runat="server">
			<P><CC1:COLLECTIONPAGER id="CollectionPager1" runat="server" BackNextDisplay="HyperLinks" BackNextLocation="Right"
					BackText="« Back" ShowFirstLast="True" ResultsLocation="Top" PagingMode="PostBack" MaxPages="50"></CC1:COLLECTIONPAGER></P>
			<ASP:REPEATER id="Repeater1" runat="server">
				<HEADERTEMPLATE>
					<TABLE BORDER="1" CELLPADDING="3" CELLSPACING="0">
						<TR>
							<TH>
								Column1</TH>
							<TH>
								Column2</TH>
							<TH>
								Column3</TH>
							<TH>
								Column4</TH>
							<TH>
								Column5</TH>
						</TR>
					</ASP:LABEL>
				</HEADERTEMPLATE>
				<ITEMTEMPLATE>
					<TR>
						<TD><%# DataBinder.Eval(Container.DataItem, "Column1") %></TD>
						<TD><%# DataBinder.Eval(Container.DataItem, "Column2") %></TD>
						<TD><%# DataBinder.Eval(Container.DataItem, "Column3") %></TD>
						<TD><%# DataBinder.Eval(Container.DataItem, "Column4") %></TD>
						<TD><%# DataBinder.Eval(Container.DataItem, "Column5") %></TD>
					</TR>
				</ITEMTEMPLATE>
				<FOOTERTEMPLATE>
					</TABLE>
				</FOOTERTEMPLATE>
			</ASP:REPEATER><asp:button id="Button1" runat="server" Text="Button"></asp:button>
			<P>&nbsp;</P>
			<P>
			<P></P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
		</FORM>
	</BODY>
</HTML>
