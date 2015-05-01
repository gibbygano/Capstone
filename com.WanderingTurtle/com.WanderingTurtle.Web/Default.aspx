<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="com.WanderingTurtle.Web._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %></h1><br />
                <h2>Welcome to the Wandering Turtle Supplier Portal</h2>
            </hgroup>
            <p>
                Please <a href="/Login">Log in</a> to continue.<br />
                Not a supplier? <a href="/application">Submit</a> an application today to get started!
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    
</asp:Content>
