Public Class EventManager

#Region "Properties"
    Private _EditMode As Integer

    Private _DataStore As String = AppDomain.CurrentDomain.BaseDirectory & "events.wdke"
    Public Property DataStore() As String
        Get
            Return _DataStore
        End Get
        Set(ByVal Value As String)
            _DataStore = Value
        End Set
    End Property

    Private _Template As String = AppDomain.CurrentDomain.BaseDirectory & "\events.xslt"
    Public Property Template() As String
        Get
            Return _Template
        End Get
        Set(ByVal Value As String)
            _Template = Value
        End Set
    End Property

    Public Property EditMode() As Integer
        Get
            Return _EditMode
        End Get
        Set(ByVal Value As Integer)
            _EditMode = Value
        End Set
    End Property
#End Region

#Region " .ListEvents() "
    Private Function ListEvents(Optional ByVal EventRows As Integer = 0) As String
        If DataStore = "" Then Return ""

        If IO.File.Exists(DataStore) = False Then
            Return "[WDK.EventManager].ListEvents: DataStore Not Found"
            Exit Function
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim XPath As String = "//event"
        If EventRows > 0 Then
            XPath = "//event[position() > last()-" & EventRows & "]"
        End If

        Dim tmpNode As New Xml.XmlDocument

        Dim SelectedNodes As Xml.XmlNodeList = xmlDoc.SelectNodes(XPath)

        Dim tmpXML As String = Nothing

        Dim itemNode As Xml.XmlElement
        For Each itemNode In SelectedNodes
            tmpXML = tmpXML & itemNode.OuterXml
        Next

        tmpNode.LoadXml("<events>" & tmpXML & "</events>")

        Dim Transformer As Object = Activator.CreateInstance(Type.GetType("WDK.Utilities.XmlExtensions,wdk.utilities"))
        Return Transformer.TransformXml(tmpNode, Template, Nothing)
    End Function
#End Region

#Region " .EventsCount() "
    Public Function Count() As Integer
        If IO.File.Exists(DataStore) = True Then
            Dim xmlDoc As New Xml.XmlDocument
            xmlDoc.Load(DataStore)

            Dim objList As Xml.XmlNodeList = xmlDoc.SelectNodes("//event")
            Return objList.Count
        Else
            Throw New Exception("DataStore is not exists")
        End If
    End Function
#End Region

#Region " .AddEvent() "
    Public Function AddEvent(ByVal EventID As String, ByVal Title As String, ByVal Content As String, Optional ByVal AutoCreateFile As Boolean = False) As Boolean
        If EditMode < 1 Then
            Throw New Exception("[EventManager].SaveEvent Error. EditMode is not specified.")
        End If

        If IO.File.Exists(DataStore) = False Then
            If AutoCreateFile = False Then
                Throw New Exception("[EventManager].SaveEvent Error: File Not Found")
            End If
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        If xmlDoc.OuterXml = "" Then
            xmlDoc.LoadXml("<events/>")
        End If

        Dim CurrentNode As Xml.XmlElement

        Select Case EditMode
            Case 1
                CurrentNode = xmlDoc.SelectSingleNode("//events")

                Dim NewNode As Xml.XmlElement = xmlDoc.CreateElement("event")
                CurrentNode.AppendChild(NewNode)

                NewNode.SetAttribute("id", ParseTitle(Title))
                NewNode.SetAttribute("date", Now())
                NewNode.SetAttribute("title", Title)
                NewNode.SetAttribute("content", Content)

            Case 2
                If EventID = "" Then
                    Throw New Exception("[EventManager].SaveEvent Error: EventID not specified.")
                End If

                CurrentNode = xmlDoc.SelectSingleNode("//event[@id='" & EventID & "']")

                If (CurrentNode Is Nothing) Then
                    Throw New Exception("[EventManager].SaveEvent Error: Event record does not exists")
                End If

                CurrentNode.SetAttribute("id", EventID)
                CurrentNode.SetAttribute("title", Title)
                CurrentNode.SetAttribute("content", Content)

            Case Else
                Throw New Exception("[EventManager].SavePage Error: EditMode must be equal to 1 or 2")
        End Select

        xmlDoc.Save(DataStore)
    End Function
#End Region

#Region " .RemoveEvent() "
    Public Function RemoveEvent(ByVal EventID As String) As Boolean
        If IO.File.Exists(DataStore) = False Then
            Throw New Exception("[EventManager].RemoveEvent Error: File  Not Found")
        End If

        Dim xmlDoc As New Xml.XmlDocument
        xmlDoc.Load(DataStore)

        Dim EventNode As Xml.XmlNodeList = xmlDoc.SelectNodes("//event[@id='" & EventID & "']")
        If Not (EventNode Is Nothing) Then
            Dim Node As Xml.XmlElement
            For Each Node In EventNode
                Node.ParentNode.RemoveChild(Node)
            Next
            xmlDoc.Save(DataStore)
            Return True
        Else
            Throw New Exception("[EventManager].RemoveEvent Error: Event record does not exists.")
        End If
    End Function
#End Region

#Region " .ParseTitle() "
    Private Function ParseTitle(ByVal Data As String) As String
        Data = Data.Replace(" ", "_")
        Return Data
    End Function
#End Region
End Class