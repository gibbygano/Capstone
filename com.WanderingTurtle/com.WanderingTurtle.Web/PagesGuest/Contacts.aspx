<%@ Page Title="" Language="C#" MasterPageFile="SiteGuest.Master" AutoEventWireup="true" CodeBehind="Contacts.aspx.cs" Inherits="com.WanderingTurtle.Web.PagesGuest.Contacts" %>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
    </hgroup>

    <section class="contact">
        <header>
            <h3>Phone:</h3>
        </header>
        <p>
            <span class="label">Main:</span>
            <span>425.555.0100</span>
        </p>
        <p>
            <span class="label">After Hours:</span>
            <span>425.555.0199</span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Email:</h3>
        </header>
        <p>
            <span class="label">Support:</span>
            <span><a href="mailto:Support@wanderingTurtle.com">Support@WanderingTurtle.com</a></span>
        </p>
        <p>
            <span class="label">Marketing:</span>
            <span><a href="mailto:Marketing@WanderingTurtle.com">Marketing@WanderingTurtle.com</a></span>
        </p>
        <p>
            <span class="label">General:</span>
            <span><a href="mailto:General@WanderingTurtle.com">General@WanderingTurtle.com</a></span>
        </p>
    </section>

    <section class="contact">
        <header>
            <h3>Address:</h3>
        </header>
        <p>
            42 Wallabee Way<br />
        </p>
    </section>
</asp:Content>
