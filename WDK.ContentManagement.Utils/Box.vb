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
	Private _Radius As Integer = 7
	<Bindable(True), Category("Appearance"), DefaultValue("7")> _
	Public Property Radius() As Integer
		Get
			Return _Radius
		End Get
		Set(ByVal value As Integer)
			_Radius = value
		End Set
	End Property
#End Region

#Region " AddAttributesToRender "
	Protected Overrides Sub AddAttributesToRender(ByVal w As HtmlTextWriter)
		w.AddAttribute("id", Me.ID)
		MyBase.AddAttributesToRender(w)
	End Sub
#End Region

#Region " RenderContents "
	Protected Overrides Sub RenderContents(ByVal w As HtmlTextWriter)
		If Not Site Is Nothing AndAlso Site.DesignMode Then
			'- do nothing
		Else
			MyBase.RenderContents(w)

			w.WriteBeginTag("script")
			w.WriteAttribute("type", "text/javascript")
			w.Write(">")
			w.Write("settings_" & Me.ID & " = {tl: { radius: " & Radius & " }, tr: { radius: " & Radius & " }, bl: { radius: " & Radius & " }, br: { radius: " & Radius & " }, antiAlias: true, autoPad: false}" & vbCrLf)
			w.Write("var divObj_" & Me.ID & " = document.getElementById('" & Me.ID & "');" & vbCrLf)
			w.Write("var cornersObj_" & Me.ID & " = new curvyCorners(settings_" & Me.ID & ", divObj_" & Me.ID & ");" & vbCrLf)
			w.Write("cornersObj_" & Me.ID & ".applyCornersToAll();" & vbCrLf)
			w.WriteEndTag("script")
		End If
	End Sub
#End Region

End Class
