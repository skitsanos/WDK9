<%@ Register TagPrefix="cc1" Namespace="SiteUtils" Assembly="CollectionPager" %>
<%@ Page language="c#" Codebehind="Example.aspx.cs" AutoEventWireup="false" Inherits="CollectionPagerExample.Example1" %>
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
		<FORM id="Form2" method="post" runat="server">
			<H3 id="DIV1" style="TEXT-ALIGN: left" runat="server">Repeater and Pager on Same 
				Page [<A href="Default.aspx">back to menu</A>]</H3>
			<P style="TEXT-ALIGN: left" runat="server">QueryString mode (not postback)</P>
			<P style="TEXT-ALIGN: left" runat="server"><ASP:REPEATER id="Repeater1" runat="server">
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
				</ASP:REPEATER></P>
			<P style="TEXT-ALIGN: left" runat="server"><asp:button id="Button1" runat="server" Text="Button"></asp:button></P>
			<P><CC1:COLLECTIONPAGER id="CollectionPager1" runat="server" ResultsLocation="Top" ShowFirstLast="True"
					BackText="« Back" BackNextLocation="Right" BackNextDisplay="HyperLinks" LabelStyle="FONT-WEIGHT: bold;"
					ResultsStyle="PADDING-TOP:4px;FONT-WEIGHT: bold;" LabelText="Page:"></CC1:COLLECTIONPAGER></P>
			<P>
			<P></P>
			<P>&nbsp;</P>
			<P>&nbsp;</P>
		</FORM>
	</BODY>
</HTML>
