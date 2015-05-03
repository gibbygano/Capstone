<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewItemListing.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.ViewItemListing" MasterPageFile="../Site.Master" %>

<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Web.Pages" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>
<%@ Import Namespace="com.WanderingTurtle" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" ID="body">
    <script src="<%: ResolveUrl("~/Scripts/datescript.js") %>"></script>
    <!--Created 2015/04/09
        Kelsey 
        View List of Item Listings and Options to Edit or Delete
        -->
    <div id="main">
    <h1>Your Listed Events
    </h1>
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:ListView ID="lvLists" ItemType="com.WanderingTurtle.Common.ItemListing" SelectMethod="GetLists" DataKeyNames="ItemListID" UpdateMethod="UpdateList"
        DeleteMethod="DeleteList" EnableViewState="False" runat="server" OnPagePropertiesChanging="lvLists_PagePropertiesChanging">
        <ItemTemplate>
            <tr>
                <td><%# Item.EventName.Truncate(20) %></td>
                <td><%# Item.StartDate.ToString("MM/dd/yy hh:mmt") %></td>
                <td><%# Item.EndDate.ToString("MM/dd/yy hh:mmt") %></td>
                <td><%# Item.Price.ToString("C") %></td>
                <td><%# Item.MaxNumGuests %></td>
                <td>
                    <asp:Button CommandName="Edit" CommandArgument="<%# Item.EventID %>" Text="Edit" runat="server" />
                    
                    <input type="hidden" name="myListID" value="<%# Item.ItemListID %>" />
                    <input type="button" class="myID" name="<%# Item.ItemListID %>"  value="Delete" />
                    <%--<asp:Button Text="Delete" ID="<%# Item.ItemListID %>" runat="server" OnClientClick="return showConfirm(this)"  UseSubmitBehavior="false" />--%>

                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="tblitems" class="sortable" >
                <thead>
                    <tr id="tr1" runat="server">
                        <td id="td8" class="eventheader" runat="server">Event Name</td>
                        <td id="td2" class="eventheader" runat="server">Start Time</td>
                        <td id="td3" class="eventheader" runat="server">End Time</td>
                        <td id="td4" class="eventheader" runat="server">Price</td>
                        <td id="td7" class="eventheader" runat="server">Max</td>
                        <td style="display: none;"></td>
                </tr>
                </thead>
                <tr id="ItemPlaceholder" runat="server">
                </tr>
            </table>
        </LayoutTemplate>
        <EditItemTemplate>
            <tr>
                <input type="hidden" name="ListID" value="<%# Item.ItemListID %>" />
                <td><%# Item.EventName.Truncate(25) %></td>
                <td>
                    <input name="start" id="eventStart" value="<%# Item.StartDate %>" maxlength="255" class="myDate" /></td>
                <td>
                    <input name="end" id="eventEnd" value="<%# Item.EndDate %>" maxlength="255" class="myDate" /></td>
                <td>
                    $<input name="price" id="listPrice" value="<%# Item.Price.ToString() %>" maxlength="255" class="myMoney" /></td>
                <td>
                    <input size="3" name="max" id="listmax" class="myspinner myDate" value="<%# Item.MaxNumGuests %>" maxlength="3" /></td>
                <td>
                    <asp:Button CommandName="Update" Text="Update" CausesValidation="true" runat="server" ID="btnUpdate" />

                    <asp:Button CommandName="Cancel" Text="Cancel" runat="server" />
                <input type="hidden" id="listCurrent" value="<%# Item.CurrentNumGuests %>" />
                <input type="hidden" name="itemID" value="<%# Item.ItemListID %>" /></td>
            </tr>
        </EditItemTemplate>
    </asp:ListView>
</div>

        <div id="deletemessage" title="Are you sure??" style="display: none;">
            <div class="errorMsg">
                <asp:Label ID="lblDeleteMessage" runat="server" Text="Are you sure you want to delete this event?" ForeColor="#FFFFFF"></asp:Label>
                <br />
            </div>
        </div>
        <div id="otherMessage" title="Message" style="display: none;">
            <div class="errorMsg">
                <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="#FFFFFF"></asp:Label>
                <br />
            </div>
        </div>

</asp:Content>
