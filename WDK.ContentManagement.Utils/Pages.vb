Public Class Pages
#Region " Properties "
    Private _DataStore As String
    Public Property DataStore() As String
        Get
            Return _DataStore
        End Get
        Set(ByVal Value As String)
            _DataStore = Value
        End Set
    End Property

    Private _AutoCreateStorage As Boolean = True
    Public Property AutoCreateStorage() As Boolean
        Get
            Return _AutoCreateStorage
        End Get
        Set(ByVal Value As Boolean)
            _AutoCreateStorage = Value
        End Set
    End Property

    Private _EditMode As Integer = 1
    Public Property EditMode() As Integer
        Get
            Return _EditMode
        End Get
        Set(ByVal Value As Integer)
            _EditMode = Value
        End Set
    End Property
#End Region

#Region " .AllowedRole() "
    Public Function AllowedRole(ByVal PageName As String) As String
        If IO.File.Exists(DataStore) = False Then
            Return "{.Datastore} not found."
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim Page As Xml.XmlElement = xmlDoc.SelectSingleNode("//page[@file='" & PageName & "']")

        If IsNothing(Page) = False Then
            Return Page.GetAttribute("role")
        Else
            Return "0"
        End If
    End Function
#End Region

#Region " .GetPage() "
    Public Function GetPage(ByVal PageName As String) As String
        If IO.File.Exists(DataStore) = False Then
            Return "{.Datastore} not found."
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim Page As Xml.XmlElement = xmlDoc.SelectSingleNode("//page[@file='" & PageName & "']")

        If IsNothing(Page) = False Then
            Dim tmpContent As String = Page.GetAttribute("content")

            If tmpContent.StartsWith("http://") Then
                Dim Http As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.Http,wdk.utilities"))
                Return Http.FetchURL(tmpContent)
            ElseIf LCase(tmpContent).StartsWith("xmlcs://") Then
                Return "{.GetPage} Error: XML Content Server connections are not supported in this version of WDK"
            Else
                Return tmpContent
            End If
        Else
            Return "{.GetPage} Error: " & PageName & " not found."
        End If
    End Function
#End Region

#Region " .SavePage() "
    Public Function SavePage(ByVal PageName As String, ByVal PageTitle As String, ByVal PageContent As String, Optional ByVal Role As Integer = -1) As String
        Dim _Trigger As Boolean = False
        If IO.File.Exists(DataStore) = False Then
            If AutoCreateStorage = False Then
                Return False
                Exit Function
            Else
                _Trigger = True
            End If
        End If

        Dim xmlDoc As New Xml.XmlDocument
        If _Trigger = True Then
            xmlDoc.LoadXml("<pages version=""WDK6""/>")
        Else
            xmlDoc.Load(DataStore)
        End If

        Dim PagesNode As Xml.XmlElement = xmlDoc.SelectSingleNode("//page[@file='" & PageName & "']")

        Select Case EditMode
            Case 1
                If IsNothing(PagesNode) = True Then
                    SavePage = "[WDK.Pages].SavePage: " & "An Error occured while tried to save a page. The page <u>" & PageName & "</u> already exists."
                    Exit Function
                End If

                PagesNode = xmlDoc.SelectSingleNode("//pages")

                Dim NewNode As Xml.XmlElement = xmlDoc.CreateElement("page")
                NewNode.SetAttribute("file", PageName)
                NewNode.SetAttribute("title", PageTitle)
                NewNode.SetAttribute("role", Role)
                NewNode.SetAttribute("created", Now())
                NewNode.SetAttribute("content", PageContent)

                PagesNode.AppendChild(NewNode)

            Case 2
                If (PagesNode Is Nothing) Then
                    SavePage = "{.SavePage} Error: " & PageName & "An Error occured while tried to modify a page. The page <u>" & PageName & "</u> is not exists."
                    Exit Function
                End If
                PagesNode.SetAttribute("file", PageName)
                PagesNode.SetAttribute("title", PageTitle)
                PagesNode.SetAttribute("content", PageContent)
                PagesNode.SetAttribute("role", Role)
                PagesNode.SetAttribute("modified", Now())
        End Select

        xmlDoc.Save(DataStore)
        Return PageName & " was saved."
    End Function
#End Region

#Region " .RemovePage() "
    Public Function RemovePage(ByVal PageName As String) As Boolean
        If IO.File.Exists(DataStore) = False Then
            Return False
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim MailNode As Xml.XmlNodeList = xmlDoc.SelectNodes("//page[@file='" & PageName & "']")

        If IsNothing(MailNode) = True Then
            Return False
        Else
            Dim Node As Xml.XmlElement
            For Each Node In MailNode
                Node.ParentNode.RemoveChild(Node)
            Next
            xmlDoc.Save(DataStore)
            Return True
        End If
    End Function
#End Region

#Region " .ListPages() "
    Public Function ListPages(ByVal Template As String) As String
        Dim FSO As New IO.FileInfo(DataStore)
        If FSO.Exists = False Then
            Return "{.ListPages} Error: Datastore not found."
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim Transformer As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.XmlExtensions,wdk.utilities"))

        Return Transformer.TransformXml(xmlDoc, Template)
    End Function
#End Region

#Region " .GetPageTitle() "
    Public Function GetPageTitle(ByVal PageName As String) As String
        If IO.File.Exists(DataStore) = False Then
            Return "404 Page Not Found"
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim Page As Xml.XmlElement = xmlDoc.SelectSingleNode("//page[@file='" & PageName & "']")

        If IsNothing(Page) = False Then
            Return Page.GetAttribute("title")
        Else
            Return ""
        End If
    End Function
#End Region
End Class