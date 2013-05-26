<%@ Page language="c#" Codebehind="IISServers.aspx.cs" AutoEventWireup="false" Inherits="IISManagerSample.IISServers" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>IISServers</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<div align="center" class="title">This sample show how you can use the <a href="http://www.realcomponents.com/" target="_blank">
					RealComponents</a> IIS Manager component</div>
			<br>
			<div align="center" class="error" runat="server" id="divError"></div>
			<br>
			<table align="center" border="0">
				<tr>
					<td>
						<asp:Label id="lblWebServerName" runat="server" CssClass="title">Web Server Name :</asp:Label></td>
					<td>
						<asp:TextBox id="txtWebServerName" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="error" ErrorMessage="* Please enter the name"
							ControlToValidate="txtWebServerName" Display="Dynamic"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblWebServerRootPath" runat="server" CssClass="title">Web Server Root path :</asp:Label></td>
					<td>
						<asp:TextBox id="txtWebServerRootPath" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="error" ErrorMessage="* Please enter the path"
							ControlToValidate="txtWebServerRootPath" Display="Dynamic"></asp:RequiredFieldValidator></td>
				</tr>
				<TR>
					<TD align="center" colSpan="2">
						<asp:Button id="btnAdd" runat="server" Text="Add new" CssClass="title"></asp:Button></TD>
				</TR>
			</table>
			<br>
			<asp:DataGrid id="grdServers" runat="server" AutoGenerateColumns="False" Width="90%" BorderColor="Teal"
				BorderWidth="1px">
				<HeaderStyle HorizontalAlign="Center" CssClass="title"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<asp:LinkButton id=lnkName CausesValidation="False" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.name") %>' OnClick=lnkName_Click>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Path">
						<ItemTemplate>
							<asp:Label id=lblPath runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.path") %>' CssClass="text">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Status">
						<ItemTemplate>
							<asp:Label id=lblStatus runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.status") %>' CssClass="text">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id=lnkStop CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' onclick=lnkStop_Click>Stop</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="lnkStart" CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' onclick=lnkStart_Click>Start</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="lnkPause" CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' onclick=lnkPause_Click>Pause</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="lnkDelete" CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.id") %>' onclick=lnkDelete_Click>Delete</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
		</form>
	</body>
</HTML>
