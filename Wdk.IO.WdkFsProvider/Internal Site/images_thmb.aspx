<%@ Page Language="VB" %>

<%
    Response.Clear()
  
    Dim th As New WDK.Imaging.NetTumbnailer
    
    Dim Width As Integer = 160
    If IsNumeric(Request("w")) Then Width = Request("w")
    
    Dim Height As Integer = 120
    If IsNumeric(Request("h")) Then Height = Request("h")
    
    th.ImageWidth = Width
    th.ImageHeight = Height
    
    Dim imgFiles As New WDK.ContentManagement.Galleries.Images
    Dim FileContent As Byte() = imgFiles.GetPropertyById(Request("id"), "FileContent")
    Dim ms As New IO.MemoryStream(FileContent)
    
    th.Stream = Response.OutputStream
    th.DrawImage(ms)
    th.Dispose()
    
    ms.Close()
    ms.Dispose()
    
 %>