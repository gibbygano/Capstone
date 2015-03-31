<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GuestViewListings.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.GuestViewListings" MasterPageFile="../Site.Master" %>

<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Web.Pages" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>
<%@ Import Namespace="com.WanderingTurtle" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" ID="body">
    <!--Created 2015/03/27
        Pat Banks
        View List of current listings for hotel guest to sign up 
        -->
    <h2>Upcoming Event Listings</h2>
    <h3>Please select an event to book your tickets:</h3>
    
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>

    <asp:GridView ID="gvListings" runat="server" AutoGenerateColumns="False" DataKeyNames="ItemListID" AllowSorting ="True" AutoGenerateSelectButton="True" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="None"   >
        <FooterStyle BackColor="Tan" />
        <HeaderStyle BackColor="Tan" BorderStyle="Solid" Font-Bold="True" />
        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
        <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
        <AlternatingRowStyle BackColor="PaleGoldenrod" />
        <Columns>
            <asp:BoundField DataField="StartDate" HeaderText="Start Date"/>
            <asp:BoundField DataField="EventName" HeaderText="Event Name"/>
            <asp:BoundField DataField="EventDescription" HeaderText="Event Description"/>
            <asp:BoundField DataField="QuantityOffered" HeaderText="Qty Avail"/>
            <asp:BoundField DataField="Price" HeaderText="Ticket Price" DataFormatString="{0:C}"/>
        </Columns>
    </asp:GridView>
        <br />
        <asp:Label  ID="lblTickets" Text="# Tickets To Add:" runat="server" Height="35px" Font-Bold="True" ></asp:Label>
        <asp:TextBox ID="txtGuestTickets" runat="server" TabIndex="0" ></asp:TextBox>
        <asp:Label  ID="lblGuestPin" Text="Guest Pin:" runat="server" Font-Bold="True" Height="35px" ></asp:Label>  
        <asp:TextBox ID="txtGuestPin" runat="server" TabIndex="1" ViewStateMode="Enabled"></asp:TextBox>
        <br />
        <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" />

</asp:Content>
