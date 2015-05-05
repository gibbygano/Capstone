<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Password.aspx.cs" Inherits="com.WanderingTurtle.Web.Pages.Password" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FeaturedContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
        <section id="passwordForm">
        <h2>Password Reset</h2>
               <p class="validation-summary-errors">
                    <asp:Literal runat="server" ID="FailureText" />
                   <asp:Label ID="lblError" runat="server" Text=""></asp:Label>
                </p>
                <fieldset>
                    <legend></legend>
                    <ol>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="txtCurrent">Current Password</asp:Label>
                            <asp:TextBox runat="server" ID="txtCurrent" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCurrent" CssClass="field-validation-error" ErrorMessage="You must enter your current password." /> 
                           
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="txtNewPassword">New Password</asp:Label>
                            <asp:TextBox runat="server" ID="txtNewPassword" TextMode="Password" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNewPassword" CssClass="field-validation-error" ErrorMessage="The new password field is required." />
                            
                        </li>
                        <li>
                            <asp:Label runat="server" AssociatedControlID="txtNewPassword2">Confirm New Password</asp:Label>
                            <asp:TextBox runat="server" ID="txtNewPassword2" TextMode="Password" />
                          
                            <asp:CompareValidator CssClass="field-validation-error" ID="passValidator" runat="server" ErrorMessage="Passwords do not match!" ControlToValidate="txtNewPassword2" ControlToCompare="txtNewPassword"></asp:CompareValidator>
                        </li>
                   
                    </ol>
                    <asp:Button ID="btnChange" runat="server" OnClick="btnChange_Click" Text="Change Password" />
                </fieldset>
        <p>
            
        </p>
    </section>
    <aside class="pass">
        <asp:Panel ID="passRules" runat="server" Visible="false" CssClass="passwordRules">
        
            Passwords must:
            <ol>
                <li>Be a minimum 8 characters long</li>
                <li>Contain at least 3 of the following:</li>
                <ul>
                    <li>One Number</li>
                    <li>One UPPERCASE letter</li>
                    <li>One Special Characer (!, #, $,&, etc.)</li>
                </ul>
            </ol>
        </asp:Panel>
    </aside>
</asp:Content>
