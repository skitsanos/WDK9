Imports System.Xml.Serialization

<XmlTypeAttribute(AnonymousType:=True)> _
Public Enum PageContentType
    <XmlEnum("html")> HTML
    <XmlEnum("rss")> RSS
    <XmlEnum("wiki")> WIKI
    <XmlEnum("text")> Text
End Enum
