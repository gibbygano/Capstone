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
        private List<CityState> _zips;
        private CityStateManager _cityStateManager = new CityStateManager();

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
            fillComboBox();
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

                    ResultsEdit userNameCheck = myLoginManager.CheckSupplierUserName(txtUserName.Text);

                    //if the name wasn't found - that's good
                    if (userNameCheck == ResultsEdit.NotFound)
                    {
                        decimal supplyCost = (decimal)(numSupplyCost.Value);

                        SupplierResult result = MySupplierManager.ApproveSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication, txtUserName.Text, supplyCost);

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
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        } 
      

        private bool ValidateApprovalFields()
        {
            //fields for approved supplier
            //send info to BLL
            if (!Validator.ValidateString(txtUserName.Text))
            {
                throw new InputValidationException(txtUserName, "Enter a user name.");

            }
            if(!Validator.ValidateDecimal(numSupplyCost.Value.ToString()))
            {
                throw new InputValidationException(numSupplyCost, "Enter a valid supply cost.");
            }
            return true;
        }


        private bool Validate()
        {
            if (!Validator.ValidateCompanyName(txtCompanyName.Text))
            {
                throw new InputValidationException(txtCompanyName, "Enter a company name.");
            }
            if (!Validator.ValidateAddress(txtAddress.Text))
            {
                throw new InputValidationException(txtAddress, "Enter an address.");
            }
            if (!Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                throw new InputValidationException(txtPhoneNumber, "Enter a phone number.");
            }
            if (!Validator.ValidateEmail(txtEmailAddress.Text))
            {
                throw new InputValidationException(txtEmailAddress, "Enter an email address.");
            }
            if (!Validator.ValidateString(txtFirstName.Text))
            {
                throw new InputValidationException(txtFirstName, "Enter a first name.");
            }
            if (!Validator.ValidateString(txtLastName.Text))
            {
                throw new InputValidationException(txtLastName, "Enter a last name.");
            }
            if (cboZip.SelectedItem == null)
            {
                throw new InputValidationException(cboZip, "You must select an zip from the drop down");
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
            UpdatedSupplierApplication.Zip = cboZip.SelectedValue.ToString();

            UpdatedSupplierApplication.PhoneNumber = this.txtPhoneNumber.Text;
            UpdatedSupplierApplication.EmailAddress = this.txtEmailAddress.Text;
            UpdatedSupplierApplication.ApplicationDate = CurrentSupplierApplication.ApplicationDate;
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
            this.txtPhoneNumber.Text = CurrentSupplierApplication.PhoneNumber;
            this.txtEmailAddress.Text = CurrentSupplierApplication.EmailAddress;
            this.dateApplicationDate.Content = CurrentSupplierApplication.ApplicationDate.ToString("D");
            this.cboAppStatus.Text = CurrentSupplierApplication.ApplicationStatus;
            this.txtRemarks.Text = CurrentSupplierApplication.Remarks;

            for (int i = 0; i < _zips.Count; i++)
            {
                if (_zips[i].Zip == CurrentSupplierApplication.Zip)
                {
                    cboZip.SelectedValue = _zips[i].Zip;
                }
            }
        }

        /// <summary>
        /// fills the zip code combo box
        /// created by will fritz 2/19/2015
        /// </summary>
        private void fillComboBox()
        {
            try
            {
                _zips = _cityStateManager.GetCityStateList();
                cboZip.ItemsSource = _zips;
                cboZip.DisplayMemberPath = "GetZipStateCity";
                cboZip.SelectedValuePath = "Zip";
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Error Retrieving the list of zip codes");
            }
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


        private void cboAppStatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboAppStatus.SelectedIndex.Equals(1))
            {
                numSupplyCost.IsEnabled = true;
                txtUserName.IsEnabled = true;
            }
            else
            {
                numSupplyCost.Value = .70;
                txtUserName.Text = "";

                numSupplyCost.IsEnabled = false;
                txtUserName.IsEnabled = false;
            }
        }
    }
}