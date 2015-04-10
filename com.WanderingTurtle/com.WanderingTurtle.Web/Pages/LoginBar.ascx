<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginBar.ascx.cs" Inherits="com.WanderingTurtle.Web.Pages.LoginBar" %>
<%@ Import Namespace="com.WanderingTurtle.Web.Pages" %>
<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.Web" %>

<% if (_currentSupplier != null)
   {
       supplierName = _currentSupplier.FirstName + " " + _currentSupplier.LastName;
   } %>
<div id="userLoggedIn" runat="server" style="display: none;">Welcome <%= supplierName %></div>
<div id="userLoggedOut" runat="server" style="display: none;"><a href="../login">Login</a> <a href="application">Submit an Application</a></div>
