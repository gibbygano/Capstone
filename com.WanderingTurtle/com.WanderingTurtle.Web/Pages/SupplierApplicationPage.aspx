<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierApplicationPage.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierApplicationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 441px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <section>
    <h1>Supplier Application</h1>
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:Label ID="lblFinish" runat="server" ForeColor="Black"></asp:Label>
    <br />
    <table>
        <tr>
            <td>Company Name</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtCompanyName" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Description</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtDescription" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>First Name</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtFirstName" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Last Name</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtLastName" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Email</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtEmail" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Phone Number</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtPhoneNumber" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Address</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtAddress" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
                <tr>
            <td>Address 2</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtAddress2" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Zip</td>
            <td class="auto-style1">
                <asp:TextBox ID="txtZip" runat="server" Width="168px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:Button ID="btnSubmitApplication" runat="server" OnClick="btnSubmitApplication_Click" Text="Submit Application" Width="182px" />
    </section>
    <aside>
    </aside>
</asp:Content>
