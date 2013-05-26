Imports Avanticore

Module Module1

    Sub Main()
        Dim fs As New Fsif("resources", FsifDll.DatabaseMode.FS_RW)
        fs.FileCreate("/Details/hotels.xml")
        fs.Close()

    End Sub

End Module
