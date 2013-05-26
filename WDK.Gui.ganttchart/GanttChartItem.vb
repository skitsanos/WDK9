Imports System
Imports System.Data
Imports System.Web.UI
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.ComponentModel.Design.Serialization
Imports System.Globalization
Imports Microsoft.VisualBasic

<Serializable(), PersistenceMode(PersistenceMode.InnerDefaultProperty)> Public Class GanttChartItem

#Region " Properties "
    Private _Task As String = ""
    <DefaultValue(""), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> Public Property Task() As String
        Get
            Return _Task
        End Get
        Set(ByVal Value As String)
            _Task = Value
        End Set
    End Property

    Private _StartDate As DateTime = Now
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> Public Property StartDate() As DateTime
        Get
            Return _StartDate
        End Get
        Set(ByVal Value As DateTime)
            _StartDate = Value
        End Set
    End Property

    Private _EndDate As DateTime = Now
    <DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)> Public Property EndDate() As DateTime
        Get
            Return _EndDate
        End Get
        Set(ByVal Value As DateTime)
            _EndDate = Value
        End Set
    End Property

#End Region

#Region " Constructor "
    Sub New()
        MyBase.New()
    End Sub
#End Region

End Class
