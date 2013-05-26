Imports System.Web.Profile
Imports System.Configuration.Provider
Imports System.Collections.Specialized
Imports System
Imports System.Data
Imports System.Data.Odbc
Imports System.Configuration
Imports System.Diagnostics
Imports System.Web
Imports System.Collections
Imports Microsoft.VisualBasic

#Region " SQL Syntax "
'
'
' This provider works with the following schema for the table of user data.
' CREATE TABLE Profiles
' (
'   [id] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
'   Username varchar (255) NOT NULL,
'   ApplicationName varchar (255) NOT NULL,
'   IsAnonymous int, 
'   LastActivityDate DateTime,
'   LastUpdatedDate DateTime,
'   XmlProfile xml,
'     CONSTRAINT PKProfiles UNIQUE (Username, ApplicationName)
') 
#End Region

#Region " web.config behavior "
'<anonymousIdentification enabled="true" />
'
'<profile defaultProvider="OdbcProvider">
'   <providers>
'     <add
'       name="OdbcProvider"
'       type="Samples.AspNet.Profile.OdbcProfileProvider" 
'       connectionStringName="OdbcProfile" /> 
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

Namespace Providers
    Public NotInheritable Class WDKProfileProvider
        Inherits ProfileProvider

#Region " Properties "
        '
        ' Global connection string, generic exception message, event log info.
        '
        Private eventSource As String = "WdkProfileProvider"
        Private eventLog As String = "Application"
        Private exceptionMessage As String = "An exception occurred. Please check the event log."
        Private connectionString As String

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
        Private pApplicationName As String

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

            If name Is Nothing OrElse name.Length = 0 Then _
             name = "OdbcProfileProvider"

            If String.IsNullOrEmpty(config("description")) Then
                config.Remove("description")
                config.Add("description", "Sample ODBC Profile provider")
            End If

            ' Initialize the abstract base class.
            MyBase.Initialize(name, config)


            If config("applicationName") Is Nothing OrElse config("applicationName").Trim() = "" Then
                pApplicationName = System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
            Else
                pApplicationName = config("applicationName")
            End If


            '
            ' Initialize connection string.
            '

            Dim pConnectionStringSettings As ConnectionStringSettings = _
              ConfigurationManager.ConnectionStrings(config("connectionStringName"))

            If pConnectionStringSettings Is Nothing OrElse _
                pConnectionStringSettings.ConnectionString.Trim() = "" _
            Then
                Throw New ProviderException("Connection String cannot be blank.")
            End If

            connectionString = pConnectionStringSettings.ConnectionString
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
                    WriteToEventLog(ex, "SetPropertyValues")
                    Throw New Exception(exceptionMessage)
                Else
                    Throw New Exception(ex.ToString)
                End If
            End Try
        End Sub
#End Region

#Region " IsPropertyExists "
        Private Function IsPropertyExists(ByVal ProfileId As Integer, ByVal PropertyName As String) As Integer
            Dim conn As OdbcConnection = Nothing
            Dim reader As OdbcDataReader = Nothing

            Dim res As Integer = 0

            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()

                Dim strSql As String = "SELECT id FROM ProfileData WHERE PropertyName=? AND ProfileId=? "

                Dim cmd As OdbcCommand = New OdbcCommand(strSql, conn)
                cmd.Parameters.AddWithValue("@PropertyName", PropertyName)
                cmd.Parameters.AddWithValue("@ProfileId", ProfileId)

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                If reader.HasRows Then res = reader.GetInt32(0)

            Catch ex As Exception
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(ex, "IsPropertyExists")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw ex
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
            Dim conn As OdbcConnection = Nothing

            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()

                Dim strSql As String = ""
                Dim cmd As New OdbcCommand
                cmd.Connection = conn

                Dim id As Integer = IsPropertyExists(ProfileId, PropertyName)

                If id = 0 Then
                    strSql = "INSERT INTO ProfileData (ProfileId, PropertyName, PropertyValue) VALUES (?,?,?)"
                Else
                    strSql = "UPDATE ProfileData SET ProfileId=?, PropertyName=?, PropertyValue=? WHERE id=" & id
                End If

                cmd.CommandText = strSql
                cmd.Parameters.AddWithValue("@ProfileId", ProfileId)
                cmd.Parameters.AddWithValue("@PropertyName", PropertyName)
                cmd.Parameters.AddWithValue("@PropertyValue", Value)

                cmd.ExecuteNonQuery()

            Catch ex As Exception
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(ex, "SetProfileData")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw ex
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
        Private Function GetProfileData(ByVal PropertyName As String, ByVal Username As String, ByVal IsAuthenticated As Boolean) As Object
            Dim conn As OdbcConnection = New OdbcConnection(connectionString)
            Dim strSql As String = "SELECT PropertyValue FROM ProfileData WHERE PropertyName=? AND ProfileId=?"

            Dim cmd As OdbcCommand = New OdbcCommand(strSql, conn)
            cmd.Parameters.AddWithValue("@PropertyName", PropertyName)
            cmd.Parameters.AddWithValue("@ProfileId", GetProfileId(Username, IsAuthenticated, True))

            Dim reader As OdbcDataReader = Nothing
            Dim ret As Object = Nothing
            Try
                conn.Open()

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                If reader.HasRows Then ret = reader.GetValue(0)

            Catch e As OdbcException
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "GetProfileData")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw e
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

            Dim conn As OdbcConnection = New OdbcConnection(connectionString)
            Dim cmd As OdbcCommand = New OdbcCommand()
            cmd.Connection = conn

            If activityOnly Then
                cmd.CommandText = "UPDATE Profiles Set LastActivityDate = ? " & _
                      "WHERE AccountUsername = ? AND ApplicationName = ? AND IsAnonymous = ?"
                cmd.Parameters.AddWithValue("@LastActivityDate", activityDate)
                cmd.Parameters.AddWithValue("@AccountUsername", username)
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
                cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)
            Else
                cmd.CommandText = "UPDATE Profiles Set LastActivityDate = ?, LastUpdatedDate = ? " & _
                      "WHERE AccountUsername = ? AND ApplicationName = ? AND IsAnonymous = ?"
                cmd.Parameters.AddWithValue("@LastActivityDate", activityDate)
                cmd.Parameters.AddWithValue("@LastUpdatedDate", activityDate)
                cmd.Parameters.AddWithValue("@AccountUsername", username)
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
                cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)
            End If

            Try
                conn.Open()

                cmd.ExecuteNonQuery()
            Catch e As OdbcException
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "UpdateActivityDates")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw e
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
            Dim conn As OdbcConnection = Nothing
            Dim reader As OdbcDataReader = Nothing

            Dim ProfileId As Integer = 0

            Try
                conn = New OdbcConnection(connectionString)
                conn.Open()

                Dim cmd As New OdbcCommand("SELECT id FROM Profiles WHERE AccountUsername = ? AND ApplicationName = ?", conn)
                cmd.Parameters.AddWithValue("@AccountUsername", username)
                cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

                If Not ignoreAuthenticationType Then
                    cmd.CommandText &= " AND IsAnonymous = ?"
                    cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)
                End If

                'Dim dbs As New DataSet
                'dbs.Clear()

                'Dim dba As New OdbcDataAdapter(cmd)
                'dba.Fill(dbs)

                'If dbs.Tables(0).Rows.Count > 0 Then ProfileId = dbs.Tables(0).Rows(0).Item(0)

                reader = cmd.ExecuteReader(CommandBehavior.SingleRow)
                If reader.HasRows Then ProfileId = reader.GetInt32(0)

                'dba.Dispose()
                'dbs.Dispose()
                cmd.Dispose()

            Catch e As OdbcException
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "GetProfileId")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw e
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

            Dim conn As OdbcConnection = New OdbcConnection(connectionString)
            Dim cmd As OdbcCommand = New OdbcCommand("INSERT INTO Profiles (AccountUsername, ApplicationName, LastActivityDate, LastUpdatedDate, IsAnonymous) Values(?, ?, ?, ?, ?)", conn)
            cmd.Parameters.AddWithValue("@AccountUsername", username)
            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            cmd.Parameters.AddWithValue("@LastActivityDate", DateTime.Now)
            cmd.Parameters.AddWithValue("@LastUpdatedDate", DateTime.Now)
            cmd.Parameters.AddWithValue("@IsAnonymous", Not isAuthenticated)

            Dim cmd2 As OdbcCommand = New OdbcCommand("SELECT @@IDENTITY", conn)

            Dim ProfileId As Integer = 0

            Try
                conn.Open()

                cmd.ExecuteNonQuery()

                ProfileId = CType(cmd2.ExecuteScalar(), Integer)

            Catch e As OdbcException
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "CreateProfileForUser")
                    Throw New HttpException(exceptionMessage)
                Else
                    Throw e
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

            Dim conn As OdbcConnection = New OdbcConnection(connectionString)
            Dim tran As OdbcTransaction = Nothing

            Try
                conn.Open()
                tran = conn.BeginTransaction()

                For Each p As ProfileInfo In profiles
                    If DeleteProfile(p.UserName, conn, tran) Then deleteCount += 1
                Next

                tran.Commit()
            Catch e As Exception
                Try
                    tran.Rollback()
                Catch
                End Try

                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "DeleteProfiles(String())")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw e
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

            Dim conn As OdbcConnection = New OdbcConnection(connectionString)
            Dim tran As OdbcTransaction = Nothing

            Try
                conn.Open()
                tran = conn.BeginTransaction()

                For Each user As String In usernames
                    If (DeleteProfile(user, conn, tran)) Then deleteCount += 1
                Next

                tran.Commit()
            Catch e As Exception
                Try
                    tran.Rollback()
                Catch
                End Try

                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "DeleteProfiles(String())")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw e
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

            Dim conn As OdbcConnection = New OdbcConnection(connectionString)
            Dim cmd As OdbcCommand = New OdbcCommand("SELECT AccountUsername FROM Profiles WHERE ApplicationName = ? AND LastActivityDate <= ?", conn)
            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)
            cmd.Parameters.AddWithValue("@LastActivityDate", userInactiveSinceDate)

            Select Case authenticationOption
                Case ProfileAuthenticationOption.Anonymous
                    cmd.CommandText &= " AND IsAnonymous = ?"
                    cmd.Parameters.AddWithValue("@IsAnonymous", True)
                Case ProfileAuthenticationOption.Authenticated
                    cmd.CommandText &= " AND IsAnonymous = ?"
                    cmd.Parameters.AddWithValue("@IsAnonymous", False)
            End Select

            Dim reader As OdbcDataReader = Nothing
            Dim usernames As String = ""

            Try
                conn.Open()

                reader = cmd.ExecuteReader()

                Do While reader.Read()
                    usernames &= reader.GetString(0) + ","
                Loop
            Catch e As OdbcException
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "DeleteInactiveProfiles")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw e
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
        ' Deletes profile data from the database for the specified user name. Expects an OdbcConnection and 
        ' an OdbcTransaction as it supports deleting multiple profiles in a transaction.
        '
        Private Function DeleteProfile(ByVal username As String, ByVal conn As OdbcConnection, ByVal tran As OdbcTransaction) As Boolean
            ' Check for valid user name.
            If username Is Nothing Then Throw New ArgumentNullException("username")
            If username.Length > 255 Then Throw New ArgumentException("User name exceeds 255 characters.")
            If username.IndexOf(",") > 0 Then Throw New ArgumentException("User name cannot contain a comma (,).")

            Dim ProfileId As Integer = GetProfileId(username, False, True)

            '-delete profile data
            Dim cmd1 As OdbcCommand = New OdbcCommand("DELETE * FROM ProfileData WHERE ProfileId = ?", conn)
            cmd1.Parameters.AddWithValue("@ProfileId", ProfileId)

            '-delete pofiel itself
            Dim cmd3 As OdbcCommand = New OdbcCommand("DELETE * FROM Profiles WHERE id = ?", conn)
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

        Public Overrides Function FindProfilesByUserName( _
        ByVal authenticationOption As ProfileAuthenticationOption, _
        ByVal usernameToMatch As String, _
        ByVal pageIndex As Integer, _
        ByVal pageSize As Integer, _
          ByRef totalRecords As Integer) As ProfileInfoCollection

            CheckParameters(pageIndex, pageSize)

            Return GetProfileInfo(authenticationOption, usernameToMatch, Nothing, _
                  pageIndex, pageSize, totalRecords)
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

            Return GetProfileInfo(authenticationOption, usernameToMatch, userInactiveSinceDate, _
                  pageIndex, pageSize, totalRecords)
        End Function
#End Region

#Region " GetAllProfiles "
        '
        ' ProfileProvider.GetAllProfiles
        '
        Public Overrides Function GetAllProfiles( _
        ByVal authenticationOption As ProfileAuthenticationOption, _
        ByVal pageIndex As Integer, _
        ByVal pageSize As Integer, _
          ByRef totalRecords As Integer) As ProfileInfoCollection

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

            Dim conn As OdbcConnection = New OdbcConnection(connectionString)

            ' Command to retrieve the total count.

            Dim cmd As OdbcCommand = New OdbcCommand("SELECT COUNT(id) FROM Profiles WHERE ApplicationName = ? ", conn)
            cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)


            ' Command to retrieve the profile data.

            Dim cmd2 As OdbcCommand = New OdbcCommand("SELECT AccountUsername, LastActivityDate, LastUpdatedDate, " & _
                    "IsAnonymous FROM Profiles WHERE ApplicationName = ? ", conn)
            cmd2.Parameters.AddWithValue("@ApplicationName", ApplicationName)


            ' If searching for a user name to match, 
            ' add the command text and parameters.

            If Not usernameToMatch Is Nothing Then
                cmd.CommandText &= " AND AccountUsername LIKE ? "
                cmd.Parameters.AddWithValue("@Username", usernameToMatch)

                cmd2.CommandText &= " AND AccountUsername LIKE ? "
                cmd2.Parameters.AddWithValue("@AccountUsername", usernameToMatch)
            End If


            ' If searching for inactive profiles, 
            ' add the command text and parameters.

            If Not userInactiveSinceDate Is Nothing Then
                cmd.CommandText &= " AND LastActivityDate <= ? "
                cmd.Parameters.AddWithValue("@LastActivityDate", CType(userInactiveSinceDate, DateTime))

                cmd2.CommandText &= " AND LastActivityDate <= ? "
                cmd2.Parameters.AddWithValue("@LastActivityDate", CType(userInactiveSinceDate, DateTime))
            End If

            ' If searching for a anonymous or authenticated profiles, add the command text 
            ' and parameters.

            Select Case authenticationOption
                Case ProfileAuthenticationOption.Anonymous
                    cmd.CommandText &= " AND IsAnonymous = ?"
                    cmd.Parameters.AddWithValue("@IsAnonymous", True)
                    cmd2.CommandText &= " AND IsAnonymous = ?"
                    cmd2.Parameters.AddWithValue("@IsAnonymous", True)
                Case ProfileAuthenticationOption.Authenticated
                    cmd.CommandText &= " AND IsAnonymous = ?"
                    cmd.Parameters.AddWithValue("@IsAnonymous", False)
                    cmd2.CommandText &= " AND IsAnonymous = ?"
                    cmd2.Parameters.AddWithValue("@IsAnonymous", False)
            End Select

            ' Get the data.

            Dim reader As OdbcDataReader = Nothing
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
            Catch e As OdbcException
                If WriteExceptionsToEventLog Then
                    WriteToEventLog(e, "GetProfileInfo")
                    Throw New ProviderException(exceptionMessage)
                Else
                    Throw e
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
        ' Takes the current row from the OdbcDataReader
        ' and populates a ProfileInfo object from the values. 
        '
        Private Function GetProfileInfoFromReader(ByVal reader As OdbcDataReader) As ProfileInfo

            Dim username As String = reader.GetString(0)

            Dim lastActivityDate As DateTime = New DateTime()
            If Not reader.GetValue(1) Is DBNull.Value Then _
              lastActivityDate = reader.GetDateTime(1)

            Dim lastUpdatedDate As DateTime = New DateTime()
            If Not reader.GetValue(2) Is DBNull.Value Then _
              lastUpdatedDate = reader.GetDateTime(2)

            Dim isAnonymous As Boolean = reader.GetBoolean(3)

            ' ProfileInfo.Size not currently implemented.
            Dim p As ProfileInfo = New ProfileInfo(username, _
                isAnonymous, lastActivityDate, lastUpdatedDate, 0)

            Return p
        End Function
#End Region

    End Class
End Namespace