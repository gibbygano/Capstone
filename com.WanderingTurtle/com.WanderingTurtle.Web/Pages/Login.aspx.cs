using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Web
{
    public partial class About : Page
    {
        private SupplierLoginManager _myMan = new SupplierLoginManager();
        private SupplierManager _mySuppMan = new SupplierManager();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void CheckLogin(object sender, EventArgs e)
        {
            try
            {
                var user = _myMan.retrieveSupplierLogin(txtPassword.Text, txtUserName.Text);
                var supplier = _mySuppMan.RetrieveSupplier(user.SupplierID);
                Session["user"] = supplier;
                Session["loggedIn"] = true;
                lblError.Text = "";
            }
            catch
            {
                lblError.Text = "Authentication Error. Please try again.";
            }

            Response.Redirect("~/Pages/SupplierViewEvents.aspx");
        }
    }
}