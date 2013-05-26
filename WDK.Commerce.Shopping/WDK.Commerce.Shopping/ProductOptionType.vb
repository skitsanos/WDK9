Imports System.Xml.Serialization

''' <summary>
''' Product Item Option Type defenition
''' </summary>
''' <remarks></remarks>

Public Class ProductOptionType
    <XmlAttribute("key")> Public Key As String
    <XmlAttribute("value")> Public Value As String
End Class
