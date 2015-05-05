using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.Web.Pages
{
    public partial class Password : System.Web.UI.Page
    {
        private bool loggedIn;
        private Supplier _currentSupplier;
        private SupplierLoginManager _myMan = new SupplierLoginManager();
        private SupplierLogin _currentLogin;
        protected void Page_PreLoad(object sender, EventArgs e)
        {
            try
            {
                //attempt to get session value if they are logged in
                loggedIn = (bool)Session["loggedin"];
                _currentSupplier = (Supplier)Session["user"];
                _currentLogin = (SupplierLogin)Session["login"];
            }
            catch (Exception)
            {
                //if it fails, the user must not have logged in on this
                //session yet, so set it to false
                Session["loggedIn"] = false;
                //send them to the login page
                Response.Redirect("~/login");
            }
            if (!loggedIn)
            {
                //if not logged in, send them to login page
                Response.Redirect("~/login");
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnChange_Click(object sender, EventArgs e)
        {
            //doublecheck client side validation
            if (txtNewPassword.Text.Trim() != txtNewPassword2.Text.Trim())
            {
                return;
            }
            if (!txtNewPassword.Text.Trim().ValidatePassword())
            {
                passRules.Visible = true;
                return;
            }
            passRules.Visible = false;
            PasswordManager myPass = new PasswordManager();
            string pass = myPass.supplierHash(_currentLogin.UserName, txtCurrent.Text);
            if (pass != _currentLogin.UserPassword)
            {
               
                lblError.Text = "Invalid Password";
                return;
            }
            lblError.Text = "";
            try
            {
                
                var result = _myMan.UpdateSupplierLogin(txtNewPassword.Text, _currentLogin);
                if (result == ResultsEdit.Success)
                {
                    _currentLogin.UserPassword = txtNewPassword.Text;
                    Session["login"] = _currentLogin;
                    //show alert!
                    Response.Redirect("~/portal");
                }
                else
                {
                    Label errorLabel = (Label)Master.FindControl("lblErrorMessage");
                    errorLabel.Text = "Error Updating Password. Please try again.";
                    Control c = Master.FindControl("ErrorMess");
                    c.Visible = true;

                    return;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //protected void BtnGen_Click(object sender, EventArgs e)
        //{
        //    PasswordManager myPass = new PasswordManager();
        //    txthash.Text = myPass.supplierHash(txtUser.Text, txtPass.Text);

        //}
    }
}