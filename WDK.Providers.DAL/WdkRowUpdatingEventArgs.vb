Imports System
Imports System.Data
Imports System.Data.Common

Public Class WdkRowUpdatingEventArgs
    Inherits RowUpdatingEventArgs

    Public Sub New(ByVal row As DataRow, ByVal command As IDbCommand, ByVal statementType As StatementType, ByVal tableMapping As DataTableMapping)
        MyBase.New(row, command, statementType, tableMapping)
    End Sub

    ' Hide the inherited implementation of the command property. 
    Public Shadows Property Command() As WdkCommand
        Get
            Return CType(MyBase.Command, WdkCommand)
        End Get
        Set(ByVal value As WdkCommand)
            MyBase.Command = value
        End Set
    End Property
End Class