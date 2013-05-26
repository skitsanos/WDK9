Public Class MenuBuilder

#Region " Properties "
    Private _DataStore As String = ""
    Private _Template As String = ""

    Public Property DataStore() As String
        Get
            Return _DataStore
        End Get
        Set(ByVal Value As String)
            _DataStore = Value
        End Set
    End Property

    Public Property Template() As String
        Get
            Return _Template
        End Get
        Set(ByVal Value As String)
            _Template = Value
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

#Region "[.ShowNodes]"
    Public Function ShowNodes(Optional ByVal TargetPath As String = "") As String
        If IO.File.Exists(DataStore) = False Then
            Return "{.ShowNodes} Error: Datastore not found."
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim Xpath As String = BuildXPath(TargetPath)
        Dim MainNode As Xml.XmlElement = xmlDoc.SelectSingleNode("/" & Xpath)

        If IsNothing(MainNode) Then
            Return ".ShowNodes Error on " & Xpath
            Exit Function
        Else
            Dim CategoryName As String = "[ROOT]"
            If Xpath <> "/menubuilder" Then
                CategoryName = MainNode.GetAttribute("title")
            End If

            Dim SectionNode As Xml.XmlNodeList = xmlDoc.SelectNodes("/" & Xpath & "/*")
            Dim Utils As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.Helpers,wdk.utilities"))

            '- Generate output XML tree
            Dim trgXmlDoc As New Xml.XmlDocument
            Dim trgRoot As Xml.XmlElement = trgXmlDoc.CreateElement("menubuilder")
            trgRoot.SetAttribute("path", TargetPath)
            trgXmlDoc.AppendChild(trgRoot)

            Dim Node As Xml.XmlElement
            For Each Node In SectionNode
                Dim trgItem As Xml.XmlElement = trgXmlDoc.CreateElement("menu")
                trgItem.SetAttribute("path", TargetPath & "/" & Node.GetAttribute("id"))
                trgItem.SetAttribute("title", Node.GetAttribute("title"))
                trgItem.SetAttribute("type", Node.GetAttribute("type"))
                trgRoot.AppendChild(trgItem)
            Next

            Return vbCrLf & trgXmlDoc.OuterXml
        End If
    End Function
#End Region

#Region "[.GetMenuItems]"
    Public Function GetMenuItems(ByVal TargetPath As String, Optional ByVal RenderTemplate As String = "") As String
        If IO.File.Exists(DataStore) = False Then
            Return "{.GetMenuItems} Error: Data location not found"
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim Xpath As String = BuildXPath(TargetPath)
        Dim MainNode As Xml.XmlElement = xmlDoc.SelectSingleNode("/" & Xpath)

        If IsNothing(MainNode) Then
            Return "{.GetMenuItems} Error on " & Xpath
            Exit Function
        Else
            Dim CategoryName As String = "[ROOT]"
            If Xpath <> "/menubuilder" Then
                CategoryName = MainNode.GetAttribute("title")
            End If

            Dim SectionNode As Xml.XmlNodeList = xmlDoc.SelectNodes("/" & Xpath & "/*")
            Dim Utils As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.Helpers,wdk.utilities"))

            '- Generate output XML tree
            Dim trgXmlDoc As New Xml.XmlDocument
            Dim trgRoot As Xml.XmlElement = trgXmlDoc.CreateElement("menubuilder")
            trgRoot.SetAttribute("path", TargetPath)
            trgRoot.SetAttribute("title", MainNode.GetAttribute("title"))
            trgRoot.SetAttribute("id", MainNode.GetAttribute("id"))
            trgXmlDoc.AppendChild(trgRoot)

            Dim Node As Xml.XmlElement
            For Each Node In SectionNode
                Dim trgItem As Xml.XmlElement = trgXmlDoc.CreateElement("menu")
                'trgItem.SetAttribute("path", Node.GetAttribute("id"))
                trgItem.SetAttribute("path", TargetPath & "/" & Node.GetAttribute("id"))
                trgItem.SetAttribute("title", Node.GetAttribute("title"))
                trgItem.SetAttribute("type", Node.GetAttribute("type"))
                Select Case Node.GetAttribute("type")
                    Case 1
                        trgItem.SetAttribute("content", Node.GetAttribute("content"))
                End Select

                trgRoot.AppendChild(trgItem)
            Next

            Dim Transformer As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.XmlExtensions,wdk.utilities"))
            If RenderTemplate = "" Then
                Return Transformer.TransformXml(trgXmlDoc, Template) '& trgXmlDoc.OuterXml
            Else
                Return Transformer.TransformXml(trgXmlDoc, RenderTemplate)
            End If
        End If
    End Function
#End Region

#Region "[.GetMenuContent]"
    Public Function GetMenuContent(ByVal TargetPath As String) As String
        Dim FSO As New IO.FileInfo(DataStore)
        If FSO.Exists = False Then
            Return "Data location not found"
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim XPath As String = BuildXPath(TargetPath)
        Dim MainNode As Xml.XmlElement = xmlDoc.SelectSingleNode("/" & XPath)

        If IsNothing(MainNode) Then
            Return ".ShowNodes Error on " & XPath
            Exit Function
        Else
            Dim tmpContent As String = MainNode.GetAttribute("content")
            If LCase(tmpContent).StartsWith("http://") Then
                Dim Http As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.Http,wdk.utilities"))
                Return Http.FetchURL(tmpContent)
            ElseIf LCase(tmpContent).StartsWith("xmlcs://") Then
                Return "XML Content Server connections are not supported in this version of WDK"
            Else
                Return tmpContent
            End If
        End If
    End Function
#End Region

#Region "[.SavePage]"
    Public Function SavePage(ByVal TargetPath As String, ByVal Type As String, ByVal Title As String, ByVal Content As String) As Boolean
        If Title = "" Then Title = "[Untitled]"
        Dim ID As String = NormalizeText(Title)
        If Type = "" Then Type = "0"

        Dim _Trigger As Boolean = False
        Dim FSO As New IO.FileInfo(DataStore)
        If FSO.Exists = False Then
            If AutoCreateStorage = False Then
                Return False
                Exit Function
            Else
                _Trigger = True
            End If
        End If

        Dim xmlDoc As New Xml.XmlDocument
        If _Trigger = True Then
            xmlDoc.LoadXml("<menunuilder version=""WDK6""/>")
        Else
            xmlDoc.Load(DataStore)
        End If

        Dim XPath As String = BuildXPath(TargetPath)
        Dim PagesNode As Xml.XmlElement

        Select Case EditMode
            Case 1 ' New page
                PagesNode = xmlDoc.SelectSingleNode("/" & BuildXPath(TargetPath & "/" & ID))
                If IsNothing(PagesNode) = False Then
                    Return False
                    Exit Function
                Else
                    PagesNode = xmlDoc.SelectSingleNode("/" & XPath)
                    Dim NewNode As Xml.XmlElement = xmlDoc.CreateElement("menu")
                    NewNode.SetAttribute("id", ID)
                    NewNode.SetAttribute("type", Type)
                    NewNode.SetAttribute("title", Title)
                    NewNode.SetAttribute("content", Content)
                    PagesNode.AppendChild(NewNode)

                    xmlDoc.Save(DataStore)

                    Return True
                End If

            Case 2
                PagesNode = xmlDoc.SelectSingleNode("/" & BuildXPath(TargetPath))
                If IsNothing(PagesNode) Then
                    Return False
                    Exit Function
                Else
                    PagesNode.SetAttribute("type", Type)
                    PagesNode.SetAttribute("title", Title)
                    PagesNode.SetAttribute("content", Content)

                    xmlDoc.Save(DataStore)

                    Return True
                End If
            Case Else
                Return False
        End Select
    End Function
#End Region

#Region "[.RemovePage]"
    Public Function RemovePage(ByVal TargetPath As String) As Boolean
        Dim FSO As New IO.FileInfo(DataStore)
        If FSO.Exists = False Then
            Return "Data location not found"
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim XPath As String = BuildXPath(TargetPath)
        Dim MainNode As Xml.XmlElement = xmlDoc.SelectSingleNode("/" & XPath)

        If IsNothing(MainNode) Then
            Return False
            Exit Function
        Else
            Dim Node As Xml.XmlElement
            For Each Node In MainNode
                Node.ParentNode.RemoveChild(Node)
            Next

            xmlDoc.Save(DataStore)
            RemovePage = True
        End If
    End Function
#End Region

#Region "[.GetProperty]"
    Public Function GetProperty(ByVal TargetPath As String, ByVal Key As String) As String
        If IO.File.Exists(DataStore) = False Then
            Return "Data location not found"
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim XPath As String = BuildXPath(TargetPath)
        Dim MainNode As Xml.XmlElement = xmlDoc.SelectSingleNode("/" & XPath)

        If IsNothing(MainNode) Then
            Return Nothing
        Else
            Return MainNode.GetAttribute(Key)
        End If
    End Function
#End Region

#Region "[.SetProperty]"
    Public Function SetProperty(ByVal ID As String, ByVal Key As String, Optional ByVal Value As String = "") As Boolean
        If ID = "" Or ID = "0" Then
            Return False
        End If

        Dim FSO As New IO.FileInfo(DataStore)
        If FSO.Exists = False Then
            Return False
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim MainNode As Xml.XmlElement = xmlDoc.SelectSingleNode("//menu[@id='" & ID & "']")
        If IsNothing(MainNode) Then
            Return Nothing
        Else
            MainNode.SetAttribute(Key, Value)
            xmlDoc.Save(DataStore)
        End If
    End Function
#End Region

#Region " Helpers "
    Function BuildXPath(ByVal TargetPath As String) As String
        TargetPath = Replace(TargetPath, "\", "/")
        If TargetPath = "/" Then TargetPath = ""

        If TargetPath <> "" Then
            Dim strArr As String() = Split(TargetPath, "/")
            Dim arrItem As String = ""
            Dim XPath As String = ""
            For Each arrItem In strArr
                If arrItem <> "" Then XPath += "/menu[@id='" & arrItem & "']"
            Next
            Return XPath
        Else
            Return "/menubuilder"
        End If
    End Function

    Private Function NormalizeText(ByVal Text As String) As String
        Text = LCase(Text)
        Text = Replace(Text, " ", "_")
        Text = Replace(Text, "'", "")
        Text = Replace(Text, """", "")
        Return Text
    End Function
#End Region
End Class