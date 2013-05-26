Imports System.ComponentModel
Imports System.Configuration.Install

<RunInstaller(True)> Public Class ProjectInstaller
    Inherits System.Configuration.Install.Installer

#Region " Component Designer generated code "

    Public Sub New()
        MyBase.New()

        'This call is required by the Component Designer.
        InitializeComponent()

        'Add any initialization after the InitializeComponent() call

    End Sub

    'Installer overrides dispose to clean up the component list.
    Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing Then
            If Not (components Is Nothing) Then
                components.Dispose()
            End If
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Component Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Component Designer
    'It can be modified using the Component Designer.  
    'Do not modify it using the code editor.
    Friend WithEvents AilProcessInstaller As System.ServiceProcess.ServiceProcessInstaller
    Friend WithEvents AILInstaller As System.ServiceProcess.ServiceInstaller
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.AilProcessInstaller = New System.ServiceProcess.ServiceProcessInstaller
        Me.AILInstaller = New System.ServiceProcess.ServiceInstaller
        '
        'AilProcessInstaller
        '
        Me.AilProcessInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem
        Me.AilProcessInstaller.Password = Nothing
        Me.AilProcessInstaller.Username = Nothing
        '
        'AILInstaller
        '
        Me.AILInstaller.Description = "Application Instance Launcher Server"
        Me.AILInstaller.DisplayName = "AIL Server"
        Me.AILInstaller.ServiceName = "Skitsanos AIL Server"
        Me.AILInstaller.StartType = System.ServiceProcess.ServiceStartMode.Automatic
        '
        'ProjectInstaller
        '
        Me.Installers.AddRange(New System.Configuration.Install.Installer() {Me.AilProcessInstaller, Me.AILInstaller})

    End Sub

#End Region

End Class
