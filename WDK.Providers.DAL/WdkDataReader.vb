Imports System
Imports System.Data
Imports System.Globalization

#Region " MSDN Notes "
' http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpguide/html/cpcontemplateparametervb.asp
#End Region
Public Class WdkDataReader
    Implements IDataReader

#Region " Properties "
    Private dr As IDataReader

#End Region

#Region " New "
    Public Sub New()
        MyBase.New()
    End Sub
#End Region

#Region " Close "
    Public Sub Close() Implements IDataReader.Close
        dr.Close()
    End Sub
#End Region

#Region " Depth "
    Public ReadOnly Property Depth() As Integer Implements IDataReader.Depth
        Get
            Return dr.Depth
        End Get
    End Property
#End Region

#Region " GetSchemaTable "
    Public Function GetSchemaTable() As DataTable Implements IDataReader.GetSchemaTable
        Return dr.GetSchemaTable
    End Function
#End Region

#Region " IsClosed "
    Public ReadOnly Property IsClosed() As Boolean Implements IDataReader.IsClosed
        Get
            Return dr.IsClosed
        End Get
    End Property
#End Region

#Region " NextResult "
    Public Function NextResult() As Boolean Implements IDataReader.NextResult
        dr.NextResult()
    End Function
#End Region

#Region " Read "
    Public Function Read() As Boolean Implements IDataReader.Read
        Return dr.Read
    End Function
#End Region

#Region " RecordsAffected "
    Public ReadOnly Property RecordsAffected() As Integer Implements IDataReader.RecordsAffected
        Get
            Return dr.RecordsAffected
        End Get
    End Property
#End Region

#Region " FieldCount "
    Public ReadOnly Property FieldCount() As Integer Implements IDataRecord.FieldCount
        Get
            Return dr.FieldCount
        End Get
    End Property
#End Region

#Region " GetBoolean "
    Public Function GetBoolean(ByVal i As Integer) As Boolean Implements IDataRecord.GetBoolean
        Return dr.GetBoolean(i)
    End Function
#End Region

#Region " GetByte "
    Public Function GetByte(ByVal i As Integer) As Byte Implements IDataRecord.GetByte
        Return dr.GetByte(i)
    End Function
#End Region

#Region " GetBytes "
    Public Function GetBytes(ByVal i As Integer, ByVal fieldOffset As Long, ByVal buffer() As Byte, ByVal bufferoffset As Integer, ByVal length As Integer) As Long Implements IDataRecord.GetBytes
        Return dr.GetBytes(i, fieldOffset, buffer, bufferoffset, length)
    End Function
#End Region

#Region " GetChar "
    Public Function GetChar(ByVal i As Integer) As Char Implements IDataRecord.GetChar
        Return dr.GetChar(i)
    End Function
#End Region

#Region " GetChars "
    Public Function GetChars(ByVal i As Integer, ByVal fieldoffset As Long, ByVal buffer() As Char, ByVal bufferoffset As Integer, ByVal length As Integer) As Long Implements IDataRecord.GetChars
        Return dr.GetChars(i, fieldoffset, buffer, bufferoffset, length)
    End Function
#End Region

#Region " GetData "
    Public Function GetData(ByVal i As Integer) As IDataReader Implements IDataRecord.GetData
        Return dr.GetData(i)
    End Function
#End Region

#Region " GetDataTypeName "
    Public Function GetDataTypeName(ByVal i As Integer) As String Implements IDataRecord.GetDataTypeName
        Return dr.GetDataTypeName(i)
    End Function
#End Region

#Region "  GetDateTime"
    Public Function GetDateTime(ByVal i As Integer) As Date Implements IDataRecord.GetDateTime
        Return dr.GetDateTime(i)
    End Function
#End Region

#Region " GetDecimal "
    Public Function GetDecimal(ByVal i As Integer) As Decimal Implements IDataRecord.GetDecimal
        Return dr.GetDecimal(i)
    End Function
#End Region

#Region " GetDouble "
    Public Function GetDouble(ByVal i As Integer) As Double Implements IDataRecord.GetDouble
        Return dr.GetDouble(i)
    End Function
#End Region

#Region " GetFieldType "
    Public Function GetFieldType(ByVal i As Integer) As Type Implements IDataRecord.GetFieldType
        Return dr.GetFieldType(i)
    End Function
#End Region

#Region " GetFloat "
    Public Function GetFloat(ByVal i As Integer) As Single Implements IDataRecord.GetFloat
        Return dr.GetFloat(i)
    End Function
#End Region

#Region " GetGuid "
    Public Function GetGuid(ByVal i As Integer) As Guid Implements IDataRecord.GetGuid
        Return dr.GetGuid(i)
    End Function
#End Region

#Region " GetInt16 "
    Public Function GetInt16(ByVal i As Integer) As Short Implements IDataRecord.GetInt16
        Return dr.GetInt16(i)
    End Function
#End Region

#Region " GetInt32 "
    Public Function GetInt32(ByVal i As Integer) As Integer Implements IDataRecord.GetInt32
        Return dr.GetInt32(i)
    End Function
#End Region

#Region " GetInt64 "
    Public Function GetInt64(ByVal i As Integer) As Long Implements IDataRecord.GetInt64
        Return dr.GetInt64(i)
    End Function
#End Region

#Region " GetName "
    Public Function GetName(ByVal i As Integer) As String Implements IDataRecord.GetName
        Return dr.GetName(i)
    End Function
#End Region

#Region "  GetOrdinal "
    Public Function GetOrdinal(ByVal name As String) As Integer Implements IDataRecord.GetOrdinal
        Return dr.GetOrdinal(name)
    End Function
#End Region

#Region " GetString "
    Public Function GetString(ByVal i As Integer) As String Implements IDataRecord.GetString
        Return dr.GetString(i)
    End Function
#End Region

#Region " GetValue "
    Public Function GetValue(ByVal i As Integer) As Object Implements IDataRecord.GetValue
        Return dr.GetValue(i)
    End Function
#End Region

#Region " GetValues "
    Public Function GetValues(ByVal values() As Object) As Integer Implements IDataRecord.GetValues
        Return dr.GetValues(values)
    End Function
#End Region

#Region " IsDBNull "
    Public Function IsDBNull(ByVal i As Integer) As Boolean Implements IDataRecord.IsDBNull
        Return dr.IsDBNull(i)
    End Function
#End Region

#Region " Item "
    Default Public Overloads ReadOnly Property Item(ByVal i As Integer) As Object Implements IDataRecord.Item
        Get
            Return dr.Item(i)
        End Get
    End Property
#End Region

#Region " Item {name}"
    Default Public Overloads ReadOnly Property Item(ByVal name As String) As Object Implements IDataRecord.Item
        Get
            Return dr.Item(name)
        End Get
    End Property
#End Region

#Region " Dispose "
    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
            dr.Dispose()
        End If
        Me.disposedValue = True
    End Sub
#End Region

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class