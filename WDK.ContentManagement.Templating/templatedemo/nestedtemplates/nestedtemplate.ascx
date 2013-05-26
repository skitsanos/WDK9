<%@ Control Language="c#" AutoEventWireup="false" Codebehind="nestedtemplate.ascx.cs" Inherits="templatedemo.nestedtemplates.nestedtemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="epf" Namespace="Evolve.Portals.Framework" Assembly="Evolve.Portals.TemplateEngine" %>
<div class="contentcell" style="BORDER-TOP:black 1px solid;BACKGROUND-COLOR:#f2f0f0"><p>This 
    is the nested template. It provides the template region <EM><STRONG>Content</STRONG></EM>
    .<BR>
    Important: This nested template is a standard UserControl. Unlike the parent 
    template, this control&nbsp;<FONT color="#cc0000">must not derive from the <EM>PortalTemplate</EM>
      class</FONT>. This is because <EM>ContentPlaceHolder</EM> controls search the 
    control tree for a <EM>PortalTemplate</EM> to register themselves. As this 
    nested template does not derive from <EM>PortalTemplate</EM>, the placeholder 
    registers itself with the parent template.</p>
  <P><STRONG>Received content of the master page:</STRONG></P>
  <P>
    <epf:RegionPlaceHolder id="ContentPlaceHolder" runat="server" RegionId="Content"></epf:RegionPlaceHolder></P>
</div>
