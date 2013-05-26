Module Module1

    Sub Main()
        Dim ci As WDK.GIS.CountryInformationType
        ci = WDK.GIS.Geonames.countryInfo("RO")

        Debug.WriteLine(ci.capital)
    End Sub

End Module
