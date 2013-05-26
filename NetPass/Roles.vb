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
Imports Microsoft.VisualBasic

#Region " SQL Syntax "
'
'
' This provider works with the following schema for the tables of role data.
' 
' CREATE TABLE Roles
' (
'   Rolename varchar (255) NOT NULL,
'   ApplicationName varchar (255) NOT NULL,
'     CONSTRAINT PKRoles PRIMARY KEY (Rolename, ApplicationName)
' )
'
' CREATE TABLE UsersInRoles
' (
'   AccountUsername varchar (255) NOT NULL,
'   Rolename varchar (255) NOT NULL,
'   ApplicationName varchar (255) NOT NULL,
'     CONSTRAINT PKUsersInRoles PRIMARY KEY (AccountUsername, Rolename, ApplicationName)
' )
'
#End Region

Public NotInheritable Class NetPassRoleProvider
    Inherits RoleProvider

#Region " Properties "
    '
    ' Global SqlConnection, generated password length, generic exception message, event log info.
    '
    Private conn As SqlConnection
    Private rolesTable As String = "Roles"
    Private usersInRolesTable As String = "UsersInRoles"

    Private eventSource As String = "NetPassRoleProvider"
    Private eventLog As String = "Application"
    Private exceptionMessage As String = "An exception occurred. Please check the Event Log."

    Private pConnectionStringSettings As ConnectionStringSettings
    Private connectionString As String

    '
    ' If false, exceptions are Thrown to the caller. If true,
    ' exceptions are written to the event log.
    '
    Private pWriteExceptionsToEventLog As Boolean = False

    Public Property WriteExceptionsToEventLog() As Boolean
        Get
            Return pWriteExceptionsToEventLog
        End Get
        Set(ByVal value As Boolean)
            pWriteExceptionsToEventLog = value
        End Set
    End Property

    '
    ' System.Web.Security.RoleProvider properties.
    '    
    Private pApplicationName As String = "NetPass"

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

        If config Is Nothing Then Throw New ArgumentNullException("config")

        If name Is Nothing OrElse name.Length = 0 Then name = "NetPassRoleProvider"

        If String.IsNullOrEmpty(config("description")) Then
            config.Remove("description")
            config.Add("description", "NetPass Role provider")
        End If

        ' Initialize the abstract base class.
        MyBase.Initialize(name, config)


        If config("applicationName") Is Nothing OrElse config("applicationName").Trim() = "" Then
            pApplicationName = System.Web.HttpContext.Current.Request.Url.Host

            If System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath <> "/" Then _
            pApplicationName += System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath
        Else
            pApplicationName = config("applicationName")
        End If


        If Not config("writeExceptionsToEventLog") Is Nothing Then
            If config("writeExceptionsToEventLog").ToUpper() = "TRUE" Then
                pWriteExceptionsToEventLog = True
            End If
        End If


        '
        ' Initialize SqlConnection.
        '

        pConnectionStringSettings = _
          ConfigurationManager.ConnectionStrings(config("connectionStringName"))

        If pConnectionStringSettings Is Nothing OrElse pConnectionStringSettings.ConnectionString.Trim() = "" Then
            Throw New ProviderException("Connection string cannot be blank.")
        End If

        connectionString = pConnectionStringSettings.ConnectionString
    End Sub
#End Region

#Region " AddUsersToRoles "
    '
    ' System.Web.Security.RoleProvider methods.
    '

    '
    ' RoleProvider.AddUsersToRoles
    '

    Public Overrides Sub AddUsersToRoles(ByVal AccountUsernames As String(), ByVal rolenames As String())

        For Each rolename As String In rolenames
            If Not RoleExists(rolename) Then
                Throw New ProviderException("Role name not found.")
            End If
        Next

        For Each AccountUsername As String In AccountUsernames
            If AccountUsername.IndexOf(",") > 0 Then
                Throw New ArgumentException("User names cannot contain commas.")
            End If

            For Each rolename As String In rolenames
                If IsUserInRole(AccountUsername, rolename) Then
                    Throw New ProviderException("User is already in role.")
                End If
            Next
        Next


        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("INSERT INTO " & usersInRolesTable & " (AccountUsername, Rolename, ApplicationName) Values (@AccountUsername, @Rolename, @ApplicationName)", conn)

        Dim userParm As SqlParameter = cmd.Parameters.Add("@AccountUsername", SqlDbType.VarChar, 255)
        Dim roleParm As SqlParameter = cmd.Parameters.Add("@Rolename", SqlDbType.VarChar, 255)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim tran As SqlTransaction = Nothing

        Try
            conn.Open()
            tran = conn.BeginTransaction()
            cmd.Transaction = tran

            For Each AccountUsername As String In AccountUsernames
                For Each rolename As String In rolenames
                    userParm.Value = AccountUsername
                    roleParm.Value = rolename
                    cmd.ExecuteNonQuery()
                Next
            Next

            tran.Commit()
        Catch e As SqlException
            Try
                tran.Rollback()
            Catch
            End Try

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

#Region " CreateRole "
    '
    ' RoleProvider.CreateRole
    '
    Public Overrides Sub CreateRole(ByVal rolename As String)

        If rolename.IndexOf(",") > 0 Then
            Throw New ArgumentException("Role names cannot contain commas.")
        End If

        If RoleExists(rolename) Then
            Throw New ProviderException("Role name already exists.")
        End If

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("INSERT INTO " + rolesTable + " (Rolename, ApplicationName) Values (@Rolename, @ApplicationName)", conn)

        cmd.Parameters.AddWithValue("@Rolename", rolename)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

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

#Region " DeleteRole "
    '
    ' RoleProvider.DeleteRole
    '

    Public Overrides Function DeleteRole(ByVal rolename As String, ByVal throwOnPopulatedRole As Boolean) As Boolean

        If Not RoleExists(rolename) Then
            Throw New ProviderException("Role does not exist.")
        End If

        If throwOnPopulatedRole AndAlso GetUsersInRole(rolename).Length > 0 Then
            Throw New ProviderException("Cannot delete a populated role.")
        End If

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("DELETE FROM " + rolesTable + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@Rolename", rolename)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)


        Dim cmd2 As SqlCommand = New SqlCommand("DELETE FROM " + usersInRolesTable + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn)

        cmd2.Parameters.AddWithValue("@Rolename", rolename)
        cmd2.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim tran As SqlTransaction = Nothing

        Try
            conn.Open()
            tran = conn.BeginTransaction()
            cmd.Transaction = tran
            cmd2.Transaction = tran

            cmd2.ExecuteNonQuery()
            cmd.ExecuteNonQuery()

            tran.Commit()
        Catch e As SqlException
            Try
                tran.Rollback()
            Catch
            End Try

            If WriteExceptionsToEventLog Then
                Log(e.ToString, True)
                Throw New ProviderException(exceptionMessage)
            End If

            Return False
        Finally
            conn.Close()
        End Try

        Return True
    End Function
#End Region

#Region " GetAllRoles "
    '
    ' RoleProvider.GetAllRoles
    '
    Public Overrides Function GetAllRoles() As String()
        Dim tmpRoleNames As String = ""

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT Rolename FROM " + rolesTable + " WHERE ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader()

            Do While reader.Read()
                tmpRoleNames &= reader.GetString(0) & ","
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

        If tmpRoleNames.Length > 0 Then
            ' Remove trailing comma.
            tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1)
            Return tmpRoleNames.Split(CChar(","))
        End If

        Return New String() {}
    End Function
#End Region

#Region " GetRolesForUser "
    '
    ' RoleProvider.GetRolesForUser
    '
    Public Overrides Function GetRolesForUser(ByVal AccountUsername As String) As String()
        Dim tmpRoleNames As String = ""

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT Rolename FROM " & usersInRolesTable + " WHERE AccountUsername = @AccountUsername AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader()

            Do While reader.Read()
                tmpRoleNames &= reader.GetString(0) & ","
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

        If tmpRoleNames.Length > 0 Then
            ' Remove trailing comma.
            tmpRoleNames = tmpRoleNames.Substring(0, tmpRoleNames.Length - 1)
            Return tmpRoleNames.Split(CChar(","))
        End If

        Return New String() {}
    End Function
#End Region

#Region " GetUsersInRole "
    '
    ' RoleProvider.GetUsersInRole
    '
    Public Overrides Function GetUsersInRole(ByVal rolename As String) As String()
        Dim tmpAccountUsernames As String = ""

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT AccountUsername FROM " + usersInRolesTable + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@Rolename", rolename)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader()

            Do While reader.Read()
                tmpAccountUsernames &= reader.GetString(0) & ","
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

        If tmpAccountUsernames.Length > 0 Then
            ' Remove trailing comma.
            tmpAccountUsernames = tmpAccountUsernames.Substring(0, tmpAccountUsernames.Length - 1)
            Return tmpAccountUsernames.Split(CChar(","))
        End If

        Return New String() {}
    End Function
#End Region

#Region " IsUserInRole "
    '
    ' RoleProvider.IsUserInRole
    '

    Public Overrides Function IsUserInRole(ByVal AccountUsername As String, ByVal rolename As String) As Boolean

        Dim userIsInRole As Boolean = False

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT COUNT(AccountUsername) FROM " + usersInRolesTable + " WHERE AccountUsername = @AccountUsername AND Rolename = @Rolename AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@AccountUsername", AccountUsername)
        cmd.Parameters.AddWithValue("@Rolename", rolename)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Try
            conn.Open()

            Dim numRecs As Integer = CType(cmd.ExecuteScalar(), Integer)

            If numRecs > 0 Then
                userIsInRole = True
            End If
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

        Return userIsInRole
    End Function
#End Region

#Region " RemoveUsersFromRoles "
    '
    ' RoleProvider.RemoveUsersFromRoles
    '

    Public Overrides Sub RemoveUsersFromRoles(ByVal AccountUsernames As String(), ByVal rolenames As String())

        For Each rolename As String In rolenames
            If Not RoleExists(rolename) Then
                Throw New ProviderException("Role name not found.")
            End If
        Next

        For Each AccountUsername As String In AccountUsernames
            For Each rolename As String In rolenames
                If Not IsUserInRole(AccountUsername, rolename) Then
                    Throw New ProviderException("User is not in role.")
                End If
            Next
        Next

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("DELETE FROM " + usersInRolesTable + " WHERE AccountUsername = @AccountUsername AND Rolename = @Rolename AND ApplicationName = @ApplicationName", conn)

        Dim userParm As SqlParameter = cmd.Parameters.Add("@AccountUsername", SqlDbType.VarChar, 255)
        Dim roleParm As SqlParameter = cmd.Parameters.Add("@Rolename", SqlDbType.VarChar, 255)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Dim tran As SqlTransaction = Nothing

        Try
            conn.Open()
            tran = conn.BeginTransaction
            cmd.Transaction = tran

            For Each AccountUsername As String In AccountUsernames
                For Each rolename As String In rolenames
                    userParm.Value = AccountUsername
                    roleParm.Value = rolename
                    cmd.ExecuteNonQuery()
                Next
            Next

            tran.Commit()
        Catch e As SqlException
            Try
                tran.Rollback()
            Catch
            End Try

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

#Region " RoleExists "
    '
    ' RoleProvider.RoleExists
    '

    Public Overrides Function RoleExists(ByVal rolename As String) As Boolean
        Dim exists As Boolean = False

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT COUNT(Rolename) FROM " + rolesTable + " WHERE Rolename = @Rolename AND ApplicationName = @ApplicationName", conn)

        cmd.Parameters.AddWithValue("@Rolename", rolename)
        cmd.Parameters.AddWithValue("@ApplicationName", ApplicationName)

        Try
            conn.Open()

            Dim numRecs As Integer = CType(cmd.ExecuteScalar(), Integer)

            If numRecs > 0 Then
                exists = True
            End If
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

        Return exists
    End Function
#End Region

#Region " FindUsersInRole "
    '
    ' RoleProvider.FindUsersInRole
    '
    Public Overrides Function FindUsersInRole(ByVal rolename As String, ByVal AccountUsernameToMatch As String) As String()

        Dim conn As SqlConnection = New SqlConnection(connectionString)
        Dim cmd As SqlCommand = New SqlCommand("SELECT AccountUsername FROM " + usersInRolesTable + " WHERE AccountUsername LIKE @AccountUsernameSearch AND RoleName = @RoleName AND ApplicationName = @ApplicationName", conn)
        cmd.Parameters.AddWithValue("@AccountUsernameSearch", AccountUsernameToMatch)
        cmd.Parameters.AddWithValue("@RoleName", rolename)
        cmd.Parameters.AddWithValue("@ApplicationName", pApplicationName)

        Dim tmpAccountUsernames As String = ""
        Dim reader As SqlDataReader = Nothing

        Try
            conn.Open()

            reader = cmd.ExecuteReader()

            Do While reader.Read()
                tmpAccountUsernames &= reader.GetString(0) & ","
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

        If tmpAccountUsernames.Length > 0 Then
            ' Remove trailing comma.
            tmpAccountUsernames = tmpAccountUsernames.Substring(0, tmpAccountUsernames.Length - 1)
            Return tmpAccountUsernames.Split(CChar(","))
        End If

        Return New String() {}
    End Function
#End Region

End Class