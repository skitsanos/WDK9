Imports System.Threading
Imports System.Xml

Namespace NewsLetters
    Public Class Manager

#Region " Properties "
        Private _SortOrder As String = "id DESC"
        Public Property SortOrder() As String
            Get
                Return _SortOrder
            End Get
            Set(ByVal Value As String)
                _SortOrder = Value
            End Set
        End Property

        Private _MessageTemplate As String
        Public Property MessageTemplate() As String
            Get
                Return _MessageTemplate
            End Get
            Set(ByVal value As String)
                _MessageTemplate = value
            End Set
        End Property
#End Region

#Region " .Add() "
        Public Function Add(ByVal List As Integer, ByVal Title As String, ByVal Content As String, Optional ByVal IsEnabled As Boolean = True) As Boolean
            If Title = "" Then
                Log("Newsletter should have Title/Subject.", True)
                Return False
            End If

            Dim ret As Boolean = False
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim dbc As New Data.Odbc.OdbcCommand("INSERT INTO Newsletters (ApplicationName, List, Title, NewsletterContent, IsEnabled, CreatedOn) VALUES (?, ?,?,?,?,?)", db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@List", List)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@NewsletterContent", Content)
                dbc.Parameters.AddWithValue("@IsEnabled", IsEnabled)
                dbc.Parameters.AddWithValue("@CreatedOn", Now)
                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log(ex.Message, True)
                Return False
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " .Update() "
        Public Function Update(ByVal Id As Integer, ByVal List As Integer, ByVal Title As String, ByVal Content As String, Optional ByVal IsEnabled As Boolean = True) As Boolean
            If Title = "" Then
                Log("Newsletter should have Title/Subject.", True)
                Return False
            End If

            Dim ret As Boolean = False
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "UPDATE Newsletters SET List=?, Title=?, NewsletterContent=?, IsEnabled=? WHERE ApplicationName=? AND id=" & Id
                Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@List", List)
                dbc.Parameters.AddWithValue("@Title", Title)
                dbc.Parameters.AddWithValue("@NewsletterContent", Content)
                dbc.Parameters.AddWithValue("@IsEnabled", IsEnabled)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.ExecuteNonQuery()

                dbc.Dispose()

                ret = True

            Catch ex As Exception
                Log(ex.ToString, True)
                Return False
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

#Region " GetDataset() "
        Public Function GetDataset(Optional ByVal Criteria As String = "") As System.Data.DataSet
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

               If Criteria <> "" Then Criteria = "AND (" & Criteria & ")"

                Dim dbc As New Data.Odbc.OdbcCommand("SELECT * FROM viewNewsletters WHERE ApplicationName=? " & Criteria & " ORDER BY " & SortOrder, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

                Dim dba As New Data.Odbc.OdbcDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "Newsletters")

                Return dbs

            Catch ex As Exception
                Log(ex.ToString, True)
                Return Nothing
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        End Function
#End Region

#Region " .SendToAll "
        Public Function SendToAll(ByVal NewsletterId As Integer) As Boolean
            Try
                Dim winTask As New Thread(AddressOf RunBroadcasting)
                winTask.IsBackground = True
                winTask.Name = NewsletterId
                winTask.Start()

                Return True

            Catch ex As Exception
                Log(ex.ToString, True)
                Return False
            End Try
        End Function
#End Region

#Region " GetTemplate "
        Private Function GetTemplate(ByVal NewsletterId As Integer) As String
            Try
                Dim dl As New DistributionLists.Manager
                Dim dlId As Integer = GetRecordAsXml(NewsletterId).DocumentElement.GetAttribute("List")

                Return dl.Template(dlId).ToString

            Catch ex As Exception
                Return "%%content%%"
            End Try
        End Function
#End Region

#Region " .SendToOne() "
        Public Function SendToOne(ByVal NewsletterId As Integer, ByVal Address As String) As Boolean
            If Address = "" Then
                Log("Failed to send newsletter. Recepient address is missing.", True)
                Return False
            End If

            Try
                Dim thisNewsletter As XmlElement = GetRecordAsXml(NewsletterId).DocumentElement
                Dim Content As String = thisNewsletter.GetAttribute("NewsletterContent")
                Dim Title As String = thisNewsletter.GetAttribute("Title")

                Dim Template As String = GetTemplate(NewsletterId)

                Template = Template.Replace("%%date%%", Now)
                Template = Template.Replace("%%title%%", Title)
                Template = Template.Replace("%%content%%", Content)

                Dim FromAddress As String = "noreply@mywdk.com"
                Dim Settings As Object = Activator.CreateInstance(Type.GetType("WDK.Providers.Settings,WDK.Providers.Settings"))
                If Settings IsNot Nothing Then
                    FromAddress = Settings.Get("eNews.Email")
                    If FromAddress = "" Then FromAddress = "noreply@mywdk.com"
                End If

                Dim res As Boolean = SendMail(FromAddress, Address, Title, Template, True)
                If res = False Then
                    Log("Sending mail to {" & Address & "} failed.", True)
                    Return False
                Else
                    Log("Newsletter {" & Title & "} has been sent to {" & Address & "}")
                    Return True
                End If

            Catch ex As Exception
                Log(ex.ToString, True)
                Return False
            End Try
        End Function
#End Region

#Region " .RunBroadcasting() "
        Private Sub RunBroadcasting()
            Dim NewsletterId As Integer = CInt(Threading.Thread.CurrentThread.Name)
            Dim thisNewsletter As XmlElement = GetRecordAsXml(NewsletterId).DocumentElement

            Dim dList As Integer = thisNewsletter.GetAttribute("List")
            If dList < 1 Then
                Log("Can't send newsletter titled {" & thisNewsletter.GetAttribute("Title") & "}. Distribution list unknown.", True)
                Exit Sub
            End If

            Try
                Dim Template As String = GetTemplate(NewsletterId)
                Dim Title As String = GetRecordAsXml(NewsletterId).DocumentElement.GetAttribute("Title")

                Dim Content As String = thisNewsletter.GetAttribute("NewsletterContent")

                Log("Sending {" & Title & "} newsletter ...")

                Template = Template.Replace("%%date%%", Now)
                Template = Template.Replace("%%title%%", Title)
                Template = Template.Replace("%%content%%", Content)

                Log("Loaded template and content. Size: " & FormatNumber(Template.Length \ 1024, 2) & " Kb")

                Dim subs As New Subscriptions.Manager
                Dim dbs As DataSet = subs.GetDataset("List=" & dList)

                Log("Found subscribers: " & dbs.Tables("Subscriptions").Rows.Count, False)

                Dim FromAddress As String = "noreply@mywdk.com"
                Dim Settings As Object = Activator.CreateInstance(Type.GetType("WDK.Providers.Settings,WDK.Providers.Settings"))
                If Settings IsNot Nothing Then
                    FromAddress = Settings.Get("eNews.Email")
                    If FromAddress = "" Then FromAddress = "noreply@mywdk.com"
                End If

                If dbs.Tables("subscriptions").Rows.Count > 0 Then
                    Dim dbRow As Data.DataRow
                    For Each dbRow In dbs.Tables("Subscriptions").Rows
                        Dim Email As String = dbRow("Email")

                        Template = Replace(Template, "{%email%}", Email)
                        '******************************************************************************************************************************************
                        'Adding message to the queue
                        Dim res As Boolean = SendMail(FromAddress, Email, Title, Template, True)
                        Dim eMessage As New WDK.Network.Mail.EmailMessage()
						eMessage.AddressFrom(FromAddress, FromAddress)
                        eMessage.AddAddressTo(Email, Email)
                        eMessage.Subject = Title
                        eMessage.Body = Template
                        eMessage.IsBodyHtml = True
                        Dim strMessageToQueue As String = eMessage.ToXmlString()
                        WDK.Utilities.MSMQ.CreateQueue("SiteAdminNewsletters", False)
                        WDK.Utilities.MSMQ.SendPrivate("SiteAdminNewsletters", strMessageToQueue, "SiteAdminNewsletters", WDK.Utilities.QueueMessageFormat.Default)
                        '******************************************************************************************************************************************

                        If res = False Then
                            Log("Sending mail to {" & Email & "} failed.", True)
                        Else
                            Log("Newsletter {" & Title & "} has been sent to {" & Email & "}", False)
                        End If
                    Next
                Else
                    Log("Newsletter {" & Title & "} has not been sent. There are no subscribers found.", True)
                End If

                dbs.Dispose()

            Catch ex As Exception
                Log(ex.ToString, True)
            End Try
        End Sub
#End Region

#Region " .Delete() "
        Public Function Delete(ByVal Id As Integer) As Boolean
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim dbc As New Data.Odbc.OdbcCommand("DELETE FROM NewsLetters WHERE ApplicationName=? AND id=" & Id, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.ExecuteNonQuery()
                dbc.Dispose()

                Return True

            Catch ex As Exception
                Log(ex.Message, True)
                Return False
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        End Function
#End Region

#Region " GetRecordAsXml "
        Public Function GetRecordAsXml(ByVal id As Integer) As XmlDocument
            Dim db As Data.Odbc.OdbcConnection = Nothing
            Dim ret As XmlDocument = Nothing

            Try
                db = New Data.Odbc.OdbcConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM viewNewsLetters WHERE ApplicationName=? AND id=? for xml raw"
                Dim dbc As New Data.Odbc.OdbcCommand(strSQL, db)
                dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
                dbc.Parameters.AddWithValue("@id", id)

                Dim reader As Data.Odbc.OdbcDataReader = dbc.ExecuteReader(Data.CommandBehavior.SingleRow)
                If reader.HasRows Then
                    ret = New XmlDocument
                    ret.LoadXml(reader(0).ToString)
                End If

            Catch ex As Exception
                Log(ex.ToString, True)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try

            Return ret
        End Function
#End Region

    End Class

End Namespace