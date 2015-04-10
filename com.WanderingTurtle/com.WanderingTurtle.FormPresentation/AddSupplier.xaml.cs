﻿using System;
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
    public partial class AddSupplier 
    {
        public static AddSupplier Instance;
        private int _userID;
        private SupplierManager _manager = new SupplierManager();
        private Supplier _UpdatableSupplier;
        private List<CityState> _zips;
        private CityStateManager _cityStateManager = new CityStateManager();

        /// <summary>
        /// Constructs the object and will fill the list of suppliers
        /// created by Will Fritz 2/6/15
        /// </summary>
        public AddSupplier()
        {
            InitializeComponent();
            btnEdit.IsEnabled = false;
            fillComboBox();  
            Instance = this;
        }
        public AddSupplier(Supplier supplierToEdit, bool ReadOnly = false)
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
                this.Close();
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
                this.Close();
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
                DialogBox.ShowMessageDialog(this, "Company Name field must be filled out and not contain special characters");
                return false;
            }
            else if (!Validator.ValidateInt(txtUserID.Text))
            {
                DialogBox.ShowMessageDialog(this, "User ID field must filled out and be a numeric value and must be 10 digits or less");
                return false;
            }
            else if (!Validator.ValidateEmail(txtEmail.Text.Trim()))
            {
                DialogBox.ShowMessageDialog(this, "Not a valid e-mail address");
                return false;
            }
            else if (!Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                DialogBox.ShowMessageDialog(this, "The phone number cannot start with a 1 and must filled out and be formated correctly (10 numeric digits)");
                return false;
            }
            else if (cboZip.SelectedItem == null)
            {
                DialogBox.ShowMessageDialog(this, "You must select an zip from the drop down");
                return false;
            }
            else if (!Validator.ValidateAlphaNumeric(txtAddress1.Text.Trim()))
            {
                DialogBox.ShowMessageDialog(this, "The address must be filled out and not contain special characters (spaces allowed)");
                return false;
            }
            else if (!Validator.ValidateString(txtFirstName.Text.Trim()))
            {
                DialogBox.ShowMessageDialog(this, "The fist name field filled out and must not contain special characters (No Spaces)");
                return false;
            }
            else if (!Validator.ValidateString(txtLastName.Text.Trim()))
            {
                DialogBox.ShowMessageDialog(this, "The last name field must be filled out and not contain special characters (No Spaces)");
                return false;
            }


            try
            {
                _userID = Int32.Parse(txtUserID.Text);
            }
            catch (Exception)
            {
                DialogBox.ShowMessageDialog(this, "User ID must be numeric");
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
        private void AddTheSupplier()
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

                _manager.AddANewSupplier(tempSupplier);

                DialogBox.ShowMessageDialog(this, "Supplier was added to the database");
            }
            catch (Exception)
            {
                DialogBox.ShowMessageDialog(this, "There was a problem adding the supplier to the database");
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
            //DialogBox.ShowMessageDialog(supplierUpdate.PhoneNumber);
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
            txtUserID.Text = supplierUpdate.UserID.ToString();

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
        private void EditSupplier()
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

                tempSupplier.SupplierID = _UpdatableSupplier.SupplierID;
                
                _manager.EditSupplier(_UpdatableSupplier, tempSupplier);

                DialogBox.ShowMessageDialog(this, "The Supplier was succefully edited");
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message);
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
            catch (Exception)
            {
                DialogBox.ShowMessageDialog(this, "There was a problem retriving the list of zip codes");
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
