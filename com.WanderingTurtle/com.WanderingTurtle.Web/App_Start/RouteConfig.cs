using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace com.WanderingTurtle.Web
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);
            routes.MapPageRoute(null, "login", "~/Pages/Login.aspx");
            routes.MapPageRoute(null, "application", "~/Pages/SupplierApplicationPage.aspx");
            routes.MapPageRoute(null, "events", "~/Pages/SupplierViewEvents.aspx");
            routes.MapPageRoute(null, "events/add", "~/Pages/SupplierAddEvent.aspx");
            routes.MapPageRoute(null, "listings", "~/Pages/GuestViewListings.aspx");
        }
    }
}
