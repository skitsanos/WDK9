Imports System.Threading

Public Class Encoder

    Private _PathToFFMpeg As String
    Public Property PathToFFMpeg() As String
        Get
            Return _PathToFFMpeg
        End Get
        Set(ByVal value As String)
            _PathToFFMpeg = value
        End Set
    End Property

    Public Sub Encode(ByVal source As String)
        Try
            Dim winTask As New Thread(AddressOf _Encode)
            winTask.IsBackground = True
            winTask.Name = source
            winTask.Start()

        Catch ex As Exception
            Log(ex.ToString, True)
        End Try
    End Sub

    Private Sub _Encode()        
        Dim ffmpeg As New FFMpegScout.CFFMPegScout
        ffmpeg.PathToFFMpeg = PathToFFMpeg
        ffmpeg.InputFileName = Thread.CurrentThread.Name
        ffmpeg.OutputFileName = IO.Path.GetDirectoryName(Thread.CurrentThread.Name) + "\" + IO.Path.GetFileNameWithoutExtension(Thread.CurrentThread.Name) + ".flv" ' target
        ffmpeg.Execute()
        ffmpeg = Nothing
    End Sub
End Class
