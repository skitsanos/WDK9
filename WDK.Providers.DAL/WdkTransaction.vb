Imports System
Imports System.Data

Public Class WdkTransaction
    Implements IDbTransaction

    Public Sub Commit() Implements IDbTransaction.Commit

    End Sub

    Public ReadOnly Property Connection() As IDbConnection Implements IDbTransaction.Connection
        Get

        End Get
    End Property

    Public ReadOnly Property IsolationLevel() As IsolationLevel Implements IDbTransaction.IsolationLevel
        Get

        End Get
    End Property

    Public Sub Rollback() Implements IDbTransaction.Rollback

    End Sub

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
