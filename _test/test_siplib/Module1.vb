'http://www.sipcenter.com/sip.nsf/html/Developers+Toolkit

Imports AstaSipCSharp

Module Module1
    Private ep As New SIPEndPoint

    Sub Main()
      
        'ep.OnRegisterFailed = New SIPEndPoint.OnRegisterFailedEvent(AddressOf OnRegisterFailed)
        'ep.OnRegisterSuccess = New SIPEndPoint.OnRegisterSuccessEvent(AddressOf OnRegisterSuccess)
        'ep.Contact = "sip:1834464@inphonex.com"
        'ep.Password = "shemesh"
        'ep.ProxyHost = "inphonex.com"
        'ep.ProxyPort = 5060
        'ep.Register()

        Console.ReadKey()
    End Sub

    Public Sub OnRegisterSuccess(ByVal contact As String, ByVal expires As Integer)

    End Sub


    Public Sub OnRegisterFailed(ByVal responseCode As Integer, ByVal contact As Integer)
        Debug.WriteLine(responseCode)
    End Sub

End Module
