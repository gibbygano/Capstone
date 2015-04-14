<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBar.ascx.cs" Inherits="com.WanderingTurtle.Web.Pages.LoginBar" %>
<%@ Import Namespace="com.WanderingTurtle.Web.Pages" %>
<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.Web" %>

<% if (_currentSupplier != null)
   {
       supplierName = _currentSupplier.FirstName + " " + _currentSupplier.LastName;
   } %>
<div id="loginBar">
<div id="userLoggedIn" runat="server" style="display: none;"><div id="supplierName" style="float: left">Welcome <%= supplierName %>! </div><div id="logout"><a href="../logout">Logout</a></div><div class="clear"></div></div>
<div id="userLoggedOut" runat="server" style="display: none;"><a href="../application">Submit an Application</a><div id="loginlink"><a href="../login">Login</a></div> </div>
<div class="clear"></div>
</div>