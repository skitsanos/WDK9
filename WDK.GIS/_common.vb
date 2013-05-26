Friend Class Utils

#Region " FetchURL() "
    Friend Shared Function FetchData(ByVal URL As String) As Byte()
        Try
            Dim webClient As Net.WebClient = New Net.WebClient
            webClient.Headers.Add("pragma", "no-cache")
            webClient.Headers.Add("cache-control", "private")

            Dim b As Byte() = webClient.DownloadData(URL)
            Return b

        Catch e As Exception
            Return Nothing
        End Try
    End Function
#End Region

End Class
