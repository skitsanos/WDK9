Imports System.ServiceProcess
Imports System.Threading

Public Class AILService
    Inherits System.ServiceProcess.ServiceBase

#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()

        ' This call is required by the Component Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call

    End Sub

    'UserService overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    ' The main entry point for the process
    <MTAThread()> _
    Shared Sub Main()
        Dim ServicesToRun() As System.ServiceProcess.ServiceBase

        ' More than one NT Service may run within the same process. To add
        ' another service to this process, change the following line to
        ' create a second service object. For example,
        '
        '   ServicesToRun = New System.ServiceProcess.ServiceBase () {New Service1, New MySecondUserService}
        '
        ServicesToRun = New System.ServiceProcess.ServiceBase() {New AILService}

        System.ServiceProcess.ServiceBase.Run(ServicesToRun)
	End Sub
	Private WithEvents httpd As RemObjects.InternetPack.Http.HttpServer

	'Required by the Component Designer
	Private components As System.ComponentModel.IContainer

	' NOTE: The following procedure is required by the Component Designer
	' It can be modified using the Component Designer.  
	' Do not modify it using the code editor.
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
		Me.components = New System.ComponentModel.Container
		Me.httpd = New RemObjects.InternetPack.Http.HttpServer(Me.components)
		'
		'httpd
		'
		Me.httpd.Port = 8069
		Me.httpd.ServerName = "AIL Server"
		Me.httpd.ValidateRequests = False
		'
		'AILService
		'
		Me.ServiceName = "Skitsanos AIL Server"

	End Sub

#End Region

#Region " Properties "
	Private xmlDoc As Xml.XmlDocument
	Private controller As InstanceController
#End Region

#Region " OnStart() "
	Protected Overrides Sub OnStart(ByVal args() As String)
		Me.AutoLog = True

		controller = New InstanceController

		httpd.Open()

		ReloadInstances()

	End Sub
#End Region

#Region " OnStop() "
	Protected Overrides Sub OnStop()
		controller.RemoveAll()

		httpd.Close()
	End Sub
#End Region

#Region " ReloadInstances "
	Private Sub ReloadInstances()
		Try
			xmlDoc = New Xml.XmlDocument

			If IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory & "ail.xml") = True Then
				xmlDoc.Load(AppDomain.CurrentDomain.BaseDirectory & "ail.xml")

				Dim xmlInstances As Xml.XmlNodeList = xmlDoc.SelectNodes("//instance")
				If xmlInstances.Count > 0 Then
					Log.WriteEntry("AIL found " & xmlInstances.Count & " application(s) to run")
					Dim app As Xml.XmlElement = Nothing
					For Each app In xmlInstances
						'<instance name="IRC History Bot" type="Skitsanos.IRC.HistoryBot, ail_HistoryBot"/>

						Dim appInstance As Object = Activator.CreateInstance(Type.GetType(app.GetAttribute("type")))

						controller.Add(app.GetAttribute("name"), appInstance)

						appInstance.start()
						Log.WriteEntry("AIL Run: " & app.GetAttribute("type"))
						'Dim thr As New Thread(AddressOf apppinstance.start)
					Next
				Else
					Log.WriteEntry("AIL did not found any applications to run.")
				End If

			Else
				Dim xmlPi As Xml.XmlProcessingInstruction = xmlDoc.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
				xmlDoc.AppendChild(xmlPi)

				Dim xmlAil As Xml.XmlElement = xmlDoc.CreateElement("ail")
				xmlDoc.AppendChild(xmlAil)

				Dim xmlApps As Xml.XmlElement = xmlDoc.CreateElement("applications")
				xmlAil.AppendChild(xmlApps)

				xmlDoc.Save(AppDomain.CurrentDomain.BaseDirectory & "ail.xml")
			End If
		Catch ex As Exception
			Log.WriteEntry(ex.ToString)

		End Try
	End Sub
#End Region

#Region " httpd_OnHttpRequest "
	Private Sub httpd_OnHttpRequest(ByVal aSender As Object, ByVal ea As RemObjects.InternetPack.Http.OnHttpRequestArgs) Handles httpd.OnHttpRequest
		'For Each header As RemObjects.InternetPack.Http.HttpHeader In ea.Request.Header
		'	Debug.WriteLine(header.Name & " = " & header.Value)
		'Next

		Dim xmlResponse As New Xml.XmlDocument
		Dim xmlPi As Xml.XmlProcessingInstruction = xmlResponse.CreateProcessingInstruction("xml", "version=""1.0"" encoding=""utf-8""")
		xmlResponse.AppendChild(xmlPi)

		Dim root As Xml.XmlElement = xmlResponse.CreateElement("AilResponse")
		xmlResponse.AppendChild(root)

		Select Case IO.Path.GetFileNameWithoutExtension(ea.Request.Path)
			Case "list"
				Dim xmlInstances As Xml.XmlElement = xmlResponse.CreateElement("List")
				For Each ins As Instance In controller
					Dim xmlRow As Xml.XmlElement = xmlResponse.CreateElement("Instance")
					xmlRow.SetAttribute("name", ins.Name)

					xmlInstances.AppendChild(xmlRow)
				Next
				root.SetAttribute("status", "200")
				root.AppendChild(xmlInstances)

			Case Else
				renderError(xmlResponse, 501, "Not Implemeted")

		End Select

		ea.Response.Encoding = System.Text.Encoding.UTF8
		ea.Response.Header.SetHeaderValue("Content-Type", "text/xml")
		ea.Response.ContentString = xmlResponse.OuterXml

		'If ea.Request.Header.GetHeaderValue("Authorization") <> "" Then
		'	Dim auth As String = ea.Request.Header.GetHeaderValue("Authorization").Replace("Basic", "")
		'	Dim authData As String() = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(auth)).Split(":")

		'	Dim Username As String = authData(0)
		'	Dim Password As String = authData(1)

		'	Debug.WriteLine("Username: " & Username)
		'	Debug.WriteLine("Password: " & Password)

		'	If Username = "admin" And Password = "admin" Then
		'		ea.Response.Header.SetHeaderValue("Content-Type", "text/xml")
		'		ea.Response.ContentString = "<Ail />"
		'	Else
		'		ea.Response.Code = 403
		'		ea.Response.ResponseText = "Forbidden"
		'	End If

		'Else
		'	ea.Response.Header.SetHeaderValue("WWW-Authenticate", "Basic")

		'	ea.Response.Code = 401
		'	ea.Response.ResponseText = "Unauthorized"
		'End If
	End Sub
#End Region

#Region " renderError "
	Private Sub renderError(ByVal doc As Xml.XmlDocument, ByVal status As Integer, ByVal statusText As String)
		Dim xmlError As Xml.XmlElement = doc.CreateElement("Error")

		Dim errorContent As Xml.XmlCDataSection = doc.CreateCDataSection(statusText)
		xmlError.AppendChild(errorContent)

		doc.DocumentElement.SetAttribute("status", status)
		doc.DocumentElement.AppendChild(xmlError)
	End Sub
#End Region

End Class
