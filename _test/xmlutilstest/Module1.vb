Imports System.Xml.Serialization

Module Module1

    Sub Main()
        Dim pics As New List(Of Types.LinkType)
        Dim item As New Types.LinkType
        item.url = "xxx"
        pics.Add(item)

        Dim ml As String = Trim(WDK.XML.Utils.Serializer.ObjectToXmlString(pics))
        Debug.WriteLine(ml)

        Dim obj As List(Of String) = WDK.XML.Utils.Serializer.SetXml(GetType(List(Of Types.LinkType)), ml)
        'WDK.XML.Utils.Serializer.XmlStringToObject(Of List(Of String))(ml)
        ' Debug.WriteLine(obj(0))
    End Sub



End Module

#Region " ReportsItemType "
Namespace Types

    <XmlRoot("Report")> _
    Public Class ReportsItemType

        <XmlAttribute()> Public uid As String
        <XmlAttribute()> Public representativeId As String
        <XmlAttribute()> Public manufacturerId As String
        <XmlAttribute()> Public storeId As String
        <XmlAttribute()> Public signInOut As String
        <XmlAttribute()> Public cleanMerchandisingArea As String
        <XmlAttribute()> Public replaceRepairDisplay As String
        <XmlAttribute()> Public replaceTornLabels As String
        <XmlAttribute()> Public stockPartsKits As String
        <XmlAttribute()> Public faceShelves As String
        <XmlAttribute()> Public checkLowOutofSocks As String
        <XmlAttribute()> Public serviceMerchandisedAreas As String
        <XmlAttribute()> Public suggestOrder As String
        <XmlAttribute()> Public orderInSystem As String
        <XmlAttribute()> Public orderInSystemNumber As String
        <XmlAttribute()> Public reviewResolveRtv As String
        <XmlAttribute()> Public discussWithAmDm As String
        <XmlAttribute()> Public createdOn As DateTime
        <XmlAttribute()> Public adminReplyedRep As String
        <XmlAttribute()> Public adminReplyedMan As String
        <XmlAttribute()> Public repManReplyed As String
        <XmlAttribute()> Public adminViewed As String
        <XmlAttribute()> Public manufacturerViewed As String
        <XmlAttribute()> Public representativeViewed As String

        <XmlArray("stock"), XmlArrayItem("item")> Public Stocks As New List(Of StockType)
        <XmlArray("pictures"), XmlArrayItem("link")> Public Pictures As New List(Of LinkType)
        <XmlArray("comments"), XmlArrayItem("comment")> Public Notes As New List(Of CommentType)
    End Class

    <XmlRoot("Report")> _
    Public Class ReportsViewItemType

        Public uid As String
        Public representative As String
        Public manufacturer As String
        Public store As String
        Public representativeId As String
        Public manufacturerId As String
        Public storeId As String
        Public signInOut As String
        Public cleanMerchandisingArea As String
        Public replaceRepairDisplay As String
        Public replaceTornLabels As String
        Public stockPartsKits As String
        Public faceShelves As String
        Public checkLowOutofSocks As String
        Public serviceMerchandisedAreas As String
        Public suggestOrder As String
        Public orderInSystem As String
        Public orderInSystemNumber As String
        Public reviewResolveRtv As String
        Public discussWithAmDm As String
        Public createdOn As DateTime
        Public adminReplyedRep As String
        Public adminReplyedMan As String
        Public repManReplyed As String
        Public adminViewed As String
        Public manufacturerViewed As String
        Public representativeViewed As String

        <XmlArray("stock"), XmlArrayItem("item")> Public Stocks As New List(Of StockType)
        <XmlArray("pictures"), XmlArrayItem("link")> Public Pictures As New List(Of LinkType)
        <XmlArray("comments"), XmlArrayItem("comment")> Public Notes As New List(Of CommentType)
    End Class

    <XmlRoot("comment")> _
    Public Class CommentType
        <XmlAttribute()> Public createdOn As DateTime
        <XmlAttribute()> Public username As String
        <XmlAttribute()> Public subject As String
        <XmlAttribute()> Public notes As String
    End Class

    <XmlRoot("stock")> _
    Public Class StockType
        <XmlAttribute()> Public sku As String
        <XmlAttribute()> Public onOrder As String
        <XmlAttribute()> Public onHand As String
    End Class

    <XmlRoot("link")> _
    Public Class LinkType
        <XmlAttribute()> Public url As String
    End Class
End Namespace
#End Region
