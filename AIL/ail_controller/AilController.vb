Imports System
Imports System.ServiceProcess

Public Class AilController

#Region " Properties "
    Private ServiceName As String = "AIL Server"
    Private mySC As ServiceController
#End Region

#Region " Start "
    Public Sub Start()
        mySC = New ServiceController(ServiceName)
        mySC.Start()
    End Sub
#End Region

#Region " Stop "
    Public Sub [Stop]()
        mySC = New ServiceController(ServiceName)
        mySC.Stop()
    End Sub
#End Region

#Region " Status "
    Public Function Status() As String
        mySC = New ServiceController(ServiceName)

        Dim st As String = ""

        Try
            st = mySC.Status.ToString

        Catch ex As Exception
            Return "Service not found. It is probably not installed." ' [exception=" & ex.Message & "]")
        End Try

        Return st
    End Function
#End Region

End Class
