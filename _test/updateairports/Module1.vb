Imports System.Xml


Module Module1

    Sub Main()
        Dim docIata As New XmlDocument
        docIata.Load(AppDomain.CurrentDomain.BaseDirectory + "IataAirports.xml")

        Dim docRaw As New XmlDocument
        docRaw.Load(AppDomain.CurrentDomain.BaseDirectory + "Airports.xml")

        For Each el As XmlElement In docIata.SelectNodes("//Airport")
            Dim elFound As XmlElement = docRaw.SelectSingleNode("//Airport[@code='" + el.GetAttribute("code") + "']")
            If elFound IsNot Nothing Then
                Dim name As String = elFound.GetAttribute("name")
                name = name.ToLower
                name = name.Replace(Left(name, 1), Left(name, 1).ToUpper)
                el.SetAttribute("name", name)
            Else
                el.SetAttribute("name", "UNTITLED")
            End If

        Next

        docIata.DocumentElement.SetAttribute("version", "2.0")
        docIata.Save(AppDomain.CurrentDomain.BaseDirectory + "IataAirports-updated.xml")

    End Sub

End Module
