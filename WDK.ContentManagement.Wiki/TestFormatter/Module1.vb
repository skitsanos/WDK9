Module Module1

    Sub Main()
        Dim sr As New IO.StreamReader(AppDomain.CurrentDomain.BaseDirectory + "wikiContent.txt")
        Dim sw As New IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "htmlContent.htm")

        Dim wikiContent As String = sr.ReadToEnd
        sr.Close()

        Dim fmt As New WDK.ContentManagement.Wiki.WikiTextFormatter

        Dim htmlContent As String = fmt.Format(wikiContent)

        sw.WriteLine(htmlContent)
        sw.Close()

        Debug.WriteLine(htmlContent)

    End Sub

End Module
