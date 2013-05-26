Friend Module Common

#Region " Log "
    Friend Sub Log(ByVal Data As String, Optional ByVal IsError As Boolean = False)
#If Not Debug Then
        Dim logs As New WDK.Providers.Logs.ApplicationLog
        logs.write(Data, IsError)
#Else
        Debug.WriteLine(Data)
#End If

    End Sub
#End Region

End Module
