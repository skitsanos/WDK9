Module Module1

    Sub Main()
        Dim ev As New WDK.ContentManagement.Events.EventType
        ev.title = "ȘÂȚÎĂ"

        Debug.WriteLine(WDK.XML.Utils.Serializer.ObjectToXmlString(ev))
    End Sub

End Module
