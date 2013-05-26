Imports System.Xml.Serialization

<XmlRoot("Resume")> _
Public Class ResumeType

    <XmlElement("Applicant")> Public Applicant As ApplicantType
    <XmlArray("Education"), XmlArrayItem("Study")> Public Education As New EducationCollectionType
    <XmlElement("Experience")> Public Experience As New ExperienceType

#Region " GetXml "
    Public Function GetXml() As String
        Try
            Dim sb As New Text.StringBuilder
            Dim tw As IO.TextWriter = New IO.StringWriter(sb)

            Dim ser As New XmlSerializer(GetType(ResumeType))
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
            Dim ser As New XmlSerializer(GetType(ResumeType))
            ser.Serialize(xmlfile, Me)

        Catch ex As Exception
            Throw ex.InnerException
        End Try
    End Sub
#End Region

End Class
