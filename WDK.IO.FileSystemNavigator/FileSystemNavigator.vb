Imports Microsoft.VisualBasic
Imports System
Imports System.IO
Imports System.Xml
Imports System.Xml.XPath

Public Class FileSystemNavigator
    Inherits XPathNavigator

#Region "Private"
    Private Shared AttributeIds As String()() = New String()() {New String() {"Name", "CreationTime", "URI"}, New String() {"Name", "CreationTime", "Length", "URI"}}
    Friend State As NavigatorState
    Private _NameTable As NameTable
    Private Shared NumberOfAttributes As Integer() = New Integer() {3, 4}
#End Region

#Region "Enumerations"
    Public Enum NodeTypes
        Root = 0
        Element = 1
        Attribute = 2
        Text = 3
    End Enum
#End Region

#Region "Properties"

    Public Overrides ReadOnly Property HasAttributes() As Boolean
        Get
            Dim flag As Boolean

            If State.Node <> NodeTypes.Root AndAlso State.Node <> NodeTypes.Attribute AndAlso State.Node <> NodeTypes.Text Then
                flag = True
            Else
                flag = False
            End If
            Return flag
        End Get
    End Property

    Public Overrides ReadOnly Property HasChildren() As Boolean
        Get
            Return ChildCount > 0
        End Get
    End Property

    Public Overrides ReadOnly Property IsEmptyElement() As Boolean
        Get
            Dim flag As Boolean

            If State.ElementType = 1 Then
                flag = True
            Else
                flag = False
            End If
            Return flag
        End Get
    End Property

    Public Overrides ReadOnly Property BaseURI() As String
        Get
            Return String.Empty
        End Get
    End Property

    Public Overrides ReadOnly Property Name() As String
        Get
            Dim str As String

            Select Case state.Node
                Case NodeTypes.Text
                    str = state.TextValue

                Case NodeTypes.Attribute
                    str = state.AttributeText

                Case NodeTypes.Element
                    str = state.ElementText

                Case Else
                    str = String.Empty
            End Select
            Return str
        End Get
    End Property

    Public Overrides ReadOnly Property NamespaceURI() As String
        Get
            Return NameTable.Get(String.Empty)
        End Get
    End Property

    Public Overrides ReadOnly Property NameTable() As System.Xml.XmlNameTable
        Get
            Return _NameTable
        End Get
    End Property

    Public Overrides ReadOnly Property NodeType() As System.Xml.XPath.XPathNodeType
        Get
            Dim xPathNodeType As xPathNodeType

            Select Case state.Node
                Case NodeTypes.Root
                    xPathNodeType = xPathNodeType.Root

                Case NodeTypes.Element
                    xPathNodeType = xPathNodeType.Element

                Case NodeTypes.Attribute
                    xPathNodeType = xPathNodeType.Attribute

                Case NodeTypes.Text
                    xPathNodeType = xPathNodeType.Text

                Case Else
                    xPathNodeType = xPathNodeType.All
            End Select
            Return xPathNodeType
        End Get
    End Property

    Public Overrides ReadOnly Property Prefix() As String
        Get
            Return NameTable.Get(String.Empty)
        End Get
    End Property

    Public Overrides ReadOnly Property Value() As String
        Get
            Return state.TextValue
        End Get
    End Property

    Public Overrides ReadOnly Property XmlLang() As String
        Get
            Return "en-us"
        End Get
    End Property

    '- Helpers
    Public ReadOnly Property AttributeCount() As Integer
        Get
            Dim i As Integer

            If state.Node <> NodeTypes.Root Then
                i = NumberOfAttributes(state.ElementType)
            Else
                i = 0
            End If
            Return i
        End Get
    End Property
#End Region

#Region "Friend"
    Friend Class NavigatorState
        Public _Doc As FileSystemInfo
        Private _Root As String
        Public _Attribute As Integer
        Public _ElementType As Integer
        Public _ElementIndex As Integer
        Public _Node As FileSystemNavigator.NodeTypes

        Public Property Doc() As FileSystemInfo
            Get
                Return _Doc
            End Get

            Set(ByVal value As FileSystemInfo)
                _Doc = value
            End Set
        End Property

        Public Property Root() As String
            Get
                Return _Root
            End Get

            Set(ByVal value As String)
                _Root = value
            End Set
        End Property

        Public Property Attribute() As Integer
            Get
                Return _Attribute
            End Get

            Set(ByVal value As Integer)
                _Attribute = value
            End Set
        End Property

        Public Property ElementType() As Integer
            Get
                Return _ElementType
            End Get

            Set(ByVal value As Integer)
                _ElementType = value
            End Set
        End Property

        Public Property ElementIndex() As Integer
            Get
                Return _ElementIndex
            End Get

            Set(ByVal value As Integer)
                _ElementIndex = value
            End Set
        End Property

        Public Property Node() As FileSystemNavigator.NodeTypes
            Get
                Return _Node
            End Get

            Set(ByVal value As FileSystemNavigator.NodeTypes)
                _Node = value
            End Set
        End Property

        Public ReadOnly Property TextValue() As String
            Get
                Dim str As String

                Select Case Node
                    Case FileSystemNavigator.NodeTypes.Root
                        str = Nothing

                    Case FileSystemNavigator.NodeTypes.Element
                        str = Nothing

                    Case FileSystemNavigator.NodeTypes.Attribute
                        If ElementType = 0 Then
                            '- folder based resource
                            Dim dInfo As DirectoryInfo = CType(Doc, DirectoryInfo)
                            Select Case Attribute
                                Case 0
                                    str = dInfo.Name

                                Case 1
                                    str = dInfo.CreationTime.ToString()

                                Case 2
                                    str = dInfo.FullName

                                Case Else
                                    str = Nothing
                            End Select
                        Else
                            If ElementType <> 1 Then str = Nothing
                            '- file based resource
                            Dim fInfo As FileInfo = CType(Doc, FileInfo)
                            Select Case Attribute
                                Case 0
                                    str = fInfo.Name

                                Case 1
                                    str = fInfo.CreationTime.ToString()

                                Case 2
                                    str = fInfo.Length.ToString()

                                Case 3
                                    str = fInfo.FullName.ToString

                                Case Else
                                    str = Nothing
                            End Select
                        End If

                    Case FileSystemNavigator.NodeTypes.Text
                        str = Nothing

                    Case Else
                        str = Nothing
                End Select
                Return str
            End Get
        End Property

        Public ReadOnly Property AttributeText() As String
            Get
                Dim str As String

                If Node = FileSystemNavigator.NodeTypes.Attribute Then
                    str = FileSystemNavigator.AttributeIds(ElementType)(Attribute)
                Else
                    str = Nothing
                End If
                Return str
            End Get
        End Property

        Public ReadOnly Property ElementText() As String
            Get
                Return _Doc.Name
            End Get
        End Property

        Public Sub New(ByVal Document As FileSystemInfo)
            _Doc = Document
            _Root = Doc.FullName
            _Node = FileSystemNavigator.NodeTypes.Root
            _Attribute = -1
            _ElementType = -1
            _ElementIndex = -1
        End Sub

        Public Sub New(ByVal NavState As NavigatorState)
            _Doc = NavState.Doc
            _Root = NavState.Root
            _Node = NavState.Node
            _Attribute = NavState.Attribute
            _ElementType = NavState.ElementType
            _ElementIndex = NavState.ElementIndex
        End Sub
    End Class
#End Region

    Public Overrides Function Clone() As System.Xml.XPath.XPathNavigator
        Return New FileSystemNavigator(Me)
    End Function

    Public Overrides Function GetAttribute(ByVal localName As String, ByVal namespaceURI As String) As String
        Dim i As Integer

        Dim str As String

        If Not HasAttributes Then
            GoTo IL_00b1
        End If
        i = 0
        While i < NumberOfAttributes(State.ElementType) AndAlso Not (AttributeIds(State.ElementType)(i) = localName)
            i += 1
        End While

        If i < NumberOfAttributes(State.ElementType) Then
            Dim TempAttribute As Integer = State.Attribute
            Dim TempNodeType As NodeTypes = State.Node
            State.Attribute = i
            State.Node = NodeTypes.Attribute
            Dim AttributeValue As String = State.TextValue
            State.Node = TempNodeType
            State.Attribute = TempAttribute
            str = AttributeValue
        Else
IL_00b1:
            str = String.Empty
        End If
        Return str
    End Function

    Public Overrides Function GetNamespace(ByVal name As String) As String
        Return String.Empty
    End Function

    Public Overrides Function IsSamePosition(ByVal other As System.Xml.XPath.XPathNavigator) As Boolean
        Dim flag As Boolean

        If Not (TypeOf other Is FileSystemNavigator) Then
            flag = False
        ElseIf State.Node = NodeTypes.Root Then
            flag = CType(other, FileSystemNavigator).State.Node = NodeTypes.Root
        Else
            flag = State.Doc.FullName = CType(other, FileSystemNavigator).State.Doc.FullName
        End If
        Return flag
    End Function

    Public Overrides ReadOnly Property LocalName() As String
        Get
            NameTable.Add(Name)
            Return NameTable.Get(Name)
        End Get
    End Property

    Public Overrides Function MoveTo(ByVal other As System.Xml.XPath.XPathNavigator) As Boolean
        Dim flag As Boolean

        If TypeOf other Is FileSystemNavigator Then
            State = New NavigatorState(CType(other, FileSystemNavigator).State)
            flag = True
        Else
            flag = False
        End If
        Return flag
    End Function

    Public Overrides Function MoveToAttribute(ByVal localName As String, ByVal namespaceURI As String) As Boolean
        Dim flag As Boolean = False

        If State.Node = NodeTypes.Attribute Then MoveToElement()

        If State.Node <> NodeTypes.Element Then
            Return True
        End If

        For i As Integer = 0 To AttributeCount - 1
            If AttributeIds(State.ElementType)(i) = localName Then
                State.Attribute = i
                State.Node = NodeTypes.Attribute
                flag = True
            End If
        Next i

        Return flag
    End Function

    Public Overrides Function MoveToFirst() As Boolean
        Dim flag As Boolean

        Dim TempState As FileSystemNavigator = CType(Clone(), FileSystemNavigator)
        If MoveToParent() AndAlso MoveToChild(0) Then
            flag = True
        Else
            State = New NavigatorState(TempState.State)
            flag = False
        End If
        Return flag
    End Function

    Public Overrides Function MoveToFirstAttribute() As Boolean
        Dim flag As Boolean

        If State.Node = NodeTypes.Attribute Then
            MoveToElement()
        End If
        If AttributeCount > 0 Then
            State.Attribute = 0
            State.Node = NodeTypes.Attribute
            flag = True
        Else
            flag = False
        End If
        Return flag
    End Function

    Public Overrides Function MoveToFirstChild() As Boolean
        Dim flag As Boolean

        Dim TempState As FileSystemNavigator = CType(Clone(), FileSystemNavigator)
        If MoveToChild(0) Then
            flag = True
        Else
            State = New NavigatorState(TempState.State)
            flag = False
        End If
        Return flag
    End Function

    Public Overloads Overrides Function MoveToFirstNamespace(ByVal namespaceScope As System.Xml.XPath.XPathNamespaceScope) As Boolean
        Return False
    End Function

    Public Overrides Function MoveToId(ByVal id As String) As Boolean
        Return False
    End Function

    Public Overrides Function MoveToNamespace(ByVal name As String) As Boolean
        Return False
    End Function

    Public Overrides Function MoveToNext() As Boolean
        Dim flag As Boolean = False

        Dim NextElement As Integer = IndexInParent + 1
        Dim TempState As FileSystemNavigator = CType(Clone(), FileSystemNavigator)
        If MoveToParent() AndAlso MoveToChild(NextElement) Then
            flag = True
        Else
            State = New NavigatorState(TempState.State)
            flag = False
        End If

        Return flag
    End Function

    Public Overrides Function MoveToNextAttribute() As Boolean
        Dim flag As Boolean

        Dim TempAttribute As Integer = -1
        If State.Node = NodeTypes.Attribute Then
            TempAttribute = State.Attribute
            MoveToElement()
        End If
        If TempAttribute + 1 < AttributeCount Then
            State.Attribute = TempAttribute + 1
            State.Node = NodeTypes.Attribute
            flag = True
        Else
            State.Node = NodeTypes.Attribute
            State.Attribute = TempAttribute
            flag = False
        End If
        Return flag
    End Function

    Public Overloads Overrides Function MoveToNextNamespace(ByVal namespaceScope As System.Xml.XPath.XPathNamespaceScope) As Boolean
        Return False
    End Function

    Public Overrides Function MoveToParent() As Boolean
        Dim flag As Boolean

        If State.Node = NodeTypes.Root Then
            flag = False
        ElseIf State.Root <> State.Doc.FullName Then
            If TypeOf State.Doc Is DirectoryInfo Then
                State.Doc = CType(State.Doc, DirectoryInfo).Parent
            ElseIf TypeOf State.Doc Is FileInfo Then
                State.Doc = CType(State.Doc, FileInfo).Directory
            End If
            State.Node = NodeTypes.Element
            State.Attribute = -1
            State.ElementType = 0
            If State.Root <> State.Doc.FullName Then
                Dim i As Integer

                Dim FileSystemEnumerator As FileSystemInfo() = CType(State.Doc, DirectoryInfo).Parent.GetFileSystemInfos()
                For i = 0 To CInt(FileSystemEnumerator.Length) - 1
                    If FileSystemEnumerator(i).Name = State.Doc.Name Then
                        State.ElementIndex = i
                    End If
                Next i
            Else
                State.ElementIndex = 0
            End If
            flag = True
        Else
            MoveToRoot()
            flag = True
        End If
        Return flag
    End Function

    Public Overrides Function MoveToPrevious() As Boolean
        Dim flag As Boolean

        Dim NextElement As Integer = IndexInParent - 1
        Dim TempState As FileSystemNavigator = CType(Clone(), FileSystemNavigator)
        If MoveToParent() AndAlso MoveToChild(NextElement) Then
            flag = True
        Else
            State = New NavigatorState(TempState.State)
            flag = False
        End If
        Return flag
    End Function

    Public Overrides Sub MoveToRoot()
        State.Node = NodeTypes.Root
        State.Doc = New FileInfo(State.Root)
        State.Attribute = -1
        State.ElementType = -1
        State.ElementIndex = -1
    End Sub

    '- Helpers
    Public Function MoveToElement() As Boolean
        State.Attribute = -1
        State.Node = NodeTypes.Element
        If TypeOf State.Doc Is DirectoryInfo Then
            State.ElementType = 0
        Else
            State.ElementType = 1
        End If
        Dim flag As Boolean = True
        Return flag
    End Function

    Public ReadOnly Property IndexInParent() As Integer
        Get
            Return State.ElementIndex
        End Get
    End Property

    Public ReadOnly Property ChildCount() As Integer
        Get
            Dim i As Integer

            Select Case State.Node
                Case NodeTypes.Root
                    i = 1

                Case NodeTypes.Element
                    If State.ElementType = 0 Then
                        i = CInt(CType(State.Doc, DirectoryInfo).GetFileSystemInfos().Length)
                    Else
                        i = 0
                    End If

                Case Else
                    i = 0
            End Select
            Return i
        End Get
    End Property

    Public Shadows Function MoveToChild(ByVal i As Integer) As Boolean
        Dim flag As Boolean

        If i < 0 Then
            GoTo IL_016f
        End If
        If State.Node = NodeTypes.Root AndAlso i = 0 Then
            State.Doc = Directory.CreateDirectory(State.Root)
            State.ElementType = 0
            If Not State.Doc.Exists Then
                File.Open(State.Root, FileMode.OpenOrCreate).Close()
                State.Doc = New FileInfo(State.Root)
                State.ElementType = 1
            End If
            State.Node = NodeTypes.Element
            State.Attribute = -1
            State.ElementIndex = 0
            flag = True
            GoTo IL_0173
        End If
        If State.Node <> NodeTypes.Element OrElse State.ElementType <> 0 Then
            GoTo IL_016f
        End If
        Dim DirectoryEnumerator As FileSystemInfo() = CType(State.Doc, DirectoryInfo).GetFileSystemInfos()
        If i < CInt(DirectoryEnumerator.Length) Then
            State.Node = NodeTypes.Element
            State.Attribute = -1
            State.ElementIndex = i
            If TypeOf DirectoryEnumerator(i) Is DirectoryInfo Then
                State.Doc = DirectoryEnumerator(i)
                State.ElementType = 0
            ElseIf TypeOf DirectoryEnumerator(i) Is FileInfo Then
                State.Doc = DirectoryEnumerator(i)
                State.ElementType = 1
            End If
            flag = True
        Else
IL_016f:
            flag = False
        End If
IL_0173:
        Return flag
    End Function

#Region "New"
    Public Sub New(ByVal document As FileSystemInfo)
        _NameTable = New NameTable
        _NameTable.Add(String.Empty)
        If Not document.Exists Then
            Throw New Exception("Root node must be a directory or a file")
        End If
        State = New NavigatorState(document)
    End Sub

    Public Sub New(ByVal navigator As FileSystemNavigator)
        State = New NavigatorState(navigator.State)
        _NameTable = CType(navigator.NameTable, NameTable)
    End Sub

    Public Sub New(ByVal rootNode As String)
        Dim document As FileSystemInfo = Directory.CreateDirectory(rootNode)
        _NameTable = New NameTable
        _NameTable.Add(String.Empty)
        If Not document.Exists Then
            File.Open(rootNode, FileMode.OpenOrCreate).Close()
            document = New FileInfo(rootNode)
        End If
        If Not document.Exists Then
            Throw New Exception("Root node must be a directory or a file")
        End If
        State = New NavigatorState(document)
    End Sub
#End Region

End Class