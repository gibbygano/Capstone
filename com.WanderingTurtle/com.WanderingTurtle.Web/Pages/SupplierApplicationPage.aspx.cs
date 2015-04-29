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
                    lblError.Text = "There was an error loading Zip Code Information: " + ex.Message;
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

        protected void btnSubmitApplication_Click(object sender, EventArgs e)
        {
            int errorCount = 0;
            lblFinish.Text = "";
            //Validate
            if (validateText(txtCompanyName, "Please enter a company name")) errorCount++;
            if (validateText(txtFirstName, "Please enter your first name")) errorCount++;
            if (validateText(txtLastName, "Please enter your second name")) errorCount++;
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
            if (!Validator.ValidateEmail(txtEmail.Text))
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
            if (!Validator.ValidatePhone(txtPhoneNumber.Text))
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
                    //create new Suppler Application                                                                           
                    SupplierApplication application = new SupplierApplication(txtCompanyName.Text, txtDescription.Text, txtFirstName.Text, txtLastName.Text, txtAddress.Text, txtAddress2.Text, txtZip.Text, txtPhoneNumber.Text, txtEmail.Text, DateTime.Now);
                    SupplierResult result = ApplicationManager.AddASupplierApplication(application);

                    if (result==SupplierResult.Success)
                    {
                        showError("Record added");
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
            lblError.Text = _errorMessage;
        }
    }
}