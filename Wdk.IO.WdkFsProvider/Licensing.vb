Imports System.Security.Cryptography
Imports System.IO
Imports System.Text

Module _Licensing

#Region " IsLicensed "
	Function IsLicensed() As Boolean
		If GetWebConfigValue("WDK.VFS.Key") Is Nothing Then
			Throw New Exception("WDK.VFS.Key can't be read, please contact your nearest WDK Support representative to obtain new license")
		End If

		Dim strSrc As String = "wdk.vfs" & My.Application.Info.Version.ToString & My.Computer.Name & My.Computer.Info.OSVersion & "|-1"

		'Dim encKey As String = EncryptString(strSrc, GetApplicationName)
		Dim decKey As String = DecryptString(GetWebConfigValue("WDK.VFS.Key"), GetApplicationName)

		If decKey = strSrc Then
			Return True
		Else
			Return False
		End If
	End Function
#End Region

#Region " EncryptString() "
	Friend Function EncryptString(ByVal vstrTextToBeEncrypted As String, ByVal vstrEncryptionKey As String) As String

		Dim bytValue() As Byte
		Dim bytKey() As Byte
		Dim bytEncoded() As Byte = Nothing
		Dim bytIV() As Byte = {121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62}
		Dim intLength As Integer
		Dim intRemaining As Integer
		Dim objMemoryStream As New MemoryStream
		Dim objCryptoStream As CryptoStream
		Dim objRijndaelManaged As RijndaelManaged


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

		objRijndaelManaged = New RijndaelManaged

		'   ***********************************************************************
		'   ******  Create the encryptor and write value to it after it is   ******
		'   ******  converted into a byte array                              ******
		'   ***********************************************************************

		Try

			objCryptoStream = New CryptoStream(objMemoryStream, objRijndaelManaged.CreateEncryptor(bytKey, bytIV), CryptoStreamMode.Write)
			objCryptoStream.Write(bytValue, 0, bytValue.Length)

			objCryptoStream.FlushFinalBlock()

			bytEncoded = objMemoryStream.ToArray
			objMemoryStream.Close()
			objCryptoStream.Close()
		Catch



		End Try

		'   ***********************************************************************
		'   ******   Return encryptes value (converted from  byte Array to   ******
		'   ******   a base64 string).  Base64 is MIME encoding)             ******
		'   ***********************************************************************

		Return Convert.ToBase64String(bytEncoded)

	End Function
#End Region

#Region " DecryptString() "
	Friend Function DecryptString(ByVal StringToBeDecrypted As String, ByVal DecryptionKey As String) As String
		Dim bytDataToBeDecrypted() As Byte
		Dim bytTemp() As Byte
		Dim bytIV() As Byte = {121, 241, 10, 1, 132, 74, 11, 39, 255, 91, 45, 78, 14, 211, 22, 62}
		Dim objRijndaelManaged As New RijndaelManaged
		Dim objMemoryStream As MemoryStream
		Dim objCryptoStream As CryptoStream
		Dim bytDecryptionKey() As Byte

		Dim intLength As Integer
		Dim intRemaining As Integer
		'Dim intCtr As Integer
		Dim strReturnString As String = String.Empty

		'   *****************************************************************
		'   ******   Convert base64 encrypted value to byte array      ******
		'   *****************************************************************

		bytDataToBeDecrypted = Convert.FromBase64String(StringToBeDecrypted)

		'   ********************************************************************
		'   ******   Encryption Key must be 256 bits long (32 bytes)      ******
		'   ******   If it is longer than 32 bytes it will be truncated.  ******
		'   ******   If it is shorter than 32 bytes it will be padded     ******
		'   ******   with upper-case Xs.                                  ****** 
		'   ********************************************************************

		intLength = Len(DecryptionKey)

		If intLength >= 32 Then
			DecryptionKey = Strings.Left(DecryptionKey, 32)
		Else
			intLength = Len(DecryptionKey)
			intRemaining = 32 - intLength
			DecryptionKey = DecryptionKey & Strings.StrDup(intRemaining, "X")
		End If

		bytDecryptionKey = Encoding.ASCII.GetBytes(DecryptionKey.ToCharArray)

		ReDim bytTemp(bytDataToBeDecrypted.Length)

		objMemoryStream = New MemoryStream(bytDataToBeDecrypted)

		'   ***********************************************************************
		'   ******  Create the decryptor and write value to it after it is   ******
		'   ******  converted into a byte array                              ******
		'   ***********************************************************************

		Try

			objCryptoStream = New CryptoStream(objMemoryStream, _
			   objRijndaelManaged.CreateDecryptor(bytDecryptionKey, bytIV), _
			   CryptoStreamMode.Read)

			objCryptoStream.Read(bytTemp, 0, bytTemp.Length)

			objCryptoStream.FlushFinalBlock()
			objMemoryStream.Close()
			objCryptoStream.Close()

		Catch

		End Try

		'   *****************************************
		'   ******   Return decypted value     ******
		'   *****************************************

		Return StripNullCharacters(Encoding.ASCII.GetString(bytTemp))

	End Function
#End Region

#Region " StripNullCharacters() "
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
#End Region

End Module