Imports System.Net

Public Class Utils
    Public Shared Function getData(ByVal url As String) As String
        Dim request As WebRequest = WebRequest.Create(url)
        Dim res As WebResponse = request.GetResponse

        Dim temp As String = ""

        Using reader As New IO.StreamReader(res.GetResponseStream)
            temp = reader.ReadToEnd
        End Using

        Return temp
    End Function
End Class
