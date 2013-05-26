Imports System.Web.Configuration
Imports System.Configuration
Imports System.Text.RegularExpressions
Imports System.Net

Public Class Helpers

#Region " AppSettings "
    Friend Shared Function AppSettings(ByVal Key As String) As Object
        Return WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath).AppSettings.Settings(Key).Value
    End Function
#End Region

#Region " .Comment() "
    Public Shared Function Comment(ByVal CommentLine As String) As String
        Return "<!--// " & vbCrLf & CommentLine & vbCrLf & " //-->" & vbCrLf
    End Function
#End Region

#Region " .SendMail() "
    Public Shared Function SendMail(ByVal FromAddress As String, ByVal ToAddress As String, ByVal CCAddress As String, ByVal msgSubject As String, ByVal msgBody As String, Optional ByVal useSSL As Boolean = False) As Boolean
        Try
            Dim objMail As New Net.Mail.SmtpClient
            objMail.EnableSsl = useSSL

            Dim msg As New Net.Mail.MailMessage(FromAddress, ToAddress)

            msg.Headers.Add("X-Mailer", "Skitsanos WDK")
            If CCAddress <> "" Then msg.Bcc.Add(New Net.Mail.MailAddress(CCAddress))
            msg.Subject = msgSubject
            msg.IsBodyHtml = True
            msg.Body = msgBody

            objMail.Send(msg)

            Return True

        Catch ex As Exception
            Throw New Exception("{WDK.Helpers.SendMail} " & ex.ToString)
        End Try
    End Function
#End Region

#Region " .Whois() "
    Public Shared Function Whois(ByVal DomainName As String) As String
        If DomainName = "" Then
            Return ""
            Exit Function
        End If

        Try
            Dim htmlParser As Object = Activator.CreateInstance(Type.GetType("HtmlAgilityPack.HtmlWeb,HtmlAgilityPack"))
            Dim htmlDoc As Object = Activator.CreateInstance(Type.GetType("HtmlAgilityPack.HtmlDocument,HtmlAgilityPack"))
            htmlDoc = htmlParser.Load("http://www.directnic.com/whois/index.php?query=" & DomainName)

            Dim htmlContent As Object = Activator.CreateInstance(Type.GetType("HtmlAgilityPack.HtmlNode,HtmlAgilityPack"))
            htmlContent = htmlDoc.DocumentNode.SelectSingleNode("//p[@class='text12']")

            Return htmlContent.InnerHtml

            htmlDoc = Nothing
            htmlParser = Nothing
        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
#End Region

#Region " .IsLeapYear() "
    Public Shared Function IsLeapYear(ByVal Year As Integer) As Boolean
        Return DateTime.IsLeapYear(Year)
    End Function
#End Region

#Region " .IsValidIP() "
    Public Shared Function IsValidIP(ByVal Address As String) As Boolean  'has to be in the dotted aaa.bbb.ccc.ddd with a..d from 0 to 255
        Dim vBytes As String() = Nothing
        vBytes = Split(Trim(Address), ".")
        If UBound(vBytes) = 3 Then
            If IsNumeric(vBytes(0)) And IsNumeric(vBytes(1)) And IsNumeric(vBytes(2)) And IsNumeric(vBytes(3)) Then
                If vBytes(0) >= 0 And vBytes(0) <= 255 And vBytes(1) >= 0 And vBytes(1) <= 255 And vBytes(2) >= 0 And vBytes(2) <= 255 And vBytes(3) >= 0 And vBytes(3) <= 255 Then IsValidIP = True
            Else
                Throw New Exception("IP address invalid")
            End If
        End If
    End Function
#End Region

#Region " .GeneratePassword() "
    Public Shared Function GeneratePassword(ByVal PasswordLength As Integer) As String
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

#Region " .GUID() "
    Public Shared Function GUID() As String
        Return System.Guid.NewGuid().ToString()
    End Function
#End Region

#Region " .GetComputerName() "
    Public Shared Function GetComputerName() As String
        Return Environment.GetEnvironmentVariable("COMPUTERNAME")
    End Function
#End Region

#Region " .BrakeFrames() "
    Public Shared Function BrakeFrames() As String
        Dim tmpStr As String = ""
        tmpStr += "<script type=""text/javascript"">" & vbCrLf
        tmpStr += "if (window!=window.top)" & vbCrLf
        tmpStr += "top.location.href=location.href;"
        tmpStr += "</script>"
        Return tmpStr
    End Function
#End Region

#Region " KeepFrames "
    Public Shared Function KeepFrames(ByVal Url As String, Optional ByVal Frame As String = "top") As String
        Dim html As String = "<script type=""text/javascript"">"
        html += "if (window==window." & Frame & ")"
        html += "top.location.href=""" & Url & """;"
        html += "</script>"
        Return html
    End Function

#End Region

#Region " .IsObjectInstalled() "
    Public Shared Function IsObjectInstalled(ByVal ObjectName As String) As Boolean
        Dim tmpObj As Object = Nothing

        Try
            tmpObj = CreateObject(ObjectName)
            If tmpObj Is Nothing Then
                Return False
            Else
                tmpObj = Nothing
                Return True
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
#End Region

#Region " .ExtractValue() "
    Public Shared Function ExtractValue(ByVal VariableName As String, ByVal QueryString As String, Optional ByVal Splitter As String = "&") As String
        If QueryString = "" Then Return ""

        Dim myVar As String() = Split(QueryString, Splitter)
        Dim maxVar As Integer = UBound(myVar)

        Dim x As Integer = 0
        Dim extVar As String = ""
        Dim res As String = ""

        For x = 0 To maxVar
            extVar = Mid(myVar(x), 1, InStr(1, myVar(x), "=") - 1)
            If VariableName = extVar Then
                res = Mid(myVar(x), InStr(1, myVar(x), "=") + 1)
            End If
        Next

        Return res
    End Function
#End Region

#Region " .DisableIEImagetoolbar() "
    Public Shared Function DisableIEImagetoolbar() As String
        Return "<meta http-equiv=""imagetoolbar"" content=""no""/>"
    End Function
#End Region

#Region " GetODBCDriversList() "
    Public Shared Function GetODBCDriversList() As String()
        'HKEY_LOCAL_MACHINE\SOFTWARE\ODBC\ODBCINST.INI\ODBC Drivers
        Dim regKey As Microsoft.Win32.RegistryKey = _
        Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE", False).OpenSubKey("ODBC", False).OpenSubKey("ODBCINST.INI", False).OpenSubKey("ODBC Drivers", False)

        Return regKey.GetValueNames
    End Function
#End Region

#Region " removeAccents "
    Public Shared Function removeAccents(ByVal src As String) As String
        Dim strAccents As String() = New String() {"À", "Á", "Â", "Ã", "Ä", "Ç", "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ñ", "Ò", "Ó", "Ô", "Õ", "Ö", "Ù", "Ú", "Û", "Ü", "ß", "à", "á", "â", "ã", "ä", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", "ñ", "ò", "ó", "ô", "õ", "ö", "ù", "ú", "û", "ü"}
        Dim strRemoveAccents As String() = New String() {"&Agrave;", "&Aacute;", "&Acirc;", "&Atilde;", "&Auml;", "&Ccedil;", "&Egrave;", "&Eacute;", "&Ecirc;", "&Euml;", "&Igrave;", "&Iacute;", "&Icirc;", "&Iuml;", "&Ntilde;", "&Ograve;", "&Oacute;", "&Ocirc;", "&Otilde;", "&Ouml;", "&Ugrave;", "&Uacute;", "&Ucirc;", "&Uuml;", "&szlig;", "&agrave;", "&aacute;", "&acirc;", "&atilde;", "&auml;", "&ccedil;", "&egrave;", "&eacute;", "&ecirc;", "&euml;", "&igrave;", "&iacute;", "&icirc;", "&iuml;", "&ntilde;", "&ograve;", "&oacute;", "&ocirc;", "&otilde;", "&ouml;", "&ugrave;", "&uacute;", "&ucirc;", "&uuml;"}

        For q As Integer = 0 To UBound(strAccents)
            If InStr(src, strAccents(q)) Then
                src = src.Replace(strAccents(q), strRemoveAccents(q))
            End If
        Next

        Return src
    End Function
#End Region

#Region " createAccents "
    Public Shared Function createAccents(ByVal src As String) As String
        Dim strAccents As String() = New String() {"À", "Á", "Â", "Ã", "Ä", "Ç", "È", "É", "Ê", "Ë", "Ì", "Í", "Î", "Ï", "Ñ", "Ò", "Ó", "Ô", "Õ", "Ö", "Ù", "Ú", "Û", "Ü", "ß", "à", "á", "â", "ã", "ä", "ç", "è", "é", "ê", "ë", "ì", "í", "î", "ï", "ñ", "ò", "ó", "ô", "õ", "ö", "ù", "ú", "û", "ü"}
        Dim strRemoveAccents As String() = New String() {"&Agrave;", "&Aacute;", "&Acirc;", "&Atilde;", "&Auml;", "&Ccedil;", "&Egrave;", "&Eacute;", "&Ecirc;", "&Euml;", "&Igrave;", "&Iacute;", "&Icirc;", "&Iuml;", "&Ntilde;", "&Ograve;", "&Oacute;", "&Ocirc;", "&Otilde;", "&Ouml;", "&Ugrave;", "&Uacute;", "&Ucirc;", "&Uuml;", "&szlig;", "&agrave;", "&aacute;", "&acirc;", "&atilde;", "&auml;", "&ccedil;", "&egrave;", "&eacute;", "&ecirc;", "&euml;", "&igrave;", "&iacute;", "&icirc;", "&iuml;", "&ntilde;", "&ograve;", "&oacute;", "&ocirc;", "&otilde;", "&ouml;", "&ugrave;", "&uacute;", "&ucirc;", "&uuml;"}

        For q As Integer = 0 To UBound(strAccents)
            If InStr(src, strAccents(q)) Then
                src = src.Replace(strRemoveAccents(q), strAccents(q))
            End If
        Next

        Return src
    End Function
#End Region

#Region " getTinyUrl "
    Public Shared Function getTinyUrl(ByVal url As String) As String
        If url.Length <= 12 Then Return url

        If Not url.ToLower.StartsWith("http") And Not url.ToLower.StartsWith("ftp") Then
            url = "http://" + url
        End If

        Dim request As WebRequest = WebRequest.Create("http://tinyurl.com/api-create.php?url=" + url)
        Dim res As WebResponse = request.GetResponse

        Dim temp As String = url

        Using reader As New IO.StreamReader(res.GetResponseStream)
            temp = reader.ReadToEnd
        End Using

        Return temp
    End Function
#End Region

#Region " Log "
    Public Shared Sub Log(ByVal data As String)
        IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "wdk.log", data)
    End Sub
#End Region

#Region " stripHtml "
    Public Shared Function stripHtml(ByVal htmlString As String) As String
        '(<[^>]+>) 
        Return Regex.Replace(htmlString, "<(.|\n)*?>", String.Empty)
    End Function
#End Region

End Class