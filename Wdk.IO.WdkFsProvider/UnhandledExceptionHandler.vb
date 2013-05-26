Imports System.Web
Imports System.Xml
Imports System.Xml.Serialization

'<system.web>
'
'  <httpModules>
'  <add name="UnhandledException" type="WDK.Handlers.UnhandledException, wdk.engine" />  
'  </httpModules>
'
'</system.web>

Namespace Handlers
    Public Class UnhandledException
        Implements IHttpModule

#Region " Init "
        Public Sub Init(ByVal context As System.Web.HttpApplication) _
        Implements System.Web.IHttpModule.Init
            AddHandler context.Error, AddressOf OnError
        End Sub
#End Region

#Region " Dispose "
        Public Sub Dispose() Implements System.Web.IHttpModule.Dispose

        End Sub
#End Region

#Region " OnError "
        Protected Overridable Sub OnError(ByVal sender As Object, ByVal args As EventArgs)
            'Dim app As HttpApplication = CType(sender, HttpApplication)

            Select Case SupportMethod.ToLower
                Case "email"
                    EmailReport()

                Case "xmlpost"
                    XmlPost()

            End Select
        End Sub
#End Region

#Region " EmailReport "
        Private Sub EmailReport()
            Dim msgBody As String = ""
            msgBody += "Host: " & HttpContext.Current.Request.Url.Host
            msgBody += vbCrLf
            msgBody += "Application Path: " & HttpContext.Current.Request.ApplicationPath
            msgBody += vbCrLf
            msgBody += "Query String: " & HttpContext.Current.Request.ServerVariables("QUERY_STRING")
            msgBody += vbCrLf
            msgBody += "Referrer: " & HttpContext.Current.Request.UrlReferrer.OriginalString
            msgBody += vbCrLf

            msgBody += "Errors found during execution: " & HttpContext.Current.AllErrors.Length
            msgBody += vbCrLf

            msgBody += "More details you can find in attached XML file" & vbCrLf

            Try
                Dim objMail As New Net.Mail.SmtpClient

                Dim msg As New Net.Mail.MailMessage(SupportTarget, SupportTarget)

                msg.Headers.Add("X-Mailer", "Skitsanos WDK")
                msg.Subject = "Server Error Report"
                msg.IsBodyHtml = False
                msg.Body = msgBody

				Dim ms As New System.IO.MemoryStream(Text.Encoding.UTF8.GetBytes(XmlReport.OuterXml))
                msg.Attachments.Add(New Net.Mail.Attachment(ms, "ErrorReport.xml"))

                objMail.Send(msg)

            Catch ex As Exception
                Throw New Exception("{WDK.Handlers.UnhandledException} " & ex.ToString)
            End Try
        End Sub
#End Region

#Region " XmlPost "
        Private Sub XmlPost()
            Try
                Dim xmlSocket As New Net.WebClient()
                xmlSocket.Headers.Add("Content-Type", "application/x-www-form-urlencoded")

                Dim ByteArray As Byte() = xmlSocket.UploadData(SupportTarget, System.Text.Encoding.ASCII.GetBytes(XmlReport.OuterXml.ToCharArray))
            Catch ex As Exception

            End Try
        End Sub
#End Region

#Region " XmlReport "
        ''' <summary>
        ''' Geenrates XML document that reperesent information about error fired
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
		Public Function XmlReport() As XmlDocument
			Dim xmlDoc As New XmlDocument
			Dim xmlDec As XmlDeclaration = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", "yes")
			xmlDoc.AppendChild(xmlDec)

			Dim xmlErrors As XmlElement = xmlDoc.CreateElement("ErrorReport")
			xmlDoc.AppendChild(xmlErrors)

			Try
				Dim errNum As Integer = 1
				For Each err As Exception In HttpContext.Current.AllErrors
					Dim xmlDebug As XmlElement = xmlDoc.CreateElement("Debug")
					xmlErrors.AppendChild(xmlDebug)

					Dim xmlApp As XmlElement = xmlDoc.CreateElement("Application")
					xmlDebug.AppendChild(xmlApp)

					xmlApp.SetAttribute("Url", HttpContext.Current.Request.Url.ToString)
					xmlApp.SetAttribute("Host", HttpContext.Current.Request.Url.Host)
					xmlApp.SetAttribute("ApplicationPath", HttpContext.Current.Request.ApplicationPath)
					xmlApp.SetAttribute("QueryString", HttpContext.Current.Request.ServerVariables("QUERY_STRING"))
					xmlApp.SetAttribute("HttpMethod", HttpContext.Current.Request.HttpMethod)
					xmlApp.SetAttribute("IsAuthenticated", HttpContext.Current.Request.IsAuthenticated)
					xmlApp.SetAttribute("IsSecureConnection", HttpContext.Current.Request.IsSecureConnection)
					xmlApp.SetAttribute("UserAgent", HttpContext.Current.Request.UserAgent)

					Dim xmlErr As XmlElement = xmlDoc.CreateElement("Error")
					xmlDebug.AppendChild(xmlErr)

					Dim xmlErrData As XmlCDataSection = xmlDoc.CreateCDataSection(err.ToString)
					xmlErr.AppendChild(xmlErrData)
					xmlErr.SetAttribute("Source", err.Source)
					xmlErr.SetAttribute("TargetSiteName", err.TargetSite.Name)
					xmlErr.SetAttribute("HelpLink", err.HelpLink)
					xmlErr.SetAttribute("Message", err.Message)
				Next

			Catch ex As Exception
				Throw New Exception("{WDK.Handlers.UnhandledException} " & ex.ToString)
			End Try

			Return xmlDoc
		End Function
#End Region

    End Class
End Namespace

