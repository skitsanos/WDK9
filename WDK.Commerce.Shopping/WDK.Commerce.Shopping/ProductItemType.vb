Imports System.Xml.Serialization

''' <summary>
''' Product Item Type defenition
''' </summary>
''' <remarks></remarks>

Public Class ProductItemType
    <XmlAttribute("id")> Public Id As String = Guid.NewGuid.ToString
    <XmlAttribute("title")> Public Title As String
    <XmlAttribute("description")> Public Descriptiuon As String
    <XmlAttribute("price")> Public Price As String
End Class
