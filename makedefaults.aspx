<%@ Page Language="VB" Debug="true" %>

<%@ Import Namespace="System.Web.Configuration" %>

<script runat="server">
    
	Public Function appHome() As String
		Return AppDomain.CurrentDomain.BaseDirectory
	End Function

	Public Function urlHome() As String
		Return "http://" & System.Web.HttpContext.Current.Request.Url.Host & System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
	End Function

	Public Function GetApplicationName() As String
		Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
		Dim mSection As New MembershipSection
		mSection = conf.GetSection("system.web/membership")

		Dim appName As String = mSection.Providers(mSection.DefaultProvider).Parameters("applicationName")
		If appName = "" Then
			appName = System.Web.HttpContext.Current.Request.Url.Host

			If System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath <> "/" Then _
			appName += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
		End If

		Return appName
	End Function
    
	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
		If Page.IsPostBack = False Then
			lblInstance.Text = GetApplicationName()
			txtUsername.Text = "admin"
			txtEmail.Text = "info@" & GetApplicationName()
		End If
	End Sub

	Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		Try
			Dim Roles As New WDK.CommunityServices.Membership.Roles
			Dim Users As New WDK.CommunityServices.Membership.Users
    
			If Roles.Exists("Administrator") = False Then Roles.CreateRole("Administrator")
			If Roles.Exists("User") = False Then Roles.CreateRole("User")
            
			'- Generate Password
			Dim Password As String = "admin"
    
			If Users.Exists(txtUsername.Text) = False Then
				Users.Add(txtUsername.Text, txtEmail.Text, Password, "Color", "Black")
			Else
				Users.SetPropertyById(Users.GetIdByEmail(Users.GetUser(txtUsername.Text, False).Email), "AccountPassword", Password)
			End If
        
			If Roles.IsUserInRole(txtUsername.Text, "Administrator") = False Then Roles.AddUserToRole(txtUsername.Text, "Administrator")
                                    
			tableForm.Visible = False
			btnSave.Visible = False
            
			Dim msg As String = "Your SiteAdmin CMS now ready to use. In order to login please use: <br/>"
			msg += "Username: " + txtUsername.Text + "<br/>"
			msg += "Password: " + Password + "<br/>"
			msg += "URL: " + urlHome() + "siteadmin/"
            
			Status.Text = "Defaults are stored. Now you can <a href=""" + urlHome() + "siteadmin/"">login</a> For your information default secret question/answer is: <i>Color/Black</i>."
    
		Catch ex As Exception
			Status.Text = "<b>There is an error occured during creating membership defaults:</b><br />" + ex.ToString
		End Try
	End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Membership defaults</title>

	<script type="text/javascript" src="http://res.mywdk.com/jquery/jquery-1.3.1.min.js"></script>

	<script type="text/javascript" src="http://res.mywdk.com/jquery/jquery.roundCorners.js"></script>

	<style type="text/css">
		* { font-family: Arial; }
		body { background-color: #ECF1EF; }
		h1 { color: #999999; font-size: 20px; }
		td { font-size: 11px; }
		hr {border: none; border-bottom: solid 1px #EBECE4; margin-top:10px; margin-bottom: 15px;}
		.main { background-color: #FDFDF0; border: solid 2px #EBECE4; padding: 10px; width: 500px; margin-top: 100px; font-size: 11px; text-align: left; }
		.navbar { border: solid 1px #EBECE4; background-color: #FEFFF1; padding: 10px; }
		.fieldname { font-weight: bold; }
	</style>

	<script type="text/javascript">
		$(document).ready(function() {
			$('.rounded').roundCorners();
		})
	</script>

</head>
<body>
	<form runat="server" id="form1">
	<center>
		<div class="rounded main">
			<h1>
				Membership Defaults</h1>
			This wizard will create default administrative credentials for your application space running by SiteAdmin CMS.
			<table width="100%" border="0" cellpadding="3" cellspacing="0" runat="server" id="tableForm">
				<tr>
					<td class="fieldname">
						Instance:
					</td>
					<td class="Status">
						<asp:Label ID="lblInstance" runat="server" />
					</td>
				</tr>
				<tr>
					<td class="fieldname">
						Username:
					</td>
					<td>
						<asp:TextBox CssClass="field" ID="txtUsername" runat="server">admin</asp:TextBox>
						<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtUsername" ErrorMessage="* Username required"></asp:RequiredFieldValidator>
					</td>
				</tr>
				<tr>
					<td class="fieldname">
						Password:
					</td>
					<td>
						admin&nbsp;
					</td>
				</tr>
				<tr>
					<td class="fieldname">
						Email:
					</td>
					<td>
						<asp:TextBox CssClass="field" ID="txtEmail" runat="server" />
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmail" ErrorMessage="* Email required"></asp:RequiredFieldValidator>
					</td>
				</tr>
			</table>
			<hr />
			<div class="navbar rounded">
				<asp:Button ID="btnSave" Text="Submit" runat="server" OnClick="btnSave_Click" />
				<br />
				<asp:Literal runat="server" ID="Status" />
			</div>
		</div>
	</center>
	</form>
</body>
</html>
