Public Class HistoryBot

#Region " Properties "
    Private WithEvents irc As WDK.Network.IRC.IrcClient
    Private db As Odbc.OdbcConnection
#End Region

#Region " New "
    Public Sub New()
        MyBase.New()
        irc = New WDK.Network.IRC.IrcClient

        db = New Odbc.OdbcConnection("DRIVER={SQL Native Client}; SERVER=labs.skitsanos.com,1038;Database=IrcHistory;UID=ircbot;PWD=siteadmin;")
        db.Open()

        Log("IRC bot db connected")
    End Sub
#End Region

#Region " Start "
    Public Sub Start()
        '- Connect to database


        '- Start IRC Bot
        irc.RealName = "Skitsanos.com History Bot"
        irc.Nickname = "HistoryBot"
        irc.QuitMessage = "Getting out of here...."

        irc.Host = "brown.freenode.net"
        irc.Port = 6667

        irc.Connect()
    End Sub
#End Region

#Region " Stop "
    Public Sub [Stop]()
        irc.Disconnect()
    End Sub
#End Region

#Region " irc_Console "
    Private Sub irc_Console(ByVal Message As String) Handles irc.Console
        Log(Message)
    End Sub
#End Region

#Region " irc_OnInvite "
    Private Sub irc_OnInvite(ByVal Channel As String, ByVal User As String) Handles irc.OnInvite
        Log("User " + User + " invited to " + Channel)

        irc.Join(Channel)
    End Sub
#End Region

#Region " irc_OnKick "
    Private Sub irc_OnKick(ByVal Channel As String, ByVal User As String, ByVal Message As String) Handles irc.OnKick
        irc.Join(Channel)
    End Sub
#End Region

#Region " RegExp Functions "
    Private Function GetCommand(ByVal Message As String) As String
        '^!(?<command>\w+)
        Dim rRegEx As System.Text.RegularExpressions.Regex
        Dim mMatches As System.Text.RegularExpressions.MatchCollection

        Dim rXpression As String = "^!(?<command>\w+)"
        rRegEx = New System.Text.RegularExpressions.Regex(rXpression, System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)
        mMatches = rRegEx.Matches(Message)

        Return mMatches.Item(0).Result("${command}")
    End Function
#End Region

#Region " irc_OnMessage"
    ''' <summary>
    ''' Records message into database
    ''' </summary>
    ''' <param name="Channel"></param>
    ''' <param name="User"></param>
    ''' <param name="Message"></param>
    ''' <remarks></remarks>
    Private Sub irc_OnMessage(ByVal Channel As String, ByVal User As String, ByVal Message As String) Handles irc.OnMessage
        Dim cmd As New Odbc.OdbcCommand("INSERT INTO ChannelHstory (TimeStamp, Channel, IrcUser, Message) VALUES (?, ? ,?, ?)", db)
        cmd.Parameters.AddWithValue("@TimeStamp", Now)
        cmd.Parameters.AddWithValue("@Channel", Channel)
        cmd.Parameters.AddWithValue("@IrcUser", User)
        cmd.Parameters.AddWithValue("@Message", Message)

        cmd.ExecuteNonQuery()
    End Sub
#End Region

#Region " irc_OnNotice "
    Private Sub irc_OnNotice(ByVal Noticer As String, ByVal Message As String) Handles irc.OnNotice
        Log(Message)
    End Sub
#End Region

#Region " irc_OnReady "
    Private Sub irc_OnReady() Handles irc.OnReady
        irc.Identify("skitsanos")
        irc.Join("#aswing")
    End Sub
#End Region

#Region " Log "
    Private Sub Log(ByVal message As String)
        Dim sw As New IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "historyBot.log", True, Text.Encoding.ASCII)
        sw.WriteLine(message)
        sw.Close()
    End Sub
#End Region

End Class
