Imports System.Xml.Serialization

Module _common

#Region " Xml Serialization "
    Public Function GetXml(ByVal obj As Object) As Xml.XmlDocument
        Dim xmldoc As Xml.XmlDocument = Nothing

        Dim sb As New Text.StringBuilder
        Dim tw As IO.TextWriter = New IO.StringWriter(sb)

        Dim ser As New XmlSerializer(obj.GetType)
        ser.Serialize(tw, obj)

        xmldoc = New Xml.XmlDocument
        xmldoc.LoadXml(sb.ToString)

        Return xmldoc
    End Function

    Public Function SetXml(ByVal Type As System.Type, ByVal XmlData As String) As Object
        Dim obj As Object = Nothing
        Dim ser As New XmlSerializer(Type)
        Dim tr As IO.TextReader = New IO.StringReader(XmlData)

        obj = ser.Deserialize(tr)

        Return obj
    End Function
#End Region

End Module
