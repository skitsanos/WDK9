<%@ Page Language="VB"  %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

	Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
		If Not Page.IsPostBack Then
			ComputerName.Text = My.Computer.Name
			OsVersion.Text = My.Computer.Info.OSVersion
		End If
	End Sub
	
	Friend Function EncryptString(ByVal vstrTextToBeEncrypted As String, ByVal vstrEncryptionKey As String) As String
		Dim bytValue() As Byte
		Dim bytKey() As Byte
		Dim bytEncoded() As Byte = Nothing
		Dim bytIV() As Byte = {121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62}
		Dim intLength As Integer
		Dim intRemaining As Integer
		Dim objMemoryStream As New IO.MemoryStream
		Dim objCryptoStream As System.Security.Cryptography.CryptoStream
		Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged


		'   **********************************************************************
		'   ******  Strip any null character from string to be encrypted    ******
		'   **********************************************************************

		vstrTextToBeEncrypted = StripNullCharacters(vstrTextToBeEncrypted)

		'   **********************************************************************
		'   ******  Value must be within ASCII range (i.e., no DBCS chars)  ******
		'   **********************************************************************

		bytValue = Encoding.ASCII.GetBytes(vstrTextToBeEncrypted.ToCharArray)

		intLength = Len(vstrEncryptionKey)

		'   ********************************************************************
		'   ******   Encryption Key must be 256 bits long (32 bytes)      ******
		'   ******   If it is longer than 32 bytes it will be truncated.  ******
		'   ******   If it is shorter than 32 bytes it will be padded     ******
		'   ******   with upper-case Xs.                                  ****** 
		'   ********************************************************************

		If intLength >= 32 Then
			vstrEncryptionKey = Strings.Left(vstrEncryptionKey, 32)
		Else
			intLength = Len(vstrEncryptionKey)
			intRemaining = 32 - intLength
			vstrEncryptionKey = vstrEncryptionKey & Strings.StrDup(intRemaining, "X")
		End If

		bytKey = Encoding.ASCII.GetBytes(vstrEncryptionKey.ToCharArray)

		objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged

		'   ***********************************************************************
		'   ******  Create the encryptor and write value to it after it is   ******
		'   ******  converted into a byte array                              ******
		'   ***********************************************************************

		Try
			objCryptoStream = New System.Security.Cryptography.CryptoStream(objMemoryStream, objRijndaelManaged.CreateEncryptor(bytKey, bytIV), System.Security.Cryptography.CryptoStreamMode.Write)
			objCryptoStream.Write(bytValue, 0, bytValue.Length)

			objCryptoStream.FlushFinalBlock()

			bytEncoded = objMemoryStream.ToArray
			objMemoryStream.Close()
			objCryptoStream.Close()
		Catch
		End Try

		Return Convert.ToBase64String(bytEncoded)
	End Function

	Friend Function StripNullCharacters(ByVal vstrStringWithNulls As String) As String
		Dim intPosition As Integer
		Dim strStringWithOutNulls As String

		intPosition = 1
		strStringWithOutNulls = vstrStringWithNulls

		Do While intPosition > 0
			intPosition = InStr(intPosition, vstrStringWithNulls, vbNullChar)

			If intPosition > 0 Then
				strStringWithOutNulls = Left$(strStringWithOutNulls, intPosition - 1) & _
				   Right$(strStringWithOutNulls, Len(strStringWithOutNulls) - intPosition)
			End If

			If intPosition > strStringWithOutNulls.Length Then
				Exit Do
			End If
		Loop

		Return strStringWithOutNulls

	End Function
	
	Friend Function GetApplicationName() As String
		Dim conf As Configuration = Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
		Dim mSection As New Web.Configuration.MembershipSection
		mSection = conf.GetSection("system.web/membership")

		Dim appName As String = mSection.Providers(mSection.DefaultProvider).Parameters("applicationName")
		If appName = "" Then
			appName = System.Web.HttpContext.Current.Request.Url.Host

			If System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath <> "/" Then _
			appName += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
		End If

		Return appName
	End Function

	Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs)
		Dim asm As Reflection.Assembly = System.Reflection.Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory & "bin\wdk.vfs.dll")
		
		Dim strSrc As String = "wdk.vfs" & asm.GetName.Version.ToString & ComputerName.Text & OsVersion.Text & "|-1"

		licenseKey.Text = EncryptString(strSrc, GetApplicationName)
		
		asm = Nothing
	End Sub
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <style type="text/css">
		*
		{
			font-family: Calibri, sans;
			font-size:9pt;
		}
		td
		{
			padding:3px;
		}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table style="border:none; border-collapse:collapse;">
		<tr>
			<td>
				Computer name:</td>
			<td>
				<asp:TextBox ID="ComputerName" runat="server">BLACKICE</asp:TextBox></td>
		</tr>
		<tr>
			<td>
				OS:</td>
			<td>
				<asp:TextBox ID="OsVersion" runat="server"></asp:TextBox></td>
		</tr>
		<tr>
			<td>License:</td>
			<td><asp:TextBox ID="licenseKey" runat="server" Width="398px" ReadOnly="true"></asp:TextBox></td>
		</tr>
		<tr>
			<td>
			</td>
			<td>
				<asp:Button ID="Button1" runat="server" Text="Generate Key" OnClick="Button1_Click" /></td>
		</tr>
    </table>
   
    </form>
</body>
</html>
