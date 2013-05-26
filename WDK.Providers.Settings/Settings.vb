Imports System.Configuration
Imports System.Web.Configuration
Imports System.Data.SqlClient

Public Class [Settings]

#Region " Set "
	Public Shared Sub [Set](ByVal PropertyName As String, ByVal PropertyValue As String)
		If [Get](PropertyName) Is Nothing Then
			Add(PropertyName, PropertyValue)
		Else
			Update(PropertyName, PropertyValue)
		End If
	End Sub
#End Region

#Region " .Add() "
	Private Shared Function Add(ByVal PropertyName As String, ByVal PropertyValue As String) As Boolean
		Dim db As SqlConnection = Nothing
		Dim ret As Boolean = False
		Try
			db = New SqlConnection(ConnectionString)
			db.Open()

			Dim dbc As New SqlCommand("INSERT INTO Settings (ApplicationName, PropertyName, PropertyValue) VALUES (@ApplicationName, @PropertyName, @PropertyValue)", db)
			dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
			dbc.Parameters.AddWithValue("@PropertyName", PropertyName)
			dbc.Parameters.AddWithValue("@PropertyValue", PropertyValue)

			dbc.ExecuteNonQuery()

			dbc.Dispose()

			Return True

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

#Region " Update() "
	Private Shared Function Update(ByVal PropertyName As String, ByVal PropertyValue As String) As Boolean
		Dim db As SqlConnection = Nothing
		Dim ret As Boolean = False
		Try
			db = New SqlConnection(ConnectionString)
			db.Open()

			Dim strSQL As String = "UPDATE Settings SET PropertyValue=@PropertyValue WHERE ApplicationName=@ApplicationName AND PropertyName=@PropertyName"
			Dim dbc As New SqlCommand(strSQL, db)
			dbc.Parameters.AddWithValue("@PropertyValue", PropertyValue)
			dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
			dbc.Parameters.AddWithValue("@PropertyName", PropertyName)

			dbc.ExecuteNonQuery()

			dbc.Dispose()

			ret = True

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

#Region " .Delete() "
	Public Shared Function Delete(ByVal PropertyName As String) As Boolean
		Dim db As SqlConnection = Nothing
		Try
			db = New SqlConnection(ConnectionString)
			db.Open()

			Dim dbc As New SqlCommand("DELETE FROM Settings WHERE ApplicationName=@ApplicationName AND PropertyName=@PropertyName", db)
			dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
			dbc.Parameters.AddWithValue("@PropertyName", PropertyName)
			dbc.ExecuteNonQuery()

			dbc.Dispose()

			Return True

		Catch ex As Exception
			Throw ex
		Finally
			If Not db Is Nothing Then
				db.Close()
				db.Dispose()
			End If
		End Try
	End Function
#End Region

#Region " .Get() "
	Public Shared Function [Get](ByVal PropertyName As String) As Object
		Dim db As SqlConnection = Nothing
		Dim ret As Object = Nothing

		Try
			db = New SqlConnection(ConnectionString)
			db.Open()

			Dim dbc As New SqlCommand("SELECT PropertyValue FROM Settings WHERE ApplicationName=@ApplicationName AND PropertyName=@PropertyName", db)
			dbc.Parameters.AddWithValue("@ApplicationName", GetApplicationName)
			dbc.Parameters.AddWithValue("@PropertyName", PropertyName)

			ret = dbc.ExecuteScalar()

			dbc.Dispose()

		Catch ex As Exception
			Log("Settings provider error: " + GetApplicationName() + " requested settings value for key {" + PropertyName + "}, but there is no settings registered with such key", True)
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
