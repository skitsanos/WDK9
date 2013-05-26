Imports System.Xml

Friend Module _commons

#Region " newElement "
    Public Function newElement(ByVal name As String, ByVal value As String, ByVal doc As XmlDocument) As XmlElement
        Dim el As XmlElement = doc.CreateElement(name)
        el.InnerText = value
        Return el
    End Function
#End Region

#Region " CamelCase "
    Public Function CamelCase(ByVal source As String) As String
        If source.Split(" ").Length < 1 Then
            Return source.Replace(source.Substring(1), source.Substring(1).ToLower)
        Else
            Return Replace(Left(source, 1).ToLower & Mid(source, 2), " ", "")
        End If
    End Function
#End Region

End Module
