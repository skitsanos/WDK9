Imports System.Web.Security
Imports System.Configuration.Provider
Imports System.Collections.Specialized
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics
Imports System.Web
Imports System.Globalization
Imports System.Security.Cryptography
Imports System.Text
Imports System.Web.Configuration

#Region " SQL Syntax "
' This provider works with the following schema for the table of user data.
' 
' CREATE TABLE Users
' (
'   id Guid NOT NULL PRIMARY KEY,
'   AccountUsername Text (255) NOT NULL,
'   ApplicationName Text (255) NOT NULL,
'   Email Text (255) NOT NULL,
'   Comment Text (255),
'   AccountPassword Text (128) NOT NULL,
'   PasswordQuestion Text (255),
'   PasswordAnswer Text (255),
'   IsApproved YesNo, 
'   LastActivityDate DateTime,
'   LastLoginDate DateTime,
'   LastPasswordChangedDate DateTime,
'   CreationDate DateTime, 
'   IsOnLine YesNo,
'   IsLockedOut YesNo,
'   LastLockedOutDate DateTime,
'   FailedPasswordAttemptCount Integer,
'   FailedPasswordAttemptWindowStart DateTime,
'   FailedPasswordAnswerAttemptCount Integer,
'   FailedPasswordAnswerAttemptWindowStart DateTime
' )
' 
#End Region

#Region " Compilation Instructions "
'vbc /out:netpass.dll /t:library netpass.vb /r:System.Web.dll /r:System.Configuration.dll
#End Region

Public NotInheritable Class NetPassMembershipProvider
    Inherits MembershipProvider

#Region " Properties "
    '
    ' Global generated password length, generic exception message, event log info.
    '

    Private newPasswordLength As Integer = 8
    Private eventSource As String = "NetPassProvider"
    Private eventLog As String = "Application"
    Private exceptionMessage As String = "An exception occurred. Please check the Event Log."
    Private tableName As String = "Users"
    Private connectionString As String

    '
    ' Used when determining encryption key values.
    '

    Private machineKey As MachineKeySection

    '
    ' If False, exceptions are thrown to the caller. If True,
    ' exceptions are written to the event log.
    '

    Private pWriteExceptionsToEventLog As Boolean

    Public Property WriteExceptionsToEventLog() As Boolean
        Get
            Return pWriteExceptionsToEventLog
        End Get
        Set(ByVal value As Boolean)
            pWriteExceptionsToEventLog = value
        End Set
    End Property

    '
    ' System.Web.Security.MembershipProvider properties.
    '

    Private pApplicationName As String = defAppName()
    Private pEnablePasswordReset As Boolean
    Private pEnablePasswordRetrieval As Boolean
    Private pRequiresQuestionAndAnswer As Boolean
    Private pRequiresUniqueEmail As Boolean
    Private pMaxInvalidPasswordAttempts As Integer
    Private pPasswordAttemptWindow As Integer
    Private pPasswordFormat As MembershipPasswordFormat

    Public Overrides Property ApplicationName() As String
        Get
            Return pApplicationName
        End Get
        Set(ByVal value As String)
            pApplicationName = value
        End Set
    End Property

    Public Overrides ReadOnly Property EnablePasswordReset() As Boolean
        Get
            Return pEnablePasswordReset
        End Get
    End Property

    Public Overrides ReadOnly Property EnablePasswordRetrieval() As Boolean
        Get
            Return pEnablePasswordRetrieval
        End Get
    End Property

    Public Overrides ReadOnly Property RequiresQuestionAndAnswer() As Boolean
        Get
            Return pRequiresQuestionAndAnswer
        End Get
    End Property

    Public Overrides ReadOnly Property RequiresUniqueEmail() As Boolean
        Get
            Return pRequiresUniqueEmail
        End Get
    End Property


    Public Overrides ReadOnly Property MaxInvalidPasswordAttempts() As Integer
        Get
            Return pMaxInvalidPasswordAttempts
        End Get
    End Property


    Public Overrides ReadOnly Property PasswordAttemptWindow() As Integer
        Get
            Return pPasswordAttemptWindow
        End Get
    End Property


    Public Overrides ReadOnly Property PasswordFormat() As MembershipPasswordFormat
        Get
            Return pPasswordFormat
        End Get
    End Property

    Private pMinRequiredNonAlphanumericCharacters As Integer

    Public Overrides ReadOnly Property MinRequiredNonAlphanumericCharacters() As Integer
        Get
            Return pMinRequiredNonAlphanumericCharacters
        End Get
    End Property

    Private pMinRequiredPasswordLength As Integer

    Public Overrides ReadOnly Property MinRequiredPasswordLength() As Integer
        Get
            Return pMinRequiredPasswordLength
        End Get
    End Property

    Private pPasswordStrengthRegularExpression As String

    Public Overrides ReadOnly Property PasswordStrengthRegularExpression() As String
        Get
            Return pPasswordStrengthRegularExpression
        End Get
    End Property
#End Region

#Region " Initialize "
    '
    ' System.Configuration.Provider.ProviderBase.Initialize Method
    '

    Public Overrides Sub Initialize(ByVal name As String, ByVal config As NameValueCollection)
        '
        ' Initialize values from web.config.
        '
        If config Is Nothing Then Throw New ArgumentNullException("config")

        If name Is Nothing OrElse name.Length = 0 Then name = "NetPassProvider"

        If String.IsNullOrEmpty(config("description")) Then
            config.Remove("description")
            config.Add("description", "NetPass Membership provider")
        End If

        ' Initialize the abstract base class.
        MyBase.Initialize(name, config)

        pApplicationName = GetConfigValue(config("applicationName"), defAppName)
        pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config("maxInvalidPasswordAttempts"), "5"))
        pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config("passwordAttemptWindow"), "10"))
        pMinRequiredNonAlphanumericCharacters = Convert.ToInt32(GetConfigValue(config("minRequiredAlphaNumericCharacters"), "1"))
        pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config("minRequiredPasswordLength"), "7"))
        pPasswordStrengthRegularExpression = Convert.ToString(GetConfigValue(config("passwordStrengthRegularExpression"), ""))
        pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config("enablePasswordReset"), "True"))
        pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config("enablePasswordRetrieval"), "True"))
        pRequiresQuestionAndAnswer = Convert.ToBoolean(GetConfigValue(config("requiresQuestionAndAnswer"), "False"))
        pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config("requiresUniqueEmail"), "True"))
        pWriteExceptionsToEventLog = Convert.ToBoolean(GetConfigValue(config("writeExceptionsToEventLog"), "True"))

        Dim temp_format As String = config("passwordFormat")
        If temp_format Is Nothing Then
            temp_format = "Hashed"
        End If

        Select Case temp_format
            Case "Hashed"
                pPasswordFormat = MembershipPasswordFormat.Hashed
            Case "Encrypted"
                pPasswordFormat = MembershipPasswordFormat.Encrypted
            Case "Clear"
                pPasswordFormat = MembershipPasswordFormat.Clear
            Case Else
                Throw New ProviderException("AccountPassword format not supported.")
        End Select

        '
        ' Initialize SqlConnection.
        '

        Dim ConnectionStringSettings As ConnectionStringSettings = _
          ConfigurationManager.ConnectionStrings(config("connectionStringName"))

        If ConnectionStringSettings Is Nothing OrElse ConnectionStringSettings.ConnectionString.Trim() = "" Then
            Throw New ProviderException("Connection string cannot be blank.")
        End If

        connectionString = ConnectionStringSettings.ConnectionString


        ' Get encryption and decryption key information from the configuration.
        Dim cfg As System.Configuration.Configuration = _
          WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
        machineKey = CType(cfg.GetSection("system.web/machineKey"), MachineKeySection)

        If machineKey.ValidationKey.Contains("AutoGenerate") Then _
          If PasswordFormat <> MembershipPasswordFormat.Clear Then _
            Throw New ProviderException("Hashed or Encrypted passwords are not supported with auto-generated keys.")
    End Sub
#End Region

#Region " GetConfigValue "
    '
    ' A helper function to retrieve config values from the configuration file.
    '

    Private Function GetConfigValue(ByVal configValue As String, ByVal defaultValue As String) As String
        If String.IsNullOrEmpty(configValue) Then _
          Return defaultValue

        Return configValue
    End Function
#End Region

#Region " ChangePassword "
    '
    ' System.Web.Security.MembershipProvider methods.
    '

    '
    ' MembershipProvider.ChangePassword
    '

    Public Overrides Function ChangePassword(ByVal AccountUsername As String, _
    ByVal oldPwd As String, _
    ByVal newPwd As String) As Boolean
        If Not ValidateUser(AccountUsername, oldPwd) Then Return False


        Dim args As ValidatePasswordEventArgs = New ValidatePasswordEventArgs(AccountUsername, newPwd, True)

        OnValidatingPassword(args)

        If args.Cancel Then
            If Not args.FailureInformation Is Nothing Then
                Throw args.FailureInformation
            Else
                Throw New ProviderException("Change password canceled due to New password validation failure.")
            End If
        End If


        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("UPDATE " + tableName + " SET AccountPassword = @AccountPassword, LastPasswordChangedDate = @LastPasswordChangedDate WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@AccountPassword", EncodePassword(newPwd))
        cmd.Parameters.AddWithValue("@LastPasswordChangedDate", DateTime.Now)
        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim rowsAffected As Integer = 0

        Try
            conn.Open()

            rowsAffected = cmd.ExecuteNonQuery()
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            conn.Close()
        End Try

        If rowsAffected > 0 Then
            Return True
        End If

        Return False
    End Function
#End Region

#Region " ChangePasswordQuestionAndAnswer "
    '
    ' MembershipProvider.ChangePasswordQuestionAndAnswer
    '

    Public Overrides Function ChangePasswordQuestionAndAnswer(ByVal AccountUsername As String, _
    ByVal password As String, _
    ByVal newPwdQuestion As String, _
    ByVal newPwdAnswer As String) As Boolean

        If Not ValidateUser(AccountUsername, password) Then _
          Return False

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("UPDATE " + tableName + " SET PasswordQuestion = @Question, PasswordAnswer = @Answer WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@Question", newPwdQuestion)
        cmd.Parameters.AddWithValue("@Answer", EncodePassword(newPwdAnswer))
        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)


        Dim rowsAffected As Integer = 0

        Try
            conn.Open()

            rowsAffected = cmd.ExecuteNonQuery()
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            conn.Close()
        End Try

        If rowsAffected > 0 Then
            Return True
        End If

        Return False
    End Function
#End Region

#Region " CreateUser "
    '
    ' MembershipProvider.CreateUser
    '

    Public Overrides Function CreateUser(ByVal AccountUsername As String, ByVal password As String, ByVal email As String, ByVal passwordQuestion As String, ByVal passwordAnswer As String, ByVal isApproved As Boolean, ByVal providerUserKey As Object, ByRef status As MembershipCreateStatus) As MembershipUser
        Dim Args As ValidatePasswordEventArgs = New ValidatePasswordEventArgs(AccountUsername, password, True)

        OnValidatingPassword(Args)

        If Args.Cancel Then
            status = MembershipCreateStatus.InvalidPassword
            Return Nothing
        End If


        If RequiresUniqueEmail AndAlso GetUsernameByEmail(email) <> "" Then
            status = MembershipCreateStatus.DuplicateEmail
            Return Nothing
        End If

        Dim u As MembershipUser = GetUser(AccountUsername, False)

        If u Is Nothing Then
            Dim createDate As DateTime = DateTime.Now

            If providerUserKey Is Nothing Then
                providerUserKey = Guid.NewGuid()
            Else
                If Not TypeOf providerUserKey Is Guid Then
                    status = MembershipCreateStatus.InvalidProviderUserKey
                    Return Nothing
                End If
            End If

            Dim conn As SqlConnection = New SqlConnection(connectionString)
            Dim cmd As SqlCommand = New SqlCommand("INSERT INTO " & tableName & "" & _
             " (AccountUsername, AccountPassword, Email, PasswordQuestion, PasswordAnswer, IsApproved, Comment, CreationDate, LastPasswordChangedDate, LastActivityDate, ApplicationName, IsLockedOut, LastLockedOutDate, FailedPasswordAttemptCount, FailedPasswordAttemptWindowStart,  FailedPasswordAnswerAttemptCount, FailedPasswordAnswerAttemptWindowStart) " & _
             " Values (@AccountUsername, @AccountPassword, @Email, @PasswordQuestion, @PasswordAnswer, @IsApproved, @Comment, @CreationDate, @LastPasswordChangedDate, @LastActivityDate, @ApplicationName, @IsLockedOut, @LastLockedOutDate, @FailedPasswordAttemptCount, @FailedPasswordAttemptWindowStart,  @FailedPasswordAnswerAttemptCount, @FailedPasswordAnswerAttemptWindowStart)", conn)

            'cmd.Parameters.Add("@id", SqlType.Int).Value = providerUserKey
            cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
            cmd.Parameters.AddWithValue("@AccountPassword", EncodePassword(password))
            cmd.Parameters.AddWithValue("@Email", email)
            cmd.Parameters.AddWithValue("@PasswordQuestion", passwordQuestion)
            cmd.Parameters.AddWithValue("@PasswordAnswer", EncodePassword(passwordAnswer))
            cmd.Parameters.AddWithValue("@IsApproved", isApproved)
            cmd.Parameters.AddWithValue("@Comment", "")
            cmd.Parameters.AddWithValue("@CreationDate", createDate)
            cmd.Parameters.AddWithValue("@LastPasswordChangedDate", createDate)
            cmd.Parameters.AddWithValue("@LastActivityDate", createDate)
            cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)
            cmd.Parameters.AddWithValue("@IsLockedOut", False)
            cmd.Parameters.AddWithValue("@LastLockedOutDate", createDate)
            cmd.Parameters.AddWithValue("@FailedPasswordAttemptCount", 0)
            cmd.Parameters.AddWithValue("@FailedPasswordAttemptWindowStart", createDate)
            cmd.Parameters.AddWithValue("@FailedPasswordAnswerAttemptCount", 0)
            cmd.Parameters.AddWithValue("@FailedPasswordAnswerAttemptWindowStart", createDate)

            Try
                conn.Open()

                Dim recAdded As Integer = cmd.ExecuteNonQuery()

                If recAdded > 0 Then
                    status = MembershipCreateStatus.Success
                Else
                    status = MembershipCreateStatus.UserRejected
                End If
            Catch e As Exception
                If WriteExceptionsToEventLog Then Log(e.ToString, True)

                status = MembershipCreateStatus.ProviderError
            Finally
                conn.Close()
            End Try

            Return GetUser(AccountUsername, False)
        Else
            status = MembershipCreateStatus.DuplicateUserName
        End If

        Return Nothing
    End Function
#End Region

#Region " DeleteUser "
    '
    ' MembershipProvider.DeleteUser
    '
    Public Overrides Function DeleteUser(ByVal AccountUsername As String, ByVal deleteAllRelatedData As Boolean) As Boolean
        Dim conn As SqlConnection = Nothing
        Dim rowsAffected As Integer = 0

        Try
            conn = New SqlConnection(connectionString)
            conn.Open()

            Dim cmd As New SqlCommand("DELETE FROM " & tableName & " WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)
            cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
            cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

            rowsAffected = cmd.ExecuteNonQuery()

            If deleteAllRelatedData Then
                cmd = New SqlCommand("DELETE FROM UsersInRoles WHERE ApplicationName=@ApplicationName AND AccountUsername=@AccountUsername", conn)
                cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)
                cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
                cmd.ExecuteNonQuery()
            End If

        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If conn IsNot Nothing Then conn.Close()
        End Try

        If rowsAffected > 0 Then Return True

        Return False
    End Function
#End Region

#Region " GetAllUsers {paged}"
    '
    ' MembershipProvider.GetAllUsers
    '

    Public Overrides Function GetAllUsers(ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT Count(id) FROM " & tableName & " WHERE ApplicationName = @ApplicationName", conn)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim users As New MembershipUserCollection()

        Dim reader As SqlDataReader = Nothing
        totalRecords = 0

        Try
            conn.Open()
            totalRecords = CInt(cmd.ExecuteScalar())

            If totalRecords <= 0 Then Return users

            cmd.CommandText = "SELECT id, AccountUsername, Email, PasswordQuestion," & _
                     " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," & _
                     " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " & _
                     " FROM " & tableName & " WHERE ApplicationName = @ApplicationName ORDER BY AccountUsername Asc"

            reader = cmd.ExecuteReader()

            Dim counter As Integer = 0
            Dim startIndex As Integer = pageSize * pageIndex
            Dim endIndex As Integer = startIndex + pageSize - 1

            Do While reader.Read()
                If counter >= startIndex Then
                    Dim u As MembershipUser = GetUserFromReader(reader)
                    users.Add(u)
                End If
                If counter >= endIndex Then cmd.Cancel()
                counter += 1
            Loop

        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()
            conn.Close()
        End Try

        Return users
    End Function
#End Region

#Region " GetAllUsers "
    '
    ' MembershipProvider.GetAllUsers
    '

    Public Overloads Function GetAllUsers() As MembershipUserCollection
        Dim conn As New SqlConnection(connectionString)
        Dim users As New MembershipUserCollection()
        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()
            Dim cmd As New SqlCommand
            cmd.Connection = conn
            cmd.CommandText = "SELECT id, AccountUsername, Email, PasswordQuestion," & _
                     " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," & _
                     " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " & _
                     " FROM " & tableName & " WHERE ApplicationName = @ApplicationName ORDER BY AccountUsername Asc"

            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

            reader = cmd.ExecuteReader()

            Dim counter As Integer = 0

            Do While reader.Read()
                Dim u As MembershipUser = GetUserFromReader(reader)
                users.Add(u)
            Loop

        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()
            conn.Close()
        End Try

        Return users
    End Function
#End Region

#Region " GetNumberOfUsersOnline "
    '
    ' MembershipProvider.GetNumberOfUsersOnline
    '

    Public Overrides Function GetNumberOfUsersOnline() As Integer

        Dim onlineSpan As TimeSpan = New TimeSpan(0, System.Web.Security.Membership.UserIsOnlineTimeWindow, 0)
        Dim compareTime As DateTime = DateTime.Now.Subtract(onlineSpan)

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT Count(*) FROM " & tableName & "" & _
                " WHERE LastActivityDate > @CompareDate AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@CompareDate", compareTime)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim numOnline As Integer = 0

        Try
            conn.Open()

            numOnline = CInt(cmd.ExecuteScalar())
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            conn.Close()
        End Try

        Return numOnline
    End Function

#End Region

#Region " GetPassword "
    '
    ' MembershipProvider.GetPassword
    '

    Public Overrides Function GetPassword(ByVal AccountUsername As String, ByVal answer As String) As String

        If Not EnablePasswordRetrieval Then
            Throw New ProviderException("AccountPassword Retrieval Not Enabled.")
        End If

        If PasswordFormat = MembershipPasswordFormat.Hashed Then
            Throw New ProviderException("Cannot retrieve Hashed passwords.")
        End If

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT AccountPassword, PasswordAnswer, IsLockedOut FROM " & tableName & "" & _
              " WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim password As String = ""
        Dim passwordAnswer As String = ""
        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader(CommandBehavior.SingleRow)

            If reader.HasRows Then
                reader.Read()

                If reader.GetBoolean(2) Then _
                  Throw New MembershipPasswordException("The supplied user is locked out.")

                password = reader.GetString(0)
                passwordAnswer = reader.GetString(1)
            Else
                Throw New MembershipPasswordException("The supplied user name is not found.")
            End If
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()
            conn.Close()
        End Try


        If RequiresQuestionAndAnswer AndAlso Not CheckPassword(answer, passwordAnswer) Then
            UpdateFailureCount(AccountUsername, "passwordAnswer")

            Throw New MembershipPasswordException("Incorrect password answer.")
        End If


        If PasswordFormat = MembershipPasswordFormat.Encrypted Then
            password = UnEncodePassword(password)
        End If

        Return password
    End Function
#End Region

#Region " GetUser "
    '
    ' MembershipProvider.GetUser(String, Boolean)
    '
    Public Overrides Function GetUser(ByVal AccountUsername As String, ByVal userIsOnline As Boolean) As MembershipUser

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT id, AccountUsername, Email, PasswordQuestion, Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate, LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " & _
              " FROM " & tableName & " WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim u As MembershipUser = Nothing
        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                u = GetUserFromReader(reader)

                If userIsOnline Then
                    Dim updateCmd As SqlCommand = New SqlCommand("UPDATE " + tableName + " SET LastActivityDate = @LastActivityDate WHERE AccountUsername = @AccountUsername AND Applicationname = @ApplicationName", conn)

                    updateCmd.Parameters.AddWithValue("@LastActivityDate", DateTime.Now)
                    updateCmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
                    updateCmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

                    updateCmd.ExecuteNonQuery()
                End If
            End If

        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()

            conn.Close()
        End Try

        Return u
    End Function
#End Region

#Region " GetUser {byProvider} "
    '
    ' MembershipProvider.GetUser(Object, Boolean)
    '

    Public Overrides Function GetUser(ByVal providerUserKey As Object, ByVal userIsOnline As Boolean) As MembershipUser

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT id, AccountUsername, Email, PasswordQuestion," & _
              " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," & _
              " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate" & _
              " FROM " & tableName & " WHERE id = @id", conn)

        cmd.Parameters.AddWithValue("@id", providerUserKey)

        Dim u As MembershipUser = Nothing
        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader()

            If reader.HasRows Then
                reader.Read()
                u = GetUserFromReader(reader)

                If userIsOnline Then
                    Dim updateCmd As SqlCommand = New SqlCommand("UPDATE " + tableName + " SET LastActivityDate = @LastActivityDate WHERE id = @id", conn)

                    updateCmd.Parameters.AddWithValue("@LastActivityDate", DateTime.Now)
                    updateCmd.Parameters.AddWithValue("@id", providerUserKey)

                    updateCmd.ExecuteNonQuery()
                End If
            End If
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()

            conn.Close()
        End Try

        Return u
    End Function

#End Region

#Region " GetUserFromReader "
    '
    ' GetUserFromReader
    '    A helper function that takes the current row from the SqlDataReader
    ' and hydrates a MembershiUser from the values. Called by the 
    ' MembershipUser.GetUser implementation.
    '
    Private Function GetUserFromReader(ByVal reader As SqlDataReader) As MembershipUser
        'id, AccountUsername, Email, PasswordQuestion, Comment, IsApproved, 
        'isLockedOut, creationDate, lastLoginDate, lastActivityDate, 
        'lastPasswordChangedDate, lastLockedOutDate

        Dim providerUserKey As Object = reader("id")
        Dim AccountUsername As String = reader("AccountUsername")
        Dim email As String = reader("Email")

        Dim passwordQuestion As String = ""
        If Not reader("PasswordQuestion") Is DBNull.Value Then passwordQuestion = reader("PasswordQuestion")

        Dim comment As String = ""
        If Not reader("Comment") Is DBNull.Value Then comment = reader("Comment")

        Dim isApproved As Boolean = reader("IsApproved")
        Dim isLockedOut As Boolean = reader("isLockedOut")
        Dim creationDate As DateTime = reader("creationDate")

        Dim lastLoginDate As DateTime = New DateTime()
        If Not reader("lastLoginDate") Is DBNull.Value Then lastLoginDate = reader("lastLoginDate")

        Dim lastActivityDate As DateTime = reader("lastActivityDate")
        Dim lastPasswordChangedDate As DateTime = reader("lastPasswordChangedDate")

        Dim lastLockedOutDate As DateTime = New DateTime()
        If Not reader("lastLockedOutDate") Is DBNull.Value Then lastLockedOutDate = reader("lastLockedOutDate")

        Dim u As MembershipUser = New MembershipUser(Me.Name, _
                                              AccountUsername, _
                                              providerUserKey, _
                                              email, _
                                              passwordQuestion, _
                                              comment, _
                                              isApproved, _
                                              isLockedOut, _
                                              creationDate, _
                                              lastLoginDate, _
                                              lastActivityDate, _
                                              lastPasswordChangedDate, _
                                              lastLockedOutDate)


        Return u



    End Function
#End Region

#Region " UnlockUser "
    '
    ' MembershipProvider.UnlockUser
    '
    Public Overrides Function UnlockUser(ByVal AccountUsername As String) As Boolean
        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("UPDATE " + tableName + " SET IsLockedOut = 0, LastLockedOutDate = @LastLockedOutDate WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@LastLockedOutDate", DateTime.Now)
        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim rowsAffected As Integer = 0

        Try
            conn.Open()

            rowsAffected = cmd.ExecuteNonQuery()
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            conn.Close()
        End Try

        If rowsAffected > 0 Then Return True

        Return False
    End Function
#End Region

#Region " GetAccountUsernameByEmail "
    '
    ' MembershipProvider.GetAccountUsernameByEmail
    '
    Public Overrides Function GetUsernameByEmail(ByVal email As String) As String
        Dim conn As SqlConnection = New SqlConnection(connectionString)

        Dim AccountUsername As String = ""

        Try
            conn.Open()

            Dim cmd As SqlCommand = New SqlCommand("SELECT AccountUsername FROM " & tableName & " WHERE Email = @Email AND ApplicationName = @ApplicationName", conn)

            cmd.Parameters.AddWithValue("@Email", email)
            cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

            Dim dba As New SqlDataAdapter(cmd)
            Dim dbs As New DataSet
            dbs.Clear()
            dba.Fill(dbs)

            If IsNothing(dbs.Tables(tableName)) = False Then
                AccountUsername = dbs.Tables(tableName).Rows(0).Item("AccountUsername").ToString
            Else
                AccountUsername = ""
            End If

            'AccountUsername = cmd.ExecuteScalar().ToString()

        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            conn.Close()
        End Try

        If AccountUsername Is Nothing Then AccountUsername = ""

        Return AccountUsername
    End Function
#End Region

#Region " ResetPassword "
    '
    ' MembershipProvider.ResetPassword
    '
    Public Overrides Function ResetPassword(ByVal AccountUsername As String, ByVal answer As String) As String

        If Not EnablePasswordReset Then
            Throw New NotSupportedException("AccountPassword Reset is not enabled.")
        End If

        If answer Is Nothing AndAlso RequiresQuestionAndAnswer Then
            UpdateFailureCount(AccountUsername, "passwordAnswer")

            Throw New ProviderException("AccountPassword answer required for password Reset.")
        End If

        Dim newPassword As String = _
          System.Web.Security.Membership.GeneratePassword(newPasswordLength, MinRequiredNonAlphanumericCharacters)


        Dim Args As ValidatePasswordEventArgs = _
          New ValidatePasswordEventArgs(AccountUsername, newPassword, True)

        OnValidatingPassword(Args)

        If Args.Cancel Then
            If Not Args.FailureInformation Is Nothing Then
                Throw Args.FailureInformation
            Else
                Throw New MembershipPasswordException("Reset password canceled due to password validation failure.")
            End If
        End If


        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT PasswordAnswer, IsLockedOut FROM " & tableName & "" & _
              " WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim rowsAffected As Integer = 0
        Dim passwordAnswer As String = ""
        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader(CommandBehavior.SingleRow)

            If reader.HasRows Then
                reader.Read()

                If reader.GetBoolean(1) Then _
                  Throw New MembershipPasswordException("The supplied user is locked out.")

                passwordAnswer = reader.GetString(0)
            Else
                Throw New MembershipPasswordException("The supplied user name is not found.")
            End If

            If RequiresQuestionAndAnswer AndAlso Not CheckPassword(answer, passwordAnswer) Then
                UpdateFailureCount(AccountUsername, "passwordAnswer")

                Throw New MembershipPasswordException("Incorrect password answer.")
            End If

            Dim updateCmd As SqlCommand = New SqlCommand("UPDATE " & tableName & "" & _
                " SET AccountPassword = @AccountPassword, LastPasswordChangedDate = @LastPasswordChangedDate" & _
                " WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName AND IsLockedOut = 0", conn)

            updateCmd.Parameters.AddWithValue("@AccountPassword", EncodePassword(newPassword))
            updateCmd.Parameters.AddWithValue("@LastPasswordChangedDate", DateTime.Now)
            updateCmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
            updateCmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

            rowsAffected = updateCmd.ExecuteNonQuery()
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()
            conn.Close()
        End Try

        If rowsAffected > 0 Then
            Return newPassword
        Else
            Throw New MembershipPasswordException("User not found, or user is locked out. AccountPassword not Reset.")
        End If
    End Function
#End Region

#Region " UpdateUser "
    '
    ' MembershipProvider.UpdateUser
    '
    Public Overrides Sub UpdateUser(ByVal user As MembershipUser)

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("UPDATE " & tableName & "" & _
                " SET Email = @Email, Comment = @Comment," & _
                " IsApproved = @IsApproved" & _
                " WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@Email", user.Email)
        cmd.Parameters.AddWithValue("@Comment", user.Comment)
        cmd.Parameters.AddWithValue("@IsApproved", user.IsApproved)
        cmd.Parameters.AddWithValue("@AccountUsername", user.UserName)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)


        Try
            conn.Open()

            cmd.ExecuteNonQuery()
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            conn.Close()
        End Try
    End Sub
#End Region

#Region " ValidateUser "
    '
    ' MembershipProvider.ValidateUser
    '
    Public Overrides Function ValidateUser(ByVal AccountUsername As String, ByVal password As String) As Boolean
        Dim isValid As Boolean = False

        Dim conn As New SqlConnection(connectionString)
        Dim cmd As New SqlCommand("SELECT AccountPassword, IsApproved FROM " & tableName & " WHERE AccountUsername=@AccountUsername AND ApplicationName=@ApplicationName AND IsLockedOut=0", conn)

        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim reader As SqlDataReader = Nothing
        Dim isApproved As Boolean = False
        Dim pwd As String = ""

        Try
            conn.Open()

            reader = cmd.ExecuteReader(CommandBehavior.SingleRow)

            If reader.HasRows Then
                reader.Read()
                pwd = reader("AccountPassword")
                isApproved = reader("IsApproved")
            Else
                Return False
            End If

            reader.Close()

            If CheckPassword(password, pwd) Then
                If isApproved Then
                    isValid = True

                    Dim updateCmd As SqlCommand = New SqlCommand("UPDATE " & tableName & " SET LastLoginDate=@LastLoginDate WHERE AccountUsername=@AccountUsername AND ApplicationName=@ApplicationName", conn)

                    updateCmd.Parameters.AddWithValue("@LastLoginDate", Now)
                    updateCmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
                    updateCmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

                    updateCmd.ExecuteNonQuery()
                End If
            Else
                conn.Close()

                UpdateFailureCount(AccountUsername, "password")
            End If
        Catch e As Exception
            Log("NetPass.ValidateUser {" & AccountUsername & "}" & e.ToString, True)

            If WriteExceptionsToEventLog Then
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()
            conn.Close()
        End Try

        Return isValid
    End Function
#End Region

#Region " UpdateFailureCount "
    '
    ' UpdateFailureCount
    '   A helper method that performs the checks and updates associated with
    ' password failure tracking.
    '
    Private Sub UpdateFailureCount(ByVal AccountUsername As String, ByVal failureType As String)

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT FailedPasswordAttemptCount, " & _
                                          "  FailedPasswordAttemptWindowStart, " & _
                                          "  FailedPasswordAnswerAttemptCount, " & _
                                          "  FailedPasswordAnswerAttemptWindowStart " & _
                                          "  FROM " & tableName & " " & _
                                          "  WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim reader As SqlDataReader = Nothing
        Dim windowStart As DateTime = New DateTime()
        Dim failureCount As Integer = 0

        Try
            conn.Open()

            reader = cmd.ExecuteReader(CommandBehavior.SingleRow)

            If reader.HasRows Then
                reader.Read()

                If failureType = "password" Then
                    failureCount = reader.GetInt32(0)
                    windowStart = reader.GetDateTime(1)
                End If

                If failureType = "passwordAnswer" Then
                    failureCount = reader.GetInt32(2)
                    windowStart = reader.GetDateTime(3)
                End If
            End If

            reader.Close()

            Dim windowEnd As DateTime = windowStart.AddMinutes(PasswordAttemptWindow)

            If failureCount = 0 OrElse DateTime.Now > windowEnd Then
                ' First password failure or outside of PasswordAttemptWindow. 
                ' Start a New password failure count from 1 and a New window starting now.

                If failureType = "password" Then _
                  cmd.CommandText = "UPDATE " & tableName & " " & _
                                    "  SET FailedPasswordAttemptCount = @Count, " & _
                                    "      FailedPasswordAttemptWindowStart = @WindowStart " & _
                                    "  WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName"

                If failureType = "passwordAnswer" Then _
                  cmd.CommandText = "UPDATE " & tableName & " " & _
                                    "  SET FailedPasswordAnswerAttemptCount = @Count, " & _
                                    "      FailedPasswordAnswerAttemptWindowStart = @WindowStart " & _
                                    "  WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName"

                cmd.Parameters.Clear()

                cmd.Parameters.AddWithValue("@Count", 1)
                cmd.Parameters.AddWithValue("@WindowStart", DateTime.Now)
                cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
                cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

                If cmd.ExecuteNonQuery() < 0 Then _
                  Throw New ProviderException("Unable to update failure count and window start.")
            Else
                failureCount += 1

                If failureCount >= MaxInvalidPasswordAttempts Then
                    ' AccountPassword attempts have exceeded the failure threshold. Lock out
                    ' the user.

                    cmd.CommandText = "UPDATE " & tableName & " " & _
                                      "  SET IsLockedOut = @IsLockedOut, LastLockedOutDate = @LastLockedOutDate " & _
                                      "  WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName"

                    cmd.Parameters.Clear()

                    cmd.Parameters.AddWithValue("@IsLockedOut", True)
                    cmd.Parameters.AddWithValue("@LastLockedOutDate", DateTime.Now)
                    cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
                    cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

                    If cmd.ExecuteNonQuery() < 0 Then _
                      Throw New ProviderException("Unable to lock out user.")
                Else
                    ' AccountPassword attempts have not exceeded the failure threshold. Update
                    ' the failure counts. Leave the window the same.

                    If failureType = "password" Then _
                      cmd.CommandText = "UPDATE " & tableName & " " & _
                                        "  SET FailedPasswordAttemptCount = @Count" & _
                                        "  WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName"

                    If failureType = "passwordAnswer" Then _
                      cmd.CommandText = "UPDATE " & tableName & " " & _
                                        "  SET FailedPasswordAnswerAttemptCount = @Count" & _
                                        "  WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName"

                    cmd.Parameters.Clear()

                    cmd.Parameters.AddWithValue("@Count", failureCount)
                    cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
                    cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

                    If cmd.ExecuteNonQuery() < 0 Then _
                      Throw New ProviderException("Unable to update failure count.")
                End If
            End If
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()
            conn.Close()
        End Try
    End Sub
#End Region

#Region " CheckPassword "
    '
    ' CheckPassword
    '   Compares password values based on the MembershipPasswordFormat.
    '
    Private Function CheckPassword(ByVal password As String, ByVal dbpassword As String) As Boolean
        Dim pass1 As String = password
        Dim pass2 As String = dbpassword

        Select Case PasswordFormat
            Case MembershipPasswordFormat.Encrypted
                pass2 = UnEncodePassword(dbpassword)
            Case MembershipPasswordFormat.Hashed
                pass1 = EncodePassword(password)
            Case Else
        End Select

        If pass1 = pass2 Then
            Return True
        End If

        Return False
    End Function
#End Region

#Region " EncodePassword "
    '
    ' EncodePassword
    '   Encrypts, Hashes, or leaves the password clear based on the PasswordFormat.
    '

    Private Function EncodePassword(ByVal password As String) As String
        Dim encodedPassword As String = password

        Select Case PasswordFormat
            Case MembershipPasswordFormat.Clear

            Case MembershipPasswordFormat.Encrypted
                encodedPassword = _
                  Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)))
            Case MembershipPasswordFormat.Hashed
                Dim hash As HMACSHA1 = New HMACSHA1()
                hash.Key = HexToByte(machineKey.ValidationKey)
                encodedPassword = _
                  Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)))
            Case Else
                Throw New ProviderException("Unsupported password format.")
        End Select

        Return encodedPassword
    End Function
#End Region

#Region " UnEncodePassword "
    '
    ' UnEncodePassword
    '   Decrypts or leaves the password clear based on the PasswordFormat.
    '

    Private Function UnEncodePassword(ByVal encodedPassword As String) As String
        Dim password As String = encodedPassword

        Select Case PasswordFormat
            Case MembershipPasswordFormat.Clear

            Case MembershipPasswordFormat.Encrypted
                password = _
                  Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)))
            Case MembershipPasswordFormat.Hashed
                Throw New ProviderException("Cannot unencode a hashed password.")
            Case Else
                Throw New ProviderException("Unsupported password format.")
        End Select

        Return password
    End Function
#End Region

#Region " HexToByte "
    '
    ' HexToByte
    '   Converts a hexadecimal string to a byte array. Used to convert encryption
    ' key values from the configuration.
    '

    Private Function HexToByte(ByVal hexString As String) As Byte()
        Dim ReturnBytes((hexString.Length \ 2) - 1) As Byte
        For i As Integer = 0 To ReturnBytes.Length - 1
            ReturnBytes(i) = Convert.ToByte(hexString.Substring(i * 2, 2), 16)
        Next
        Return ReturnBytes
    End Function
#End Region

#Region " FindUsersByName - paged "
    '
    ' MembershipProvider.FindUsersByName
    '
    Public Overrides Function FindUsersByName(ByVal AccountUsernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection
        AccountUsernameToMatch = AccountUsernameToMatch.Replace("'", "")

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT Count(*) FROM " & tableName & " " & _
                  "WHERE AccountUsername LIKE @AccountUsername AND ApplicationName = @ApplicationName", conn)
        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsernameToMatch)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim users As MembershipUserCollection = New MembershipUserCollection()

        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()
            totalRecords = CInt(cmd.ExecuteScalar())

            If totalRecords <= 0 Then Return users

            cmd.CommandText = "SELECT id, AccountUsername, Email, PasswordQuestion," & _
              " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," & _
              " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " & _
              " FROM " & tableName & " " & _
              " WHERE ApplicationName = @ApplicationName AND AccountUsername LIKE '%" & AccountUsernameToMatch & "%' ORDER BY AccountUsername Asc"

            reader = cmd.ExecuteReader()

            Dim counter As Integer = 0
            Dim startIndex As Integer = pageSize * pageIndex
            Dim endIndex As Integer = startIndex + pageSize - 1

            Do While reader.Read()
                If counter >= startIndex Then
                    Dim u As MembershipUser = GetUserFromReader(reader)
                    users.Add(u)
                End If

                If counter >= endIndex Then cmd.Cancel()

                counter += 1
            Loop
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()

            conn.Close()
        End Try

        Return users
    End Function
#End Region

#Region " FindUsersbyName "
    Public Overloads Function FindUsersByName(ByVal AccountUsernameToMatch As String) As MembershipUserCollection
        AccountUsernameToMatch = AccountUsernameToMatch.Replace("'", "")


        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim users As New MembershipUserCollection

        Try
            conn.Open()

            Dim reader As SqlDataReader = Nothing

            Dim sqlStr As String = "SELECT id, AccountUsername, Email, PasswordQuestion," & _
              " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," & _
              " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " & _
              " FROM " & tableName & " " & _
              " WHERE ApplicationName = @ApplicationName AND AccountUsername LIKE '%" & AccountUsernameToMatch & "%' ORDER BY AccountUsername Asc"

            Dim cmd As New SqlCommand(sqlStr, conn)
            cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)
            cmd.Parameters.AddWithValue("@AccountUsername", AccountUsernameToMatch)

            reader = cmd.ExecuteReader()

            Do While reader.Read()
                Dim u As MembershipUser = GetUserFromReader(reader)
                users.Add(u)
            Loop

        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            conn.Close()
        End Try

        Return users
    End Function
#End Region

#Region " FindUsersByEmail "
    '
    ' MembershipProvider.FindUsersByEmail
    '
    Public Overrides Function FindUsersByEmail(ByVal emailToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As MembershipUserCollection

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT Count(*) FROM " + tableName + " WHERE Email LIKE @EmailSearch AND ApplicationName = @ApplicationName", conn)
        cmd.Parameters.AddWithValue("@EmailSearch", emailToMatch)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim users As MembershipUserCollection = New MembershipUserCollection()

        Dim reader As SqlDataReader = Nothing
        totalRecords = 0

        Try
            conn.Open()
            totalRecords = CInt(cmd.ExecuteScalar())

            If totalRecords <= 0 Then Return users

            cmd.CommandText = "SELECT id, AccountUsername, Email, PasswordQuestion," & _
                     " Comment, IsApproved, IsLockedOut, CreationDate, LastLoginDate," & _
                     " LastActivityDate, LastPasswordChangedDate, LastLockedOutDate " & _
                     " FROM " & tableName & " " & _
                     " WHERE Email LIKE @EmailSearch AND ApplicationName = @ApplicationName " & _
                     " ORDER BY AccountUsername Asc"

            reader = cmd.ExecuteReader()

            Dim counter As Integer = 0
            Dim startIndex As Integer = pageSize * pageIndex
            Dim endIndex As Integer = startIndex + pageSize - 1

            Do While reader.Read()
                If counter >= startIndex Then
                    Dim u As MembershipUser = GetUserFromReader(reader)
                    users.Add(u)
                End If

                If counter >= endIndex Then cmd.Cancel()

                counter += 1
            Loop
        Catch e As SqlException
            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw e
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()

            conn.Close()
        End Try

        Return users
    End Function
#End Region

End Class