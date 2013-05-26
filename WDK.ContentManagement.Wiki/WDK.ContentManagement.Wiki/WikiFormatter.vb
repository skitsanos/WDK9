Imports System.Text.RegularExpressions
Imports System.Collections.Generic

Public Class WikiTextFormatter

#Region " Properties "
    Private ReadOnly Replacements As Dictionary(Of String, String)
#End Region

#Region " New "
    Public Sub New()
        If IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "wikirules.xml") Then
            Replacements = New Dictionary(Of String, String)()

            Dim xmldoc As New Xml.XmlDocument
            xmldoc.Load(AppDomain.CurrentDomain.BaseDirectory & "wikirules.xml")

            For Each wikiElement As Xml.XmlElement In xmldoc.SelectNodes("//Replacements/*")
                Replacements.Add(wikiElement.SelectSingleNode("Expression").InnerText, wikiElement.SelectSingleNode("Replacement").InnerText)
            Next
        End If
    End Sub
#End Region

#Region " Format "
    Public Function Format(ByVal WikiContent As String) As String
        Dim Output As String = WikiContent

        For Each Expression As String In Replacements.Keys
            Output = Regex.Replace(Output, Expression, Replacements(Expression))
        Next

        Return Output
    End Function
#End Region

End Class