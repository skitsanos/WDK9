Imports System.Xml.Serialization

Public Structure JobLocation
    <XmlAttribute()> Public City As String
    <XmlAttribute()> Public State As String
    <XmlAttribute()> Public Zip As String
    <XmlAttribute()> Public Country As String
End Structure