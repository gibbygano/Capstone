using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.Web.Pages
{
    /// <summary>
    /// Matt Lapka
    /// Created: on 2015/04/10
    /// Defines the area on all supplier portal pages that gives allows a user to log in or out
    /// or welcomes them if they are logged in.
    /// </summary>
    public partial class LoginBar : System.Web.UI.UserControl
    {
        public bool loggedIn = false;
        public Supplier _currentSupplier = null;
        public string supplierName = "";
        protected void Page_Load(object sender, EventArgs e)
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
                _currentSupplier = (Supplier)Session["user"];
                userLoggedIn.Style.Add("display", "block");
                userLoggedOut.Style.Add("display", "none");
            }
            else
            {
                _currentSupplier = null;
                userLoggedIn.Style.Add("display", "none");
                userLoggedOut.Style.Add("display", "block");
            }


        }
    }
}