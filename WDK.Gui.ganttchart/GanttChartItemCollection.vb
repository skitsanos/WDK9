Option Strict On
Option Explicit On 

Imports System

<Serializable()> _
Public Class GanttChartItemCollection
    Inherits System.Collections.CollectionBase

#Region " Properties "
    Private _Owner As GanttChart

    Private Property Owner() As GanttChart
        Get
            Return CType(_Owner, GanttChart)
        End Get
        Set(ByVal Value As GanttChart)
            _Owner = Value
        End Set
    End Property

    Default Public ReadOnly Property Item(ByVal index As Int32) As GanttChartItem
        Get
            Return CType(List.Item(index), GanttChartItem)
        End Get
    End Property
#End Region

#Region " Constructors "
    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal Owner As GanttChart)
        MyBase.New()
        _Owner = Owner
    End Sub
#End Region

#Region " Add() "
    Public Function Add(ByVal Item As GanttChartItem) As Integer
        Return List.Add(Item)
    End Function
#End Region

End Class
