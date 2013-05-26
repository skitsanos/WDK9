Public Class EdxFiles
    Inherits CollectionBase

#Region " Properties "
    Private _UseCompression As Boolean = False
    Public Property UseCompression() As Boolean
        Get
            Return _UseCompression
        End Get
        Set(ByVal value As Boolean)
            _UseCompression = value
        End Set
    End Property
#End Region

#Region " Item "
    Default Public ReadOnly Property Item(ByVal Index As Integer) As EdxFile
        Get
            Return CType(MyBase.List.Item(Index), EdxFile)
        End Get
    End Property
#End Region

#Region " Add "
    Public Sub Add(ByVal Tag As String, ByVal Path As String)
        Dim File As New EdxFile
        File.Tag = Tag
        File.File = Path
        File.Content = ReadFileToBase64(Path)

        List.Add(File)
    End Sub
#End Region

#Region " Remove "
    Public Sub Remove(ByVal Tag As String)
        For Each file As EdxFile In MyBase.List
            If file.Tag = Tag Then
                MyBase.List.Remove(Tag)
            End If
        Next
    End Sub

    Public Sub Remove(ByVal File As EdxFile)
        MyBase.List.Remove(File)
    End Sub
#End Region

#Region " ReadFiletoBase64 "
    Private Function ReadFileToBase64(ByVal FileName As String) As String
        Try
            If IO.File.Exists(FileName) Then
                Dim myStream As IO.Stream = IO.File.Open(FileName, IO.FileMode.Open)
                Dim myBuffer(myStream.Length) As Byte

                Dim myFileArray As Integer = myStream.Read(myBuffer, 0, myStream.Length)
                myStream.Close()
                myStream.Dispose()

                Return System.Convert.ToBase64String(myBuffer)
            Else
                Return ""
            End If
        Catch ex As Exception
            Return ""
        End Try
    End Function
#End Region

End Class
