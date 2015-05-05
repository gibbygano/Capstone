<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierPortal.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierPortal" %>

<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <script src="<%: ResolveUrl("~/Scripts/datescript.js") %>"></script>
    <div id="mainpage">
        <div id="leftcontainer" runat="server">
            <div id="supplierdetails">
                <h2><%= _currentSupplier.CompanyName %></h2>
            </div>
            <div id="quickdetails">
                <h3>Quick Event Details</h3>
                You have: <%= currentListingCount %> Events Scheduled
            <br />
                with a total of <%=currentGuestsCount %> guests signed up.<br />
            </div>
            <asp:Button ID="btnViewMoneyDets" runat="server" Text="View Financial Details" OnClick="btnViewMoneyDets_Click" UseSubmitBehavior="False" />

        </div>
        <div id="rightcontainer">
            <div id="actions" runat="server">
                <h2>Upcoming Events</h2>
                <asp:ListView ID="lvLists" ItemType="com.WanderingTurtle.Common.ItemListing" SelectMethod="GetItemLists" DataKeyNames="ItemListID" EnableViewState="False" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Item.EventName.Truncate(25) %></td>
                            <td><%# Item.StartDate.ToString("MM/dd/yy hh:mmt") %></td>
                            <td><%# Item.EndDate.ToString("MM/dd/yy hh:mmt") %></td>
                            <td><%# Item.CurrentNumGuests %></td>
                            <td>
                                <asp:Button ID="btnDetails" runat="server" CommandArgument="<%#Item.ItemListID %>" Text="View Details" OnClick="btnDetails_Click" UseSubmitBehavior="False" />
                            </td>
                        </tr>

                    </ItemTemplate>

                    <LayoutTemplate>
                        <table id="tblmain" class="sortable">
                            <thead>
                                <tr id="tr1" runat="server">
                                    <th id="td8" class="eventheader" runat="server">Event Name</th>
                                    <th id="td2" class="eventheader" runat="server">Start Time</th>
                                    <th id="td3" class="eventheader" runat="server">End Time</th>
                                    <th id="td5" class="eventheader" runat="server"># Guests</th>
                                    <th style="display: none;"></th>
                                </tr>

                            </thead>
                            <tr id="ItemPlaceholder" runat="server">
                            </tr>

                        </table>                       
                    </LayoutTemplate>
                </asp:ListView>

            </div>

            <div id="eventsDetails" runat="server" style="display: none;">
                <asp:ListView ID="lvDetails" ItemType="com.WanderingTurtle.Common.BookingNumbers" DataKeyNames="Room" EnableViewState="False" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Item.FirstName +" " + Item.LastName%></td>
                            <td><%# Item.Quantity %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="tbl1" class="sortable">
                            <thead>
                                <tr id="tr1" runat="server">
                                    <th id="td8" class="eventheader" runat="server">Guest Name</th>
                                    <th id="td2" class="eventheader" runat="server">Number of Tickets</th>
                                </tr>

                            </thead>
                            <tr id="ItemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <asp:Button ID="btnGoBack" runat="server" Text="Go Back" OnClick="btnGoBack_Click" UseSubmitBehavior="False" />
                <asp:Button ID="btnPrint" runat="server" Text="Print" OnClientClick="javascript:window.print();" OnClick="btnGoBack_Click" UseSubmitBehavior="False" />
            </div>

            <div id="ViewMoneyDets" runat="server" style="display: none;">
                <h2>Selected Events</h2>

                <div id="dateRange" class="dontPrint">
                    <h3>Date Range</h3>
                    <div id="dateFrom" style="float: left">
                        <label>From:</label>
                        <input type="text" name="dateFrom" id="eventStart" />
                    </div>
                    <div id="dateTo" style="float: left">
                        <label>To:</label>
                        <input type="text" name="dateTo" id="eventEnd" />
                    </div>
                   
                    <div style="float: left">
                        <label>&nbsp;</label>
                        <asp:Button ID="btnRefreshDate" runat="server" Text="Refresh List" OnClick="btnRefreshDate_Click" />  
                    </div>
                     <asp:Label ID="lblDateError" CssClass="dontPrint" runat="server" Text="" BorderStyle="NotSet" ForeColor="#CC0000"></asp:Label>
                </div>
                <div style="float: left">
                    <h3 style="float: left; margin:0; padding:0">Dates Selected: </h3>
                    <div class="LeftFloatMargin">
                        <p class="LeftFloatBold">From: </p>
                    <% 
                        if(Session["dateFrom"] != null)
                       { 
                    %>
                        <p class="LeftFloatMargin"><%= Session["dateFrom"] %></p>
                        </div>
                    <%} else { %>
                        <p class="LeftFloatMargin">--/--/----</p>
                        </div>
                    <%} %>
                    
                     <div class="LeftFloatMargin">
                     <p class="LeftFloatBold">To: </p>
                    <%
                      if(Session["dateTo"] != null)
                      { 
                    %>
                        <p class="LeftFloatMargin"><%= Session["dateTo"] %></p>
                        </div>
                    <% }
                    else { 
                        Response.Write("<p class='LeftFloatMargin'>--/--/----</p>" +
                        "</div>");
                   } %>
                </div>
                <div class="clear"></div>
                <asp:ListView ID="ListView1" ItemType="com.WanderingTurtle.Common.ItemListing" SelectMethod="GetItemListsByDate" DataKeyNames="ItemListID" EnableViewState="False" runat="server">
                    <ItemTemplate>
                        <tr>
                            <td><%# Item.EventName.Truncate(25) %></td>
                            <td><%# Item.StartDate.ToString("MM/dd/yy hh:mmt") %></td>
                            <td><%# Item.EndDate.ToString("MM/dd/yy hh:mmt") %></td>
                            <td><%# Item.CurrentNumGuests %></td>
                            <td><%# Item.Price.ToString("C") %></td>
                            <td><%# (Item.Price * Item.CurrentNumGuests).ToString("C") %></td>
                        </tr>
                    </ItemTemplate>
                    <LayoutTemplate>
                        <table id="tbl1">
                            <thead>
                            <tr id="tr1" runat="server">
                                <th id="td8" class="eventheader" runat="server">Event Name</th>
                                <th id="td2" class="eventheader" runat="server">Start Time</th>
                                <th id="td4" class="eventheader" runat="server">End Time</th>
                                <th id="td3" class="eventheader" runat="server"># Tickets</th>
                                <th id="td5" class="eventheader" runat="server">Ticket Cost</th>
                                <th id="td1" class="eventheader" runat="server">Extended</th>
                                
                            </tr>
                                </thead>
                            <tr id="ItemPlaceholder" runat="server">
                            </tr>
                        </table>
                    </LayoutTemplate>
                </asp:ListView>
                <table>
                    <tr>
                        <th class="eventheader">Supplier Cost </th>
                        <td><%= (int)(_currentSupplier.SupplyCost*100) %>%</td>
                    </tr>
                    <tr>
                        <th class="eventheader">Total</th>
                        <td><%= ((decimal)(getTotal() * _currentSupplier.SupplyCost)).ToString("C") %></td>

                    </tr>
                </table>
                <asp:Button ID="btnGoBackHome" runat="server" Text="Go Back" OnClick="btnGoBack_Click" UseSubmitBehavior="False" />
                <asp:Button ID="btnPrintFinacial" runat="server" Text="Print" OnClientClick="javascript:window.print();" OnClick="btnViewMoneyDets_Click" UseSubmitBehavior="False" />
            </div>

        </div>


    </div>
    <div id="otherMessage" title="Message" style="display: none;">
            <div class="errorMsg">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="#FFFFFF"></asp:Label>
                <br />
            </div>
        </div>
</asp:Content>
