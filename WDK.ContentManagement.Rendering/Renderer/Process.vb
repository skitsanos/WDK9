Imports System.Text.RegularExpressions

Public Class Process

#Region " Properties "
    Private _Template As String = ""
    Private _References() As String = _
        { _
        "System.dll", _
        "System.Drawing.dll", _
        "System.Data.dll", _
        "System.Web.dll", _
        "System.XML.dll", _
        "mscorlib.dll", _
        "Microsoft.VisualBasic.dll" _
        }

    Public Property References() As String()
        Get
            Return _References
        End Get
        Set(ByVal value() As String)
            _References = value
        End Set
    End Property
#End Region

#Region " New "
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal Template As String)
        MyBase.New()
        _Template = Template
    End Sub
#End Region

#Region " OuterHtml "
    Public Function OuterHtml() As String
        Return _Template
    End Function
#End Region

#Region " Load : URL "
    Public Sub Load(ByVal Url As String)

    End Sub
#End Region

#Region " ProcessTag "
    Private Sub ProcessTag(ByVal tag As String, ByVal func As String, ByVal method As String)
        Select Case func.ToLower
            Case "page"


            Case "="
                Dim funcWrap As String = _
                    "Public Class tmpClass" & vbCrLf & _
                        "Public Shared Function tmpFunc() As String" & vbCrLf & _
                        "   Return " & method & vbCrLf & _
                        "End Function" & vbCrLf & _
                    "End Class "

                _Template = _Template.Replace(tag, ExecuteStatement(funcWrap))

            Case Else
                _Template = _Template.Replace(tag, "{wdk.rendering error: no support for {" & func & "} has been implemented")

        End Select
    End Sub
#End Region

#Region " Execute "
    Public Sub Execute()
        Dim options As RegexOptions = RegexOptions.IgnoreCase
        '- {@(?<func>[^\w]*)\s(?<method>[^}]*)
        '- {@(?<func>[^/\r\n]+)\s(?<method>[^\r\n]*)}
        '- {@(?<func>[\w]*|[\W]*)\s(?<method>[^}]*) <-- Working one!!!
        Dim regex As Regex = New Regex("{@(?<func>[\w]*|[\W]*)\s(?<method>[^}]*)", options)

        Dim matches As MatchCollection = regex.Matches(_Template)
        For Each m As Match In matches
            Dim procFunc As String = m.Groups("func").Value
            Dim procMethod As String = m.Groups("method").Value

            ProcessTag(m.Value, procFunc, procMethod)
        Next
    End Sub
#End Region

#Region "  ExecuteStatement "
    Private Function ExecuteStatement(ByVal srcCode As String, Optional ByVal args As Object = Nothing) As String
        Dim tmpHtml As String = ""

        Dim provider As Microsoft.VisualBasic.VBCodeProvider
        Dim params As System.CodeDom.Compiler.CompilerParameters
        Dim results As System.CodeDom.Compiler.CompilerResults

        params = New System.CodeDom.Compiler.CompilerParameters
        params.GenerateInMemory = True
        params.TreatWarningsAsErrors = False
        params.WarningLevel = 4

        'Dim refs() As String = _
        '{ _
        '"System.dll", _
        '"System.Drawing.dll", _
        '"System.Data.dll", _
        '"System.Web.dll", _
        '"System.XML.dll", _
        '"mscorlib.dll", _
        '"Microsoft.VisualBasic.dll" _
        '}

        params.ReferencedAssemblies.AddRange(References)

        Try
            provider = New Microsoft.VisualBasic.VBCodeProvider
            results = provider.CompileAssemblyFromSource(params, srcCode)

            If results.Errors.Count > 0 Then
                For Each res As System.CodeDom.Compiler.CompilerError In results.Errors
                    tmpHtml = res.ErrorText & " {Line/Column: " & res.Line & "/" & res.Column & "}"
                Next
            Else
                Dim m As System.Reflection.Assembly = results.CompiledAssembly
                Dim tp As Type = m.GetType("tmpClass")

                Dim rslt As Object = tp.InvokeMember("tmpFunc", _
                       System.Reflection.BindingFlags.InvokeMethod Or _
                       System.Reflection.BindingFlags.Public Or _
                       System.Reflection.BindingFlags.Static, _
                       Nothing, Nothing, args)
                If rslt IsNot Nothing Then
                    tmpHtml = CType(rslt, String)
                End If
            End If

            Return tmpHtml

        Catch ex As Exception
            Return ex.Message
        End Try
    End Function
#End Region

End Class
