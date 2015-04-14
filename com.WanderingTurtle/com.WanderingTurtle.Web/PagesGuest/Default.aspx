<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.GuestViewListings" MasterPageFile="SiteGuest.Master" %>

<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Web.Pages" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>
<%@ Import Namespace="com.WanderingTurtle" %>

<asp:Content ContentPlaceHolderID="HeadContent" runat="server" ID="head">
     <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.3/themes/smoothness/jquery-ui.css">
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js"></script>

    <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.11.3/jquery-ui.min.js"></script>
    <script>
        $(document).ready(function () {
            console.log("jquery is ready.");
            $("#txtGuestTickets").spinner();
        });

    </script>

</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" ID="body">
    <!--Created 2015/03/27
        Pat Banks
        View List of current listings for hotel guest to sign up 
        -->
    <h2>Upcoming Event Listings</h2>
    <h3>Please select an event to book your tickets:</h3>

    <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label>

    <asp:UpdatePanel runat="server" ID="gvListingsUpdate">
        <ContentTemplate>
            <asp:GridView ID="gvListings" runat="server" AutoGenerateColumns="False" AllowSorting="True" AutoGenerateSelectButton="True" BackColor="White" BorderColor="#999999" BorderWidth="1px" CellPadding="3" GridLines="Vertical" DataSourceID="ObjectDataSource1" DataKeyNames="ItemListID" BorderStyle="None">
                <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                <HeaderStyle BackColor="#000084" BorderStyle="Solid" Font-Bold="True" BorderColor="Black" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                <SelectedRowStyle BackColor="#008A8C" ForeColor="White" Font-Bold="True" />
                <AlternatingRowStyle BackColor="#DCDCDC" />
                <Columns>
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date" DataFormatString="{0:ddd, MMM d}, {0:t}" SortExpression="StartDate">
                        <ControlStyle Width="100px" />
                        <HeaderStyle BorderWidth="1px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EventName" HeaderText="Event Name" SortExpression="EventName">
                        <ItemStyle Width="150px" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EventDescription" HeaderText="Description" SortExpression="EventDescription" />
                    <asp:BoundField DataField="QuantityOffered" HeaderText="Avail Tix" SortExpression="QuantityOffered" />
                    <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" DataFormatString="{0:c}" />
                </Columns>
                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                <SortedAscendingHeaderStyle BackColor="#0000A9" />
                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                <SortedDescendingHeaderStyle BackColor="#000065" />
            </asp:GridView>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="RetrieveActiveItemListings"
                TypeName="com.WanderingTurtle.BusinessLogic.BookingManager"></asp:ObjectDataSource>
            <br />
            </ContentTemplate>
        </asp:UpdatePanel>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblTickets" Text="# Tickets To Add:" runat="server" Height="35px" Font-Bold="True"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtGuestTickets" runat="server" ClientIDMode="Static" TabIndex="0"></asp:TextBox></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblGuestPin" Text="Guest Pin:" runat="server" Font-Bold="True" Height="35px"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox TextMode="Password"  ID ="txtGuestPin" runat="server" TabIndex="1" ViewStateMode="Enabled"></asp:TextBox></td>
                </tr>
            </table>


    <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />
</asp:Content>
