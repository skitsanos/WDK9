Public Class InstanceController
    Inherits CollectionBase

    Default ReadOnly Property Item(ByVal name As String) As Instance
        Get
            Dim ret As Instance = Nothing
            For Each obj As Instance In List
                If obj.Name = name Then
                    ret = obj
                    Exit For
                End If
            Next
            Return ret
        End Get
    End Property

    Public Sub Add(ByVal name As String, ByVal Service As Object)
        Dim ins As New Instance
        ins.Name = name
        ins.Service = Service
        List.Add(ins)
    End Sub

    Public Sub RemoveAll()
        For Each obj As Instance In List
            Log.WriteEntry("Shutting down " + obj.Name + "...")
            obj.Service.stop()
            Log.WriteEntry(obj.Name + " removed from AIL InstanceController")
        Next

        List.Clear()

    End Sub

End Class
