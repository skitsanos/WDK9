Imports System.Xml.Serialization

Module Module1

    Sub Main()
        Dim channel As New WDK.Media.Feeds.ChannelType
        channel.Title = "my feed channel"
        channel.Copyright = "Evgenios Skitsanos, 2008"
        channel.ManagingEditor = "Evgenios Skitsanos, info@skitsanos.com"

        channel.Keywords = "asp, vb, xml, ajax"
        channel.Description = "my new channel description"

        Dim item As New WDK.Media.Feeds.ChannelItemType
        item.Title = "My item"
        item.Link = "http://www.skitsanos.com/"
        item.Author = "Evgenios Skitsanos"
        item.Description = "When you subscribe to a feed, it is added to the Common Feed List. Updated information from the feed is automatically downloaded to your computer and can be viewed in Internet Explorer and other programs"
        item.Enclosure.Url = "http://ekdotis.mywdk.com/videos/City Chats.mp3"
        item.Enclosure.MimeType = "audio/mpeg"
        item.Enclosure.Length = "9282981"
        channel.Items.Add(item)

        channel.Categories.Add("Technology")
        channel.Categories(0).Categories.Add("Internet")

        Debug.WriteLine(channel.DocumentElement.OuterXml)

        Dim doc As New Xml.XmlDocument
        doc.LoadXml(channel.DocumentElement.OuterXml)
        doc.Save("C:\Sites\LeeWilkins\hsn.com\www\feed-new.xml")
    End Sub

End Module

Public Class itunes
    <XmlElement("itunes:author", GetType(String))> Public author As String
End Class
