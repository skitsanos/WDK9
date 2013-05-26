Imports System
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Windows.Forms
Imports System.Diagnostics

Namespace SiteThumbnail
	Public Class GetImage

#Region " Properties "
		Private S_Height As Integer
		Private S_Width As Integer
		Private F_Height As Integer
		Private F_Width As Integer
		Private MyURL As String

		Property ScreenHeight() As Integer
			Get
				Return S_Height
			End Get
			Set(ByVal value As Integer)
				S_Height = value
			End Set
		End Property

		Property ScreenWidth() As Integer
			Get
				Return S_Width
			End Get
			Set(ByVal value As Integer)
				S_Width = value
			End Set
		End Property

		Property ImageHeight() As Integer
			Get
				Return F_Height
			End Get
			Set(ByVal value As Integer)
				F_Height = value
			End Set
		End Property

		Property ImageWidth() As Integer
			Get
				Return F_Width
			End Get
			Set(ByVal value As Integer)
				F_Width = value
			End Set
		End Property

		Property Url() As String
			Get
				Return MyURL
			End Get
			Set(ByVal value As String)
				MyURL = value
			End Set
		End Property
#End Region

		Sub New(ByVal Url As String, ByVal ScreenWidth As Integer, ByVal ScreenHeight As Integer, ByVal ImageWidth As Integer, ByVal ImageHeight As Integer)
			Me.Url = Url
			Me.ScreenWidth = ScreenWidth
			Me.ScreenHeight = ScreenHeight
			Me.ImageHeight = ImageHeight
			Me.ImageWidth = ImageWidth
		End Sub

		Function GetBitmap() As Bitmap
			Dim Shot As New WebPageBitmap(Me.Url, Me.ScreenWidth, Me.ScreenHeight)
			Shot.GetIt()
			Dim Pic As Bitmap = Shot.DrawBitmap(Me.ImageHeight, Me.ImageWidth)
			Return Pic
		End Function
	End Class

	Class WebPageBitmap
		Dim MyBrowser As WebBrowser
		Dim URL As String
		Dim Height As Integer
		Dim Width As Integer

		Sub New(ByVal url As String, ByVal width As Integer, ByVal height As Integer)
			Me.Height = Height
			Me.Width = width
			Me.URL = url
			MyBrowser = New WebBrowser
			MyBrowser.ScrollBarsEnabled = False
			MyBrowser.Size = New Size(Me.Width, Me.Height)
		End Sub

		Sub GetIt()
			MyBrowser.Navigate(Me.URL)
			While MyBrowser.ReadyState <> WebBrowserReadyState.Complete
				Application.DoEvents()
			End While
		End Sub

#Region " DrawBitmap "
		Function DrawBitmap(ByVal theight As Integer, ByVal twidth As Integer) As Bitmap
			Dim myBitmap As New Bitmap(Width, Height)
			Dim DrawRect As New Rectangle(0, 0, Width, Height)
			MyBrowser.DrawToBitmap(myBitmap, DrawRect)
			Dim imgOutput As System.Drawing.Image = myBitmap
			Dim oThumbNail As System.Drawing.Image = New Bitmap(twidth, theight, imgOutput.PixelFormat)
			Dim g As Graphics = Graphics.FromImage(oThumbNail)
			g.CompositingQuality = Drawing2D.CompositingQuality.HighSpeed
			g.SmoothingMode = Drawing2D.SmoothingMode.HighSpeed
			g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBilinear
			Dim oRectangle As Rectangle = New Rectangle(0, 0, twidth, theight)
			g.DrawImage(imgOutput, oRectangle)
			Try
				Return oThumbNail
			Catch ex As Exception
				Throw ex
			Finally
				imgOutput.Dispose()
				imgOutput = Nothing
				MyBrowser.Dispose()
				MyBrowser = Nothing
			End Try
		End Function
#End Region

	End Class

End Namespace