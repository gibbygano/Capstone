<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SupplierAddEvent.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierAddEvent" MasterPageFile="../Site.Master" EnableEventValidation="False" %>
<asp:Content ContentPlaceholderID="MainContent" runat="server" ID="body">
    <div>
    <h1>Add a New Event</h1>
        Event Name: <asp:TextBox ID="txtEventName" runat="server" MaxLength="255"></asp:TextBox><br />
        Event Type: <asp:DropDownList ID="comboEventTypeList" runat="server" AppendDataBoundItems="True"></asp:DropDownList><br />
        Description:<br />
        <asp:TextBox ID="txtDescription" runat="server" Rows="4" TextMode="MultiLine" MaxLength="255"></asp:TextBox><br />
        Is the event on site?<br />
        <asp:RadioButtonList ID="radOnSite" runat="server" style="display: inline;" CssClass="fixed" OnSelectedIndexChanged="radOnSite_SelectedIndexChanged" AutoPostBack="True">
            <asp:ListItem Text="Yes"  Value="True" Selected="False" />
            <asp:ListItem Text="No"  Value="False" Selected="False" />
        </asp:RadioButtonList>
        <asp:Label ID="lblTransportation" runat="server" Text="Is Transportation to and from the event provided?" Visible="False"></asp:Label><br />
        <asp:RadioButtonList ID="radTransportation" runat="server"  CssClass="fixed" OnSelectedIndexChanged="radTransportation_SelectedIndexChanged" Visible="False">
            <asp:ListItem Text="Yes"  Value="True" Selected="False" />
            <asp:ListItem Text="No"  Value="False" Selected="False" />
        </asp:RadioButtonList>

        <asp:Button ID="btnSubmitEvent" runat="server" Text="Submit New Event" Visible="True" OnClick="btnSubmitEvent_Click" />
    </div>
        <div id="pageErrors">
           
            <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
        </div>
</asp:Content>
