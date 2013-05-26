<%@ Page Language="VB" %>
<%
    If Request("id") <> "" Then
        Dim dl As New WDK.ContentManagement.Galleries.Images
               
        Dim FileContent As Byte() = dl.GetPropertyById(Request("id"), "FileContent")
                
        If FileContent.Length > 0 Then
            Dim ContentType As String = dl.GetPropertyById(Request("id"), "ContentType")
            Response.ContentType = ContentType
            Response.AddHeader("Content-Type", ContentType)
            Response.Flush()
            
            Response.BinaryWrite(dl.GetPropertyById(Request("id"), "FileContent"))
            Response.End()
            
        Else
            Response.Write("File not found")
        End If
        
        dl = Nothing
    End If
%>