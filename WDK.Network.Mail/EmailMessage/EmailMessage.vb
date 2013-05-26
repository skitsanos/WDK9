Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.Net.Mail
Imports System.Reflection
Imports System.Xml
Imports System.IO
Imports System.Net.Mime
Imports WDK.Network.Mail.AttachmentTypes


Namespace Mail
	Public Class EmailMessage

#Region " Properties "
		'Private strERR As String = "ERROR"

		Private mailMessage As MailMessage = New MailMessage
		'Private _IsBroadcast As Boolean = False
		Private attachmentsCount As Integer = 0

		Public ReadOnly Property message() As MailMessage
			Get
				Return mailMessage
			End Get
		End Property

		Public Property Body() As String
			Get
				Return (mailMessage.Body)
			End Get
			Set(ByVal value As String)
				mailMessage.Body = value
			End Set
		End Property

		Public Property Subject() As String
			Get
				Return (mailMessage.Subject)
			End Get
			Set(ByVal value As String)
				mailMessage.Subject = value
			End Set
		End Property

		Public Property IsBodyHtml() As Boolean
			Get
				Return (mailMessage.IsBodyHtml)
			End Get
			Set(ByVal value As Boolean)
				mailMessage.IsBodyHtml = value
			End Set
		End Property
#End Region

#Region " New "
		Public Sub New()
			mailMessage.SubjectEncoding = Encoding.UTF8
			mailMessage.BodyEncoding = Encoding.UTF8
		End Sub
#End Region

#Region " Finalize "
		''' <summary>
		''' 
		''' </summary>
		''' <remarks></remarks>
		Protected Overrides Sub Finalize()
			Try
				mailMessage.Dispose()
			Finally
				MyBase.Finalize()
			End Try
		End Sub
#End Region

#Region " AddressFrom "
		Public Sub AddressFrom(ByVal strAddress As String, ByVal strDispName As String)
			mailMessage.From = New MailAddress(strAddress, strDispName, Encoding.UTF8)
		End Sub
#End Region

#Region " AddAddressTo "
		Public Function AddAddressTo(ByVal strAddress As String, ByVal strDispName As String) As Boolean
			mailMessage.[To].Add(New MailAddress(strAddress, strDispName))
			Return True
		End Function
#End Region

#Region " GetAttachmentMediaTypeByFileExtension "
		Public Function GetAttachmentMediaTypeByFileExtension(ByVal strFileExtension As String) As String
			Try
				Dim strRetType As String = String.Empty
				If strFileExtension.Equals(".jpg", StringComparison.OrdinalIgnoreCase) Or strFileExtension.Equals(".jpeg", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Image.JPG
				ElseIf strFileExtension.Equals(".bmp", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Image.BMP
				ElseIf strFileExtension.Equals(".tiff", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Image.TIFF
				ElseIf strFileExtension.Equals(".gif", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Image.GIF
				ElseIf strFileExtension.Equals(".mp3", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Audio.MP3
				ElseIf strFileExtension.Equals(".ogg", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Audio.OGG
				ElseIf strFileExtension.Equals(".acc", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Audio.ACC
				ElseIf strFileExtension.Equals(".wav", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Audio.WAV
				ElseIf strFileExtension.Equals(".wma", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Audio.WMA
				ElseIf strFileExtension.Equals(".zip", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Application.ZIP
				ElseIf strFileExtension.Equals(".pdf", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Application.PDF
				ElseIf strFileExtension.Equals(".mpeg", StringComparison.OrdinalIgnoreCase) Or strFileExtension.Equals(".mpg", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Video.MPEG
				ElseIf strFileExtension.Equals(".avi", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Video.AVI
				ElseIf strFileExtension.Equals(".wmv", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Video.WMV
				ElseIf strFileExtension.Equals(".html", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Text.HTML
				ElseIf strFileExtension.Equals(".txt", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Text.PLAIN
				ElseIf strFileExtension.Equals(".rtf", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Text.RICHTEXT
				ElseIf strFileExtension.Equals(".xml", StringComparison.OrdinalIgnoreCase) Then
					strRetType = Text.XML
				Else
					strRetType = Application.UNKNOWN
				End If
				Return strRetType
			Catch ex As Exception
				Return "ERROR"
			End Try

		End Function
#End Region

#Region " Attach {filename}"
		Public Function Attach(ByVal strFileName As String) As Boolean
			Try
				Dim strType As String = GetAttachmentMediaTypeByFileExtension(New FileInfo(strFileName).Extension)

				If strType.Equals("ERROR") Then strType = Application.UNKNOWN

				Attach(strFileName, strType)

				Return True

			Catch ex As Exception
				Return False
			End Try
		End Function
#End Region

#Region " Attach {filename, type} "
		Public Function Attach(ByVal strFileName As String, ByVal type As String) As Boolean
			Try
				Dim ct As New ContentType()
				ct.MediaType = type
				ct.Parameters.Add("filePath", strFileName)
				ct.Parameters.Add("type", type)
				mailMessage.Attachments.Add(New Attachment(strFileName))
				mailMessage.Attachments(System.Math.Max(System.Threading.Interlocked.Increment(attachmentsCount), attachmentsCount - 1)).ContentType = ct

				Return True

			Catch ex As Exception
				Return False
			End Try
		End Function
#End Region

#Region " AddCC "
		Public Sub AddCC(ByVal strAddress As String, ByVal strDispName As String)
			mailMessage.CC.Add(New MailAddress(strAddress, strDispName))
		End Sub
#End Region

#Region " AddBcc "
		Public Sub AddBcc(ByVal strAddress As String, ByVal strDispName As String)
			mailMessage.Bcc.Add(New MailAddress(strAddress, strDispName))
		End Sub
#End Region

#Region " SetReplyToAddress "
		Public Sub SetReplyToAddress(ByVal strAddress As String, ByVal strDispName As String)
			mailMessage.ReplyTo = New MailAddress(strAddress, strDispName, Encoding.UTF8)
		End Sub
#End Region


#Region " ToXmlString "
		Public Function ToXmlString() As String
			Dim ms As New MemoryStream()
			Try
				Dim xmlTxtWr As New XmlTextWriter(ms, Encoding.GetEncoding(65001))
				xmlTxtWr.Formatting = Formatting.Indented
				xmlTxtWr.Indentation = 4
				xmlTxtWr.WriteStartElement("mailMessage")

				If mailMessage.From IsNot Nothing Then
					xmlTxtWr.WriteStartElement("from")
					xmlTxtWr.WriteAttributeString("address", mailMessage.From.Address)

					If mailMessage.From.DisplayName IsNot Nothing And mailMessage.From.DisplayName.Length > 0 Then
						xmlTxtWr.WriteAttributeString("dispName", mailMessage.From.DisplayName)
					End If

					xmlTxtWr.WriteEndElement()
				End If

				If mailMessage.ReplyTo IsNot Nothing Then
					xmlTxtWr.WriteStartElement("ReplyTo")
					xmlTxtWr.WriteAttributeString("address", mailMessage.ReplyTo.Address)
					If mailMessage.ReplyTo.DisplayName IsNot Nothing And mailMessage.ReplyTo.DisplayName.Length > 0 Then
						xmlTxtWr.WriteAttributeString("dispName", mailMessage.ReplyTo.DisplayName)
					End If
					xmlTxtWr.WriteEndElement()
				End If

				For Each addy As MailAddress In message.[To]
					xmlTxtWr.WriteStartElement("to")
					xmlTxtWr.WriteAttributeString("address", addy.Address)
					If addy.DisplayName IsNot Nothing And addy.DisplayName.Length > 0 Then xmlTxtWr.WriteAttributeString("dispName", addy.DisplayName)
					xmlTxtWr.WriteEndElement()
				Next

				For Each addy As MailAddress In message.CC
					xmlTxtWr.WriteStartElement("CC")
					xmlTxtWr.WriteAttributeString("address", addy.Address)
					If addy.DisplayName IsNot Nothing And addy.DisplayName.Length > 0 Then xmlTxtWr.WriteAttributeString("dispName", addy.DisplayName)
					xmlTxtWr.WriteEndElement()
				Next

				For Each addy As MailAddress In message.Bcc
					xmlTxtWr.WriteStartElement("Bcc")
					xmlTxtWr.WriteAttributeString("address", addy.Address)
					If addy.DisplayName IsNot Nothing And addy.DisplayName.Length > 0 Then xmlTxtWr.WriteAttributeString("dispName", addy.DisplayName)
					xmlTxtWr.WriteEndElement()
				Next

				For Each entry As Attachment In message.Attachments
					xmlTxtWr.WriteStartElement("Attachment")
					xmlTxtWr.WriteAttributeString("filePath", entry.ContentType.Parameters("filePath"))
					If entry.ContentType.Parameters("type") IsNot Nothing Then xmlTxtWr.WriteAttributeString("type", entry.ContentType.Parameters("type"))
					xmlTxtWr.WriteEndElement()
				Next

				If mailMessage.Subject IsNot Nothing Then xmlTxtWr.WriteElementString("Subject", mailMessage.Subject)
		
				If mailMessage.Body IsNot Nothing Then
					Dim strMeesageBody As String = mailMessage.Body
					If strMeesageBody.IndexOf("<html>", StringComparison.OrdinalIgnoreCase) <> -1 Then
						strMeesageBody = strMeesageBody.Replace("<", "[")
						strMeesageBody = strMeesageBody.Replace(">", "]")
					End If
					xmlTxtWr.WriteElementString("Body", strMeesageBody)
				End If

				'xmlTxtWr.WriteElementString("IsBroadcast", bIsBroadcast.ToString())
				xmlTxtWr.WriteEndElement()
				xmlTxtWr.Flush()
				xmlTxtWr.Close()

				Return (Encoding.GetEncoding(65001).GetString(ms.ToArray()).Substring(1))

			Catch ex As Exception
				Return ("<error />")
			Finally
				ms.Dispose()
			End Try
		End Function
#End Region

#Region " FormEmailMessageFromXml "
		Public Shared Function FormEmailMessageFromXml(ByVal strXmlAsString As String) As EmailMessage
			Dim xmlParser As New XmlDocument()
			xmlParser.LoadXml(strXmlAsString)

			Dim emailMessage As New EmailMessage()
			emailMessage.Subject = xmlParser.SelectSingleNode("Subject").InnerText

			Dim strBody As String = xmlParser.SelectSingleNode("Body").InnerText
			If strBody.IndexOf("[html]", StringComparison.OrdinalIgnoreCase) <> -1 Then
				strBody = strBody.Replace("[", "<")
				strBody = strBody.Replace("]", ">")
				emailMessage.IsBodyHtml = True
			End If

			emailMessage.Body = strBody

			emailMessage.AddressFrom(xmlParser.SelectSingleNode("from").Attributes("address").InnerText, xmlParser.SelectSingleNode("from").Attributes("dispName").InnerText)

			Dim strReplyTo As String() = New String(1) {}
			strReplyTo(0) = xmlParser.SelectSingleNode("ReplyTo").Attributes("address").InnerText
			strReplyTo(1) = xmlParser.SelectSingleNode("ReplyTo").Attributes("dispName").InnerText
			emailMessage.SetReplyToAddress(strReplyTo(0), strReplyTo(1))

			For Each node As XmlNode In xmlParser.GetElementsByTagName("to")
				emailMessage.AddAddressTo(node.Attributes("address").InnerText, node.Attributes("dispName").InnerText)
			Next

			For Each node As XmlNode In xmlParser.GetElementsByTagName("CC")
				emailMessage.AddCC(node.Attributes("address").InnerText, node.Attributes("dispName").InnerText)
			Next

			For Each node As XmlNode In xmlParser.GetElementsByTagName("Bcc")
				emailMessage.AddBcc(node.Attributes("address").InnerText, node.Attributes("dispName").InnerText)
			Next

			For Each node As XmlNode In xmlParser.GetElementsByTagName("Attachment")
				emailMessage.AddBcc(node.Attributes("filePath").InnerText, node.Attributes("type").InnerText)
			Next

			Return (emailMessage)
		End Function
#End Region


	End Class
End Namespace
