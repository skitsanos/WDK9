Imports System
Imports System.Data
Imports System.Data.Common

Public Delegate Sub WdkRowUpdatingEventHandler(ByVal sender As Object, ByVal e As WdkRowUpdatingEventArgs)
Public Delegate Sub WdkRowUpdatedEventHandler(ByVal sender As Object, ByVal e As WdkRowUpdatedEventArgs)


    Public Class TemplateDataAdapter
        Inherits DbDataAdapter
        Implements IDbDataAdapter

#Region " Events "
    Public Event RowUpdating As WdkRowUpdatingEventHandler
    Public Event RowUpdated As WdkRowUpdatedEventHandler

    Private Shared ReadOnly EventRowUpdated As Object = New Object()
    Private Shared ReadOnly EventRowUpdating As Object = New Object()
#End Region

#Region " New "
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

#Region " CreateRowUpdatedEvent "
    Protected Overrides Function CreateRowUpdatedEvent(ByVal dataRow As DataRow, ByVal command As IDbCommand, ByVal statementType As StatementType, ByVal tableMapping As DataTableMapping) As RowUpdatedEventArgs
        Return New WdkRowUpdatedEventArgs(dataRow, command, statementType, tableMapping)
    End Function
#End Region

#Region " CreateRowUpdatingEvent "
    Protected Overrides Function CreateRowUpdatingEvent(ByVal dataRow As DataRow, ByVal command As IDbCommand, ByVal statementType As StatementType, ByVal tableMapping As DataTableMapping) As RowUpdatingEventArgs
        Return New WdkRowUpdatingEventArgs(dataRow, command, statementType, tableMapping)
    End Function
#End Region

#Region " OnRowUpdating "
    Protected Overrides Sub OnRowUpdating(ByVal value As RowUpdatingEventArgs)
        Dim handler As WdkRowUpdatingEventHandler = CType(Events(EventRowUpdating), WdkRowUpdatingEventHandler)
        If Not handler Is Nothing And value.GetType() Is Type.GetType("WdkRowUpdatingEventArgs") Then
            handler(Me, CType(value, WdkRowUpdatingEventArgs))
        End If
    End Sub
#End Region

#Region " OnRowUpdated "
    Protected Overrides Sub OnRowUpdated(ByVal value As RowUpdatedEventArgs)
        Dim handler As WdkRowUpdatedEventHandler = CType(Events(EventRowUpdated), WdkRowUpdatedEventHandler)
        If Not handler Is Nothing And value.GetType() Is Type.GetType("WdkRowUpdatedEventArgs") Then
            handler(Me, CType(value, WdkRowUpdatedEventArgs))
        End If
    End Sub
#End Region

End Class

