using System;
using System.Drawing;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.Web.Pages
{  
    /// <summary>
    /// Web facing form for Suppliers to add a new Event.
    /// Created by: Kelsey Blount 2015/04/07
    /// </summary>
    public partial class SupplierApplicationPage : System.Web.UI.Page
    {
        private string _errorMessage = "";
        private SupplierManager ApplicationManager = new SupplierManager();
        private bool validateText(System.Web.UI.WebControls.TextBox textBox, String toolTipText)
        {
            if (String.IsNullOrEmpty(textBox.Text)) 
            {
                textBox.ToolTip = toolTipText;
                textBox.BorderColor = Color.Red;
                return true;
            }
            return false;
        }

        protected void btnSubmitApplication_Click(object sender, EventArgs e)
        {
            int errorCount = 0;
            lblFinish.Text = "";
            //Validate
            if (validateText(txtCompanyName, "Please enter a company name")) errorCount++;
            if (validateText(txtFirstName, "Please enter your first name")) errorCount++;
            if (validateText(txtLastName, "Please enter your second name")) errorCount++;
            if (validateText(txtAddress, "Please enter your address")) errorCount++;
            if (validateText(txtZip, "Please enter your zip code")) errorCount++;
            if (validateText(txtDescription, "Please enter your description")) errorCount++;
            if (validateText(txtEmail, "Please enter your email")) errorCount++;
            if (validateText(txtPhoneNumber, "Please enter your phone number")) errorCount++;

            if (errorCount > 0)
            {
                showError("You have " + errorCount + " errors that need to be fixed.");
                return;
            }
            else
            {
                //reset border colors
                txtCompanyName.BorderColor = Color.Empty;
                txtFirstName.BorderColor = Color.Empty;
                txtLastName.BorderColor = Color.Empty;
                txtAddress.BorderColor = Color.Empty;
                txtZip.BorderColor = Color.Empty;
                txtDescription.BorderColor = Color.Empty;
                txtEmail.BorderColor = Color.Empty;
                txtPhoneNumber.BorderColor = Color.Empty;

                try
                {
                    //create new Event
                    SupplierApplication application = new SupplierApplication(txtCompanyName.Text, txtDescription.Text, txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtAddress2.Text, txtZip.Text, txtPhoneNumber.Text, txtEmail.Text, DateTime.Today);
                    ApplicationManager.AddASupplierApplication(application);
                }
                catch (Exception ex)
                {
                    showError(ex.Message);
                }
            }
        }

        private void clearForm()
        {
            txtZip.Text = "";
            txtDescription.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress.Text = "";
            txtCompanyName.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            showError("");
        }

        private void showError(String message) 
        {
            _errorMessage = message;
            lblError.Text = _errorMessage;
        }
    }
}