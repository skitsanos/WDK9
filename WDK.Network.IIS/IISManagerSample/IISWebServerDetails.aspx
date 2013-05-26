<%@ Page language="c#" Codebehind="IISWebServerDetails.aspx.cs" AutoEventWireup="false" Inherits="IISManagerSample.IISWebServerDetails" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>IISWebServerDetails</title>
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
			<div align="center" class="title" runat="server" id="divServerName">Virtual 
				Directories for {0}</div>
			<br>
			<table align="center" border="0">
				<tr>
					<td>
						<asp:Label id="lblWebVirtDirName" runat="server" CssClass="text">Web Virtual Directory Name :</asp:Label></td>
					<td>
						<asp:TextBox id="txtVirtualDirectoryName" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator2" runat="server" CssClass="error" ErrorMessage="* Please enter the name"
							ControlToValidate="txtVirtualDirectoryName" Display="Dynamic"></asp:RequiredFieldValidator></td>
				</tr>
				<tr>
					<td>
						<asp:Label id="lblWebVirtDirRootPath" runat="server" CssClass="text">Web Virtual Directory Root path :</asp:Label></td>
					<td>
						<asp:TextBox id="txtVirtualDirectoryRootPath" runat="server"></asp:TextBox>
						<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" CssClass="error" ErrorMessage="* Please enter the path"
							ControlToValidate="txtVirtualDirectoryRootPath" Display="Dynamic"></asp:RequiredFieldValidator></td>
				</tr>
				<TR>
					<TD>
						<asp:Label id="lblVirtDirApp" runat="server" CssClass="text">Create Web Application</asp:Label></TD>
					<TD>
						<asp:CheckBox id="chkApplication" runat="server"></asp:CheckBox></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="2">
						<asp:Button id="btnAdd" runat="server" Text="Add new" CssClass="title"></asp:Button></TD>
				</TR>
			</table>
			<br>
			<asp:DataGrid id="grdVirtualDirectories" runat="server" AutoGenerateColumns="False" Width="98%"
				BorderColor="Teal" BorderWidth="1px" HorizontalAlign="Center">
				<HeaderStyle HorizontalAlign="Center" CssClass="title"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<asp:LinkButton CausesValidation=False id=lnkName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.name") %>' onclick=lnkName_Click>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Path">
						<ItemTemplate>
							<asp:Label id=lblPath runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.path") %>' CssClass="text">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Web Application">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblStatus runat="server" CssClass="text" Text='<%# DataBinder.Eval(Container, "DataItem.isApplication") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="lnkCreateApplication" CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.name") %>' onclick=lnkCreateApplication_Click>&nbsp;Create App&nbsp;</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="lnkDeleteApplication" CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.name") %>' onclick=lnkDeleteApplication_Click>&nbsp;Delete App&nbsp;</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center" Wrap="False"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="lnkRestartApplication" CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.name") %>' onclick=lnkRestartApplication_Click>&nbsp;Restart App&nbsp;</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:LinkButton id="lnkDelete" CausesValidation="False" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "DataItem.name") %>' onclick=lnkDelete_Click>&nbsp;Delete&nbsp;</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<br>
			<asp:Button CausesValidation="False" id="btnBack" runat="server" CssClass="title" Text="<< Back"></asp:Button>
			<br>
			<div align="center" class="title" runat="server" id="divWebServerName">Web 
				Directories for {0}</div>
						<br>
			<asp:DataGrid id="grdWebDirectories" runat="server" AutoGenerateColumns="False" Width="98%"
				BorderColor="Teal" BorderWidth="1px" HorizontalAlign="Center">
				<HeaderStyle HorizontalAlign="Center" CssClass="title"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="Name">
						<ItemTemplate>
							<asp:LinkButton CausesValidation="False" id=lnkWebName runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>' CommandArgument='<%# DataBinder.Eval(Container, "DataItem.name") %>' onclick=lnkWebName_Click>
							</asp:LinkButton>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Path">
						<ItemTemplate>
							<asp:Label id=lblWebPath runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.path") %>' CssClass="text">
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Web Application">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label id=lblWebStatus runat="server" CssClass="text" Text='<%# DataBinder.Eval(Container, "DataItem.isApplication") %>'>
							</asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid>
			<br>
			<asp:Button CausesValidation="False" id="btnReturn" runat="server" CssClass="title" Text="<< Return"></asp:Button>
		</form>
	</body>
</HTML>
