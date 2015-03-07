<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierViewEvents.aspx.cs" Inherits="com.WanderingTurtle.Web.SupplierViewEvents" MasterPageFile="../Site.Master" %>

<%@ Import Namespace="com.WanderingTurtle.Common" %>
<asp:Content ContentPlaceHolderID="MainContent" runat="server" ID="body">

    <h1>Listed Events</h1>
    <asp:ListView ID="lvEvents" ItemType="Event" SelectMethod="GetEvents" DataKeyNames="EventItemID" UpdateMethod="UpdateEvent"
        DeleteMethod="DeleteEvent" EnableViewState="false" runat="server" OnPagePropertiesChanging="lvEvents_PagePropertiesChanging">
        <ItemTemplate>
            <tr>

                <td><%# Item.EventItemName %></td>
                <td><%# Item.EventTypeName %></td>
                <td><%# Item.TransportString %></td>
                <td><%# Item.OnSiteString %></td>
                <td>
                    <asp:Button CommandName="Edit" Text="Edit" runat="server" />
                   <!-- <asp:Button CommandName="Delete" Text="Delete" runat="server" /> -->
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="tbl1" runat="server">
                <tr id="tr1" runat="server">
                    <td id="td1" class="eventheader" runat="server">Event Name</td>
                    <td id="td2" class="eventheader" runat="server">Event Type</td>
                    <td id="td3" class="eventheader" runat="server">Transportation</td>
                    <td id="td4" class="eventheader" runat="server">On-Site</td>
                    <td></td>
                </tr>
                <tr id="ItemPlaceholder" runat="server">
                </tr>
            </table>

        </LayoutTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <input name="name" value="<%# Item.EventItemName %>" maxlength="255" />
                    <input type="hidden" name="EventItemID" value="<%# Item.EventItemID%>" />
                </td>
                <td>

                    <asp:HiddenField ID="HiddenField1" Value="<%# current = Item.EventTypeName %>" runat="server" />

                    <select name="type">
                        <%
                            foreach (var item in _eventTypes)
                            {
                                if (item.EventName == current)
                                {
                                    Response.Write("<option value=" + item.EventTypeID + " selected>" + item.EventName + "</option>");
                                }
                                else
                                {
                                    Response.Write("<option value=" + item.EventTypeID + ">" + item.EventName + "</option>");
                                }
                            }
                            
                        %>
                    </select>
                </td>
                <td>
                    <asp:HiddenField ID="HiddenField2" Value="<%# transport = Item.TransportString %>" runat="server" />
                    <select name="transport">
                        <%
                            if (transport == "Provided")
                            {
                                Response.Write("<option value=\"True\" selected>Provided</option>");
                                Response.Write("<option value=\"False\">Not Provided</option>");
                            }
                            else
                            {
                                Response.Write("<option value=\"True\">Provided</option>");
                                Response.Write("<option value=\"False\" selected>Not Provided</option>");
                            }

                        %>
                    </select>
                </td>
                <td>
                    <asp:HiddenField ID="HiddenField3" Value="<%# onsite = Item.OnSiteString %>" runat="server" />
                    <select name="onsite">
                        <%
                            if (onsite == "Yes")
                            {
                                Response.Write("<option value=\"True\" selected>Yes</option>");
                                Response.Write("<option value=\"False\">No</option>");
                            }
                            else
                            {
                                Response.Write("<option value=\"True\">Yes</option>");
                                Response.Write("<option value=\"False\" selected>No</option>");
                            }

                        %>
                    </select>
                </td>
                <td>

                    <asp:Button CommandName="Update" Text="Update" runat="server" />
                    <asp:Button CommandName="Cancel" Text="Cancel" runat="server" />
                </td>
            </tr>
        </EditItemTemplate>


    </asp:ListView>
    <%--    <asp:DataPager runat="server" ID="dpEvents" PageSize="2" PagedControlID="lvEvents">
        <Fields>
            <asp:NumericPagerField ButtonCount="10"
                PreviousPageText="<--"
                NextPageText="-->" ButtonType="Link" />
        </Fields>
    </asp:DataPager>--%>
</asp:Content>
