<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.GuestViewListings" MasterPageFile="SiteGuest.Master" %>

<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Web.Pages" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>
<%@ Import Namespace="com.WanderingTurtle" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server" ID="head">
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.3/themes/smoothness/jquery-ui.css">
    <%--  <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>
    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.3/jquery-ui.min.js"></script>--%>
    <script>
        
    </script>
    <style>
        .hide div {
            width: 500px;
            background-color: white;
            border: 1px solid #186D99;
            border-radius: 3px;
            padding: 4px;
            padding-left: 8px;
        }
    </style>
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" ID="body">
    <!--Created: 2015/03/27
        Pat Banks
        View List of current listings for hotel guest to sign up 
        -->
    <h2>Upcoming Event Listings</h2>
    <h3>Please select an event to book your tickets:</h3>

            <asp:GridView ID="gvListings" runat="server" AutoGenerateColumns="False" AutoGenerateSelectButton="True" CellPadding="4" GridLines="None" DataSourceID="ObjectDataSource1" DataKeyNames="ItemListID" OnSelectedIndexChanged="gvListings_SelectedIndexChanged" ForeColor="#333333">
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
                <HeaderStyle BackColor="#186D99" BorderStyle="None" Font-Bold="True" BorderColor="#327EA7" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" BorderWidth="0px" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" ForeColor="#333333" Font-Bold="False" />
                <Columns>
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" SortExpression="StartDate" DataFormatString="{0:ddd, MMM d}, {0:t}" ItemStyle-Width="150px"></asp:BoundField>
                    <asp:BoundField DataField="EventName" HeaderText="Event Name" SortExpression="EventName" ItemStyle-Width="150px"></asp:BoundField>
                    <asp:BoundField DataField="EventDescription" HeaderText="Description" SortExpression="EventDescription" ItemStyle-Width="250px"></asp:BoundField>
                    <asp:BoundField DataField="QuantityOffered" HeaderText="Avail Tickets" SortExpression="QuantityOffered" ItemStyle-Width="50px" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" DataFormatString="{0:c}" ItemStyle-Width="50px" />
                </Columns>
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="RetrieveActiveItemListingDetailsList"
                TypeName="com.WanderingTurtle.BusinessLogic.BookingManager"></asp:ObjectDataSource>
            <br />

            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>

            <div class="hide">
                <div id="confirmDetails" runat="server" enableviewstate="False" style="display: none;">
                    <h3>Confirmation Agreement</h3>
                    <p>
                        I, <%= guestName %>, agree to pay <%= totalPrice.ToString("C")  %> for <%= ticketQty %> tickets to
                        <%= eventName %> on <%= date %>.  This amount will be charged to my credit card on file. 
                    </p>
                    <asp:Button ID="btnConfirm" runat="server" Text="I Agree" OnClick="btnConfirm_Click" UseSubmitBehavior="False" />
                    <asp:Button ID="btnCancel" runat="server" Text="Go Back" OnClick="btnCancel_Click" UseSubmitBehavior="False" />
                </div>
            </div>
            <div id="ticketRequest" runat="server" enableviewstate="False" style="display: block;">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="lblTickets" Text="# Tickets To Add:" runat="server" Height="35px" Font-Bold="True"></asp:Label>
                        </td>
                        <td>
                            <asp:HiddenField ID="hfGuestMaxTickets" runat="server" ClientIDMode="Static" Value="0" />
                            <asp:TextBox ID="txtGuestTickets" runat="server" ClientIDMode="Static" TabIndex="0"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGuestPin" Text="Guest Pin:" runat="server" Font-Bold="True" Height="35px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox TextMode="Password" ID="txtGuestPin" ClientIDMode="Static" runat="server" TabIndex="1" ViewStateMode="Enabled" Wrap="False" MaxLength="6"></asp:TextBox>

                        </td>
                    </tr>
                </table>

                <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" UseSubmitBehavior="False" />
            </div>
            <div id="otherMessage" title="Message" style="display: none;">
                <div class="errorMsg">
                    <asp:Label ID="lblOtherMessage" runat="server" Text="Error" ForeColor="#000000"></asp:Label>
                    <br />
                </div>
            </div>

</asp:Content>

