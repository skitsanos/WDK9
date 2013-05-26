Imports System.Configuration
Imports System.Web.Configuration
Imports System.Web.Security

Public Class Roles

#Region " Properties "
    Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)

#End Region

#Region " GetDatasource "
	Public Function GetDatasource() As String()
		Dim res As String() = Nothing

		Try
			Dim rSection As New RoleManagerSection
			rSection = conf.GetSection("system.web/roleManager")

			Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
			roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

			roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

			res = roleProvider.GetAllRoles

		Catch ex As Exception
			Log(ex.ToString, True)
		End Try

		Return res
	End Function
#End Region

#Region " CreateRole "
    Public Function CreateRole(ByVal Name As String) As Boolean
        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            roleProvider.CreateRole(Name)

            Return True

        Catch ex As Exception
            Log(ex.ToString, True)
            Return False
        End Try
    End Function
#End Region

#Region " DeleteRole "
    Public Function DeleteRole(ByVal Name As String, ByVal ThrowOnPopulateRole As Boolean) As Boolean
        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            roleProvider.DeleteRole(Name, ThrowOnPopulateRole)

            Return True

        Catch ex As Exception
            Log(ex.ToString, True)
            Return False
        End Try
    End Function
#End Region

#Region " Exists() "
    Public Function Exists(ByVal Name As String) As Boolean
        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            Return roleProvider.RoleExists(Name)

        Catch ex As Exception
            Log(ex.ToString, True)
            Return False
        End Try
    End Function
#End Region

#Region " IsUserInRole "
    Public Function IsUserInRole(ByVal Username As String, ByVal Role As String) As Boolean
        Dim res As Boolean = False

        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            res = roleProvider.IsUserInRole(Username, Role)

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try

        Return res
    End Function
#End Region

#Region " AddUserToRole "
    ''' <summary>
    ''' Adds user into role
    ''' </summary>
    ''' <param name="Username">String</param>
    ''' <param name="Role">String</param>
    ''' <remarks>Use IsUserInRole to check if user already in role</remarks>

    Public Sub AddUserToRole(ByVal Username As String, ByVal Role As String)
        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            Dim users As String() = {Username}
            Dim roles As String() = {Role}

            roleProvider.AddUsersToRoles(users, roles)

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try
    End Sub
#End Region

#Region " RemoveUserFromRole "
    ''' <summary>
    ''' Removes user from role
    ''' </summary>
    ''' <param name="Username">String</param>
    ''' <param name="Role">String</param>
    ''' <remarks></remarks>
    Public Sub RemoveUserFromRole(ByVal Username As String, ByVal Role As String)
        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            Dim users As String() = {Username}
            Dim roles As String() = {Role}

            roleProvider.RemoveUsersFromRoles(users, roles)

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try
    End Sub
#End Region

#Region " GetRolesForUser "
    Public Function GetRolesForUser(ByVal Username As String) As String()
        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            Return roleProvider.GetRolesForUser(Username)

        Catch ex As Exception
            Log(ex.ToString, True)
            Return New String() {}
        End Try
    End Function
#End Region

#Region " GetUsersInRole "
    Public Function GetUsersInRole(ByVal Role As String) As String()
        Try
            Dim rSection As New RoleManagerSection
            rSection = conf.GetSection("system.web/roleManager")

            Dim roleProvider As New WDK.CommunityServices.NetPass.NetPassRoleProvider
            roleProvider.ApplicationName = rSection.Providers(rSection.DefaultProvider).Parameters("applicationName")

            roleProvider.Initialize(rSection.DefaultProvider, rSection.Providers(rSection.DefaultProvider).Parameters)

            Return roleProvider.GetUsersInRole(Role)

        Catch ex As Exception
            Log(ex.ToString, True)
            Return New String() {}
        End Try
    End Function
#End Region

End Class
