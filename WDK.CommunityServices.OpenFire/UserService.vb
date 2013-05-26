Imports System.Xml

Public Class UserServiceOperationResult
    Public status As String
    Public code As String
    Public message As String
End Class

Public Class UserService

#Region " Notes "
    'The following parameters can be passed into the request:

    'Name	    Description
    'type	    Required	The admin service required. Possible values are add, delete, update
    'secret	    Required 	The secret key that allows access to the User Service.
    'username	Required 	The username of the user to add, update or delete. ie the part before the @ symbol.
    'password	Required    for add operation 	The password of the new user or the user being updated.
    'name	    Optional 	The display name of the new user or the user being updated.
    'email	    Optional 	The email address of the new user or the user being updated.
    'groups	    Optional 	List of groups where the user is a member. Values are comma delimited.
#End Region

#Region " Properties "
    Private _secret As String
    Public Property secret() As String
        Get
            Return _secret
        End Get
        Set(ByVal value As String)
            _secret = value
        End Set
    End Property

    Private _url As String
    Public Property url() As String
        Get
            Return _url
        End Get
        Set(ByVal value As String)
            _url = value
        End Set
    End Property
#End Region

#Region " createUser "
    'http://example.com:9090/plugins/userService/userservice?type=add&secret=bigsecret&username=kafka&password=drowssap&name=franz&email=franz@kafka.com

    Public Function createUser(ByVal username As String, ByVal password As String, ByVal name As String, ByVal email As String) As UserServiceOperationResult
        Dim ret As New UserServiceOperationResult

        Dim content As String = Utils.getData(url + "/plugins/userService/userservice?type=add&secret=" + secret + "&username=" + username + "&password=" + password + "&name=" + name + "&email=" + email)
        Dim doc As New XmlDocument
        doc.LoadXml(content)

        Select Case doc.DocumentElement.LocalName.ToLower
            Case "result"
                ret.status = "ok"
                ret.code = "ok"
                ret.message = doc.DocumentElement.InnerText

            Case Else
                ret.code = "error"
                ret.message = reportError(doc.DocumentElement.InnerText)
                ret.code = doc.DocumentElement.InnerText

        End Select

        Return ret
    End Function
#End Region

#Region " deleteUser "
    'http://example.com:9090/plugins/userService/userservice?type=delete&secret=bigsecret&username=kafka
    Public Function deleteUser(ByVal username As String) As UserServiceOperationResult
        Dim ret As New UserServiceOperationResult

        Dim content As String = Utils.getData(url + "/plugins/userService/userservice?type=delete&secret=" + secret + "&username=" + username)
        Dim doc As New XmlDocument
        doc.LoadXml(content)

        Select Case doc.DocumentElement.LocalName.ToLower
            Case "result"
                ret.status = "ok"
                ret.code = "ok"
                ret.message = doc.DocumentElement.InnerText

            Case Else
                ret.code = "error"
                ret.message = reportError(doc.DocumentElement.InnerText)
                ret.code = doc.DocumentElement.InnerText

        End Select
        Return ret
    End Function
#End Region

#Region " updateUser "
    'http://example.com:9090/plugins/userService/userservice?type=update&secret=bigsecret&username=kafka&password=drowssap&name=franz&email=beetle@kafka.com
    Public Function updateUser(ByVal username As String, ByVal password As String, ByVal name As String, ByVal email As String) As UserServiceOperationResult
        Dim ret As New UserServiceOperationResult

        Dim content As String = Utils.getData(url + "/plugins/userService/userservice?type=update&secret=" + secret + "&username=" + username + "&password=" + password + "&name=" + name + "&email=" + email)
        Dim doc As New XmlDocument
        doc.LoadXml(content)

        Select Case doc.DocumentElement.LocalName.ToLower
            Case "result"
                ret.status = "ok"
                ret.code = "ok"
                ret.message = doc.DocumentElement.InnerText

            Case Else
                ret.code = "error"
                ret.message = reportError(doc.DocumentElement.InnerText)
                ret.code = doc.DocumentElement.InnerText

        End Select

        Return ret
    End Function
#End Region

#Region " status "
    '<presence type="unavailable" from="admin@grafeiolive.com">
    ' <status>Unavailable</status>
    '</presence>

    'http://grafeiolive.com:9090/plugins/presence/status?jid=admin@grafeiolive.com
    Public Function status(ByVal username As String) As UserServiceOperationResult
        Dim ret As New UserServiceOperationResult

        Dim content As String = Utils.getData(url + "/plugins/presence/status?type=xml&jid=" + username)
        Dim doc As New XmlDocument
        doc.LoadXml(content)

        ret.code = "ok"
        ret.status = doc.DocumentElement.GetAttribute("type")
        ret.message = doc.DocumentElement.FirstChild.InnerText

        Return ret
    End Function
#End Region

#Region " search "
    'http://www.packtpub.com/article/openfire-effectively-managing-users

#End Region

#Region " report error "
    Private Function reportError(ByVal data As String) As String
        Dim msg As String = ""

        Select Case data
            Case "IllegalArgumentException"
                msg = "One of the parameters passed in to the User Service was bad."

            Case "UserNotFoundException"
                msg = "No user of the name specified, for a delete or update operation, exists on this server."

            Case "UserAlreadyExistsException"
                msg = "A user with the same name as the user about to be added, already exists."

            Case "RequestNotAuthorised"
                msg = "The supplied secret does not match the secret specified in the Admin Console or the requester is not a valid IP address."

            Case "UserServiceDisabled"
                msg = "The User Service is currently set to disabled in the Admin Console."

        End Select

        Return msg
    End Function
#End Region

End Class
