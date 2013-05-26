Imports System.ComponentModel
Imports System.Web.UI

    <DefaultProperty("Source"), ToolboxData("<{0}:NetTumbnailer runat=""server""></{0}:NetTumbnailer>")> Public Class NetTumbnailer
        Inherits System.Web.UI.WebControls.WebControl

#Region " Properties "
    Private _Text As String = ""
    Private _Width As Integer = 160
    Private _Height As Integer = 120
    Private _ProportionalHeight As Boolean = False
    Private _Source As String = ""
    Private _stream As IO.Stream

    <Bindable(True), Category("Appearance"), DefaultValue("")> Property ProportionalHeight() As Boolean
        Get
            Return _ProportionalHeight
        End Get

        Set(ByVal Value As Boolean)
            _ProportionalHeight = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue("120")> Property ImageHeight() As Integer
        Get
            Return _Height
        End Get
        Set(ByVal Value As Integer)
            _Height = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue("160")> Property ImageWidth() As Integer
        Get
            Return _Width
        End Get
        Set(ByVal Value As Integer)
            _Width = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue("")> Property Source() As String
        Get
            Return _Source
        End Get

        Set(ByVal Value As String)
            _Source = Value
        End Set
    End Property

    <Bindable(True), Category("Appearance"), DefaultValue("")> Property Stream() As IO.Stream
        Get
            Return _stream
        End Get
        Set(ByVal Value As IO.Stream)
            _stream = Value
        End Set
    End Property

#End Region

#Region " Render "
        Protected Overrides Sub Render(ByVal output As System.Web.UI.HtmlTextWriter)
            DrawImage()
        End Sub
#End Region

#Region " Thumbnail "
    Function Thumbnail() As System.Drawing.Image
        Dim OriginalImg, Thumb As System.Drawing.Image
        Dim inp As New IntPtr

        If ImageWidth = 0 Then ImageWidth = 160
        If ImageHeight = 0 Then ImageHeight = 120

        Try
            If Source <> "" Then
                Dim httpSocket As New Net.WebClient
                httpSocket.Headers.Add("pragma", "no-cache")
                httpSocket.Headers.Add("cache-control", "private")

                Dim sr As New IO.StreamReader(httpSocket.OpenRead(Source))
                Dim imageStream As IO.Stream = sr.BaseStream
                OriginalImg = Drawing.Image.FromStream(imageStream)
            Else
                OriginalImg = GenerateErrorBitmap("[Wrong Source]")
            End If

            ' Get width using QueryString.
            If ImageWidth = 0 Then
                ImageWidth = OriginalImg.Width  ' Use original Width. 
            End If

            ' Get height using QueryString.
            If ImageHeight = 0 Then
                ImageHeight = OriginalImg.Height ' Use original Height.
            Else
                If ProportionalHeight = True Then ImageHeight = OriginalImg.Height / (OriginalImg.Width / ImageWidth)
            End If

            Thumb = OriginalImg.GetThumbnailImage(ImageWidth, ImageHeight, Nothing, inp)

            Dim aGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(Thumb)

            OriginalImg.Dispose()
            Return Thumb
            Thumb.Dispose()

		Catch e As Exception
			Log(e.ToString + "Downloading from " + Source)

			Return GenerateErrorBitmap("[error]")
        End Try
    End Function
#End Region

#Region " Thumbnail {from stream}"
    Function Thumbnail(ByVal ContentStream As IO.Stream) As System.Drawing.Image
        Dim OriginalImg, Thumb As System.Drawing.Image
        Dim inp As New IntPtr

        If ImageWidth = 0 Then ImageWidth = 160
        If ImageHeight = 0 Then ImageHeight = 120

        Try
            If ContentStream IsNot Nothing Then
                OriginalImg = Drawing.Image.FromStream(ContentStream)
            Else
                OriginalImg = GenerateErrorBitmap("[Wrong Source]")
            End If

            ' Get width using QueryString.
            If ImageWidth = 0 Then
                ImageWidth = OriginalImg.Width  ' Use original Width. 
            End If

            ' Get height using QueryString.
            If ImageHeight = 0 Then
                ImageHeight = OriginalImg.Height ' Use original Height.
            Else
                If ProportionalHeight = True Then ImageHeight = OriginalImg.Height / (OriginalImg.Width / ImageWidth)
            End If

            Thumb = OriginalImg.GetThumbnailImage(ImageWidth, ImageHeight, Nothing, inp)

            Dim aGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(Thumb)

            OriginalImg.Dispose()
            Return Thumb
            Thumb.Dispose()

		Catch e As Exception
			Log(e.ToString + "Downloading from " + Source)

			Return GenerateErrorBitmap("[error]")
        End Try
    End Function
#End Region

#Region " DrawImage "
    Public Sub DrawImage()
        Dim img As Drawing.Image = Thumbnail()
        img.Save(Stream, System.Drawing.Imaging.ImageFormat.Jpeg)
        img.Dispose()
    End Sub

    Public Sub DrawImage(ByVal ContentStream As IO.Stream)
        Dim img As Drawing.Image = Thumbnail(ContentStream)
        img.Save(Stream, System.Drawing.Imaging.ImageFormat.Jpeg)
        img.Dispose()
    End Sub
#End Region

#Region " GenerateErrorBitmap "
    Private Function GenerateErrorBitmap(ByVal ErrorText As String) As Drawing.Image
        Dim b As New System.Drawing.Bitmap(ImageWidth, ImageHeight, Drawing.Imaging.PixelFormat.Format24bppRgb)
        Dim hBitmap As System.IntPtr = b.GetHbitmap
        Dim img As Drawing.Image = Drawing.Image.FromHbitmap(hBitmap)

        Dim aFont As System.Drawing.Font = New System.Drawing.Font("Verdana", 10, System.Drawing.GraphicsUnit.Point)
        Dim aGraphic As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(img)
        aGraphic.Clear(Drawing.Color.White)
        aGraphic.SmoothingMode = Drawing.Drawing2D.SmoothingMode.HighQuality
        aGraphic.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault
        aGraphic.DrawString(ErrorText, aFont, New System.Drawing.SolidBrush(System.Drawing.Color.Gray), 2, 2)

        b.Dispose()

        Return img
    End Function
#End Region

	'#Region " Helpers "
	'    <Conditional("DEBUG")> Private Sub Log(ByVal Text As String)
	'        Dim AppPath As String = AppDomain.CurrentDomain.BaseDirectory
	'        Dim strW As New IO.StreamWriter(AppPath & "error.log", True)
	'        strW.Write(Text & vbCrLf & vbCrLf)
	'        strW.Close()
	'        strW = Nothing
	'    End Sub
	'#End Region

End Class