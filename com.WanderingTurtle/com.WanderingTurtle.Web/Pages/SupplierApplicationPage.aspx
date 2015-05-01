<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SupplierApplicationPage.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.SupplierApplicationPage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 441px;
        }
        .auto-style2 {
            width: 626px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <section class="app">
    <h1>Supplier Application</h1>
    <asp:Label ID="lblError" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:Label ID="lblFinish" runat="server" ForeColor="Black"></asp:Label>
    <br />
    <table id="supplierCreation">
        <tr>
            <td class="auto-style1">
                Company Name<br />
&nbsp;<asp:TextBox ID="txtCompanyName" runat="server" Width="299px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                Description<br />
&nbsp;<asp:TextBox ID="txtDescription" runat="server" Width="299px" Height="79px" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table> 
                    <tr>
                        <td class="auto-style2">
                First Name<br />
                            <asp:TextBox ID="txtFirstName" runat="server" Width="180px"></asp:TextBox>
            </td>
               <td class="auto-style1">
                Last Name
                <asp:TextBox ID="txtLastName" runat="server" Width="191px"></asp:TextBox>
            </td>
                    </tr>

                </table>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                Email<br />
&nbsp;<asp:TextBox ID="txtEmail" runat="server" Width="299px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                Phone Number<br />
                <asp:TextBox ID="txtPhoneNumber" runat="server" Width="173px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                Address<br />
&nbsp;<asp:TextBox ID="txtAddress" runat="server" Width="299px"></asp:TextBox>
            </td>
        </tr>
                <tr>
            <td class="auto-style1">
                Address 2<br />
&nbsp;<asp:TextBox ID="txtAddress2" runat="server" Width="299px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="auto-style1">
                Zip<br />
&nbsp;<asp:TextBox ID="txtZip" runat="server" Width="150px"></asp:TextBox> 
            </td>
        </tr>
    </table>
    <br />
    <br />
    <asp:Button ID="btnSubmitApplication" runat="server" OnClick="btnSubmitApplication_Click" Text="Submit Application" Width="182px" />
    </section>
    <aside style="padding-top:85px;">
        <ul class="bxslider">
  <li><img src="../Images/turtle.png" /></li>
  <li><img src="../Images/food.png" /></li>
  <li><img src="../Images/juice.png" /></li>
</ul>
    </aside>
</asp:Content>
