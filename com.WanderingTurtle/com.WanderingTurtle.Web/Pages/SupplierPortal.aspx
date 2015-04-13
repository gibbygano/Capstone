<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierPortal.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierPortal" %>

<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Common" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
<div id="supplierdetails">
    <h2><%= _currentSupplier.CompanyName %></h2>
    


</div>
<div id="quickdetails">
    <h3>Quick Event Details</h3>
    You have: <%= currentListingCount %> Events Scheduled <br />
    with a total of <%=currentGuestsCount %> guests signed up.<br />
    View Details
</div>
<div id="actions"></div>

</asp:Content>
