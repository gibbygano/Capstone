<%@ Page Title="" Language="C#" MasterPageFile="~/PagesGuest/SiteGuest.Master" AutoEventWireup="true" CodeBehind="ViewBookings.aspx.cs" Inherits="com.WanderingTurtle.Web.PagesGuest.ViewBookings" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .boldBigger{
            font-size: 14px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <%-- Created by Arik Chadima 2015/4/24
        Allows for guests to view and print the bookings their signed up for. --%>
    <div>
        <div id="loginDiv" runat="server">
            <asp:Label ID="lblError" Text="" runat="server" Visible="false" ForeColor="Red" />
            <asp:Label ID="lblLogin" Text="Enter your pin:" runat="server" />
            <asp:TextBox ID="txtLogin" runat="server" MaxLength="5" TextMode="Password" /><br />
            <asp:Button ID="btnLogin" runat="server" Text="View bookings" OnClick="btnLogin_Click" UseSubmitBehavior="True" />
        </div>
        <div id="guestDetailsDiv" runat="server" visible="false">
            <br />
            <h3>Viewing booked events for: </h3><br />
            <span class="boldBigger"><%=foundGuest.GetFullName %></span><br />
            <span class="boldBigger"><%=foundGuest.Address1 %></span><br />
            <span class="boldBigger"><%=foundGuest.Address2 %></span><br />
            <span class="boldBigger"><%=foundGuest.CityState.GetZipStateCity %></span><br />
            <span class="boldBigger"><%=foundGuest.EmailAddress %></span><br />
            <span class="boldBigger"><%=foundGuest.PhoneNumber %></span><br />
            <asp:GridView ID="gvBookings" runat="server" AutoGenerateColumns="False" CellPadding="4" GridLines="None" DataKeyNames="ItemListID" ForeColor="#333333">
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#186D99" BorderStyle="None" Font-Bold="True" BorderColor="#327EA7" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" BorderWidth="0px" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="False" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <Columns>
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString="{0:ddd, MMM d}, {0:t}">
                        <ControlStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EventItemName" HeaderText="Event Name" SortExpression="EventItemName">
                        <ItemStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Quantity" HeaderText="Number of Tickets" SortExpression="Quantity">
                    </asp:BoundField>
                    <asp:BoundField DataField="TicketPrice" HeaderText="Price per Ticket" SortExpression="TicketPrice" DataFormatString="{0:c}">
                    </asp:BoundField>
                    <asp:BoundField DataField="TotalCharge" HeaderText="Total" SortExpression="TotalCharge" DataFormatString="{0:c}">
                    </asp:BoundField>
                </Columns>
                
            </asp:GridView>
            <div class="float-right">
                Total price: <%=String.Format("{0:c}",totalPrice) %>
            </div>
        </div>
    </div>
</asp:Content>
