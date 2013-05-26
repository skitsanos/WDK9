Public Class ContentTree

#Region " Properties "
    Private _Document As New System.Xml.XmlDocument
    Public Property Document() As System.Xml.XmlDocument
        Get
            Return _Document
        End Get
        Set(ByVal value As System.Xml.XmlDocument)
            _Document = value
        End Set
    End Property
#End Region

#Region " New "
    Public Sub New(ByVal ContentTree As String)
        MyBase.New()
        _Document.LoadXml(ContentTree)
    End Sub

    Public Sub New()
        MyBase.New()

        Dim xmlContentTree As System.Xml.XmlElement = Document.CreateElement("ContentTree")
        Document.AppendChild(xmlContentTree)
    End Sub
#End Region

#Region " CreateNode "
    Public Function CreateNode(ByVal TargetPath As String, ByVal Title As String) As Boolean
        Dim XPath As String = BuildXPath(TargetPath)
        Dim xmlSelectedNode As System.Xml.XmlElement = Document.SelectSingleNode("/" & XPath)
        If xmlSelectedNode Is Nothing Then
            Return False
        Else
            Dim xmlNewNode As System.Xml.XmlElement = Document.CreateElement("Content")
            xmlNewNode.SetAttribute("title", Title)
            xmlSelectedNode.AppendChild(xmlNewNode)
        End If
    End Function
#End Region

#Region " UpdateTitle "
    Public Function UpdateTitle(ByVal TargetPath As String, ByVal Title As String) As Boolean
        Dim XPath As String = BuildXPath(TargetPath)
        Dim xmlSelectedNode As System.Xml.XmlElement = Document.SelectSingleNode("/" & XPath)

        If xmlSelectedNode Is Nothing Then
            Return False
        Else
            xmlSelectedNode.SetAttribute("title", Title)
            Return True
        End If
    End Function
#End Region

#Region " RemoveNode "
    Public Sub RemoveNode(ByVal TargetPath As String)
        Dim XPath As String = BuildXPath(TargetPath)
        Dim MainNode As System.Xml.XmlElement = Document.SelectSingleNode("/" & XPath)

        If MainNode IsNot Nothing Then
            Dim Node As System.Xml.XmlElement
            For Each Node In MainNode
                Node.ParentNode.RemoveChild(Node)
            Next
        End If
    End Sub
#End Region

#Region " GetProperty "
    Public Function GetProperty(ByVal TargetPath As String, ByVal Key As String)
        Dim XPath As String = BuildXPath(TargetPath)
        Dim MainNode As System.Xml.XmlElement = Document.SelectSingleNode("/" & XPath)

        If IsNothing(MainNode) Then
            Return Nothing
        Else
            Return MainNode.GetAttribute(Key)
        End If
    End Function
#End Region

#Region " SetProperty "
    Public Sub SetProperty(ByVal TargetPath As String, ByVal Key As String, Optional ByVal Value As String = "")
        Dim XPath As String = BuildXPath(TargetPath)

        Dim MainNode As System.Xml.XmlElement = Document.SelectSingleNode("/" & XPath)
        If MainNode IsNot Nothing Then MainNode.SetAttribute(Key, Value)

    End Sub
#End Region

#Region " BuildXPath "
    Function BuildXPath(ByVal TargetPath As String)
        TargetPath = Replace(TargetPath, "\", "/")
        If TargetPath = "/" Then TargetPath = ""

        If TargetPath <> "" Then
            Dim strArr As String() = Split(TargetPath, "/")
            Dim arrItem As String = ""
            Dim XPath As String = ""
            For Each arrItem In strArr
                If arrItem <> "" Then XPath += "/Content[@title='" & arrItem & "']"
            Next
            Return XPath
        Else
            Return "/ContentTree"
        End If
    End Function
#End Region

End Class