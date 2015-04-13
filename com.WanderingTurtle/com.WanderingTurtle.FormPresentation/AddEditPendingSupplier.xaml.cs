using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddEditPendingSupplier.xaml
    /// </summary>
    public partial class AddEditPendingSupplier
    {
        public SupplierApplication CurrentSupplierApplication;
        public SupplierApplication UpdatedSupplierApplication = new SupplierApplication();
        public SupplierManager MySupplierManager = new SupplierManager();

        public AddEditPendingSupplier()
        {
            InitializeComponent();
            SetFields();
            ReloadComboBox();
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/04/10
        ///
        /// Defines application status for the combo box
        /// </summary>
        /// <remarks>
        /// </remarks>
        private List<ApplicationStatus> GetStatusList { get { return new List<ApplicationStatus>((IEnumerable<ApplicationStatus>)Enum.GetValues(typeof(ApplicationStatus))); } }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/04/10
        /// 
        /// Reloads the combobox with values
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            cboAppStatus.ItemsSource = GetStatusList;
        }

        public AddEditPendingSupplier(SupplierApplication CurrentSupplierApplication, bool ReadOnly = false)
        {
            InitializeComponent();
            this.CurrentSupplierApplication = CurrentSupplierApplication;
            ReloadComboBox();
            SetFields();
            
            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { btnCancel }); }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (!Validator.ValidateCompanyName(txtCompanyName.Text))
            {
                throw new WanderingTurtleException(this, "Enter a company name.");
            }
            if (!Validator.ValidateAddress(txtAddress.Text))
            {
                throw new WanderingTurtleException(this, "Enter an address.");
            }
            if (!Validator.ValidatePhone(txtPhoneNumber.Text))
	        {
                throw new WanderingTurtleException(this, "Enter a phone number.");
	        }
            if (!Validator.ValidateEmail(txtEmailAddress.Text))
            {
                throw new WanderingTurtleException(this, "Enter an email address.");
            }
            if (!Validator.ValidateString(txtFirstName.Text))
            {
                throw new WanderingTurtleException(this, "Enter a first name.");
            }
            if (!Validator.ValidateString(txtLastName.Text))
            {
                throw new WanderingTurtleException(this, "Enter a last name.");
            }

            //get data from form
            UpdatedSupplierApplication.CompanyName = this.txtCompanyName.Text;
            UpdatedSupplierApplication.CompanyDescription = this.txtCompanyDescription.Text;
            UpdatedSupplierApplication.FirstName = this.txtFirstName.Text;
            UpdatedSupplierApplication.LastName = this.txtLastName.Text;
            UpdatedSupplierApplication.Address1 = this.txtAddress.Text;
            UpdatedSupplierApplication.Address2 = this.txtAddress2.Text;
            UpdatedSupplierApplication.Zip = this.txtZip.Text;
            UpdatedSupplierApplication.PhoneNumber = this.txtPhoneNumber.Text;
            UpdatedSupplierApplication.EmailAddress = this.txtEmailAddress.Text;
            UpdatedSupplierApplication.ApplicationDate = this.dateApplicationDate.DisplayDate;
            UpdatedSupplierApplication.ApplicationStatus = this.cboAppStatus.SelectedValue.ToString();
            UpdatedSupplierApplication.Remarks = this.txtRemarks.Text;

            //application id from record in memory
            UpdatedSupplierApplication.ApplicationID = CurrentSupplierApplication.ApplicationID;
            UpdatedSupplierApplication.LastStatusDate = DateTime.Now;

            if (UpdatedSupplierApplication.ApplicationStatus.Equals(ApplicationStatus.Approved.ToString()))
            {
                //fields for approved supplier
                //send info to BLL
                if (!Validator.ValidateString(txtUserName.Text))
                {
                    throw new WanderingTurtleException(this, "Enter a user name.");
                }
                if(!Validator.ValidateDecimal(numSupplyCost.Value.ToString()))
                {
                    throw new WanderingTurtleException(this, "Enter a valid supply cost.");
                }
                
                string userNameToAdd = txtUserName.Text;
                decimal supplyCost = (decimal)(numSupplyCost.Value/100);


                SupplierResult result = MySupplierManager.ApproveSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication, userNameToAdd, supplyCost );

                if (result == SupplierResult.Success)
                {
                    await DialogBox.ShowMessageDialog(this, "Supplier application approved: Supplier added.");
                    this.Close();
                }
                else
                {
                    throw new WanderingTurtleException(this, "Supplier wasnt added to the database");
                }
            }
            else if (UpdatedSupplierApplication.ApplicationStatus.Equals(ApplicationStatus.Rejected.ToString()) || UpdatedSupplierApplication.ApplicationStatus.Equals(ApplicationStatus.Pending.ToString()))
            {
                SupplierResult result = MySupplierManager.EditSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication);

                if (result == SupplierResult.Success)
                {
                    await DialogBox.ShowMessageDialog(this, "Supplier application updated.");
                    this.Close();
                }
                else
                {
                    throw new WanderingTurtleException(this, "DB Error");
                }
            }
            else
            {
                throw new WanderingTurtleException(this, "DB Error.");
            }   
        }

        private void SetFields()
        {
            this.txtCompanyName.Text = CurrentSupplierApplication.CompanyName;
            this.txtCompanyDescription.Text = CurrentSupplierApplication.CompanyDescription;
            this.txtFirstName.Text = CurrentSupplierApplication.FirstName;
            this.txtLastName.Text = CurrentSupplierApplication.LastName;
            this.txtAddress.Text = CurrentSupplierApplication.Address1;
            this.txtAddress2.Text = CurrentSupplierApplication.Address2;
            this.txtZip.Text = CurrentSupplierApplication.Zip;
            this.txtPhoneNumber.Text = CurrentSupplierApplication.PhoneNumber;
            this.txtEmailAddress.Text = CurrentSupplierApplication.EmailAddress;
            this.dateApplicationDate.SelectedDate = CurrentSupplierApplication.ApplicationDate;
            this.cboAppStatus.Text = CurrentSupplierApplication.ApplicationStatus;
            this.txtRemarks.Text = CurrentSupplierApplication.Remarks;            
        }
    }
}