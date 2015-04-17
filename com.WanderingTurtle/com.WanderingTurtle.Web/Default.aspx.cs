using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace com.WanderingTurtle.Web
{
    public partial class _Default : Page
    {
         private bool loggedIn = false;

         protected void Page_PreLoad(object sender, EventArgs e)
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
                 //if logged in, send them to portal page
                 Response.Redirect("~/portal");
             }
         }
        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}