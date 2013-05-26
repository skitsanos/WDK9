
#Region " ail.xml config "
'<ail>
'  <applications>
'    <instance name="Sedna Database Controller" type="Skitsanos.Data.SednaController, ail_SednaController"/>
'  </applications>
'</ail>
#End Region

'http://modis.ispras.ru/sedna/quick-start.htm
Public Class SednaController

#Region " Properties "
	Private SednaGuverner As String = AppDomain.CurrentDomain.BaseDirectory + "Sedna\bin\se_gov.exe"
	Private SednaShutdown As String = AppDomain.CurrentDomain.BaseDirectory + "Sedna\bin\se_stop.exe"

	Private SednaCreateDatabase As String = AppDomain.CurrentDomain.BaseDirectory + "Sedna\bin\se_cdb.exe"
	Private SednaDeleteDatabase As String = AppDomain.CurrentDomain.BaseDirectory + "Sedna\bin\se_ddb.exe"

	Private SednaLaunchDatabase As String = AppDomain.CurrentDomain.BaseDirectory + "Sedna\bin\se_sm.exe"
	Private SednaShutdownDatabase As String = AppDomain.CurrentDomain.BaseDirectory + "Sedna\bin\se_smsd.exe"
#End Region

#Region " Log "
	''' <summary>
	''' Writes log data into Sedna Controller log file
	''' </summary>
	''' <param name="data"></param>
	''' <remarks></remarks>
	Public Sub Log(ByVal data As String)
		IO.File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + "ail_SednaController.log", data)
	End Sub
#End Region

#Region " Start "
	''' <summary>
	''' Starts Sedna Controller Service via AIL Windows Service
	''' </summary>
	''' <remarks></remarks>
	Public Sub Start()
		Dim proc As New Process
		proc.StartInfo.FileName = SednaGuverner
		proc.StartInfo.UseShellExecute = False
		proc.StartInfo.RedirectStandardOutput = True
		proc.Start()

		Log(proc.StandardOutput.ReadToEnd())

		proc.WaitForExit()
		Log("se_gov" + vbTab + proc.Id.ToString + vbTab + proc.ExitCode.ToString)

		If Not Exists("sample") Then CreateDatabase("sample")

		LaunchDatabase("sample")
	End Sub
#End Region

#Region " Stop "
	''' <summary>
	''' Sends shutdown request to Sedna database
	''' </summary>
	''' <remarks></remarks>
	Public Sub [Stop]()
		Process.Start(SednaShutdown)
	End Sub
#End Region

#Region " Exists "
	''' <summary>
	''' Checks if database already exists
	''' </summary>
	''' <param name="name"></param>
	''' <returns></returns>
	''' <remarks></remarks>
	Public Function Exists(ByVal name As String) As Boolean
		Return IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "Sedna\data\" + name + "_files")
	End Function
#End Region

#Region " List "
	Public Function List() As List(Of String)
		Dim col As New List(Of String)

		For Each folder As String In IO.Directory.GetDirectories(AppDomain.CurrentDomain.BaseDirectory + "Sedna\data\")
			col.Add(folder.Replace("_files", ""))
		Next

		Return col
	End Function
#End Region

#Region " CreateDatabase "
	Public Function CreateDatabase(ByVal name As String) As Boolean
		Dim proc As New Process
		proc.StartInfo.FileName = SednaCreateDatabase
		proc.StartInfo.Arguments = name
		proc.StartInfo.UseShellExecute = False
		proc.StartInfo.RedirectStandardOutput = True
		proc.Start()

		Log(proc.StandardOutput.ReadToEnd())

		proc.WaitForExit()
		Log("se_cdb" + vbTab + proc.Id.ToString + vbTab + proc.ExitCode.ToString)
		If proc.ExitCode = 0 Then
			Return True
		Else
			Return False
		End If

	End Function
#End Region

#Region " DeleteDatabase "
	Public Function DeleteDatabase(ByVal name As String) As Boolean
		Dim proc As New Process
		proc.StartInfo.FileName = SednaDeleteDatabase
		proc.StartInfo.Arguments = name
		proc.StartInfo.UseShellExecute = False
		proc.StartInfo.RedirectStandardOutput = True
		proc.Start()

		Log(proc.StandardOutput.ReadToEnd())

		proc.WaitForExit()
		Log("se_ddb" + vbTab + proc.Id.ToString + vbTab + proc.ExitCode.ToString)
		If proc.ExitCode = 0 Then
			Return True
		Else
			Return False
		End If

	End Function
#End Region

#Region " LaunchDatabase "
	Public Function LaunchDatabase(ByVal name As String) As Boolean
		Dim proc As New Process
		proc.StartInfo.FileName = SednaLaunchDatabase
		proc.StartInfo.Arguments = name
		proc.StartInfo.UseShellExecute = False
		proc.StartInfo.RedirectStandardOutput = True
		proc.Start()

		Log(proc.StandardOutput.ReadToEnd())

		proc.WaitForExit()
		Log("se_sm" + vbTab + proc.Id.ToString + vbTab + proc.ExitCode.ToString)
		If proc.ExitCode = 0 Then
			Return True
		Else
			Return False
		End If

	End Function
#End Region

#Region " ShutdownDatabase "
	Public Function ShutdownDatabase(ByVal name As String) As Boolean
		Dim proc As New Process
		proc.StartInfo.FileName = SednaShutdownDatabase
		proc.StartInfo.Arguments = name
		proc.StartInfo.UseShellExecute = False
		proc.StartInfo.RedirectStandardOutput = True
		proc.Start()

		Log(proc.StandardOutput.ReadToEnd())

		proc.WaitForExit()
		Log("se_smsd" + vbTab + proc.Id.ToString + vbTab + proc.ExitCode.ToString)
		If proc.ExitCode = 0 Then
			Return True
		Else
			Return False
		End If

	End Function
#End Region

	'execute query
	'se_term -query "let $a:=(3, 1, 7) return <result>{ max($a) }</result>" testdb


End Class