<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierViewEvents.aspx.cs" Inherits="com.WanderingTurtle.Web.SupplierViewEvents" MasterPageFile="Site.Master" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" ID="body">

    <h1>Listed Events</h1>
    <asp:ListView ID="lvEvents" runat="server" OnPagePropertiesChanging="lvEvents_PagePropertiesChanging">
        <ItemTemplate>
            <tr>
                
                <td><%# Eval("EventItemName")%></td>
                <td><%# Eval("EventTypeName")%></td>
                <td><%# Eval("TransportString")%></td>
                <td><%# Eval("OnSiteString") %></td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="tbl1" runat="server">
                <tr id="tr1" runat="server">
                    <td id="td1" runat="server">Event Name</td>
                    <td id="td2" runat="server">Event Type</td>
                    <td id="td3" runat="server">Transportation</td>
                    <td id="td4" runat="server">On-Site</td>
                </tr>
                <tr id="ItemPlaceholder" runat="server">
                </tr>
            </table>

        </LayoutTemplate>


    </asp:ListView>
<%--    <asp:DataPager runat="server" ID="dpEvents" PageSize="2" PagedControlID="lvEvents">
        <Fields>
            <asp:NumericPagerField ButtonCount="10"
                PreviousPageText="<--"
                NextPageText="-->" ButtonType="Link" />
        </Fields>
    </asp:DataPager>--%>


</asp:Content>
