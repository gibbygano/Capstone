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
    public partial class LoginBar : System.Web.UI.UserControl
    {
        public bool loggedIn = false;
        public Supplier _currentSupplier = null;
        public string supplierName = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    loggedIn = (bool)Session["loggedin"];
                }
                catch(Exception)
                {
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
            catch (Exception)
            {
                //do nothing
            }
        }
    }
}