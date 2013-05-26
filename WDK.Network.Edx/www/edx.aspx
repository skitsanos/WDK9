<%@ Page Language="VB" %>
<%@ Import Namespace="System.Xml" %>

<script runat="server">
    Dim xmlDoc As New XmlDocument
    Dim xmlResp As XmlElement
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        xmlResp = xmlDoc.CreateElement("Response")
        xmlDoc.AppendChild(xmlResp)
        
        Try
            Dim xmlReq As New XmlDocument
            xmlReq.Load(Request.InputStream)
            
            Dim xmlAction As XmlElement = xmlReq.SelectSingleNode("/EdxMessage/Headers")
            If xmlAction Is Nothing Then
                xmlResp.SetAttribute("status", 500)
                xmlResp.SetAttribute("message", "There is no action specified in source Edx Message")
            Else
                Select Case xmlAction.GetAttribute("action")
                    Case "storefile"
                        
                    Case Else
                        xmlResp.SetAttribute("status", 501)
                        xmlResp.SetAttribute("message", "Handler not Implemented")
            
                End Select
            End If
      
        Catch ex As Exception
            xmlResp.SetAttribute("status", 200)
            xmlResp.SetAttribute("message", ex.Message)
        End Try
        
            
        Response.Clear()
        Response.Write(xmlDoc.OuterXml)
        Response.End()
        
    End Sub
</script>

