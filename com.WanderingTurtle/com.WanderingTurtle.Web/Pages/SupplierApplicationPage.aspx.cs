using System;
using System.Drawing;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System.Collections.Generic;

namespace com.WanderingTurtle.Web.Pages
{  
    /// <summary>
    /// Web facing form for Suppliers to add a new Event.
    /// Created by: Kelsey Blount 2015/04/07
    /// Updated: 2015/05/01
    /// Added jQuery Autocomplete to Zip Code field
    /// Updated: 2015/05/03 by Matt Lapka
    /// Added jQuery dialog messages
    /// </summary>
    public partial class SupplierApplicationPage : System.Web.UI.Page
    {
        public static List<CityState> zips = new List<CityState>();
        private CityStateManager zipMan = new CityStateManager();
        private string _errorMessage = "";
        private SupplierManager ApplicationManager = new SupplierManager();

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                try
                {
                    zipMan.PopulateCityStateCache();
                    zips = DataCache._currentCityStateList;
                }
                catch (Exception ex)
                {
                    lblMessage.Text = "There was an error loading Zip Code Information: " + ex.Message;
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
                }
            }
        }
        private bool validateText(System.Web.UI.WebControls.TextBox textBox, String toolTipText)
        {
            if (String.IsNullOrEmpty(textBox.Text)) 
            {
                textBox.ToolTip = toolTipText;
                textBox.BorderColor = Color.Red;
                return true;
            }
            textBox.BorderColor = Color.Empty;
            return false;
        }
        private bool validateName(System.Web.UI.WebControls.TextBox textBox, String toolTipText)
        {
            if (String.IsNullOrEmpty(textBox.Text))
            {
                textBox.ToolTip = toolTipText;
                textBox.BorderColor = Color.Red;
                return true;
            }
            else if (!Validator.ValidateString(textBox.Text))
            {
                textBox.ToolTip = toolTipText;
                textBox.BorderColor = Color.Red;
                return true;
            }
            textBox.BorderColor = Color.Empty;
            return false;
        }

        private void clearBorderColors() 
        {
            txtCompanyName.BorderColor = Color.Empty;
            txtFirstName.BorderColor = Color.Empty;
            txtLastName.BorderColor = Color.Empty;
            txtAddress.BorderColor = Color.Empty;
            txtZip.BorderColor = Color.Empty;
            txtDescription.BorderColor = Color.Empty;
            txtEmail.BorderColor = Color.Empty;
            txtPhoneNumber.BorderColor = Color.Empty;
        }
        protected void btnSubmitApplication_Click(object sender, EventArgs e)
        {

            clearBorderColors();
            int errorCount = 0;
            lblFinish.Text = "";
            //Validate
            if (validateText(txtCompanyName, "Please enter a company name")) errorCount++;
            if (validateName(txtFirstName, "Please enter your first name")) errorCount++;
            if (validateName(txtLastName, "Please enter your second name")) errorCount++;
            if (validateText(txtAddress, "Please enter your address")) errorCount++;
            //check zips
            bool goodZip = false;
            foreach (var zip in zips)
            {
                if (txtZip.Text == zip.Zip)
                {
                    goodZip = true;
                }
            }
            if (!goodZip)
            {
                txtZip.ToolTip = "Please enter a valid Zip Code";
                txtZip.BorderColor = Color.Red;
                errorCount++;
            }

            if (validateText(txtDescription, "Please enter your description")) errorCount++;
            if (!txtEmail.Text.ValidateEmail())
            {
                txtEmail.ToolTip = "Please enter a valid e-mail";
                txtEmail.BorderColor = Color.Red;
                errorCount++;
            }
            else
            {
                txtEmail.ToolTip = "";
                txtEmail.BorderColor = Color.Empty;
            }
            if (!txtPhoneNumber.Text.ValidatePhone())
            {
                txtPhoneNumber.ToolTip = "Please enter a valid phone number";
                txtPhoneNumber.BorderColor = Color.Red;
            }
            else
            {
                txtPhoneNumber.ToolTip = "";
                txtPhoneNumber.BorderColor = Color.Empty;
            }

            if (errorCount > 0)
            {
                showError("You have " + errorCount + " errors that need to be fixed.");
                return;
            }
            else
            {
                //reset border colors
                clearBorderColors();

                try
                {
                    //create new Suppler Application                                                                           
                    SupplierApplication application = new SupplierApplication(txtCompanyName.Text, txtDescription.Text, txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtAddress2.Text, txtZip.Text, txtPhoneNumber.Text, txtEmail.Text, DateTime.Now);
                    SupplierResult result = ApplicationManager.AddASupplierApplication(application);

                    if (result==SupplierResult.Success)
                    {
                        showError("Thank you for your Application!<br /> We will be in contact once we have reviewed the information. ");
                        clearForm();
                    }

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
        }

        private void showError(String message) 
        {
            _errorMessage = message;
            lblMessage.Text = _errorMessage;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "showit", "showMessage()", true);
        }
    }
}