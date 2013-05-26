Friend Module _common

#Region " .Log() "
	Friend Sub Log(ByVal Data As String, Optional ByVal IsError As Boolean = False)
		Dim logs As Object = Activator.CreateInstance(Type.GetType("WDK.Providers.Logs.ApplicationLog,WDK.Providers.Logs"))
		If logs IsNot Nothing Then
			logs.Write(Data, IsError)
		End If
	End Sub
#End Region

End Module
