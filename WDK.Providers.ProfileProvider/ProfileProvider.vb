Imports System.Web.Profile
Imports System.Configuration.Provider
Imports System.Collections.Specialized
Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Configuration
Imports System.Diagnostics
Imports System.Web
Imports System.Collections
Imports System.Web.Configuration

#Region " SQL Syntax "

#End Region

#Region " web.config behavior "
'<anonymousIdentification enabled="true" />
'
'<profile defaultProvider="SqlProvider">
'   <providers>
'     <add
'       name="SqlProvider"
'       type="Samples.AspNet.Profile.SqlProfileProvider" 
'       connectionStringName="SqlProfile" /> 
'   </providers>

'   <properties>
'     <add name="ZipCode" 
'       allowAnonymous="true" />
'     <add name="CityAndState" 
'       provider="AspNetSqlProfileProvider" 
'       allowAnonymous="true" />
'     <add name="StockSymbols" 
'       type="System.Collections.ArrayList" 
'       allowAnonymous="true" />
'   </properties>
' </profile>
#End Region

Public NotInheritable Class WdkProfileProvider
    Inherits ProfileProvider

#Region " Properties "
    '
    ' Global connection string, generic exception message, event log info.
    '
    Private eventSource As String = "WdkProfileProvider"
    Private eventLog As String = "Application"
    Private exceptionMessage As String = "An exception occurred. Please check the event log."

    '
    ' If false, exceptions are thrown to the caller. If true,
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
    ' System.Configuration.SettingsProvider.ApplicationName
    '
    Private pApplicationName As String = GetApplicationName()

    Public Overrides Property ApplicationName() As String
        Get
            Return pApplicationName
        End Get
        Set(ByVal value As String)
            pApplicationName = value
        End Set
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

        If config Is Nothing Then _
          Throw New ArgumentNullException("config")

        If name Is Nothing OrElse name.Length = 0 Then name = "WdkProfileProvider"

        If String.IsNullOrEmpty(config("description")) Then
            config.Remove("description")
            config.Add("description", "WDK Profile provider")
        End If

        ' Initialize the abstract base class.
        MyBase.Initialize(name, config)


        If config("applicationName") Is Nothing OrElse config("applicationName").Trim() = "" Then
            pApplicationName = GetApplicationName()
        Else
            pApplicationName = config("applicationName")
        End If
    End Sub
#End Region

#Region " GetPropertyValues "
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="ppc"></param>
    ''' <returns></returns>
    ''' <remarks>The serializeAs attribute is ignored in this provider implementation.</remarks>
    Public Overrides Function GetPropertyValues(ByVal context As SettingsContext, ByVal ppc As SettingsPropertyCollection) As SettingsPropertyValueCollection
        Dim username As String = CType(context("UserName"), String)
        Dim isAuthenticated As Boolean = CType(context("IsAuthenticated"), Boolean)

        Dim svc As SettingsPropertyValueCollection = New SettingsPropertyValueCollection()

        For Each prop As SettingsProperty In ppc
            Dim pv As SettingsPropertyValue = New SettingsPropertyValue(prop)

            pv.PropertyValue = GetProfileData(prop.Name, username, isAuthenticated)

            svc.Add(pv)
        Next

        UpdateActivityDates(username, isAuthenticated, True)

        Return svc

    End Function
#End Region

#Region " SetPropertyValues "
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="context"></param>
    ''' <param name="ppvc"></param>
    ''' <remarks>The serializeAs attribute is ignored in this provider implementation.</remarks>
    Public Overrides Sub SetPropertyValues(ByVal context As SettingsContext, ByVal ppvc As SettingsPropertyValueCollection)
        Dim Username As String = CType(context("UserName"), String)
        Dim isAuthenticated As Boolean = CType(context("IsAuthenticated"), Boolean)

        Try
            Dim ProfileId As Integer = GetProfileId(Username, isAuthenticated, False)
            If ProfileId = 0 Then ProfileId = CreateProfileForUser(Username, isAuthenticated)

            For Each pv As SettingsPropertyValue In ppvc
                If pv.PropertyValue IsNot Nothing Then SetProfileData(ProfileId, pv.Property.Name, pv.PropertyValue)
            Next

            UpdateActivityDates(Username, isAuthenticated, False)

        Catch ex As Exception
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New Exception(exceptionMessage)
            Else
                Throw New Exception(ex.ToString)
            End If
        End Try
    End Sub
#End Region

#Region " IsPropertyExists "
    Private Function IsPropertyExists(ByVal ProfileId As Integer, ByVal PropertyName As String) As Integer
        Dim conn As SqlConnection = Nothing
        Dim reader As SqlDataReader = Nothing

        Dim res As Integer = 0

        Try
            conn = New SqlConnection(connectionString)
            conn.Open()

            Dim strSql As String = "SELECT id FROM ProfileData WHERE PropertyName=@PropertyName AND ProfileId=@ProfileId "

            Dim cmd As SqlCommand = New SqlCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@PropertyName", PropertyName)
            cmd.Parameters.AddWithValue("@ProfileId", ProfileId)

            reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows Then res = reader.GetInt32(0)

        Catch ex As Exception
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            End If
        Finally
            If reader IsNot Nothing Then reader.Close()
            If conn IsNot Nothing Then conn.Close()
        End Try

        Return res
    End Function
#End Region

#Region " SetProfileData "
    Private Sub SetProfileData(ByVal ProfileId As Integer, ByVal PropertyName As String, ByVal Value As Object)
        Dim conn As SqlConnection = Nothing

        Try
            conn = New SqlConnection(ConnectionString)
            conn.Open()

            Dim cmd As New SqlCommand
            cmd.Connection = conn

            Dim strSql As String = ""

            Dim resultOfIsPropertyExists As Integer = IsPropertyExists(ProfileId, PropertyName)

            If resultOfIsPropertyExists = 0 Then
                strSql = "INSERT INTO ProfileData (ProfileId, PropertyName, PropertyValue) VALUES (@ProfileId, @PropertyName, @PropertyValue)"
            Else
                strSql = "UPDATE ProfileData SET ProfileId=@ProfileId, PropertyName=@PropertyName, PropertyValue=@PropertyValue WHERE id=" + resultOfIsPropertyExists
            End If

            cmd.CommandText = strSql
            cmd.Parameters.AddWithValue("@ProfileId", ProfileId)
            cmd.Parameters.AddWithValue("@PropertyName", PropertyName)
            cmd.Parameters.AddWithValue("@PropertyValue", Value)

            cmd.ExecuteNonQuery()

        Catch ex As Exception
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            End If
        Finally
            If conn IsNot Nothing Then
                conn.Close()
                conn.Dispose()
            End If
        End Try
    End Sub
#End Region

#Region " GetProfileData "
    Public Function GetProfileData(ByVal PropertyName As String, ByVal Username As String, ByVal IsAuthenticated As Boolean) As Object
        Dim conn As New SqlConnection(connectionString)

        Dim cmd As New SqlCommand("SELECT PropertyValue FROM ProfileData WHERE PropertyName=@PropertyName AND ProfileId=@ProfileId", conn)
        cmd.Parameters.AddWithValue("@PropertyName", PropertyName)
        cmd.Parameters.AddWithValue("@ProfileId", GetProfileId(Username, IsAuthenticated, True))

        Dim reader As SqlDataReader = Nothing
        Dim ret As Object = Nothing
        Try
            conn.Open()

            reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader.HasRows Then ret = reader.GetValue(0)

        Catch ex As SqlException
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw ex
            End If
        Finally
            If reader IsNot Nothing Then reader.Close()

            conn.Close()
        End Try

        Return ret
    End Function
#End Region

#Region " UpdateActivityDates "
    '
    ' UpdateActivityDates
    ' Updates LastActivityDate and LastUpdatedDate when profile properties are accessed
    ' by GetPropertyValues and SetPropertyValues. Specifying activityOnly as true will update
    ' only the LastActivityDate.
    '
    Private Sub UpdateActivityDates(ByVal username As String, ByVal isAuthenticated As Boolean, ByVal activityOnly As Boolean)
        Dim activityDate As DateTime = DateTime.Now

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand()
        cmd.Connection = conn

        If activityOnly Then
            cmd.CommandText = "UPDATE Profiles Set LastActivityDate = @LastActivityDate " & _
                  "WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName AND IsAnonymous = @IsAnonymous"
            cmd.Parameters.AddWithValue("@LastActivityDate", activityDate)
            cmd.Parameters.AddWithValue("@AccountUsername", username)
            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)
        Else
            cmd.CommandText = "UPDATE Profiles Set LastActivityDate = @LastActivityDate, LastUpdatedDate = @LastUpdatedDate " & _
                  "WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName AND IsAnonymous = @IsAnonymous"
            cmd.Parameters.AddWithValue("@LastActivityDate", activityDate)
            cmd.Parameters.AddWithValue("@LastUpdatedDate", activityDate)
            cmd.Parameters.AddWithValue("@AccountUsername", username)
            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)
        End If

        Try
            conn.Open()

            cmd.ExecuteNonQuery()
        Catch ex As SqlException
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw ex
            End If
        Finally
            conn.Close()
        End Try
    End Sub
#End Region

#Region " GetProfileId "
    ''' <summary>
    ''' GetProfileId
    ''' Retrieves the ProfileId from the database for 
    ''' the current user and application.
    ''' </summary>
    ''' <param name="username"></param>
    ''' <param name="isAuthenticated"></param>
    ''' <param name="ignoreAuthenticationType"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetProfileId(ByVal username As String, ByVal isAuthenticated As Boolean, ByVal ignoreAuthenticationType As Boolean) As Integer
        Dim conn As SqlConnection = Nothing
        Dim reader As SqlDataReader = Nothing

        Dim ProfileId As Integer = 0

        Try
            conn = New SqlConnection(connectionString)
            conn.Open()

            Dim cmd As New SqlCommand("SELECT id FROM Profiles WHERE ApplicationName=@ApplicationName AND AccountUsername=@AccountUsername", conn)
            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            cmd.Parameters.AddWithValue("@AccountUsername", username)

            If Not ignoreAuthenticationType Then
                cmd.CommandText += " AND IsAnonymous=@IsAnonymous"
                cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)
            End If

            reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
            reader.Read()

            If reader IsNot Nothing AndAlso reader.HasRows Then ProfileId = reader("id")

            cmd.Dispose()

        Catch ex As SqlException
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw ex
            End If
        Finally
            If reader IsNot Nothing Then reader.Close()
            If conn IsNot Nothing Then
                conn.Close()
                conn.Dispose()
            End If
        End Try

        Return ProfileId
    End Function
#End Region

#Region " CreateProfileForUser "
    '
    ' CreateProfileForUser
    ' If no user currently exists in the database, 
    ' a user record is created during the call to the 
    ' GetProfileId Private method.
    '
    Private Function CreateProfileForUser(ByVal username As String, ByVal isAuthenticated As Boolean) As Integer
        ' Check for valid user name.

        If username Is Nothing Then Throw New ArgumentNullException("username")
        If username.Length > 255 Then Throw New ArgumentException("User name exceeds 255 characters.")
        If username.IndexOf(",") > 0 Then Throw New ArgumentException("User name cannot contain a comma (,).")

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("INSERT INTO Profiles (AccountUsername, ApplicationName, LastActivityDate, LastUpdatedDate, IsAnonymous) Values(@AccountUsername, @ApplicationName, @LastActivityDate, @LastUpdatedDate, @IsAnonymous)", conn)
        cmd.Parameters.AddWithValue("@AccountUsername", username)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
        cmd.Parameters.AddWithValue("@LastActivityDate", DateTime.Now)
        cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now)
        cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)

        Dim cmd2 As SqlCommand = New SqlCommand("SELECT @@IDENTITY", conn)

        Dim ProfileId As Integer = 0

        Try
            conn.Open()

            cmd.ExecuteNonQuery()

            ProfileId = CType(cmd2.ExecuteScalar(), Integer)

        Catch ex As SqlException
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw ex
            Else
                Throw ex
            End If
        Finally
            conn.Close()
        End Try

        Return ProfileId
    End Function

#End Region

#Region " DeleteProfiles "
    '
    ' ProfileProvider.DeleteProfiles(ProfileInfoCollection)
    '
    Public Overrides Function DeleteProfiles(ByVal profiles As ProfileInfoCollection) As Integer
        Dim deleteCount As Integer = 0

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim tran As SqlTransaction = Nothing

        Try
            conn.Open()
            tran = conn.BeginTransaction()

            For Each p As ProfileInfo In profiles
                If DeleteProfile(p.UserName, conn, tran) Then deleteCount += 1
            Next

            tran.Commit()
        Catch ex As Exception
            Try
                tran.Rollback()
            Catch
            End Try

            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw ex
            End If
        Finally
            conn.Close()
        End Try

        Return deleteCount
    End Function
#End Region

#Region " DeleteProfiles ({}) "
    '
    ' ProfileProvider.DeleteProfiles(string())
    '
    Public Overrides Function DeleteProfiles(ByVal usernames As String()) As Integer
        Dim deleteCount As Integer = 0

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim tran As SqlTransaction = Nothing

        Try
            conn.Open()
            tran = conn.BeginTransaction()

            For Each user As String In usernames
                If (DeleteProfile(user, conn, tran)) Then deleteCount += 1
            Next

            tran.Commit()
        Catch ex As Exception
            Try
                tran.Rollback()
            Catch
            End Try

            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw ex
            End If
        Finally
            conn.Close()
        End Try

        Return deleteCount
    End Function
#End Region

#Region " DeleteInactiveProfiles "
    '
    ' ProfileProvider.DeleteInactiveProfiles
    '
    Public Overrides Function DeleteInactiveProfiles( _
    ByVal authenticationOption As ProfileAuthenticationOption, _
    ByVal userInactiveSinceDate As DateTime) As Integer

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT AccountUsername FROM Profiles WHERE ApplicationName = @ApplicationName AND LastActivityDate <= @LastActivityDate", conn)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
        cmd.Parameters.AddWithValue("@LastActivityDate", userInactiveSinceDate)

        Select Case authenticationOption
            Case ProfileAuthenticationOption.Anonymous
                cmd.CommandText &= " AND IsAnonymous = @IsAnonymous"
                cmd.Parameters.AddWithValue("@IsAnonymous", True)
            Case ProfileAuthenticationOption.Authenticated
                cmd.CommandText &= " AND IsAnonymous = @IsAnonymous"
                cmd.Parameters.AddWithValue("@IsAnonymous", False)
        End Select

        Dim reader As SqlDataReader = Nothing
        Dim usernames As String = ""

        Try
            conn.Open()

            reader = cmd.ExecuteReader()

            Do While reader.Read()
                usernames &= reader.GetString(0) + ","
            Loop
        Catch ex As SqlException
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw ex
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()

            conn.Close()
        End Try

        If usernames.Length > 0 Then
            ' Remove trailing comma.
            usernames = usernames.Substring(0, usernames.Length - 1)
        End If


        ' Delete profiles.
        Return DeleteProfiles(usernames.Split(CChar(",")))
    End Function
#End Region

#Region " DeleteProfile "
    '
    '
    ' DeleteProfile
    ' Deletes profile data from the database for the specified user name. Expects an SqlConnection and 
    ' an SqlTransaction as it supports deleting multiple profiles in a transaction.
    '
    Private Function DeleteProfile(ByVal username As String, ByVal conn As SqlConnection, ByVal tran As SqlTransaction) As Boolean
        ' Check for valid user name.
        If username Is Nothing Then Throw New ArgumentNullException("username")
        If username.Length > 255 Then Throw New ArgumentException("User name exceeds 255 characters.")
        If username.IndexOf(",") > 0 Then Throw New ArgumentException("User name cannot contain a comma (,).")

        Dim ProfileId As Integer = GetProfileId(username, False, True)

        '-delete profile data
        Dim cmd1 As SqlCommand = New SqlCommand("DELETE * FROM ProfileData WHERE ProfileId = @ProfileId", conn)
        cmd1.Parameters.AddWithValue("@ProfileId", ProfileId)

        '-delete pofiel itself
        Dim cmd3 As SqlCommand = New SqlCommand("DELETE * FROM Profiles WHERE id = @id", conn)
        cmd3.Parameters.AddWithValue("@id", ProfileId)

        cmd1.Transaction = tran
        cmd3.Transaction = tran

        Dim numDeleted As Integer = 0

        ' Exceptions will be caught by the calling method.
        numDeleted += cmd1.ExecuteNonQuery()
        numDeleted += cmd3.ExecuteNonQuery()

        If numDeleted = 0 Then
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region " FindProfilesByUserName "
    '
    ' ProfileProvider.FindProfilesByUserName
    '

    Public Overrides Function FindProfilesByUserName(ByVal authenticationOption As ProfileAuthenticationOption, ByVal usernameToMatch As String, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection

        CheckParameters(pageIndex, pageSize)

        Return GetProfileInfo(authenticationOption, usernameToMatch, Nothing, pageIndex, pageSize, totalRecords)
    End Function
#End Region

#Region " FindInactiveProfilesByUserName "
    '
    ' ProfileProvider.FindInactiveProfilesByUserName
    '
    Public Overrides Function FindInactiveProfilesByUserName( _
    ByVal authenticationOption As ProfileAuthenticationOption, _
    ByVal usernameToMatch As String, _
    ByVal userInactiveSinceDate As DateTime, _
    ByVal pageIndex As Integer, _
    ByVal pageSize As Integer, _
      ByRef totalRecords As Integer) As ProfileInfoCollection

        CheckParameters(pageIndex, pageSize)

        Return GetProfileInfo(authenticationOption, usernameToMatch, userInactiveSinceDate, pageIndex, pageSize, totalRecords)
    End Function
#End Region

#Region " GetAllProfiles "
    '
    ' ProfileProvider.GetAllProfiles
    '
    Public Overrides Function GetAllProfiles(ByVal authenticationOption As ProfileAuthenticationOption, ByVal pageIndex As Integer, ByVal pageSize As Integer, ByRef totalRecords As Integer) As ProfileInfoCollection

        CheckParameters(pageIndex, pageSize)

        Return GetProfileInfo(authenticationOption, Nothing, Nothing, _
              pageIndex, pageSize, totalRecords)
    End Function
#End Region

#Region " GetAllInactiveProfiles "
    '
    ' ProfileProvider.GetAllInactiveProfiles
    '
    Public Overrides Function GetAllInactiveProfiles( _
    ByVal authenticationOption As ProfileAuthenticationOption, _
    ByVal userInactiveSinceDate As DateTime, _
    ByVal pageIndex As Integer, _
    ByVal pageSize As Integer, _
      ByRef totalRecords As Integer) As ProfileInfoCollection

        CheckParameters(pageIndex, pageSize)

        Return GetProfileInfo(authenticationOption, Nothing, userInactiveSinceDate, pageIndex, pageSize, totalRecords)
    End Function
#End Region

#Region " GetNumberOfInactiveProfiles "
    '
    ' ProfileProvider.GetNumberOfInactiveProfiles
    '
    Public Overrides Function GetNumberOfInactiveProfiles( _
    ByVal authenticationOption As ProfileAuthenticationOption, _
    ByVal userInactiveSinceDate As DateTime) As Integer

        Dim inactiveProfiles As Integer = 0

        Dim profiles As ProfileInfoCollection = GetProfileInfo(authenticationOption, Nothing, userInactiveSinceDate, 0, 0, inactiveProfiles)

        Return inactiveProfiles
    End Function
#End Region

#Region " CheckParameters "
    '
    ' CheckParameters
    ' Verifies input parameters for page size and page index. 
    ' Called by GetAllProfiles, GetAllInactiveProfiles, 
    ' FindProfilesByUserName, and FindInactiveProfilesByUserName.
    '

    Private Sub CheckParameters(ByVal pageIndex As Integer, ByVal pageSize As Integer)
        If pageIndex < 0 Then _
          Throw New ArgumentException("Page index must 0 or greater.")
        If pageSize < 1 Then _
          Throw New ArgumentException("Page size must be greater than 0.")
    End Sub
#End Region

#Region " GetProfileInfo "
    '
    ' GetProfileInfo
    ' Retrieves a count of profiles and creates a 
    ' ProfileInfoCollection from the profile data in the database. 
    ' Called by GetAllProfiles, GetAllInactiveProfiles,
    ' FindProfilesByUserName, FindInactiveProfilesByUserName, 
    ' and GetNumberOfInactiveProfiles.
    ' Specifying a pageIndex of 0 retrieves a count of the results only.
    '
    Private Function GetProfileInfo( _
    ByVal authenticationOption As ProfileAuthenticationOption, _
    ByVal usernameToMatch As String, _
    ByVal userInactiveSinceDate As Object, _
    ByVal pageIndex As Integer, _
    ByVal pageSize As Integer, _
      ByRef totalRecords As Integer) As ProfileInfoCollection

        Dim conn As SqlConnection = New SqlConnection(connectionString)

        ' Command to retrieve the total count.

        Dim cmd As SqlCommand = New SqlCommand("SELECT COUNT(id) FROM Profiles WHERE ApplicationName = @ApplicationName ", conn)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)


        ' Command to retrieve the profile data.

        Dim cmd2 As SqlCommand = New SqlCommand("SELECT AccountUsername, LastActivityDate, LastUpdatedDate, " & _
                "IsAnonymous FROM Profiles WHERE ApplicationName = @ApplicationName ", conn)
        cmd2.Parameters.AddWithValue("@ApplicationName", ApplicationName)


        ' If searching for a user name to match, 
        ' add the command text and parameters.

        If Not usernameToMatch Is Nothing Then
            cmd.CommandText &= " AND AccountUsername LIKE @Username "
            cmd.Parameters.AddWithValue("@Username", usernameToMatch)

            cmd2.CommandText &= " AND AccountUsername LIKE @AccountUsername "
            cmd2.Parameters.AddWithValue("@AccountUsername", usernameToMatch)
        End If


        ' If searching for inactive profiles, 
        ' add the command text and parameters.

        If Not userInactiveSinceDate Is Nothing Then
            cmd.CommandText &= " AND LastActivityDate <= @LastActivityDate "
            cmd.Parameters.AddWithValue("@LastActivityDate", CType(userInactiveSinceDate, DateTime))

            cmd2.CommandText &= " AND LastActivityDate <= @LastActivityDate "
            cmd2.Parameters.AddWithValue("@LastActivityDate", CType(userInactiveSinceDate, DateTime))
        End If

        ' If searching for a anonymous or authenticated profiles, add the command text 
        ' and parameters.

        Select Case authenticationOption
            Case ProfileAuthenticationOption.Anonymous
                cmd.CommandText &= " AND IsAnonymous = @IsAnonymous"
                cmd.Parameters.AddWithValue("@IsAnonymous", True)
                cmd2.CommandText &= " AND IsAnonymous = @IsAnonymous"
                cmd2.Parameters.AddWithValue("@IsAnonymous", True)
            Case ProfileAuthenticationOption.Authenticated
                cmd.CommandText &= " AND IsAnonymous = @IsAnonymous"
                cmd.Parameters.AddWithValue("@IsAnonymous", False)
                cmd2.CommandText &= " AND IsAnonymous = @IsAnonymous"
                cmd2.Parameters.AddWithValue("@IsAnonymous", False)
        End Select

        ' Get the data.

        Dim reader As SqlDataReader = Nothing
        Dim profiles As ProfileInfoCollection = New ProfileInfoCollection()

        Try
            conn.Open()
            ' Get the profile count.
            totalRecords = CType(cmd.ExecuteScalar(), Integer)
            ' No profiles found.
            If totalRecords <= 0 Then Return profiles
            ' Count profiles only.
            If pageSize = 0 Then Return profiles


            reader = cmd2.ExecuteReader()

            Dim counter As Integer = 0
            Dim startIndex As Integer = pageSize * (pageIndex - 1)
            Dim endIndex As Integer = startIndex + pageSize - 1

            Do While reader.Read()
                If counter >= startIndex Then
                    Dim p As ProfileInfo = GetProfileInfoFromReader(reader)
                    profiles.Add(p)
                End If

                If counter >= endIndex Then cmd.Cancel()

                counter += 1
            Loop
        Catch ex As SqlException
            If WriteExceptionsToEventLog Then
                Log(ex.ToString, True)
                Throw New ProviderException(exceptionMessage)
            Else
                Throw ex
            End If
        Finally
            If Not reader Is Nothing Then reader.Close()

            conn.Close()
        End Try

        Return profiles
    End Function
#End Region

#Region " GetProfileInfoFromReader "
    '
    ' GetProfileInfoFromReader
    ' Takes the current row from the SqlDataReader
    ' and populates a ProfileInfo object from the values. 
    '
    Private Function GetProfileInfoFromReader(ByVal reader As SqlDataReader) As ProfileInfo

        Dim username As String = reader.GetString(0)

        Dim lastActivityDate As DateTime = New DateTime()
        If Not reader.GetValue(1) Is DBNull.Value Then _
          lastActivityDate = reader.GetDateTime(1)

        Dim lastUpdatedDate As DateTime = New DateTime()
        If Not reader.GetValue(2) Is DBNull.Value Then lastUpdatedDate = reader.GetDateTime(2)

        ' ProfileInfo.Size not currently implemented.
        Return New ProfileInfo(username, reader.GetBoolean(3), lastActivityDate, lastUpdatedDate, 0)
    End Function
#End Region

End Class