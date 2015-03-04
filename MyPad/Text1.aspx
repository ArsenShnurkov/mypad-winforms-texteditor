<% @PAGE Inherits="System.Web.UI.Page" Language="C#" %>
<!DOCTYPE html>
<html>
<head>
<meta charset="utf8" />
<title><% =Request["title"] %></title>
</head>
<body>

<table><tr><td valign="top">
<h1><% =Request["header"] %></h1>
</td><td valign="top">
<% =Request["links"] %>
<br />
&nbsp;
</td></tr></table>


</body>
</html>
