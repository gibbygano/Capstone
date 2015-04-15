<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierPortal.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierPortal" %>

<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <div id="leftcontainer">
<div id="supplierdetails">
    <h2><%= _currentSupplier.CompanyName %></h2>
    


</div>
<div id="quickdetails">
    <h3>Quick Event Details</h3>
    You have: <%= currentListingCount %> Events Scheduled <br />
    with a total of <%=currentGuestsCount %> guests signed up.<br />
    View Details
</div>

    </div>
    <div id="rightcontainer">
<div id="actions">
    <h2>Upcoming Events</h2>
    <asp:ListView ID="lvLists" ItemType="com.WanderingTurtle.Common.ItemListing" SelectMethod="GetItemLists" DataKeyNames="ItemListID" EnableViewState="False" runat="server" >
        <ItemTemplate>
            <tr>
                <td><%# Item.EventName.Truncate(25) %></td>
                <td><%# Item.StartDate %></td>
                <td><%# Item.EndDate %></td>
                <td><%# Item.CurrentNumGuests %></td>
                <td>
                    View Details
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="tbl1" runat="server">
                <tr id="tr1" runat="server">
                    <td id="td8" class="eventheader" runat="server">Event Name</td>
                    <td id="td2" class="eventheader" runat="server">Start Time</td>
                    <td id="td3" class="eventheader" runat="server">End Time</td>
                    <td id="td5" class="eventheader" runat="server">Current Num of Guests</td>
                    <td></td>
                </tr>
                <tr id="ItemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        </asp:ListView>

</div>
</div>
    <div id ="eventdetails">

    </div>
</asp:Content>
