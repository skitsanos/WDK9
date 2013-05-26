Imports HtmlAgilityPack

'Requires HtmlAgilityPack
'http://www.codeplex.com/htmlagilitypack

Public Class Processor
	Public Function Execute(ByVal content As String) As String
		Dim doc As New HtmlDocument
		doc.LoadHtml(content)
		doc.OptionOutputAsXml = True

		Return doc.DocumentNode.OuterHtml
	End Function
End Class
