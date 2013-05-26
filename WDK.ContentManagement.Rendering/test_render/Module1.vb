Imports WDK.ContentManagement

Module Module1

    Sub Main()
        Dim Template As String = _
        "{@= My.Computer.Info.OSFullName}" & vbCrLf & _
        "{@= 650*384*15}"
        '"{@= (New WDK.Utilities.Http).FetchUrl(""http://www.google.com"")}" & vbCrLf

        Dim proc As New Rendering.Process(Template)
        proc.Execute()

        Console.WriteLine(proc.OuterHtml)
    End Sub

End Module
