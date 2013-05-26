Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Text
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls


<ToolboxData("<{0}:Box runat=""server""></{0}:Box>"), _
PersistChildren(True), _
ParseChildren(False)> _
Public Class Box
    Inherits Panel

#Region " Properties "
    Private _Radius As Integer = 10
    <Bindable(True), Category("Appearance"), DefaultValue("7")> _
    Public Property Radius() As Integer
        Get
            Return _Radius
        End Get
        Set(ByVal value As Integer)
            _Radius = value
        End Set
    End Property

    Private _FillColor As String = "ff99cc" '"#CBD4E6"
    Public Property FillColor() As String
        Get
            Return _FillColor
        End Get
        Set(ByVal value As String)
            _FillColor = value
        End Set
    End Property


#End Region

#Region " AddAttributesToRender "
    Protected Overrides Sub AddAttributesToRender(ByVal w As HtmlTextWriter)
        'w.AddAttribute("id", Me.ID)
        'MyBase.AddAttributesToRender(w)
    End Sub
#End Region

#Region " RenderContents "
    Protected Overrides Sub RenderContents(ByVal w As HtmlTextWriter)
        If Not Site Is Nothing AndAlso Site.DesignMode Then
            '- do nothing
        Else
            Dim _bc As String = "efefef"
            w.Write("<!--" + Me.BackColor.ToArgb + " -->")
            FillColor = FillColor.Replace("#", "")

            w.WriteBeginTag("table")
            w.WriteAttribute("cellspacing", "0")
            w.WriteAttribute("cellpadding", "0")

            '- header
            w.WriteBeginTag("tr")
            w.Write(">")
            '   top-left
            w.WriteBeginTag("td")
            w.Write(">")
            w.WriteBeginTag("div")
            w.WriteAttribute("style", "background: transparent url(http://groups.google.com/groups/roundedcorners?c=" + FillColor + "&bc=" + _bc + "&w=" + Radius.ToString + "&h=" + Radius.ToString + "&a=af) repeat scroll 0px 0px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; width: " + Radius.ToString + "px; height: " + Radius.ToString + "px;")
            w.Write(">")
            w.WriteEndTag("div")
            w.WriteEndTag("td")
            '   top-center
            w.WriteBeginTag("td")
            w.WriteAttribute("width", "100%")
            w.WriteAttribute("height", Radius.ToString)
            w.WriteAttribute("bgcolor", "#" + FillColor)
            w.Write(">")
            w.WriteBeginTag("img")
            w.WriteAttribute("alt", "")
            w.WriteAttribute("width", "1")
            w.WriteAttribute("height", "1")
            w.Write(" />")
            w.WriteEndTag("td")
            '   top-bottom
            w.WriteBeginTag("td")
            w.Write(">")
            w.WriteBeginTag("div")
            '<div style="background: transparent url(/groups/roundedcorners?c=c3d9ff&bc=white&w=4&h=4&a=af) repeat scroll -4px 0px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; width: 4px; height: 4px;"/>
            w.WriteAttribute("style", "background: transparent url(http://groups.google.com/groups/roundedcorners?c=" + FillColor + "&bc=white&w=" + Radius.ToString + "&h=" + Radius.ToString + "&a=af) repeat scroll -" + Radius.ToString + "px 0px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; width: " + Radius.ToString + "px; height: " + Radius.ToString + "px;")
            w.Write(">")
            w.WriteEndTag("div")
            w.WriteEndTag("td")
            w.WriteEndTag("tr")

            '-central
            w.WriteBeginTag("tr")
            w.Write(">")
            w.WriteBeginTag("td")
            w.WriteAttribute("colspan", "3")
            w.WriteAttribute("style", "background-color: #" + FillColor)
            w.Write(">")
            w.WriteBeginTag("div")
            w.Write(" id=""" + Me.ID + """")
            w.Write(">")
            MyBase.RenderContents(w)
            w.WriteEndTag("div")
            w.WriteEndTag("td")

            w.WriteEndTag("tr")

            '- bottom
            w.WriteBeginTag("tr")
            w.Write(">")
            w.WriteBeginTag("td")
            w.Write(">")
            w.WriteBeginTag("div")
            '<div style="background: transparent url(/groups/roundedcorners?c=c3d9ff&bc=white&w=4&h=4&a=af) repeat scroll -4px 0px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; width: 4px; height: 4px;"/>
            w.WriteAttribute("style", "background: transparent url(http://groups.google.com/groups/roundedcorners?c=" + FillColor + "&bc=white&w=" + Radius.ToString + "&h=" + Radius.ToString + "&a=af) repeat scroll 0px -" + Radius.ToString + "px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; width: " + Radius.ToString + "px; height: " + Radius.ToString + "px;")
            w.Write(">")
            w.WriteEndTag("div")
            w.WriteEndTag("td")

            w.WriteBeginTag("td")
            w.WriteAttribute("width", "100%")
            w.WriteAttribute("height", Radius.ToString)
            w.WriteAttribute("bgcolor", "#" + FillColor)
            w.Write(">")
            w.WriteBeginTag("img")
            w.WriteAttribute("alt", "")
            w.WriteAttribute("width", "1")
            w.WriteAttribute("height", "1")
            w.Write("/>")
            w.WriteEndTag("td")

            w.WriteBeginTag("td")
            w.Write(">")
            w.WriteBeginTag("div")
            '<div style="background: transparent url(/groups/roundedcorners?c=c3d9ff&bc=white&w=4&h=4&a=af) repeat scroll -4px 0px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; width: 4px; height: 4px;"/>
            w.WriteAttribute("style", "background: transparent url(http://groups.google.com/groups/roundedcorners?c=" + FillColor + "&bc=white&w=" + Radius.ToString + "&h=" + Radius.ToString + "&a=af) repeat scroll -" + Radius.ToString + "px -" + Radius.ToString + "px; -moz-background-clip: -moz-initial; -moz-background-origin: -moz-initial; -moz-background-inline-policy: -moz-initial; width: " + Radius.ToString + "px; height: " + Radius.ToString + "px;")
            w.Write(">")
            w.WriteEndTag("div")
            w.WriteEndTag("td")
            w.WriteEndTag("tr")
            w.WriteEndTag("table")

            'w.WriteBeginTag("script")
            'w.WriteAttribute("type", "text/javascript")
            'w.Write(">")
            'w.Write("settings_" & Me.ID & " = {tl: { radius: " & Radius & " }, tr: { radius: " & Radius & " }, bl: { radius: " & Radius & " }, br: { radius: " & Radius & " }, antiAlias: true, autoPad: false}" & vbCrLf)
            'w.Write("var divObj_" & Me.ID & " = document.getElementById('" & Me.ID & "');" & vbCrLf)
            'w.Write("var cornersObj_" & Me.ID & " = new curvyCorners(settings_" & Me.ID & ", divObj_" & Me.ID & ");" & vbCrLf)
            'w.Write("cornersObj_" & Me.ID & ".applyCornersToAll();" & vbCrLf)
            'w.WriteEndTag("script")
        End If
    End Sub
#End Region

End Class
