Imports System.Xml

Public Class Geonames
    '<geonames>
    '−
    '<country>
    '<countryCode>DE</countryCode>
    '<countryName>Germania</countryName>
    '<isoNumeric>276</isoNumeric>
    '<isoAlpha3>DEU</isoAlpha3>
    '<fipsCode>GM</fipsCode>
    '<continent>EU</continent>
    '<capital>Berlino</capital>
    '<areaInSqKm>357021.0</areaInSqKm>
    '<population>82369000</population>
    '<currencyCode>EUR</currencyCode>
    '<languages>de</languages>
    '<geonameId>2921044</geonameId>
    '<bBoxWest>5.865638256073</bBoxWest>
    '<bBoxNorth>55.0556411743164</bBoxNorth>
    '<bBoxEast>15.0398902893066</bBoxEast>
    '<bBoxSouth>47.2757720947266</bBoxSouth>
    '</country>
    '</geonames>

    Public Shared Function countryInfo(ByVal countryCode As String, Optional ByVal language As String = "en") As CountryInformationType
        Dim ret As CountryInformationType = Nothing

        Dim url As String = "http://ws.geonames.org/countryInfo?lang=" + language + "&country=" + countryCode + "&style=full"
        Dim xmldata As String = Text.Encoding.UTF8.GetString(Utils.FetchData(url))

        Dim doc As New XmlDocument
        doc.LoadXml(xmldata)

        If doc.DocumentElement.HasChildNodes = False Then
            Return ret
        Else
            ret = New CountryInformationType
            ret.countryCode = doc.SelectSingleNode("/geonames/country/countryCode").InnerText
            ret.countryName = doc.SelectSingleNode("/geonames/country/countryName").InnerText
            ret.capital = doc.SelectSingleNode("/geonames/country/capital").InnerText
            ret.areaInSqKm = doc.SelectSingleNode("/geonames/country/areaInSqKm").InnerText
            ret.bBoxEast = doc.SelectSingleNode("/geonames/country/bBoxEast").InnerText
            ret.bBoxNorth = doc.SelectSingleNode("/geonames/country/bBoxNorth").InnerText
            ret.bBoxSouth = doc.SelectSingleNode("/geonames/country/bBoxSouth").InnerText
            ret.bBoxWest = doc.SelectSingleNode("/geonames/country/bBoxWest").InnerText
            ret.continent = doc.SelectSingleNode("/geonames/country/continent").InnerText
            ret.fipsCode = doc.SelectSingleNode("/geonames/country/fipsCode").InnerText
            ret.geonameId = doc.SelectSingleNode("/geonames/country/geonameId").InnerText
            ret.isoAlpha3 = doc.SelectSingleNode("/geonames/country/isoAlpha3").InnerText
            ret.isoNumeric = doc.SelectSingleNode("/geonames/country/isoNumeric").InnerText
            ret.languages = doc.SelectSingleNode("/geonames/country/languages").InnerText
            ret.population = doc.SelectSingleNode("/geonames/country/population").InnerText

            Return ret
        End If

    End Function

End Class
