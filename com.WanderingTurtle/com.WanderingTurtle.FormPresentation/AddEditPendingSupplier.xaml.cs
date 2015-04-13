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
        public SupplierLoginManager myLoginManager = new SupplierLoginManager();

        /// <summary>
        /// Created:  2015/04/04
        /// Miguel Santana
        /// 
        /// Interaction logic for AddEditPendingSupplier.xaml
        /// </summary>
        public AddEditPendingSupplier()
        {
            InitializeComponent();
            SetFields();
            ReloadComboBox();
        }

        /// <summary>
        /// Miguel Santana
        /// Created:  2015/04/09
        /// 
        /// Handles loading of the screen with data from the list.
        /// </summary>
        /// <param name="CurrentSupplierApplication"></param>
        /// <param name="ReadOnly"></param>
        public AddEditPendingSupplier(SupplierApplication CurrentSupplierApplication, bool ReadOnly = false)
        {
            InitializeComponent();
            this.CurrentSupplierApplication = CurrentSupplierApplication;
            ReloadComboBox();
            SetFields();
            
            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { btnCancel }); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //validates data from form
            if (!Validate()) { return; }

            try
            {
                //get data from form
                GetFormData();

                if (UpdatedSupplierApplication.ApplicationStatus.Equals(ApplicationStatus.Approved.ToString()))
                {
                    ValidateApprovalFields();

                    string userNameToAdd = txtUserName.Text;

                    ResultsEdit userNameCheck = myLoginManager.CheckSupplierUserName(userNameToAdd);

                    //if the name wasn't found - that's good
                    if (userNameCheck == ResultsEdit.NotFound)
                    {
                        decimal supplyCost = (decimal)(numSupplyCost.Value / 100);

                        SupplierResult result = MySupplierManager.ApproveSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication, userNameToAdd, supplyCost);

                        if (result == SupplierResult.Success)
                        {
                            await DialogBox.ShowMessageDialog(this, "Supplier application approved: Supplier added.");

                            this.Close();
                        }
                        else
                        {
                            throw new WanderingTurtleException(this, "Supplier wasn't added to the database");
                        }
                    }
                    else
                    {
                        throw new WanderingTurtleException(this, "UserName already in use.");
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


            }catch{
            }
        } 
      

        private bool ValidateApprovalFields()
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
            return true;
        }
        private bool Validate()
        {
            if (!Validator.ValidateCompanyName(txtCompanyName.Text))
            {
                txtCompanyName.Focus();
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

            return true;
        }

        private void GetFormData()
        {
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

        }

        /// <summary>
        /// 
        /// </summary>
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
        /// Pat Banks
        /// Created: 2015/04/11
        /// 
        /// Reloads the combobox with values
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            cboAppStatus.ItemsSource = GetStatusList;
        }
    }
}