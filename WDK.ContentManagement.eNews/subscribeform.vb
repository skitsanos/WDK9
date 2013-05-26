Imports System.ComponentModel
Imports System.Web.UI

<DefaultProperty("Text"), ToolboxData("<{0}:SubscribeForm runat=""server""></{0}:SubscribeForm>")> Public Class SubscribeForm
    Inherits System.Web.UI.WebControls.WebControl

#Region " Properties "
    Private _Status As String = ""
    Private _Email As String = ""
    Private _FirstName As String = ""
    Private _LastName As String = ""

    Private _IsSubscribed As Boolean = False

    Private _Action As String
    <Bindable(True), Category("Appearance"), DefaultValue("")> Property Action() As String
        Get
            Return _Action
        End Get

        Set(ByVal Value As String)
            _Action = Value
        End Set
    End Property

    Private _Width As String = "150"
    <Bindable(True), Category("Appearance"), DefaultValue("")> Shadows Property Width() As String
        Get
            Return _Width
        End Get

        Set(ByVal Value As String)
            _Width = Value
        End Set
    End Property
#End Region

#Region " .Render() "
    Protected Overrides Sub Render(ByVal w As System.Web.UI.HtmlTextWriter)
        If _IsSubscribed = False Then
            Dim db As Data.OleDb.OleDbConnection = Nothing
            Try
                db = New Data.OleDb.OleDbConnection(ConnectionString)
                db.Open()

                Dim strSQL As String = "SELECT * FROM distribs ORDER BY title"
                Dim dbc As New Data.OleDb.OleDbCommand(strSQL, db)

                Dim dba As New Data.OleDb.OleDbDataAdapter
                dba.SelectCommand = dbc

                Dim dbs As New Data.DataSet("result")
                dbs.Clear()
                dba.Fill(dbs, "distribs")

                w.WriteBeginTag("form")
                w.WriteAttribute("name", Me.UniqueID & "_form")
                w.WriteAttribute("action", Action)
                w.WriteAttribute("method", "post")
                w.WriteLine(">")

                w.WriteLine("<table width=""" & Width & """ border=""0"" cellpadding=""3"" cellspacing=""0"">")
                w.WriteLine("<tr>")
                w.WriteLine("<td align=""center"" class=""section"">Join our mailing lists</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                w.WriteLine("First name:")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                w.WriteBeginTag("input")
                w.WriteAttribute("name", Me.UniqueID & "_fname")
                w.WriteAttribute("type", "text")
                w.WriteAttribute("class", "field")
                w.WriteAttribute("value", _FirstName)
                w.WriteLine(" />")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                w.WriteLine("Last name:")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                w.WriteBeginTag("input")
                w.WriteAttribute("name", Me.UniqueID & "_lname")
                w.WriteAttribute("type", "text")
                w.WriteAttribute("class", "field")
                w.WriteAttribute("value", _LastName)
                w.WriteLine(" />")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                w.WriteLine("Email:")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                w.WriteBeginTag("input")
                w.WriteAttribute("name", Me.UniqueID & "_email")
                w.WriteAttribute("type", "text")
                w.WriteAttribute("class", "field")
                w.WriteAttribute("value", _Email)
                w.WriteLine(" />")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                w.WriteLine("Newsletter format:")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td align=""center"" class=""section"">Choose Newsletters</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td>")
                Dim html As String = Nothing
                If dbs.Tables("distribs").Rows.Count > 0 Then
                    Dim dbRow As Data.DataRow
                    For Each dbRow In dbs.Tables("distribs").Rows
                        w.WriteBeginTag("input")
                        w.WriteAttribute("type", "checkbox")
                        w.WriteAttribute("name", Me.UniqueID & "_distrib")
                        w.WriteAttribute("value", dbRow("id"))
                        w.WriteLine("/>")
                        w.WriteLine(dbRow("title") & "<br />")
                    Next
                End If
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td align=""center"">")
                w.WriteBeginTag("input")
                w.WriteAttribute("type", "image")
                w.WriteAttribute("name", Me.UniqueID & "_btnsubmit")
                w.WriteAttribute("src", "images/buttons/subscribe.gif")
                w.WriteAttribute("border", "0")
                w.WriteLine(" />")

                w.WriteBeginTag("input")
                w.WriteAttribute("name", Me.UniqueID & "_subscriptionaction")
                w.WriteAttribute("type", "hidden")
                w.WriteAttribute("value", "do")
                w.WriteLine(" />")
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteLine("<tr>")
                w.WriteLine("<td class=""error"">")
                w.WriteLine(_Status)
                w.WriteLine("</td>")
                w.WriteLine("</tr>")

                w.WriteEndTag("form")

                dbs.Dispose()
                dba.Dispose()
                dbc.Dispose()

            Catch ex As Exception
                Throw New Exception(ex.Message)
            Finally
                If Not db Is Nothing Then
                    db.Close()
                    db.Dispose()
                End If
            End Try
        Else
            w.WriteLine("<table width=""" & Width & """ border=""0"" cellpadding=""3"" cellspacing=""0"">")
            w.WriteLine("<tr>")
            w.WriteLine("<td align=""center"" class=""section"">Join our mailing lists</td>")
            w.WriteLine("</tr>")

            w.WriteLine("<tr>")
            w.WriteLine("<td>")
            'w.WriteLine("Thank you for taking time on subscribing to our newsletters.")
            w.WriteLine(_Status)
            w.WriteLine("</td>")
            w.WriteLine("</tr>")
        End If
    End Sub
#End Region

#Region " .OnLoad() "
    Protected Overrides Sub OnLoad(ByVal e As System.EventArgs)
        If Page.IsPostBack Then
            If Page.Request(Me.UniqueID & "_subscriptionaction") = "do" Then
                _Email = Page.Request(Me.UniqueID & "_email")
                _FirstName = Page.Request(Me.UniqueID & "_fname")
                _LastName = Page.Request(Me.UniqueID & "_lname")

                If Page.Request(Me.UniqueID & "_fname") = "" Then
                    _Status = "Please provide us your first name."
                    Exit Sub
                End If

                If Page.Request(Me.UniqueID & "_email") = "" Then
                    _Status = "Please provide us your email address."
                    Exit Sub
                End If

                If Page.Request(Me.UniqueID & "_distrib") = "" Then
                    _Status = "Please select one or more Newsleters to subscribe."
                    Exit Sub
                End If

                Dim eUsers As New Users
                If eUsers.Exists(Page.Request(Me.UniqueID & "_email")) = False Then
                    Dim res As Boolean = eUsers.Add(Page.Request(Me.UniqueID & "_email"), Page.Request(Me.UniqueID & "_fname"), Page.Request(Me.UniqueID & "_lname"))
                    If res = False Then
                        _Status = "Subscription failed."
                        Exit Sub
                    End If
                End If

                Dim sm As New Subscriptions.Manager
                Dim eDistribs As New DistributionLists.Manager
                Dim um As New Users

                Dim arrLists As String() = Page.Request(Me.UniqueID & "_distrib").Split(",")
                Dim arrItem As String = ""

                For Each arrItem In arrLists
                    _Status += "<tr>"
                    _Status += "<td>"
                    If sm.IsSubscribed(Me.UniqueID & "_email", arrItem) Then
                        _Status += "You are already subscribed to " & eDistribs.FindIdByTitle(arrItem) '.GetPropertyById(arrItem, "title")
                    Else
                        Dim UserId As Integer = um.GetIdByEmail(Page.Request(Me.UniqueID & "_email"))
                        Dim res As Boolean = sm.Subscribe(UserId, arrItem)
                        _Status += eDistribs.FindIdByTitle(arrItem) '.GetPropertyById(arrItem, "title")
                        If res Then
                            _Status += " subscription processed"
                        Else
                            _Status += " subscription failed - You are already subscribed to this newsletter."
                        End If
                    End If

                    _Status += "</td>"
                    _Status += "</tr>"
                Next
                _IsSubscribed = True
            End If
        End If
    End Sub
#End Region

End Class
