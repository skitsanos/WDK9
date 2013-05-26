Imports System
Imports System.Reflection

    Public Class IrcClient

#Region " Events "
    Public Event Connected()
    Public Event Console(ByVal Message As String)
    Public Event OnError(ByVal Number As Integer, ByVal Message As String)
    Public Event OnInvite(ByVal Channel As String, ByVal User As String)
    Public Event OnJoin(ByVal Channel As String, ByVal User As String)
    Public Event OnKick(ByVal Channel As String, ByVal User As String, ByVal Message As String)
    Public Event OnLeave(ByVal Channel As String, ByVal User As String)
    Public Event OnMessage(ByVal Channel As String, ByVal User As String, ByVal Message As String)
    Public Event OnModeChanged(ByVal Channel As String, ByVal [Operator] As String, ByVal Mode As String, ByVal User As String)
    Public Event OnNotice(ByVal Noticer As String, ByVal Message As String)
    Public Event OnPrivateMessage(ByVal User As String, ByVal Message As String)
    Public Event OnStatus(ByVal Message As String)
    Public Event OnReady()
#End Region

#Region " ErrorCodes "
        Private Enum ErrorCodes
            ' Fields
            ERR_ALREADYREGISTRED = 1
            ERR_BADCHANMASK = 7
            ERR_BADCHANNELKEY = 5
            ERR_BANNEDFROMCHAN = 3
            ERR_CHANNELISFULL = 6
            ERR_INVITEONLYCHAN = 4
            ERR_NEEDMOREPARAMS = 0
            ERR_NOSUCHCHANNEL = 8
            ERR_NOSUCHSERVER = 2
            ERR_NOTONCHANNEL = 10
            ERR_TOOMANYCHANNELS = 9
            RPL_TOPIC = 11
        End Enum
#End Region

#Region " Properties "
        ' Fields
        Private _Host As String = ""
        Private _Hostname As String = "127.0.0.1"
        Private _Nickname As String = "irccmpdotnet"
        Private _Port As Integer = 6667
        Private _QuitMessage As String = "See you next time"
        Private _RealName As String = "Skitsanos IRC Client Component for .NET"
        Private _ServerName As String = "localhost"
        Private _Username As String = "Guest"
        Private WithEvents _wsClient As SocketsClient

        ' Properties
        Public Property Host() As String
            Get
                Return _Host
            End Get
            Set(ByVal Value As String)
                _Host = Value
            End Set
        End Property

        Private Property HostName() As String
            Get
                Return _Hostname
            End Get
            Set(ByVal Value As String)
                _Hostname = Value
            End Set
        End Property

        Public Property Nickname() As String
            Get
                Return _Nickname
            End Get
            Set(ByVal Value As String)
                _Nickname = Value
            End Set
        End Property

        Public Property Port() As Integer
            Get
                Return _Port
            End Get
            Set(ByVal Value As Integer)
                _Port = Value
            End Set
        End Property

        Public Property QuitMessage() As String
            Get
                Return _QuitMessage
            End Get
            Set(ByVal Value As String)
                _QuitMessage = Value
            End Set
        End Property

        Public Property RealName() As String
            Get
                Return _RealName
            End Get
            Set(ByVal Value As String)
                _RealName = Value
            End Set
        End Property

        Private Property ServerName() As String
            Get
                Return _ServerName
            End Get
            Set(ByVal Value As String)
                _ServerName = Value
            End Set
        End Property

        Private Property Username() As String
            Get
                Return _Username
            End Get
            Set(ByVal Value As String)
                _Username = Value
            End Set
        End Property
#End Region

#Region " New "
        Public Sub New()
            MyBase.New()
            _wsClient = New SocketsClient
        End Sub
#End Region

#Region " Connect "
        Public Function Connect() As Boolean
            Dim flag As Boolean = False
            Try
                _wsClient.Connect(Host, Port)
                flag = True

            Catch ex As Exception
                RaiseEvent OnError(0, ex.Message)
            End Try

            Return flag
        End Function
#End Region

#Region " Disconnect "
        Public Sub Disconnect()
            _wsClient.Disconnect()
        End Sub
#End Region

#Region " Execute "
        Private Sub Execute(ByVal command As String)
            _wsClient.SendData(_wsClient.StringToBytes(command))
        End Sub
#End Region

#Region " Join "
        Public Sub Join(ByVal Channel As String, Optional ByVal Key As String = "")
            If Not Channel.StartsWith("#") Then Channel = ("#" & Channel)

            Execute("JOIN " + Channel + " " + Key + vbCrLf)
        End Sub
#End Region

#Region " Leave "
        Public Sub Leave(ByVal Channel As String)
            Execute("PART " + Channel + vbCrLf)
        End Sub
#End Region

#Region " Nick "
        Public Sub Nick(ByVal NewNick As String)
            Execute("NICK " + NewNick + vbCrLf)
        End Sub
#End Region

#Region " ParseData "
    Private Sub ParseData(ByVal Data As String)
        Debug.WriteLine(Data)

        Dim tockens As String() = Data.Split(" ")

        If IsNumeric(tockens(0)) Then
            Dim statusCode As Integer = tockens(1)
            Dim statusMessage As String = Strings.Mid(Data, InStrRev(Data, ":") - 1)

            If statusCode = 376 Then RaiseEvent OnReady()

            If ((statusCode >= 200) AndAlso (statusCode < 400)) Then
                RaiseEvent OnStatus(statusMessage)
            ElseIf ((statusCode >= 400) AndAlso (statusCode <= 600)) Then
                RaiseEvent OnError(statusCode, statusMessage)
            End If

        Else
            Dim rxCommand As New System.Text.RegularExpressions.Regex("\s(?<command>\w+)\s", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)

            If rxCommand.Matches(Data).Count > 0 Then
                Debug.WriteLine("-+- " + rxCommand.Matches(Data).Item(0).Result("${command}"))

                Select Case rxCommand.Matches(Data).Item(0).Result("${command}")
                    Case "PRIVMSG"
                        Dim ircmsg As New System.Text.RegularExpressions.Regex("\:(?<user>.+)\s(?<command>\w+)\s(?<channel>.+)\s\:(?<message>.+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)
                        If ircmsg.Matches(Data)(0).Result("${channel}").StartsWith("#") Then
                            RaiseEvent OnMessage(ircmsg.Matches(Data)(0).Result("${channel}"), getUserFromAddress(ircmsg.Matches(Data)(0).Result("${user}")).Nick, ircmsg.Matches(Data)(0).Result("${message}"))
                        Else
                            RaiseEvent OnPrivateMessage(getUserFromAddress(ircmsg.Matches(Data)(0).Result("${user}")).Nick, ircmsg.Matches(Data)(0).Result("${message}"))
                        End If

                    Case "NOTICE"
                        'RaiseEvent OnNotice(USER, message)

                    Case "MODE"
                        'If strArray.Length > 3 Then RaiseEvent OnModeChanged(channel, USER, strArray(3), strArray(4))

                    Case "JOIN"
                        'RaiseEvent OnJoin(channel, USER)

                    Case "PART"
                        'RaiseEvent OnLeave(channel, USER)

                    Case "KICK"
                        Dim rxKick As New System.Text.RegularExpressions.Regex("\:(?<user>.+)\s(?<command>\w+)\s(?<channel>.+)\s\:(?<message>.+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)
                        RaiseEvent OnKick(rxKick.Matches(Data)(0).Result("${channel}"), getUserFromAddress(rxKick.Matches(Data)(0).Result("${user}")).Nick, rxKick.Matches(Data)(0).Result("${message}"))

                    Case "INVITE"
                        Dim rxInvite As New System.Text.RegularExpressions.Regex("\:(?<user>.+)\s(?<command>\w+)\s(?<nick>.+)\s\:(?<channel>.+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)
                        RaiseEvent OnInvite(rxInvite.Matches(Data)(0).Result("${channel}"), getUserFromAddress(rxInvite.Matches(Data)(0).Result("${user}")).Nick)

                    Case Else
                        Debug.WriteLine("-- " + rxCommand.Matches(Data).Item(0).Result("${command}"))

                End Select
            End If
        End If
    End Sub
#End Region

#Region " Quit "
        Public Sub Quit()
            Execute("QUIT :" & QuitMessage)
        End Sub
#End Region

#Region " Identify "
    Public Sub Identify(ByVal password As String)
        Execute("PRIVMSG NickServ IDENTIFY " + password)
    End Sub
#End Region

#Region " Say "
        Public Sub Say(ByVal Channel As String, ByVal [Text] As String)
            Execute("PRIVMSG " + Channel + " :" + [Text] + vbCrLf)
        End Sub
#End Region

#Region " SetAway "
        Public Sub SetAway(ByVal Message As String)
            Execute("AWAY :" & Message & vbCrLf)
        End Sub
#End Region

#Region " USER "
        Private Sub USER()
            Execute("USER " + Nickname + " " + ServerName + " " + HostName + " :" + RealName + vbCrLf)
        End Sub
#End Region

#Region " wsClient_onConnect "
        Private Sub _wsClient_onConnect() Handles _wsClient.onConnect
            RaiseEvent Console("Skitsanos IRC Cleint")
            RaiseEvent Console(Host & " connected")

            Nick(Nickname)
            USER()

            RaiseEvent Connected()
        End Sub
#End Region

#Region " _wsClient_onDataArrival "
    Private Sub _wsClient_onDataArrival(ByVal Data() As Byte) Handles _wsClient.onDataArrival
        Dim inData As String = _wsClient.BytestoString(Data)

        Dim lData() As String = Split(inData, Chr(10))

        For Each ircLine As String In lData
            Dim tokens As String() = ircLine.Split(" ")

            If tokens(0).ToUpper = "PING" Then
                Dim rxPong As New System.Text.RegularExpressions.Regex("PING\s\:(?<pong>.+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)
                Execute("PONG :" + rxPong.Matches(ircLine)(0).Result("${pong}"))
                Exit Sub

            ElseIf tokens(0).ToUpper = "ERROR" Then
                Me.Disconnect()
                RaiseEvent OnError(-1, ircLine)
                Exit Sub
            End If

            ParseData(ircLine)
        Next
    End Sub
#End Region

#Region " _wsClient_onError "
        Private Sub _wsClient_onError(ByVal Description As String) Handles _wsClient.onError
            RaiseEvent OnError(0, Description)
        End Sub
#End Region

#Region " getUserFromAddress "
    Private Function getUserFromAddress(ByVal address As String) As IrcUser
        Dim user As New IrcUser

        Dim rx As New System.Text.RegularExpressions.Regex("(?<user>.+?)\!(?<ident>.+?)\@(?<host>.+)", System.Text.RegularExpressions.RegexOptions.IgnoreCase Or System.Text.RegularExpressions.RegexOptions.Compiled)
        user.Nick = rx.Matches(address)(0).Result("${user}")
        user.Ident = rx.Matches(address)(0).Result("${ident}")
        user.Host = rx.Matches(address)(0).Result("${host}")

        Return user
    End Function
#End Region

End Class