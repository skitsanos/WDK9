Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls

<Assembly: TagPrefix("WDK.ContentManagement", "WDK")> 

<DefaultProperty("Text"), ToolboxData("<{0}:RssReader runat=server></{0}:RssReader>")> _
Public Class RssReader
    Inherits WebControl

#Region " Properties "
    Private _Rows As Integer = 5
    <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> Property Rows() As Integer
        Get
            Return _Rows
        End Get

        Set(ByVal Value As Integer)
            _Rows = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> Property Url() As String
        Get
            Dim s As String = CStr(ViewState("Url"))
            If s Is Nothing Then
                Return String.Empty
            Else
                Return s
            End If
        End Get

        Set(ByVal Value As String)
            ViewState("Url") = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> Property TitleStyle() As String
        Get
            Dim s As String = CStr(ViewState("TitleStyle"))
            If s Is Nothing Then
                Return String.Empty
            Else
                Return s
            End If
        End Get

        Set(ByVal Value As String)
            ViewState("TitleStyle") = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> Property pubDateStyle() As String
        Get
            Dim s As String = CStr(ViewState("pubDateStyle"))
            If s Is Nothing Then
                Return String.Empty
            Else
                Return s
            End If
        End Get

        Set(ByVal Value As String)
            ViewState("pubDateStyle") = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue(""), Localizable(True)> Property DescriptionStyle() As String
        Get
            Dim s As String = CStr(ViewState("DescriptionStyle"))
            If s Is Nothing Then
                Return String.Empty
            Else
                Return s
            End If
        End Get

        Set(ByVal Value As String)
            ViewState("DescriptionStyle") = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue(False), Localizable(True)> Property ShowDescription() As Boolean
        Get
            Dim s As Boolean = ViewState("ShowDescription")
            Return s
        End Get

        Set(ByVal Value As Boolean)
            ViewState("ShowDescription") = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue("RssSeparator"), Localizable(True)> Property SeparatorStyle() As String
        Get
            Dim s As String = CStr(ViewState("SeparatorStyle"))
            If s Is Nothing Then
                Return String.Empty
            Else
                Return s
            End If
        End Get

        Set(ByVal Value As String)
            ViewState("SeparatorStyle") = Value
        End Set
    End Property
#End Region

#Region " RenderContents "
    Protected Overrides Sub RenderContents(ByVal output As HtmlTextWriter)
        If Not Site Is Nothing AndAlso Site.DesignMode Then
            Dim htmlSource As String = ""
            Dim t As Integer = 1
            'For t = 1 To Rows
            If TitleStyle <> "" Then htmlSource += "<span class=""" & TitleStyle & """>"
            htmlSource += "<a href=""#"" target=""_blank""> Title #" & t & "</a>"
            If TitleStyle <> "" Then htmlSource += "</span>"
            htmlSource += "<br/>"

            If pubDateStyle <> "" Then htmlSource += "<span class=""" & pubDateStyle & """>"
            htmlSource += Now.ToUniversalTime.ToString
            If pubDateStyle <> "" Then htmlSource += "</span>"
            htmlSource += "<br/>"

            If DescriptionStyle <> "" Then htmlSource += "<span class=""" & DescriptionStyle & """>"
            htmlSource += "Description for feed item #" & t
            If DescriptionStyle <> "" Then htmlSource += "</span>"

            htmlSource += "<p/>"
            output.Write(htmlSource)
            'Next
        Else
            output.Write(Html)
        End If
    End Sub
#End Region

#Region " Html "
    Public Function Html() As String
        Try
            Dim Http As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.Http,wdk.utilities"))
            Dim rssFeed As String = HTTP.FetchURL(Url)

            Dim xmlDoc As New System.Xml.XmlDocument
            xmlDoc.LoadXml(rssFeed)

            Dim htmlSource As String = ""

            Dim Items As System.Xml.XmlNodeList = xmlDoc.SelectNodes("/rss/channel/item")
            Dim topic As System.Xml.XmlElement = Nothing
            Dim rowsCounter As Integer = 0
            For Each topic In Items
                Dim url As String = topic.SelectSingleNode("link").InnerText

                If TitleStyle <> "" Then htmlSource += "<span class=""" & TitleStyle & """>"
                htmlSource += "<a href=""" & url & """ target=""_blank"">" & topic.SelectSingleNode("title").InnerText & "</a>"
                If TitleStyle <> "" Then htmlSource += "</span>"
                htmlSource += "<br/>"

                If pubDateStyle <> "" Then htmlSource += "<span class=""" & pubDateStyle & """>"
                htmlSource += topic.SelectSingleNode("pubDate").InnerText
                If pubDateStyle <> "" Then htmlSource += "</span>"
                htmlSource += "<br/>"

                If ShowDescription = True Then
                    If DescriptionStyle <> "" Then htmlSource += "<span class=""" & DescriptionStyle & """>"
                    htmlSource += topic.SelectSingleNode("description").InnerText
                    If DescriptionStyle <> "" Then htmlSource += "</span>"
                End If

                htmlSource += "<div class=""" & SeparatorStyle & """></div>"

                htmlSource += "<p/>"
                rowsCounter += 1
                If rowsCounter = Rows Then Exit For
            Next

            Return htmlSource
        Catch xex As System.Xml.XmlException
            Return "Error occured during reading XML content from remote server"

        Catch ex As Exception
            Return ex.ToString
        End Try
    End Function
#End Region

End Class