<%@ Control Language="c#" AutoEventWireup="false" Codebehind="WebUserControl1.ascx.cs" Inherits="CollectionPagerExample.WebUserControl1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<div style="BORDER: 1px dashed;PADDING:10px;background:#eeeeee">
	<ASP:REPEATER id="RepeaterInUC" runat="server">
		<ItemTemplate>
			<TR>
				<TD><%# DataBinder.Eval(Container.DataItem, "Column1") %></TD>
				<TD><%# DataBinder.Eval(Container.DataItem, "Column2") %></TD>
				<TD><%# DataBinder.Eval(Container.DataItem, "Column3") %></TD>
				<TD><%# DataBinder.Eval(Container.DataItem, "Column4") %></TD>
				<TD><%# DataBinder.Eval(Container.DataItem, "Column5") %></TD>
			</TR>
		</ItemTemplate>
		<HeaderTemplate>
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
		</HeaderTemplate>
		<FooterTemplate>
			</TABLE>
		</FooterTemplate>
	</ASP:REPEATER>
</div>
