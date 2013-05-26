Imports System.Web.Configuration
Imports System.Configuration
Imports System.Web.Security

Friend Module Common

#Region " .GeneratePassword() "
    Friend Function GeneratePassword(ByVal PasswordLength As Integer) As String
        Dim genPassword As String = ""
        Randomize()

        Dim i As Integer, intNum As Integer, intUpper As Integer, intLower As Integer, intRand As Integer
        Dim strPartPass As String = ""

        For i = 1 To PasswordLength
            intNum = Int(10 * Rnd() + 48)
            intUpper = Int(26 * Rnd() + 65)
            intLower = Int(26 * Rnd() + 97)
            intRand = Int(3 * Rnd() + 1)
            Select Case intRand
                Case 1
                    strPartPass = ChrW(intNum)
                Case 2
                    strPartPass = ChrW(intUpper)
                Case 3
                    strPartPass = ChrW(intLower)
            End Select
            genPassword = genPassword & strPartPass
        Next

        Return genPassword
    End Function
#End Region

#Region " ConnectionString "
    Friend Function ConnectionString() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.ConnectionStrings.ConnectionStrings("SiteAdmin").ConnectionString
    End Function
#End Region

#Region " .configPath() "
    Friend Function configPath() As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Return conf.AppSettings.Settings("enews_config").Value
    End Function
#End Region

#Region " .SendMail() "
    Public Function SendMail(ByVal From As String, ByVal [To] As String, ByVal msgSubject As String, ByVal msgBody As String, ByVal IsHtml As Boolean) As Boolean
        Try
            Dim objMail As New Net.Mail.SmtpClient

            Dim msg As New Net.Mail.MailMessage(From, [To])

            msg.Headers.Add("X-Mailer", "Skitsanos SiteAdmin CMS")
            msg.Subject = msgSubject
            msg.IsBodyHtml = IsHtml
            msg.Body = msgBody

            objMail.Send(msg)

            Return True

		Catch ex As Exception
			Log(ex.ToString, True)
			Throw New Exception(ex.Message)
        End Try
    End Function
#End Region


#Region " Log "
    Friend Sub Log(ByVal Data As String, Optional ByVal IsError As Boolean = False)
        Dim logs As Object = Activator.CreateInstance(Type.GetType("WDK.Providers.Logs.AppLog,WDK.Providers.Logs"))
        If logs IsNot Nothing Then
            logs.Write(Data, IsError)
        End If
    End Sub
#End Region

    ' Membership provider related

#Region " GetApplicationName "
    Friend Function GetApplicationName() As String
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
#End Region

#Region " GetEmailForUser "
    Public Function GetEmailForUser(ByVal Username As String) As String
        Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        Dim mSection As New MembershipSection
        mSection = conf.GetSection("system.web/membership")

        'WDK.CommunityServices.NetPass.NetPassMembershipProvider
        Dim NetPass As Object = Activator.CreateInstance(Type.GetType("WDK.CommunityServices.NetPass.NetPassMembershipProvider,NetpassProvider"))
        '
        NetPass.ApplicationName = mSection.Providers(mSection.DefaultProvider).Parameters("applicationName")
        NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

        Dim IsOnline As Boolean = False
        Dim user As MembershipUser = NetPass.GetUser(Username, IsOnline)
        Return user.Email
    End Function
#End Region

End Module
