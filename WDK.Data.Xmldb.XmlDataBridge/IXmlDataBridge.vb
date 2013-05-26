Imports System.Xml

Public Interface IXmlDataBridge
    Function Execute(ByVal xq As String) As XmlDocument
    Function Query(ByVal xq As String, Optional ByVal start As Integer = 0, Optional ByVal size As Integer = 0) As XmlDocument

    Property ConnectionString() As String
End Interface


