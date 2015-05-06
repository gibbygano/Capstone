using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace com.WanderingTurtle.Web.Pages
{
    public partial class Logout : System.Web.UI.Page
    {
        /// <summary>
        /// Matt Lapka
        /// Created 2015/04/12
        /// Clears login data from session variables
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //force the session values to be reset
            try
            {
                Session["user"] = null;
                Session["loggedIn"] = false;
            }
            catch (Exception)
            {
                Session["user"] = null;
                Session["loggedIn"] = false;
            }
            Response.Redirect("~/Default.aspx");
        }
    }
}