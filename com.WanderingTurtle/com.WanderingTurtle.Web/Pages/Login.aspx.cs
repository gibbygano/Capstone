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
                if (user.UserPassword != txtPassword.Text)
                {
                    //sql doesn't care about case, so it may return a positive user
                    //if the password entered mathces the letters, but not the case
                    //usernames are not case senstive but passwords are so we must check for
                    //this and throw an exception if they don't match
                    throw new ApplicationException();
                }
                var supplier = _mySuppMan.RetrieveSupplier(user.SupplierID);
                Session["user"] = supplier;
                Session["loggedIn"] = true;
                lblError.Text = "";
            }
            catch
            {
                Label errorLabel = (Label)Master.FindControl("lblErrorMessage");
                errorLabel.Text= "Authentication Error. Please try again.";
                Control c = Master.FindControl("ErrorMessage");
                c.Visible = true;

                return;
            }
            //if validated send to supplier portal page
            Response.Redirect("~/portal");
        }
    }
}