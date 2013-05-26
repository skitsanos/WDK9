Imports System.Configuration
Imports System.Web.Configuration
Imports System.Web.Security
Imports System.Data.SqlClient

Public Class Users

#Region " Add() "
    Public Function Add(ByVal Username As String, ByVal Email As String, ByVal Password As String, ByVal SecurityQuestion As String, ByVal SecurityAnswer As String, Optional ByVal Comment As String = "") As Boolean
        Email = Email.ToLower.Trim

        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            Dim status As MembershipCreateStatus
            NetPass.CreateUser(Username, Password, Email, SecurityQuestion, SecurityAnswer, True, Guid.NewGuid(), status)

            If status = MembershipCreateStatus.Success Then
                Return True
            Else
                Log("User creation failed: " & status.ToString, True)
                Throw New Exception("User creation failed: " & status.ToString)
            End If

            Return True

        Catch ex As Exception
            Log(ex.ToString, True)
            Return False
        End Try
    End Function
#End Region

#Region " Update() "
    Public Function Update(ByVal Username As String, ByVal Email As String) As Boolean
        Email = Email.ToLower.Trim

        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            Dim userOnline As Boolean
            Dim nUser As MembershipUser = NetPass.GetUser(Username, userOnline)

            nUser.Email = Email
            NetPass.UpdateUser(nUser)

            Return True

        Catch ex As Exception
            Log(ex.ToString, True)
            Return False
        End Try
    End Function
#End Region

#Region " ResetPassword "
    Public Function ResetPassword(ByVal Username As String, ByVal PasswordAnswer As String) As String
        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            Dim userOnline As Boolean
            Dim nUser As MembershipUser = NetPass.GetUser(Username, userOnline)

            Return nUser.ResetPassword(PasswordAnswer)

        Catch ex As Exception
            Log(ex.ToString, True)
            Return ""
        End Try
    End Function
#End Region

#Region " ResetHints "
    Public Function ResetHints(ByVal Username As String, ByVal Password As String, ByVal SecurityQuestion As String, ByVal SecurityAnswer As String) As Boolean
        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            Dim userOnline As Boolean
            Dim nUser As MembershipUser = NetPass.GetUser(Username, userOnline)

            If nUser.ChangePasswordQuestionAndAnswer(Password, SecurityQuestion, SecurityAnswer) = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            Log(ex.ToString, True)
            Return False
        End Try
    End Function
#End Region

#Region " GetUser "
    Public Function GetUser(ByVal Username As String, ByVal IsOnline As Boolean) As MembershipUser
        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            Return NetPass.GetUser(Username, IsOnline)

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
#End Region

#Region " .GetPropertyById() "
    Public Function GetPropertyById(ByVal Id As Integer, ByVal Key As String) As Object
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT * FROM Users WHERE ApplicationName=@ApplicationName AND id=" + Id, db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Users")

            Dim res As Object = Nothing
            If dbs.Tables("Users").Rows.Count > 0 Then
                res = dbs.Tables("Users").Rows(0).Item(Key)
            End If

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return res
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

#Region " .SetPropertyById() "
    Public Function SetPropertyById(ByVal Id As Integer, ByVal Key As String, ByVal Value As Object) As Integer
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("UPDATE Users SET " + Key + "=@" + Key + " WHERE id=" & Id, db)
            dbc.Parameters.AddWithValue("@" + Key, Value)

            Dim rows As Integer = dbc.ExecuteNonQuery

            dbc.Dispose()

            Return rows

        Catch ex As Exception
            Log(ex.ToString, True)
            Return 0
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " .GetPropertyByUsername() "
    Public Function GetPropertyByUsername(ByVal AccountUsername As String, ByVal Key As String) As Object
        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT * FROM Users WHERE ApplicationName=@ApplicationName AND AccountUsername=@AccountUsername", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@AccountUsername", AccountUsername)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Users")

            Dim res As Object = Nothing
            If dbs.Tables("Users").Rows.Count > 0 Then
                res = dbs.Tables("Users").Rows(0).Item(Key)
            End If

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return res
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

#Region " .SetPropertyByUsername() "
    Public Function SetPropertyByUsername(ByVal AccountUsername As String, ByVal Key As String, ByVal Value As Object) As Integer
        Dim db As SqlConnection = Nothing
        Dim ret As Integer = 0
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("UPDATE Users SET " + Key + "=@" + Key + " WHERE ApplicationName=@ApplicationName AND AccountUsername=@AccountUsername", db)
            dbc.Parameters.AddWithValue("@" + Key, Value)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@AccountUsername", AccountUsername)

            Dim rows As Integer = dbc.ExecuteNonQuery

            dbc.Dispose()

            ret = rows

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

#Region " .GetIdByEmail() "
    Public Function GetIdByEmail(ByVal Email As String) As Integer
        Email = Email.ToLower.Trim

        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT * FROM Users WHERE ApplicationName=@ApplicationName AND Email=@Email", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@Email", Email)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Users")

            Dim res As Integer = 0
            If dbs.Tables("Users").Rows.Count > 0 Then
                res = dbs.Tables("Users").Rows(0).Item("id")
            Else
                res = 0
            End If

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

            Return res
        Catch ex As Exception
            Log(ex.ToString, True)
            Return 0
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try
    End Function
#End Region

#Region " .Delete() "
    Public Function Delete(ByVal Username As String) As Boolean
        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            NetPass.DeleteUser(Username, True)

            Return True

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try
    End Function
#End Region

#Region " GetDatasource() "
    Public Function GetDatasource(Optional ByVal Criteria As String = "") As MembershipUserCollection
        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            If Criteria <> "" Then
                Return NetPass.FindUsersByName(Criteria)
            Else
                Return NetPass.GetAllUsers
            End If


        Catch ex As Exception
            Log(ex.ToString, True)
            Return Nothing
        End Try
    End Function
#End Region

#Region " Exists() "
    Public Function Exists(ByVal Username As String) As Boolean
        Dim db As SqlConnection = Nothing

        Dim res As Boolean = False
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT * FROM Users WHERE ApplicationName=@ApplicationName AND AccountUsername=@AccountUsername", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
            dbc.Parameters.AddWithValue("@AccountUsername", Username)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet
            dbs.Clear()
            dba.Fill(dbs, "Users")

            If dbs.Tables("Users").Rows.Count > 0 Then res = True

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try

        Return res
    End Function
#End Region

#Region " Validate() "
    Public Function ValidateUser(ByVal Username As String, ByVal Password As String) As Boolean
        Dim ret As Boolean = False

        Try
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)
            Dim mSection As New MembershipSection
            mSection = conf.GetSection("system.web/membership")

            Dim NetPass As New WDK.CommunityServices.NetPass.NetPassMembershipProvider
            NetPass.ApplicationName = GetApplicationName()
            NetPass.Initialize(mSection.DefaultProvider, mSection.Providers(mSection.DefaultProvider).Parameters)

            ret = NetPass.ValidateUser(Username, Password)

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try
        Return ret
    End Function
#End Region

#Region " Count() "
    Public Function Count() As Integer
        Dim cnt As Integer = 0

        Dim db As SqlConnection = Nothing
        Try
            db = New SqlConnection(ConnectionString)
            db.Open()

            Dim dbc As New SqlCommand("SELECT * FROM Users WHERE ApplicationName=@ApplicationName", db)
            dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)

            Dim dba As New SqlDataAdapter
            dba.SelectCommand = dbc

            Dim dbs As New Data.DataSet("result")
            dbs.Clear()
            dba.Fill(dbs, "Users")

            cnt = dbs.Tables("Users").Rows.Count

            dbs.Dispose()
            dba.Dispose()
            dbc.Dispose()

        Catch ex As Exception
            Log(ex.ToString, True)
        Finally
            If Not db Is Nothing Then
                db.Close()
                db.Dispose()
            End If
        End Try

        Return cnt
    End Function
#End Region

End Class
