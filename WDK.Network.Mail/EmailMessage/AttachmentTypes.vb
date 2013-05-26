Namespace Mail
    Namespace AttachmentTypes
        Public NotInheritable Class Audio
            Private Sub New()
            End Sub
            Private Shared audio As String = "audio/"
            Public Shared MP3 As String = audio + "mp3"
            Public Shared WMA As String = audio + "wma"
            Public Shared WAV As String = audio + "wav"
            Public Shared OGG As String = audio + "ogg"
            Public Shared ACC As String = audio + "acc"
        End Class
        Public NotInheritable Class Video
            Private Sub New()
            End Sub
            Private Shared video As String = "video/"
            Public Shared MPEG As String = video + "mpeg"
            Public Shared AVI As String = video + "avi"
            Public Shared WMV As String = video + "wmv"
        End Class
        Public NotInheritable Class Image
            Private Sub New()
            End Sub
            Private Shared image As String = "image/"
            Public Shared JPG As String = image + "jpeg"
            Public Shared GIF As String = image + "gif"
            Public Shared TIFF As String = image + "tiff"
            Public Shared BMP As String = image + "bmp"
        End Class
        Public NotInheritable Class Text
            Private Sub New()
            End Sub
            Private Shared text As String = "text/"
            Public Shared PLAIN As String = text + "palin"
            Public Shared XML As String = text + "xml"
            Public Shared HTML As String = text + "html"
            Public Shared RICHTEXT As String = text + "richtext"
        End Class
        Public NotInheritable Class Application
            Private Sub New()
            End Sub
            Private Shared application As String = "application/"
            Public Shared ZIP As String = application + "zip"
            Public Shared PDF As String = application + "pdf"
            Public Shared UNKNOWN As String = application + "octet-stream"
        End Class
    End Namespace
End Namespace
