Imports System.Xml.Serialization

Public Structure ScheduledEvent
    <XmlAttribute()> Public Title As String
    <XmlAttribute()> Public Description As String
    <XmlAttribute()> Public StartsOn As DateTime
    <XmlAttribute()> Public EndsOn As DateTime
    <XmlAttribute()> Public Username As String
End Structure
