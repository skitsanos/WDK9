Imports System
Imports System.Net
Imports System.Net.Sockets
Imports System.Text


#Region " StateObject "
Public Class StateObject
    Public workSocket As Socket = Nothing
    Public BufferSize As Integer = 32767
    Public buffer(32767) As Byte
    Public sb As New StringBuilder()
End Class
#End Region


Public Class SocketsClient
    Public Event onConnect()
    Public Event onError(ByVal Description As String)
    Public Event onDataArrival(ByVal Data As Byte())
    Public Event onDisconnect()
    Public Event onSendComplete(ByVal DataSize As Integer)

    Private Shared response As String = ""
    Private Shared port As Integer = 6667
    Private Shared ipHostInfo As IPHostEntry = Dns.GetHostEntry("localhost")
    Private Shared ipAddress As IPAddress = ipHostInfo.AddressList(0)
    Private Shared client As New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)

    Public Sub Connect(ByVal RemoteHostName As String, ByVal RemotePort As Integer)
        Try
            client = New Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            port = RemotePort
            ipHostInfo = Dns.GetHostEntry(RemoteHostName)
            ipAddress = ipHostInfo.AddressList(0)
            Dim remoteEP As New IPEndPoint(ipAddress, port)
            client.BeginConnect(remoteEP, AddressOf sockConnected, client)

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            RaiseEvent onError(ex.Message)
        End Try
    End Sub

    Public Sub SendData(ByVal Data() As Byte)
        Try
            Dim byteData As Byte() = Data
            client.BeginSend(byteData, 0, byteData.Length, 0, AddressOf sockSendEnd, client)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            RaiseEvent onError(ex.Message)
        End Try
    End Sub

    Public Sub Disconnect()
        Try
            client.Shutdown(SocketShutdown.Both)
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            RaiseEvent onError(ex.Message)
        End Try

        client.Close()
    End Sub

    Public Function StringToBytes(ByVal Data As String) As Byte()
        StringToBytes = System.Text.ASCIIEncoding.ASCII.GetBytes(Data)
    End Function

    Public Function BytestoString(ByVal Data As Byte()) As String
        BytestoString = System.Text.ASCIIEncoding.ASCII.GetString(Data)
    End Function

    Private Sub sockConnected(ByVal ar As IAsyncResult)
        Try
            If client.Connected = False Then RaiseEvent onError("Connection refused.") : Exit Sub
            Dim state As New StateObject()
            state.workSocket = client
            client.BeginReceive(state.buffer, 0, state.BufferSize, 0, AddressOf sockDataArrival, state)
            RaiseEvent onConnect()
        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            RaiseEvent onError(ex.Message)
        End Try
    End Sub

    Private Sub sockDataArrival(ByVal ar As IAsyncResult)
        Dim state As StateObject = CType(ar.AsyncState, StateObject)
        Dim client As Socket = state.workSocket
        Dim bytesRead As Integer

        Try
            bytesRead = client.EndReceive(ar)
        Catch
            Exit Sub
        End Try

        'Try
        Dim Data() As Byte = state.buffer

        If bytesRead = 0 Then
            client.Shutdown(SocketShutdown.Both)
            client.Close()
            RaiseEvent onDisconnect()
            Exit Sub
        End If

        ReDim state.buffer(32767)

        client.BeginReceive(state.buffer, 0, state.BufferSize, 0, AddressOf sockDataArrival, state)

        RaiseEvent onDataArrival(Data)

        'Catch ex As Exception
        '    Debug.WriteLine(ex.ToString)
        '    RaiseEvent onError(ex.Message)
        'End Try
    End Sub

    Private Sub sockSendEnd(ByVal ar As IAsyncResult)
        Try
            Dim client As Socket = CType(ar.AsyncState, Socket)
            Dim bytesSent As Integer = client.EndSend(ar)
            RaiseEvent onSendComplete(bytesSent)

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            RaiseEvent onError(ex.Message)
        End Try
    End Sub

    Public Function Connected() As Boolean
        Try
            Return client.Connected

        Catch ex As Exception
            Debug.WriteLine(ex.ToString)
            RaiseEvent onError(ex.Message)
        End Try
    End Function
End Class