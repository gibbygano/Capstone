<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="com.WanderingTurtle.Web.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        
    </hgroup>

    <section id="loginForm">
        <h2>Please Log in</h2>
               <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                   <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </p>
                <fieldset>
                    <legend>Log in Form</legend>
                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="txtUserName">User name</asp:Label>
                            <asp:TextBox runat="server" ID="txtUserName" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtUserName" CssClass="field-validation-error" ErrorMessage="The user name field is required." />
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="txtPassword">Password</asp:Label>
                            <asp:TextBox runat="server" ID="txtPassword" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" CssClass="field-validation-error" ErrorMessage="The password field is required." />
                        </li>
                   
                    </ol>
                    <asp:Button runat="server" OnClick="CheckLogin" Text="Log in" />
                </fieldset>
        <p>
            <a href="~/Pages/SupplierApplicationPage.aspx">Submit an Application</a> if you wish to become a Supplier.
        </p>
    </section>

    <aside>
        <h3>Hunter is being a dBag</h3>
        <p>        
            needs to be information of why hes being a dbag.
        </p>
 
    </aside>
</asp:Content>