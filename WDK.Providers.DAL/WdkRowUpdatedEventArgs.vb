Imports System
Imports System.Data
Imports System.Data.Common

Public Class WdkRowUpdatedEventArgs
    Inherits RowUpdatedEventArgs

    Public Sub New(ByVal row As DataRow, ByVal command As IDbCommand, ByVal statementType As StatementType, ByVal tableMapping As DataTableMapping)
        MyBase.New(row, command, statementType, tableMapping)
    End Sub

    ' Hide the inherited implementation of the command property. 
    Public Shadows ReadOnly Property Command() As WdkCommand
        Get
            Return CType(MyBase.Command, WdkCommand)
        End Get
    End Property
End Class
