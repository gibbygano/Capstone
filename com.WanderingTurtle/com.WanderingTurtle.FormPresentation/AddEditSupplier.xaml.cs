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
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Models;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// This Window allows the administrator to directly add a suplier
    /// </summary>
    public partial class AddEditSupplier 
    {
        public static AddEditSupplier Instance;
        private int _userID = 9870;
        private SupplierManager _manager = new SupplierManager();
        private Supplier _UpdatableSupplier;
        private List<CityState> _zips;
        private CityStateManager _cityStateManager = new CityStateManager();

        /// <summary>
        /// Constructs the object and will fill the list of suppliers
        /// created by Will Fritz 2/6/15
        /// </summary>
        public AddEditSupplier()
        {
            InitializeComponent();
            btnEdit.IsEnabled = false;
            fillComboBox();  
            Instance = this;
        }
        public AddEditSupplier(Supplier supplierToEdit, bool ReadOnly = false)
        {
            InitializeComponent();
            Instance = this;
            this.Title = "Edit Supplier";
            fillComboBox();
            FillUpdateList(supplierToEdit);

            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] {  }); }
        }

        //////////////////////Windows Events//////////////////////////////

        /// <summary>
        /// Will fill the list and set error message to nothing
        /// created by Will Fritz 2/6/15
        /// </summary>
        /// /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //btnEdit.IsEnabled = false;
                      
        }

        /// <summary>
        /// Will validtate the feilds and edit the current supplier
        /// created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                EditSupplier();
                ListSuppliers.Instance.FillList();
               // this.Close();
            }
        }

        /// <summary>
        /// Will validate the fields and call add supplier method
        /// created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                AddTheSupplier();
                ListSuppliers.Instance.FillList();
                //this.Close();
            }
        }


        /////////////////////////////Custom Methods/////////////////////////////////////

        /// <summary>
        /// checks to see if all the feilds are fill out and formated with the correct data
        /// It returns a false if there is an invalid feilds(s) and will output an error message to the lblError label
        /// Created By Will Fritz 2/4/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/19/15
        /// </remarks>
        /// <returns></returns>
        public bool Validate()
        {
            if (!Validator.ValidateCompanyName(txtCompanyName.Text.Trim()))
            {
                throw new WanderingTurtleException(this, "Company Name field must be filled out and not contain special characters");
            }
            else if (!Validator.ValidateEmail(txtEmail.Text.Trim()))
            {
                throw new WanderingTurtleException(this, "Not a valid e-mail address");
            }
            else if (!Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                throw new WanderingTurtleException(this, "The phone number cannot start with a 1 and must filled out and be formated correctly (10 numeric digits)");
            }
            else if (cboZip.SelectedItem == null)
            {
                throw new WanderingTurtleException(this, "You must select an zip from the drop down");
            }
            else if (!Validator.ValidateAlphaNumeric(txtAddress1.Text.Trim()))
            {
                throw new WanderingTurtleException(this, "The address must be filled out and not contain special characters (spaces allowed)");
            }
            else if (!Validator.ValidateString(txtFirstName.Text.Trim()))
            {
                throw new WanderingTurtleException(this, "The fist name field filled out and must not contain special characters (No Spaces)");
            }
            else if (!Validator.ValidateString(txtLastName.Text.Trim()))
            {
                throw new WanderingTurtleException(this, "The last name field must be filled out and not contain special characters (No Spaces)");
            }

            return true;
        }

        /// <summary>
        /// This will send a supplier object to the business logic layer
        /// Created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/15/15
        /// Added a conformation message box
        /// </remarks>
        private async void AddTheSupplier()
        {
            try
            {
                Supplier tempSupplier = new Supplier();

                tempSupplier.CompanyName = txtCompanyName.Text.Trim();
                tempSupplier.FirstName = txtFirstName.Text.Trim();
                tempSupplier.LastName = txtLastName.Text.Trim();
                tempSupplier.Address1 = txtAddress1.Text.Trim();
                tempSupplier.Address2 = txtAddress2.Text.Trim();
                tempSupplier.PhoneNumber = txtPhoneNumber.Text;
                tempSupplier.Zip = cboZip.SelectedValue.ToString();
                tempSupplier.EmailAddress = txtEmail.Text.Trim();
                tempSupplier.UserID = _userID;
                tempSupplier.SupplyCost = (decimal)0.70;

                if (_manager.AddANewSupplier(tempSupplier) == SupplierResult.Success)
                {
                    await DialogBox.ShowMessageDialog(this, "Supplier was added to the database");
                }
                else
                {
                    throw new WanderingTurtleException(this, "Supplier wasnt added to the database");
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// This will fill the add/edit tab feilds with the data from a selected Supplier from the list view
        /// Created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/15/15
        /// changed zip to a drop down
        /// </remarks>
        /// <param name="supplierUpdate"></param>
        public void FillUpdateList(Supplier supplierUpdate)
        {
            txtCompanyName.Text = supplierUpdate.CompanyName.Trim();
            txtFirstName.Text = supplierUpdate.FirstName.Trim();
            txtLastName.Text = supplierUpdate.LastName.Trim();
            txtAddress1.Text = supplierUpdate.Address1.Trim();
            txtAddress2.Text = supplierUpdate.Address2.Trim();
            txtEmail.Text = supplierUpdate.EmailAddress.Trim();
            //throw new WanderingTurtleException(supplierUpdate.PhoneNumber);
            string phone = supplierUpdate.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            txtPhoneNumber.Text = phone;

            for (int i = 0; i < _zips.Count; i++)
            {
                if (_zips[i].Zip == supplierUpdate.Zip)
                {
                    cboZip.SelectedValue = _zips[i].Zip;
                }
            }
            //cboZip.SelectedValue = supplierUpdate.Zip;
            _UpdatableSupplier = supplierUpdate;

            btnSubmit.IsEnabled = false;
            btnEdit.IsEnabled = true;
        }

        /// <summary>
        /// This will send a supplier object to the business logic layer
        /// Created by Will Fritz 2/6/15
        /// </summary>
        /// <remarks>
        /// edited by will fritz 2/15/15
        /// added conformation message box
        /// </remarks>
        private async void EditSupplier()
        {
            try
            {
                Supplier tempSupplier = new Supplier();

                tempSupplier.CompanyName = txtCompanyName.Text;
                tempSupplier.FirstName = txtFirstName.Text;
                tempSupplier.LastName = txtLastName.Text;
                tempSupplier.Address1 = txtAddress1.Text;
                tempSupplier.Address2 = txtAddress2.Text;
                tempSupplier.PhoneNumber = txtPhoneNumber.Text;
                tempSupplier.Zip = cboZip.SelectedValue.ToString();
                tempSupplier.EmailAddress = txtEmail.Text;
                tempSupplier.UserID = _userID;
                tempSupplier.SupplyCost = (decimal)0.70;

                tempSupplier.SupplierID = _UpdatableSupplier.SupplierID;

                
                if(_manager.EditSupplier(_UpdatableSupplier, tempSupplier) == SupplierResult.Success)
                {
                    await DialogBox.ShowMessageDialog(this, "The Supplier was succefully edited");
                }
                else
                {
                    throw new WanderingTurtleException(this, "Supplier wasnt added to the database");
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
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
                throw new WanderingTurtleException(this, ex, "Error Retriving the list of zip codes");
            }


           
        }

        /// <summary>
        /// necessay to make the singleton pattern work
        /// Will Fritz 2015/3/6
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }
    }
}
