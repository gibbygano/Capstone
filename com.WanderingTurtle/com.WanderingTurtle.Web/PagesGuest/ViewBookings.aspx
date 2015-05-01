<%@ Page Title="" Language="C#" MasterPageFile="~/PagesGuest/SiteGuest.Master" AutoEventWireup="true" CodeBehind="ViewBookings.aspx.cs" Inherits="com.WanderingTurtle.Web.PagesGuest.ViewBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .boldBigger {
            font-size: 14px;
        }

        #bookingTable {
            border-collapse: collapse;
            border: 0px;
            color: #333;
        }

            #bookingTable thead tr {
                background-color: #186D99;
            }

                #bookingTable thead tr td {
                    font-weight: bold;
                    color: white;
                    text-align: center;
                    vertical-align: middle;
                }

            #bookingTable tbody tr {
                background-color: #F7F6F3;
                color: #333;
            }

            #bookingTable tbody:nth-child(2n+1) {
                background-color: white;
                color: #284775;
            }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Created by Arik Chadima 2015/4/24
        Allows for guests to view and print the bookings their signed up for.
        Modified by Arik Chadima 2015/4/30
        changed gridview to a repeater to allow for easier styling and item manipulation. --%>
    <div>
        <div id="loginDiv" runat="server">
            <asp:Label ID="lblError" Text="" runat="server" Visible="false" ForeColor="Red" /><br />
            <asp:Label ID="lblLogin" Text="Enter your pin:" runat="server" />
            <asp:TextBox ID="txtLogin" runat="server" MaxLength="6" TextMode="Password" /><br />
            <asp:Button ID="btnLogin" runat="server" Text="View bookings" OnClick="btnLogin_Click" UseSubmitBehavior="True" />
        </div>
        <div id="guestDetailsDiv" runat="server" visible="false">
            <br />
            <h3>Viewing booked events for: </h3>
            <br />
            <asp:Label ID="GuestFullName" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="Address1" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="Address2" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="CityStateZip" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="EmailAddress" runat="server" CssClass="boldBigger" /><br />
            <asp:Label ID="PhoneNumber" runat="server" CssClass="boldBigger" /><br />

            <table id="bookingTable">
                <thead>
                    <tr>
                        <td>Event Start Date
                        </td>
                        <td>Event Name
                        </td>
                        <td>Number of Tickets
                        </td>
                        <td>Price per Ticket
                        </td>
                        <td>Total
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <asp:Repeater ID="repBookings" runat="server">
                        <ItemTemplate>
                            <td><%# String.Format("{0:ddd, MMM d}, {0:t}", DataBinder.Eval(Container.DataItem,"StartDate").ToString()) %></td>
                            <td><%# DataBinder.Eval(Container.DataItem,"EventItemName") %></td>
                            <td><%# DataBinder.Eval(Container.DataItem, "Quantity") %></td>
                            <td><%# String.Format("{0:c}",DataBinder.Eval(Container.DataItem,"TicketPrice").ToString()) %></td>
                            <td><%# String.Format("{0:c}",DataBinder.Eval(Container.DataItem,"TotalCharge").ToString()) %></td>
                        </ItemTemplate>
                    </asp:Repeater>
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>Total price:</td>
                        <td>
                            <asp:Label ID="TotalPrice" runat="server" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
