Public Class EdxClient

#Region " Properties "
    Private _Url As String = ""
    Public Property Url() As String
        Get
            Return _Url
        End Get
        Set(ByVal value As String)
            _Url = value
        End Set
    End Property

    Private _Username As String = ""
    Public Property Username() As String
        Get
            Return _Username
        End Get
        Set(ByVal value As String)
            _Username = value
        End Set
    End Property

    Private _Password As String = ""
    Public Property Password() As String
        Get
            Return _Password
        End Get
        Set(ByVal value As String)
            _Password = value
        End Set
    End Property

    Private _UseAuthentication As Boolean = True

    'Public Headers As New EdxHeaders
    Public Request As New EdxRequest
    Public Response As New EdxResponse

    'Private xmlDoc As Xml.XmlDocument
    Private edx As Xml.XmlElement

#End Region

#Region " New "
    Sub New()
        Request.Headers = New EdxHeaders
        Request.Document = New Xml.XmlDocument
        Request.Attachments = New EdxFiles

        edx = Request.Document.CreateElement("EdxMessage")
        Request.Document.AppendChild(edx)
    End Sub
#End Region

#Region " Send "
    Public Function Send() As Boolean
        If Url.Trim.Length = 0 Then
            Response.Status = "500"
            Response.Message = "Url can not be empty"
            Return False
        End If

        Try
            Dim xmlSocket As New Net.WebClient()
            Dim ByteArray As Byte() = _
             xmlSocket.UploadData(Url, System.Text.Encoding.ASCII.GetBytes(Request.Document.OuterXml.ToCharArray))

            Dim resp As String = System.Text.Encoding.ASCII.GetString(ByteArray)

            Debug.WriteLine(resp)

            If InStrRev(resp, "<Response") = 0 Then
                Response.Status = "500"
                Response.Message = "Server did not returned a proper Edx Response"
            Else
                Dim xmlResp As New Xml.XmlDocument
                xmlResp.LoadXml(resp)

                Response.Status = xmlResp.SelectSingleNode("/Response").Attributes("status").Value
                Response.Message = xmlResp.SelectSingleNode("/Response").Attributes("message").Value

            End If

            Return False
        Catch ex As Exception
            Response.Status = "500"
            Response.Message = ex.Message
            Return False
        End Try

    End Function
#End Region

#Region " Build "
    Public Sub Build()
        Dim auth As Xml.XmlElement = Request.Document.CreateElement("Auth")
        edx.AppendChild(auth)

        auth.SetAttribute("Username", Username)
        auth.SetAttribute("Password", Password)

        edx.SetAttribute("id", Guid.NewGuid.ToString & "@" & My.Computer.Name)

        If Request.Headers.Count > 0 Then
            Dim xmlHeaders As Xml.XmlElement = Request.Document.CreateElement("Headers")
            edx.AppendChild(xmlHeaders)

            For Each hdr As EdxHeader In Request.Headers
                xmlHeaders.SetAttribute(hdr.Name, hdr.Value)
            Next
        End If

        Dim xmlBody As Xml.XmlElement = Request.Document.CreateElement("Body")
        edx.AppendChild(xmlBody)

        If Request.Body <> "" Then xmlBody.InnerXml = Request.Body

        If Request.Attachments.Count > 0 Then
            Dim xmlAttachments As Xml.XmlElement = Request.Document.CreateElement("Attachments")
            edx.AppendChild(xmlAttachments)

            For Each file As EdxFile In Request.Attachments
                Dim xmlFile As Xml.XmlElement = Request.Document.CreateElement("File")
                xmlAttachments.AppendChild(xmlFile)
                xmlFile.SetAttribute("name", IO.Path.GetFileName(file.File))

                Dim cdata As Xml.XmlCDataSection = Request.Document.CreateCDataSection(file.Content)
                xmlFile.AppendChild(cdata)
            Next
        End If

    End Sub
#End Region

End Class
