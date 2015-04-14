<%@ Page Title="" Language="C#" MasterPageFile="SiteGuest.Master" AutoEventWireup="true" CodeBehind="About.aspx.cs" Inherits="com.WanderingTurtle.Web.PagesGuest.About" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <hgroup class="title">
        <h1><%: Title %>.</h1>
        <h2>Your app description page.</h2>
    </hgroup>

    <article>
        <p>        
            Tòti a Pèdi Resort is a large commercial resort in the Republic of Haiti. The core business is tourism on the southern portion of the island. Located outside the coastal city of Jacel, Tòti a Pèdi Resort is heavily invested in the local economy, both with physical products and entertainment experiences.
        </p>

    </article>

    <aside>
        <h3>Aside Title</h3>
        <p>        
            Use this area to provide additional information.
        </p>
        <ul>
            <li><a runat="server" href="~/">Home</a></li>
            <li><a runat="server" href="~/About">About</a></li>
            <li><a runat="server" href="~/Contact">Contact</a></li>
        </ul>
    </aside>
</asp:Content>