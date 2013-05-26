Imports System
Imports System.Collections
Imports System.ComponentModel

Public Module Declarations
    Public Const SchemaVersion As String = "http://www.topografix.com/GPX/1/1"
End Module

Public Class fixType
    Public Const TYPE_NONE As String = "none"
    Public Const TYPE_2D As String = "2d"
    Public Const TYPE_3D As String = "3d"
    Public Const TYPE_DGPS As String = "dgps"
    Public Const TYPE_PPS As String = "pps"
End Class


Public Class trksegTypeCollection
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As WDK.GIS.trksegType) As WDK.GIS.trksegType
        MyBase.Add(obj)
        Add = obj
    End Function

    Public Shadows Function Add() As WDK.GIS.trksegType
        Add = Add(New WDK.GIS.trksegType())
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As WDK.GIS.trksegType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As WDK.GIS.trksegType)
        MyBase.Remove(obj)
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As WDK.GIS.trksegType
        Get
            Item = DirectCast(MyBase.Item(index), WDK.GIS.trksegType)
        End Get
        Set(ByVal Value As WDK.GIS.trksegType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class

Public Class trkTypeCollection
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As WDK.GIS.trkType) As WDK.GIS.trkType
        MyBase.Add(obj)
        Add = obj
    End Function

    Public Shadows Function Add() As WDK.GIS.trkType
        Add = Add(New WDK.GIS.trkType())
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As WDK.GIS.trkType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As WDK.GIS.trkType)
        MyBase.Remove(obj)
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As WDK.GIS.trkType
        Get
            Item = DirectCast(MyBase.Item(index), WDK.GIS.trkType)
        End Get
        Set(ByVal Value As WDK.GIS.trkType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class

Public Class wptTypeCollection
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As WDK.GIS.wptType) As WDK.GIS.wptType
        MyBase.Add(obj)
        Add = obj
    End Function

    Public Shadows Function Add() As WDK.GIS.wptType
        Add = Add(New WDK.GIS.wptType())
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As WDK.GIS.wptType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As WDK.GIS.wptType)
        MyBase.Remove(obj)
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As WDK.GIS.wptType
        Get
            Item = DirectCast(MyBase.Item(index), WDK.GIS.wptType)
        End Get
        Set(ByVal Value As WDK.GIS.wptType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class

Public Class linkTypeCollection
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As WDK.GIS.linkType) As WDK.GIS.linkType
        MyBase.Add(obj)
        Add = obj
    End Function

    Public Shadows Function Add() As WDK.GIS.linkType
        Add = Add(New WDK.GIS.linkType())
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As WDK.GIS.linkType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As WDK.GIS.linkType)
        MyBase.Remove(obj)
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As WDK.GIS.linkType
        Get
            Item = DirectCast(MyBase.Item(index), WDK.GIS.linkType)
        End Get
        Set(ByVal Value As WDK.GIS.linkType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class

Public Class rteTypeCollection
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As WDK.GIS.rteType) As WDK.GIS.rteType
        MyBase.Add(obj)
        Add = obj
    End Function

    Public Shadows Function Add() As WDK.GIS.rteType
        Add = Add(New WDK.GIS.rteType())
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As WDK.GIS.rteType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As WDK.GIS.rteType)
        MyBase.Remove(obj)
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As WDK.GIS.rteType
        Get
            Item = DirectCast(MyBase.Item(index), WDK.GIS.rteType)
        End Get
        Set(ByVal Value As WDK.GIS.rteType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class

Public Class ptTypeCollection
    Inherits ArrayList

    Public Shadows Function Add(ByVal obj As WDK.GIS.ptType) As WDK.GIS.ptType
        MyBase.Add(obj)
        Add = obj
    End Function

    Public Shadows Function Add() As WDK.GIS.ptType
        Add = Add(New WDK.GIS.ptType())
    End Function

    Public Shadows Sub Insert(ByVal index As Integer, ByVal obj As WDK.GIS.ptType)
        MyBase.Insert(index, obj)
    End Sub

    Public Shadows Sub Remove(ByVal obj As WDK.GIS.ptType)
        MyBase.Remove(obj)
    End Sub

    Default Public Shadows Property Item(ByVal index As Integer) As WDK.GIS.ptType
        Get
            Item = DirectCast(MyBase.Item(index), WDK.GIS.ptType)
        End Get
        Set(ByVal Value As WDK.GIS.ptType)
            MyBase.Item(index) = Value
        End Set
    End Property
End Class

Public Class gpxType
    Private __version As String


    Public Property version() As String
        Get
            version = __version
        End Get
        Set(ByVal Value As String)
            __version = Value
        End Set
    End Property

    Private __creator As String

    Public Property creator() As String
        Get
            creator = __creator
        End Get
        Set(ByVal Value As String)
            __creator = Value
        End Set
    End Property

    Private __metadata As WDK.GIS.metadataType

    Public Property metadata() As WDK.GIS.metadataType
        Get
            If __metadata Is Nothing Then __metadata = New WDK.GIS.metadataType()
            metadata = __metadata
        End Get
        Set(ByVal Value As WDK.GIS.metadataType)
            __metadata = Value
        End Set
    End Property


    Private __wptCollection As wptTypeCollection

    Public Property wptCollection() As wptTypeCollection
        Get
            If __wptCollection Is Nothing Then __wptCollection = New wptTypeCollection()
            wptCollection = __wptCollection
        End Get
        Set(ByVal Value As wptTypeCollection)
            __wptCollection = Value
        End Set
    End Property

    Private __rteCollection As rteTypeCollection

    Public Property rteCollection() As rteTypeCollection
        Get
            If __rteCollection Is Nothing Then __rteCollection = New rteTypeCollection()
            rteCollection = __rteCollection
        End Get
        Set(ByVal Value As rteTypeCollection)
            __rteCollection = Value
        End Set
    End Property

    Private __trkCollection As trkTypeCollection

    Public Property trkCollection() As trkTypeCollection
        Get
            If __trkCollection Is Nothing Then __trkCollection = New trkTypeCollection()
            trkCollection = __trkCollection
        End Get
        Set(ByVal Value As trkTypeCollection)
            __trkCollection = Value
        End Set
    End Property

    Private __extensions As WDK.GIS.extensionsType

    Public Property extensions() As WDK.GIS.extensionsType
        Get
            If __extensions Is Nothing Then __extensions = New WDK.GIS.extensionsType()
            extensions = __extensions
        End Get
        Set(ByVal Value As WDK.GIS.extensionsType)
            __extensions = Value
        End Set
    End Property

    '*********************** Constructor ***********************
    Public Sub New()
    End Sub
End Class

Public Class metadataType

    Private __name As String

    Public Property name() As String
        Get
            name = __name
        End Get
        Set(ByVal Value As String)
            __name = Value
        End Set
    End Property

    Private __desc As String

    Public Property desc() As String
        Get
            desc = __desc
        End Get
        Set(ByVal Value As String)
            __desc = Value
        End Set
    End Property


    Private __author As WDK.GIS.personType

    Public Property author() As WDK.GIS.personType
        Get
            If __author Is Nothing Then __author = New WDK.GIS.personType()
            author = __author
        End Get
        Set(ByVal Value As WDK.GIS.personType)
            __author = Value
        End Set
    End Property

    Private __copyright As WDK.GIS.copyrightType

    Public Property copyright() As WDK.GIS.copyrightType
        Get
            If __copyright Is Nothing Then __copyright = New WDK.GIS.copyrightType()
            copyright = __copyright
        End Get
        Set(ByVal Value As WDK.GIS.copyrightType)
            __copyright = Value
        End Set
    End Property


    Private __linkCollection As linkTypeCollection


    Public Property linkCollection() As linkTypeCollection
        Get
            If __linkCollection Is Nothing Then __linkCollection = New linkTypeCollection()
            linkCollection = __linkCollection
        End Get
        Set(ByVal Value As linkTypeCollection)
            __linkCollection = Value
        End Set
    End Property

    Private __time As DateTime

    Private __timeSpecified As Boolean

    Public Property time() As DateTime
        Get
            time = __time
        End Get
        Set(ByVal Value As DateTime)
            __time = Value
            __timeSpecified = True
        End Set
    End Property

    Public Property timeUtc() As DateTime
        Get
            timeUtc = __time.ToUniversalTime()
        End Get
        Set(ByVal Value As DateTime)
            __time = Value.ToLocalTime()
            __timeSpecified = True
        End Set
    End Property


    Private __keywords As String

    Public Property keywords() As String
        Get
            keywords = __keywords
        End Get
        Set(ByVal Value As String)
            __keywords = Value
        End Set
    End Property

    Private __bounds As WDK.GIS.boundsType

    Public Property bounds() As WDK.GIS.boundsType
        Get
            If __bounds Is Nothing Then __bounds = New WDK.GIS.boundsType()
            bounds = __bounds
        End Get
        Set(ByVal Value As WDK.GIS.boundsType)
            __bounds = Value
        End Set
    End Property

    Private __extensions As WDK.GIS.extensionsType

    Public Property extensions() As WDK.GIS.extensionsType
        Get
            If __extensions Is Nothing Then __extensions = New WDK.GIS.extensionsType()
            extensions = __extensions
        End Get
        Set(ByVal Value As WDK.GIS.extensionsType)
            __extensions = Value
        End Set
    End Property

    '*********************** Constructor ***********************
    Public Sub New()
        __time = DateTime.Now
    End Sub
End Class


Public Class wptType

    Private __lat As Decimal

    Private __latSpecified As Boolean

    Public Property lat() As Decimal
        Get
            lat = __lat
        End Get
        Set(ByVal Value As Decimal)
            __lat = Value
            __latSpecified = True
        End Set
    End Property


    Private __lon As Decimal

    Private __lonSpecified As Boolean

    Public Property lon() As Decimal
        Get
            lon = __lon
        End Get
        Set(ByVal Value As Decimal)
            __lon = Value
            __lonSpecified = True
        End Set
    End Property


    Private __ele As Decimal

    Private __eleSpecified As Boolean

    Public Property ele() As Decimal
        Get
            ele = __ele
        End Get
        Set(ByVal Value As Decimal)
            __ele = Value
            __eleSpecified = True
        End Set
    End Property

    Private __time As DateTime

    Private __timeSpecified As Boolean


    Public Property time() As DateTime
        Get
            time = __time
        End Get
        Set(ByVal Value As DateTime)
            __time = Value
            __timeSpecified = True
        End Set
    End Property

    Public Property timeUtc() As DateTime
        Get
            timeUtc = __time.ToUniversalTime()
        End Get
        Set(ByVal Value As DateTime)
            __time = Value.ToLocalTime()
            __timeSpecified = True
        End Set
    End Property


    Private __magvar As Decimal


    Private __magvarSpecified As Boolean

    Public Property magvar() As Decimal
        Get
            magvar = __magvar
        End Get
        Set(ByVal Value As Decimal)
            __magvar = Value
            __magvarSpecified = True
        End Set
    End Property


    Private __geoidheight As Decimal

    Private __geoidheightSpecified As Boolean


    Public Property geoidheight() As Decimal
        Get
            geoidheight = __geoidheight
        End Get
        Set(ByVal Value As Decimal)
            __geoidheight = Value
            __geoidheightSpecified = True
        End Set
    End Property


    Private __name As String


    Public Property name() As String
        Get
            name = __name
        End Get
        Set(ByVal Value As String)
            __name = Value
        End Set
    End Property

    Private __cmt As String

    Public Property cmt() As String
        Get
            cmt = __cmt
        End Get
        Set(ByVal Value As String)
            __cmt = Value
        End Set
    End Property

    Private __desc As String

    Public Property desc() As String
        Get
            desc = __desc
        End Get
        Set(ByVal Value As String)
            __desc = Value
        End Set
    End Property

    Private __src As String

    Public Property src() As String
        Get
            src = __src
        End Get
        Set(ByVal Value As String)
            __src = Value
        End Set
    End Property


    Private __linkCollection As linkTypeCollection

    Public Property linkCollection() As linkTypeCollection
        Get
            If __linkCollection Is Nothing Then __linkCollection = New linkTypeCollection()
            linkCollection = __linkCollection
        End Get
        Set(ByVal Value As linkTypeCollection)
            __linkCollection = Value
        End Set
    End Property

    Private __sym As String

    Public Property sym() As String
        Get
            sym = __sym
        End Get
        Set(ByVal Value As String)
            __sym = Value
        End Set
    End Property

    Private __type As String

    Public Property type() As String
        Get
            type = __type
        End Get
        Set(ByVal Value As String)
            __type = Value
        End Set
    End Property


    Private __fix As String

    Private __fixSpecified As Boolean

    Public Property fix() As String
        Get
            fix = __fix
        End Get
        Set(ByVal Value As String)
            __fix = Value
            __fixSpecified = True
        End Set
    End Property

    Private __sat As String

    Public Property sat() As String
        Get
            sat = __sat
        End Get
        Set(ByVal Value As String)
            __sat = Value
        End Set
    End Property

    Private __hdop As Decimal


    Private __hdopSpecified As Boolean


    Public Property hdop() As Decimal
        Get
            hdop = __hdop
        End Get
        Set(ByVal Value As Decimal)
            __hdop = Value
            __hdopSpecified = True
        End Set
    End Property

    Private __vdop As Decimal

    Private __vdopSpecified As Boolean


    Public Property vdop() As Decimal
        Get
            vdop = __vdop
        End Get
        Set(ByVal Value As Decimal)
            __vdop = Value
            __vdopSpecified = True
        End Set
    End Property


    Private __pdop As Decimal


    Private __pdopSpecified As Boolean

    Public Property pdop() As Decimal
        Get
            pdop = __pdop
        End Get
        Set(ByVal Value As Decimal)
            __pdop = Value
            __pdopSpecified = True
        End Set
    End Property


    Private __ageofdgpsdata As Decimal

    Private __ageofdgpsdataSpecified As Boolean

    Public Property ageofdgpsdata() As Decimal
        Get
            ageofdgpsdata = __ageofdgpsdata
        End Get
        Set(ByVal Value As Decimal)
            __ageofdgpsdata = Value
            __ageofdgpsdataSpecified = True
        End Set
    End Property

    Private __dgpsid As String

    Public Property dgpsid() As String
        Get
            dgpsid = __dgpsid
        End Get
        Set(ByVal Value As String)
            __dgpsid = Value
        End Set
    End Property

    Private __extensions As WDK.GIS.extensionsType

    Public Property extensions() As WDK.GIS.extensionsType
        Get
            If __extensions Is Nothing Then __extensions = New WDK.GIS.extensionsType()
            extensions = __extensions
        End Get
        Set(ByVal Value As WDK.GIS.extensionsType)
            __extensions = Value
        End Set
    End Property

    '*********************** Constructor ***********************
    Public Sub New()
        __time = DateTime.Now
    End Sub
End Class


Public Class rteType

    Private __name As String

    Public Property name() As String
        Get
            name = __name
        End Get
        Set(ByVal Value As String)
            __name = Value
        End Set
    End Property

    Private __cmt As String

    Public Property cmt() As String
        Get
            cmt = __cmt
        End Get
        Set(ByVal Value As String)
            __cmt = Value
        End Set
    End Property


    Private __desc As String

    Public Property desc() As String
        Get
            desc = __desc
        End Get
        Set(ByVal Value As String)
            __desc = Value
        End Set
    End Property

    Private __src As String

    Public Property src() As String
        Get
            src = __src
        End Get
        Set(ByVal Value As String)
            __src = Value
        End Set
    End Property

    Private __linkCollection As linkTypeCollection

    Public Property linkCollection() As linkTypeCollection
        Get
            If __linkCollection Is Nothing Then __linkCollection = New linkTypeCollection()
            linkCollection = __linkCollection
        End Get
        Set(ByVal Value As linkTypeCollection)
            __linkCollection = Value
        End Set
    End Property

    Private __number As String


    Public Property number() As String
        Get
            number = __number
        End Get
        Set(ByVal Value As String)
            __number = Value
        End Set
    End Property

    Private __type As String


    Public Property type() As String
        Get
            type = __type
        End Get
        Set(ByVal Value As String)
            __type = Value
        End Set
    End Property

    Private __extensions As WDK.GIS.extensionsType


    Public Property extensions() As WDK.GIS.extensionsType
        Get
            If __extensions Is Nothing Then __extensions = New WDK.GIS.extensionsType()
            extensions = __extensions
        End Get
        Set(ByVal Value As WDK.GIS.extensionsType)
            __extensions = Value
        End Set
    End Property

    Private __rteptCollection As wptTypeCollection


    Public Property rteptCollection() As wptTypeCollection
        Get
            If __rteptCollection Is Nothing Then __rteptCollection = New wptTypeCollection()
            rteptCollection = __rteptCollection
        End Get
        Set(ByVal Value As wptTypeCollection)
            __rteptCollection = Value
        End Set
    End Property

    '*********************** Constructor ***********************
    Public Sub New()
    End Sub
End Class


Public Class trkType

    Private __name As String


    Public Property name() As String
        Get
            name = __name
        End Get
        Set(ByVal Value As String)
            __name = Value
        End Set
    End Property


    Private __cmt As String


    Public Property cmt() As String
        Get
            cmt = __cmt
        End Get
        Set(ByVal Value As String)
            __cmt = Value
        End Set
    End Property

    Private __desc As String


    Public Property desc() As String
        Get
            desc = __desc
        End Get
        Set(ByVal Value As String)
            __desc = Value
        End Set
    End Property


    Private __src As String


    Public Property src() As String
        Get
            src = __src
        End Get
        Set(ByVal Value As String)
            __src = Value
        End Set
    End Property

    Private __linkCollection As linkTypeCollection


    Public Property linkCollection() As linkTypeCollection
        Get
            If __linkCollection Is Nothing Then __linkCollection = New linkTypeCollection()
            linkCollection = __linkCollection
        End Get
        Set(ByVal Value As linkTypeCollection)
            __linkCollection = Value
        End Set
    End Property

    Private __number As String


    Public Property number() As String
        Get
            number = __number
        End Get
        Set(ByVal Value As String)
            __number = Value
        End Set
    End Property


    Private __type As String


    Public Property type() As String
        Get
            type = __type
        End Get
        Set(ByVal Value As String)
            __type = Value
        End Set
    End Property


    Private __extensions As WDK.GIS.extensionsType


    Public Property extensions() As WDK.GIS.extensionsType
        Get
            If __extensions Is Nothing Then __extensions = New WDK.GIS.extensionsType()
            extensions = __extensions
        End Get
        Set(ByVal Value As WDK.GIS.extensionsType)
            __extensions = Value
        End Set
    End Property

    Private __trksegCollection As trksegTypeCollection


    Public Property trksegCollection() As trksegTypeCollection
        Get
            If __trksegCollection Is Nothing Then __trksegCollection = New trksegTypeCollection()
            trksegCollection = __trksegCollection
        End Get
        Set(ByVal Value As trksegTypeCollection)
            __trksegCollection = Value
        End Set
    End Property

End Class

Public Class extensionsType
    Public Any() As Object
End Class


Public Class trksegType

    Private __trkptCollection As wptTypeCollection


    Public Property trkptCollection() As wptTypeCollection
        Get
            If __trkptCollection Is Nothing Then __trkptCollection = New wptTypeCollection()
            trkptCollection = __trkptCollection
        End Get
        Set(ByVal Value As wptTypeCollection)
            __trkptCollection = Value
        End Set
    End Property

    Private __extensions As WDK.GIS.extensionsType

    Public Property extensions() As WDK.GIS.extensionsType
        Get
            If __extensions Is Nothing Then __extensions = New WDK.GIS.extensionsType()
            extensions = __extensions
        End Get
        Set(ByVal Value As WDK.GIS.extensionsType)
            __extensions = Value
        End Set
    End Property

End Class


Public Class copyrightType

    Private __author As String


    Public Property author() As String
        Get
            author = __author
        End Get
        Set(ByVal Value As String)
            __author = Value
        End Set
    End Property

    Private __year As String


    Public Property year() As String
        Get
            year = __year
        End Get
        Set(ByVal Value As String)
            __year = Value
        End Set
    End Property

    Private __license As String


    Public Property license() As String
        Get
            license = __license
        End Get
        Set(ByVal Value As String)
            __license = Value
        End Set
    End Property

End Class


Public Class linkType

    Private __href As String


    Public Property href() As String
        Get
            href = __href
        End Get
        Set(ByVal Value As String)
            __href = Value
        End Set
    End Property

    Private __text As String


    Public Property text() As String
        Get
            text = __text
        End Get
        Set(ByVal Value As String)
            __text = Value
        End Set
    End Property

    Private __type As String


    Public Property type() As String
        Get
            type = __type
        End Get
        Set(ByVal Value As String)
            __type = Value
        End Set
    End Property

End Class



Public Class emailType

    Private __id As String


    Public Property id() As String
        Get
            id = __id
        End Get
        Set(ByVal Value As String)
            __id = Value
        End Set
    End Property


    Private __domain As String


    Public Property domain() As String
        Get
            domain = __domain
        End Get
        Set(ByVal Value As String)
            __domain = Value
        End Set
    End Property

End Class

Public Class personType

    Private __name As String


    Public Property name() As String
        Get
            name = __name
        End Get
        Set(ByVal Value As String)
            __name = Value
        End Set
    End Property

    Private __email As WDK.GIS.emailType


    Public Property email() As WDK.GIS.emailType
        Get
            If __email Is Nothing Then __email = New WDK.GIS.emailType()
            email = __email
        End Get
        Set(ByVal Value As WDK.GIS.emailType)
            __email = Value
        End Set
    End Property


    Private __link As WDK.GIS.linkType


    Public Property link() As WDK.GIS.linkType
        Get
            If __link Is Nothing Then __link = New WDK.GIS.linkType()
            link = __link
        End Get
        Set(ByVal Value As WDK.GIS.linkType)
            __link = Value
        End Set
    End Property

End Class


Public Class ptType

    Private __lat As Decimal

    Private __latSpecified As Boolean


    Public Property lat() As Decimal
        Get
            lat = __lat
        End Get
        Set(ByVal Value As Decimal)
            __lat = Value
            __latSpecified = True
        End Set
    End Property


    Private __lon As Decimal

    Private __lonSpecified As Boolean


    Public Property lon() As Decimal
        Get
            lon = __lon
        End Get
        Set(ByVal Value As Decimal)
            __lon = Value
            __lonSpecified = True
        End Set
    End Property

    Private __ele As Decimal

    Private __eleSpecified As Boolean


    Public Property ele() As Decimal
        Get
            ele = __ele
        End Get
        Set(ByVal Value As Decimal)
            __ele = Value
            __eleSpecified = True
        End Set
    End Property

    Private __time As DateTime

    Private __timeSpecified As Boolean


    Public Property time() As DateTime
        Get
            time = __time
        End Get
        Set(ByVal Value As DateTime)
            __time = Value
            __timeSpecified = True
        End Set
    End Property


    Public Property timeUtc() As DateTime
        Get
            timeUtc = __time.ToUniversalTime()
        End Get
        Set(ByVal Value As DateTime)
            __time = Value.ToLocalTime()
            __timeSpecified = True
        End Set
    End Property

    '*********************** Constructor ***********************
    Public Sub New()
        __time = DateTime.Now
    End Sub
End Class

Public Class ptsegType

    <System.Runtime.InteropServices.DispIdAttribute(-4)> _
    Public Function GetEnumerator() As IEnumerator
        GetEnumerator = ptCollection.GetEnumerator()
    End Function

    Public Function Add(ByVal obj As WDK.GIS.ptType) As WDK.GIS.ptType
        Add = ptCollection.Add(obj)
    End Function


    Default Public ReadOnly Property Item(ByVal index As Integer) As WDK.GIS.ptType
        Get
            Item = ptCollection(index)
        End Get
    End Property


    Public ReadOnly Property Count() As Integer
        Get
            Count = ptCollection.Count
        End Get
    End Property

    Public Sub Clear()
        ptCollection.Clear()
    End Sub

    Public Function Remove(ByVal index As Integer) As WDK.GIS.ptType
        Dim obj As WDK.GIS.ptType
        obj = ptCollection(index)
        Remove = obj
        ptCollection.Remove(obj)
    End Function

    Public Sub Remove(ByVal obj As Object)
        ptCollection.Remove(obj)
    End Sub

    Private __ptCollection As ptTypeCollection


    Public Property ptCollection() As ptTypeCollection
        Get
            If __ptCollection Is Nothing Then __ptCollection = New ptTypeCollection()
            ptCollection = __ptCollection
        End Get
        Set(ByVal Value As ptTypeCollection)
            __ptCollection = Value
        End Set
    End Property


End Class


Public Class boundsType

    Private __minlat As Decimal

    Private __minlatSpecified As Boolean


    Public Property minlat() As Decimal
        Get
            minlat = __minlat
        End Get
        Set(ByVal Value As Decimal)
            __minlat = Value
            __minlatSpecified = True
        End Set
    End Property

    Private __minlon As Decimal

    Private __minlonSpecified As Boolean


    Public Property minlon() As Decimal
        Get
            minlon = __minlon
        End Get
        Set(ByVal Value As Decimal)
            __minlon = Value
            __minlonSpecified = True
        End Set
    End Property

    Private __maxlat As Decimal

    Private __maxlatSpecified As Boolean


    Public Property maxlat() As Decimal
        Get
            maxlat = __maxlat
        End Get
        Set(ByVal Value As Decimal)
            __maxlat = Value
            __maxlatSpecified = True
        End Set
    End Property

    Private __maxlon As Decimal

    Private __maxlonSpecified As Boolean


    Public Property maxlon() As Decimal
        Get
            maxlon = __maxlon
        End Get
        Set(ByVal Value As Decimal)
            __maxlon = Value
            __maxlonSpecified = True
        End Set
    End Property

End Class