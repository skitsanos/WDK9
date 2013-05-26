<%@ Page Language="VB" ContentType="text/html" ResponseEncoding="utf-8" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>doxAgent</title>
<style type="text/css" media="all">
<!--
@import url("site.css");
-->
</style>
</head>
<body>
<table width="90%" border="0" align="center" cellpadding="3" cellspacing="0">
  <tr>
    <td class="logo"><img src="images/logo.jpg" alt="doxAgent" width="200" height="50" /></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>Your search for <strong><%=Request("q")%></strong> returned:</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
</body>
</html>
