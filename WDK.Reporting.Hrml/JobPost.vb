Imports System.Xml.Serialization

'<XmlRootAttribute("JobPost", _
' Namespace:="http://www.arachnoware.com/hr", IsNullable:=False)> _
<XmlRoot("JobPost")> _
Public Class JobPostType

#Region " Properties "
    <XmlElement(isnullable:=False)> Public Company As CompanyDataType
    <XmlElement(isnullable:=False)> Public Recruiter As RecruiterType
    <XmlArrayAttribute("Jobs"), XmlArrayItem("Job")> Public Jobs As New JobsCollectionType
#End Region

#Region " GetXml "
    Public Function GetXml() As String
        Try
            Dim sb As New Text.StringBuilder
            Dim tw As IO.TextWriter = New IO.StringWriter(sb)

            Dim ser As New XmlSerializer(GetType(JobPostType))
            ser.Serialize(tw, Me)

            Return sb.ToString
        Catch ex As Exception
            Throw ex.InnerException
        End Try
    End Function
#End Region

#Region " Save "
    Public Sub Save(ByVal File As String)
        Try
            Dim xmlfile As New System.IO.StreamWriter(File)
            Dim ser As New XmlSerializer(GetType(JobPostType))
            ser.Serialize(xmlfile, Me)
            xmlfile.Close()

        Catch ex As Exception
            Throw ex.InnerException
        End Try
    End Sub
#End Region


End Class
