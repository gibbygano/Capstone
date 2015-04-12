using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Web
{

    public partial class SiteMaster : MasterPage
    {
        private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        private string _antiXsrfTokenValue;
        private bool loggedIn = false;
        

        protected void Page_Init(object sender, EventArgs e)
        {
            // The code below helps to protect against XSRF attacks
            var requestCookie = Request.Cookies[AntiXsrfTokenKey];
            Guid requestCookieGuidValue;
            if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
            {
                // Use the Anti-XSRF token from the cookie
                _antiXsrfTokenValue = requestCookie.Value;
                Page.ViewStateUserKey = _antiXsrfTokenValue;
            }
            else
            {
                // Generate a new Anti-XSRF token and save to the cookie
                _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
                Page.ViewStateUserKey = _antiXsrfTokenValue;

                var responseCookie = new HttpCookie(AntiXsrfTokenKey)
                {
                    HttpOnly = true,
                    Value = _antiXsrfTokenValue
                };
                if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
                {
                    responseCookie.Secure = true;
                }
                Response.Cookies.Set(responseCookie);
            }

            Page.PreLoad += master_Page_PreLoad;
        }

        protected void master_Page_PreLoad(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Set Anti-XSRF token
                ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
                ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
            }
            else
            {
                // Validate the Anti-XSRF token
                if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
                    || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
                {
                    throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
                }
            }

        
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //create menu based on login -- need to create this logic
            if (!Page.IsPostBack)
            {
                try
                {
                    //attempt to get session value if they are logged in
                    loggedIn = (bool)Session["loggedin"];
                }
                catch (Exception)
                {
                    //if it fails, the user must not have logged in on this
                    //session yet, so set it to false
                    Session["loggedIn"] = false;
                }

                if (loggedIn)
                {
                    var stuff1 = new MenuItem();
                    var stuff2 = new MenuItem();
                    var stuff3 = new MenuItem();
                    var stuff4 = new MenuItem();

                    stuff1.NavigateUrl = "/events";
                    stuff1.Text = "View/Edit Events";
                    stuff2.NavigateUrl = "/events/add";
                    stuff2.Text = "Add an Event";
                    stuff3.NavigateUrl = "/listings";
                    stuff3.Text = "View Listings";
                    stuff4.NavigateUrl = "/supplierlistings";
                    stuff4.Text = "View/Edit Listings";

                    var stuffList = new List<MenuItem>();

                    stuffList.Add(stuff1);
                    stuffList.Add(stuff2);
                    //stuffList.Add(stuff3);
                    stuffList.Add(stuff4);

                    for (int i = 0; i < stuffList.Count; i++)
                    {
                        Menu1.Items.Add(stuffList[i]);
                    }
                    Menu1.SkipLinkText = "";
                }
            }
        }
    }
}