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