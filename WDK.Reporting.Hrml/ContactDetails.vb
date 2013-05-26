Imports System.Xml.Serialization

Public Structure ContactDetailsType
    <XmlAttribute("address")> Public Address As String
    <XmlAttribute("city")> Public City As String
    <XmlAttribute("state")> Public State As String
    <XmlAttribute("zip")> Public Zip As String
    <XmlAttribute("country")> Public Country As String
    <XmlAttribute("email")> Public Email As String
    <XmlAttribute("phone")> Public Phone As String
    <XmlAttribute("tollFree")> Public TollFree As String
    <XmlAttribute("mobile")> Public Mobile As String
    <XmlAttribute("fax")> Public Fax As String
    <XmlAttribute("website")> Public Website As String
    <XmlAttribute("skype")> Public Skype As String
    <XmlAttribute("icq")> Public ICQ As String
    <XmlAttribute("yahoo")> Public Yahoo As String
    <XmlAttribute("gtalk")> Public GTalk As String
    <XmlAttribute("msn")> Public Msn As String
End Structure
