Imports System.Net

Module Module1

    Sub Main()
        Dim conv As New WDK.XML.Utils.CsvToXmlConvertor
        conv.HasFieldNamses = False

        Dim xmlSource As String = conv.Convert(AppDomain.CurrentDomain.BaseDirectory + "export.htm", "Product", New String() {"Category", "ProductId", "Title", "Description", "MoreDetails", "Price", "Currency", "Image"})

        'get XML Rates from the bank http://www.bnro.ro/nbrfxrates.xml
        Dim http As New WDK.Utilities.Http
        Dim xmlRates As String = http.FetchURL("http://www.bnro.ro/nbrfxrates.xml")
        Dim docRates As New Xml.XmlDocument
        docRates.LoadXml(xmlRates)

        Dim rateEur As Double = docRates.SelectSingleNode("//*[@currency='EUR']").InnerText
        Dim rateDollar As Double = docRates.SelectSingleNode("//*[@currency='USD']").InnerText

        Dim rateIndex As Double = rateEur / rateDollar

        Debug.WriteLine("Rate index EUR/USD: " + rateIndex.ToString)

        Dim xmldoc As New Xml.XmlDocument
        xmldoc.LoadXml(xmlSource)

        '-make all products in USD and recalclulate their price with 15% interest rate + 1% of currency exchange
        'For Each item As Xml.XmlElement In xmldoc.SelectNodes("//Product[Currency/text()='usd']")
        '    Dim priceNew As Double = CDbl(item.SelectSingleNode("Price").InnerText) / rateIndex
        '    priceNew = priceNew + (priceNew / 100 * 16)
        '    item.SelectSingleNode("Price").InnerText = FormatNumber(priceNew, 2)
        '    item.SelectSingleNode("Currency").InnerText = "eur"
        'Next

        If IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "images") = False Then IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "images")

        Dim wc As New WebClient
        wc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows; U; Windows NT 5.2; en-US; rv:1.8.1.13) Gecko/20080311 Firefox/2.0.0.13")

        For Each item As Xml.XmlElement In xmldoc.SelectNodes("//Product")
            Select Case item.SelectSingleNode("Currency").InnerText
                Case "usd"
                    Dim priceNew As Double = CDbl(item.SelectSingleNode("Price").InnerText) * rateDollar
                    priceNew = priceNew + (priceNew / 100 * 16)
                    Dim vat As Double = priceNew / 100 * 19
                    priceNew += vat

                    item.SelectSingleNode("Price").InnerText = FormatNumber(priceNew, 2, TriState.False, , TriState.False)
                    item.SelectSingleNode("Currency").InnerText = "ron"

                Case "eur"
                    Dim priceNew As Double = CDbl(item.SelectSingleNode("Price").InnerText) * rateEur
                    priceNew = priceNew + (priceNew / 100 * 16)
                    Dim vat As Double = priceNew / 100 * 19
                    priceNew += vat

                    item.SelectSingleNode("Price").InnerText = FormatNumber(priceNew, 2, TriState.False, , TriState.False)
                    item.SelectSingleNode("Currency").InnerText = "ron"
            End Select

            Dim urlParser As New WDK.Utilities.Parsers.UrlParser(item.SelectSingleNode("Image").InnerText)
            Debug.WriteLine(urlParser.Filename)

            If urlParser.Filename <> "" Then wc.DownloadFile(New Uri(item.SelectSingleNode("Image").InnerText), AppDomain.CurrentDomain.BaseDirectory + "images\" + urlParser.Filename)
            item.SelectSingleNode("Image").InnerText = "images/products/" + urlParser.Filename

            Debug.WriteLine("pause...")
            For i As Integer = 0 To 500000
                'pause
            Next

        Next

        Dim sw As New IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "store.xml")
        sw.Write(xmldoc.OuterXml)
        sw.Close()
    End Sub

End Module
