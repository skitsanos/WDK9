<%@ Page Language="VB" %>

<%
    Dim envPath As String = AppDomain.CurrentDomain.BaseDirectory & "db"
    If IO.Directory.Exists(envPath) = False Then IO.Directory.CreateDirectory(envPath)
    
    Try
        Dim xmldb As New WDK.Data.XmlDb
        xmldb.EnvironmentPath = envPath
    
        xmldb.CreateDatabase("demo.xmldb", True)
        
        Response.Write("Done")
        
    Catch ex As Exception
        Response.Write("<pre>" & ex.ToString.Replace(Chr(10), "<br/>") & "</pre>")
    End Try
 %>