<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierViewEvents.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierViewEvents" MasterPageFile="../Site.Master" %>

<%@ Import Namespace="com.WanderingTurtle.Common" %>
<%@ Import Namespace="com.WanderingTurtle.Web" %>
<%@ Import Namespace="com.WanderingTurtle.Web.Pages" %>
<%@ Import Namespace="com.WanderingTurtle.BusinessLogic" %>
<%@ Import Namespace="com.WanderingTurtle" %>

<asp:Content ContentPlaceHolderID="MainContent" runat="server" ID="body">
    <script src="<%: ResolveUrl("~/Scripts/datescript.js") %>"></script>
    <!--Created 2015/02/27
        Matt Lapka
        Hunter Lind
        View List of Events and Options to Edit or Delete
        -->
    <div id="theLists" runat="server">
    <h1>Events Available to be Offered</h1>
    <div class="clear"></div>
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:ListView ID="lvEvents" ItemType="com.WanderingTurtle.Common.Event" 
        SelectMethod="GetEvents" DataKeyNames="EventItemID" UpdateMethod="UpdateEvent"
        DeleteMethod="DeleteEvent" EnableViewState="false" runat="server" 
        OnPagePropertiesChanging="lvEvents_PagePropertiesChanging">


        <ItemTemplate>
            <tr>
                <!--show a tooltip with full event name, but only display up to 25 characters-->
                <td title="<%# Item.EventItemName %>"><%# Item.EventItemName.Truncate(25) %></td>
                <td><%# Item.EventTypeName %></td>
                <!--show a tooltip with full description, but only display 20 characters-->
                <td title='<%# Item.Description %>'><%# Item.Description.Truncate(20) %></td>
                <td><%# Item.TransportString %></td>
                <td><%# Item.OnSiteString %></td>
                <td>
                    <%--  <asp:Button CommandName="Edit" Text="Edit" runat="server" /> --%>
                    <asp:Button ID="btnList" runat="server" Text="List This Event" CommandArgument ="<%# Item.EventItemID %>" OnClick="btn_Click"/>
                    <%-- <asp:Button CommandName="Delete" Text="Delete" runat="server" OnClientClick="return confirm('Are you sure you want to delete this?')" /> --%>
                </td>
            </tr>
        </ItemTemplate>
        <LayoutTemplate>
            <table id="tblevents" class="sortable">
                <thead>
                <tr id="tr1" runat="server">
                    <td id="td1" class="eventheader" runat="server">Event Name</td>
                    <td id="td2" class="eventheader" runat="server">Event Type</td>
                    <td id="td3" class="eventheader" runat="server">Description</td>
                    <td id="td4" class="eventheader" runat="server">Transportation</td>
                    <td id="td5" class="eventheader" runat="server">On-Site</td>
                    <td style="display: none;"></td>
                </tr>

                </thead>
                <tr id="ItemPlaceholder" runat="server">
                </tr>
            </table>

        </LayoutTemplate>
        <EditItemTemplate>
            <tr>
                <td>
                    <input name="name" value="<%# Item.EventItemName %>" maxlength="255" class="editInput" title="<%= nameError %>" />
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
                    <textarea name="description" title="<%= descError %>" class="editInput" maxlength="255"><%# Item.Description %></textarea>

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

                    <asp:Button CommandName="Update" Text="Update" CausesValidation="true" runat="server" ID="btnUpdate" />
                    <asp:Button CommandName="Cancel" Text="Cancel" runat="server" />
                </td>
            </tr>
        </EditItemTemplate>


    </asp:ListView>
</div>
    <div id="addListing" runat="server" style="display: none;">
        <asp:Label ID="lblEventName" runat="server" Text="" Font-Size="X-Large"></asp:Label><br />
        <asp:Label ID="lblAddError" runat="server" Text="" ForeColor="#CC0000"></asp:Label><br />
        Start Date & Time:
        <input type="text" id="eventStart" name="startdate" class="mydate" /> <br />
        End Date & Time
        <input type="text" id="eventEnd" name="enddate" class="mydate" /> <br />
        Ticket Price: $
        <input id="listPrice" name="price" value="0.00" class="myspinner"> <br />
        Number of Tickets Available:
        <input id="listTickets" name="tickets" value="0" class="myspinner"><br />
        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"/>
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
    </div>
</asp:Content>
