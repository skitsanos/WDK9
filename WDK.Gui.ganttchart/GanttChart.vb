Imports System.ComponentModel
Imports System.Web.UI
Imports System.IO
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging

<Assembly: TagPrefix("WDK.GUI", "wdk")> 

<DefaultProperty("Text"), ToolboxData("<{0}:GanttChart runat=""server""></{0}:GanttChart>"), ParseChildren(True, "ChartItems")> Public Class GanttChart
    Inherits System.Web.UI.WebControls.WebControl

#Region " Properties "
    Private _Items As GanttChartItemCollection

    <Category("Chart Common"), DesignerSerializationVisibility(DesignerSerializationVisibility.Content), PersistenceMode(PersistenceMode.InnerDefaultProperty), Description("Supply data values for selected chart.")> _
 Public ReadOnly Property ChartItems() As GanttChartItemCollection
        Get
            If _Items Is Nothing Then
                _Items = New GanttChartItemCollection(Me)
            End If
            Return _Items
        End Get
    End Property

    Private _ImageFormat As ChartFormat
    <DefaultValue(GetType(ChartFormat), "Gif"), Category("Chart Common"), Description("Compression format for image.")> _
  Public Property Format() As ChartFormat
        Get
            Return _ImageFormat
        End Get
        Set(ByVal Value As ChartFormat)
            _ImageFormat = Value
        End Set
    End Property

    Private _Copyright As String = "© 2005, WDK/GanttChart"
	<Category("Chart Common"), DefaultValue("© 2005, WDK/GanttChart"), Description("Copyright displayed on top of chart.")> Public Property Copyright() As String
		Get
			Return _Copyright
		End Get
		Set(ByVal Value As String)
			_Copyright = Value
		End Set
	End Property

    <Browsable(False)> _
   Public ReadOnly Property HtmlContentType() As String
        Get
            Return "image/" & ChartFormat.GetName(GetType(ChartFormat), Me.Format).ToString.ToLower
        End Get
    End Property

    <Browsable(False)> _
   Public ReadOnly Property ImageData() As Byte()
        Get
			Dim imgStream As MemoryStream = MakeChart()
            Dim img(imgStream.Length) As Byte
            imgStream.Read(img, 0, imgStream.Length)
            Return img
        End Get
    End Property
#End Region

#Region " Enum: ChartFormat "
    Public Enum ChartFormat
        Gif
        Jpeg
        Png
        Bmp
    End Enum
#End Region

#Region " Render() "
    Protected Overrides Sub Render(ByVal output As System.Web.UI.HtmlTextWriter)
        If Not Site Is Nothing AndAlso Site.DesignMode Then
            output.Write("<font size=""2"" color=""navy"" face=""arial"">[Gantt Chart: """ & Me.ID & """ ]</font><br>")
            output.Write("<font size=""1""  color=""SeaGreen"" face=""arial"">&nbsp;Gantt Chart not visible in VS.NET designer!</font><br>")
        Else
            Page.Response.Clear()
            Page.Response.ContentType = HtmlContentType
            Dim memStream As System.IO.MemoryStream = MakeChart()
            memStream.WriteTo(Page.Response.OutputStream)
            memStream.Close()
            Page.Response.End()
        End If
    End Sub
#End Region

#Region " MakeChart() "
    Private Function MakeChart() As MemoryStream
        Dim tmpGraphics As Graphics = Graphics.FromImage(New Bitmap(1, 1))
        Dim rowsHeight As Integer = (tmpGraphics.MeasureString("0", New Font(Me.Font.Name, 7)).Height + 4) * (ChartItems.Count + 1)
        Dim colsWidth As Integer = (tmpGraphics.MeasureString("30", New Font(Me.Font.Name, 7)).Width + 4) * 31

        'Declare object variables
        Dim objBitMap As New Bitmap(colsWidth + 8, rowsHeight + 20)
        Dim objGraphics As Graphics

        objGraphics = Graphics.FromImage(objBitMap)
        objGraphics.Clear(Me.BackColor)
        objGraphics.SmoothingMode = SmoothingMode.AntiAlias
        objGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAliasGridFit

        Dim fBrush As New SolidBrush(Me.ForeColor)
        Dim oPen As New Pen(getDarkColor(Me.BackColor, 55))

        'Dim gPen As New Pen(Color.Gainsboro)
        Dim colSize As Integer = tmpGraphics.MeasureString("30", New Font(Me.Font.Name, 7)).Width + 4
        Dim rowSize As Integer = tmpGraphics.MeasureString("0", New Font(Me.Font.Name, 7)).Height + 4

        Dim i As Integer = 0
        Dim rPrimary As New Rectangle(1, 0, 1, CInt(rowSize) + 1)

        Dim x As Integer = 0
        For i = 1 To 31
            objGraphics.DrawRectangle(New Pen(getDarkColor(Me.BackColor, 55)), i * colSize - colSize, 1, colSize, rowSize)
            objGraphics.FillRectangle(New SolidBrush(Color.White), i * colSize - colSize, 1, colSize, rowSize)
            Dim shift As Integer = 2
            If i < 10 Then shift = 5
            objGraphics.DrawString(i, New Font(Me.Font.Name, 7), fBrush, i * colSize - colSize + shift, 2)

            For x = 1 To ChartItems.Count
				Dim cellBrush As Brush = New SolidBrush(getLightColor(Me.BackColor, 1))
				'New SolidBrush(getLightColor(Me.BackColor, 1))
                'New LinearGradientBrush(rPrimary, getLightColor(Me.BackColor, 55), getDarkColor(Me.BackColor, 55), LinearGradientMode.Vertical)

				objGraphics.FillRectangle(cellBrush, i * colSize - colSize, x * rowSize + 3, colSize, rowSize)
                objGraphics.DrawRectangle(oPen, i * colSize - colSize, x * rowSize + 3, colSize, rowSize)
            Next
        Next

        Dim brushTasks As Brush = New SolidBrush(getDarkColor(Me.BackColor, 55))
        If Format <> ChartFormat.Gif Then
            'brushTasks = New LinearGradientBrush(rPrimary, getLightColor(Color.SteelBlue, 55), getDarkColor(Color.SteelBlue, 55), LinearGradientMode.Vertical)
        End If

        For i = 1 To 31
            For x = 1 To ChartItems.Count
                Dim numDates As Integer = Day(ChartItems(x - 1).EndDate) - Day(ChartItems(x - 1).StartDate)
                If numDates = 0 Then numDates = 1
                If Day(ChartItems(x - 1).StartDate) = i Then
                    objGraphics.FillRectangle(brushTasks, i * colSize - colSize + 1, x * rowSize + 4, numDates * colSize - 2, rowSize - 2)
					'objGraphics.DrawString(ChartItems(x - 1).Task, New Font(Me.Font.Name, 7), New SolidBrush(Color.White), 5, x * rowSize + 6)
					objGraphics.DrawString(ChartItems(x - 1).Task, New Font(Me.Font.Name, 8), fBrush, 5, x * rowSize + 5)
                End If
            Next
        Next

        'Draw Copyright info
        objGraphics.DrawString(Copyright, New Font("Verdana", 7), Brushes.Silver, 10, rowsHeight + 5)

        Dim memStream As New MemoryStream
        objBitMap.Save(memStream, ImageFormat(Format))
        objBitMap.Dispose()
        objGraphics.Dispose()

        Return memStream
    End Function

#End Region

#Region " ImageFormat() "
    Private Function ImageFormat(ByVal enumFormat As ChartFormat) As ImageFormat
        Dim imgFormat As ImageFormat
        Select Case enumFormat
            Case ChartFormat.Bmp
                imgFormat = Imaging.ImageFormat.Bmp
            Case ChartFormat.Gif
                imgFormat = Imaging.ImageFormat.Gif
            Case ChartFormat.Jpeg
                imgFormat = Imaging.ImageFormat.Jpeg
            Case ChartFormat.Png
                imgFormat = Imaging.ImageFormat.Png
            Case Else
                imgFormat = Imaging.ImageFormat.Gif
        End Select
        Return imgFormat
    End Function
#End Region

#Region " Service functions "
    Private Function getDarkColor(ByVal c As Color, ByVal d As Byte) As Color
        Dim r As Byte = 0
        Dim g As Byte = 0
        Dim b As Byte = 0

        If (c.R > d) Then r = (c.R - d)
        If (c.G > d) Then g = (c.G - d)
        If (c.B > d) Then b = (c.B - d)

        Dim c1 As Color = Color.FromArgb(r, g, b)
        Return c1
    End Function

    Private Function getLightColor(ByVal c As Color, ByVal d As Byte) As Color
        Dim r As Byte = 255
        Dim g As Byte = 255
        Dim b As Byte = 255

        If (CInt(c.R) + CInt(d) <= 255) Then r = (c.R + d)
        If (CInt(c.G) + CInt(d) <= 255) Then g = (c.G + d)
        If (CInt(c.B) + CInt(d) <= 255) Then b = (c.B + d)

        Dim c2 As Color = Color.FromArgb(r, g, b)
        Return c2
    End Function
#End Region
End Class
