using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using com.WanderingTurtle.Web;

namespace com.WanderingTurtle.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // Code that runs on application startup
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/03/07
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(object sender, EventArgs e)
        {
            var context = HttpContext.Current;
            var exception = context.Server.GetLastError();
            if (exception is HttpRequestValidationException)
            {
                context.Server.ClearError();
                Response.Clear();
                string url = Request.UrlReferrer.ToString();
                Response.StatusCode = 200;
                Response.Write("<html><head><meta http-equiv=\"refresh\" content=\"1;url=" + url + "\"><script>alert('You have entered potentially dangerous characters into a form on our website. The Wandering Turtle Resort takes security very seriously. Your IP address has been logged and authorities have been notified.'); window.location.href =\"" + url + "\";</script></head> <body></body></html>");
                Response.End();
                return;
            }
        }
    }
}
