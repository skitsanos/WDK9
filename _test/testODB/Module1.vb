Imports Neodatis.Odb.Main

Public Class sampleObject
    Public title As String = "untitled"
End Class

Module Module1

    Sub Main()
        Dim db As ODB = ODBFactory.Open("somefile.odb", "demo", "demo")
        db.store(New sampleObject)
    End Sub

End Module
