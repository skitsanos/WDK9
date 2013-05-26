Imports System.Web
Imports System.Drawing

Imports System.Configuration
Imports System.Web.Configuration


    Public Class Watermarking
        Implements IHttpHandler

#Region " Properties "
        Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
            Get
                Return False
            End Get
        End Property
#End Region

#Region " ProcessRequest "
        Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
            Dim output As ImageWatermark = New ImageWatermark(context.Request.PhysicalPath)
            Dim conf As Configuration = WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath)

            If conf.AppSettings.Settings("WatermarkText").Value = "" Then
                output.AddWaterMark("Copyrighted Image")
            Else
                output.AddWaterMark(conf.AppSettings.Settings("WatermarkText").Value)
            End If
            output.Image.Save(context.Response.OutputStream, Drawing.Imaging.ImageFormat.Jpeg)
        End Sub
#End Region

#Region " ImageWatermark "
        Private Class ImageWatermark
            Private bmp As Bitmap

            Public Sub New(ByVal physicalPathToImage As String)
                bmp = New Bitmap(physicalPathToImage)
            End Sub

            Public Sub AddWaterMark(ByVal watermark As String)
                'get the drawing canvas (graphics object) from the bitmap
                Dim canvas As Graphics
                Try
                    canvas = Graphics.FromImage(bmp)
                Catch e As Exception
                    'You cannot create a Graphics object 
                    'from an image with an indexed pixel format.
                    'If you want to open this image and draw 
                    'on it you need to do the following...
                    'size the new bitmap to the source bitmaps dimensions
                    Dim bmpNew As Bitmap = New Bitmap(bmp.Width, bmp.Height)
                    canvas = Graphics.FromImage(bmpNew)
                    'draw the old bitmaps contents to the new bitmap
                    'paint the entire region of the old bitmap to the 
                    'new bitmap..use the rectangle type to 
                    'select area of the source image
                    canvas.DrawImage(bmp, New Rectangle(0, 0, bmpNew.Width, bmpNew.Height), 0, 0, bmp.Width, bmp.Height, GraphicsUnit.Pixel)
                    bmp = bmpNew
                End Try
                canvas.DrawString(watermark, New Font("Verdana", 14, FontStyle.Bold), New SolidBrush(Color.Beige), 0, 0)
            End Sub

            Public ReadOnly Property Image() As Bitmap
                Get
                    Return bmp
                End Get
            End Property
        End Class
#End Region

    End Class
