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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// This Window allows the administrator to directly add a suplier
    /// </summary>
    public partial class AddSupplier : Window
    {
        private int _userID;
        private SupplierManager _manager = new SupplierManager();
        private Supplier _UpdatableSupplier;
        private List<CityState> _zips;
        private CityStateManager _cityStateManager = new CityStateManager();

        //Constructs the object and will fill the list of suppliers
        //created by Will Fritz 2/6/15
        //edited by will fritz 2/15/15
        public AddSupplier()
        {
            InitializeComponent();
        }

        //////////////////////Windows Events//////////////////////////////

        //Will fill the list and set error message to nothing
        //created by Will Fritz 2/6/15
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblError.Content = "";
            btnEdit.IsEnabled = false;
            fillComboBox();
            
        }

        //Will validtate the feilds and edit the current supplier
        //created by Will Fritz 2/6/15
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                lblError.Content = "";
                EditSupplier();
                ListSuppliers.Instance.FillList();
                this.Close();
            }
        }

        //Will validate the fields and call add supplier method
        //created by Will Fritz 2/6/15
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                lblError.Content = "";
                AddTheSupplier();
                ListSuppliers.Instance.FillList();
                this.Close();
            }
        }


        /////////////////////////////Custom Methods/////////////////////////////////////

        //checks to see if all the feilds are fill out and formated with the correct data
        //It returns a false if there is an invalid feilds(s) and will output an error message to the lblError label
        //Created By Will Fritz 2/4/15
        public bool Validate()
        {
            if (!Validator.ValidateAlphaNumeric(txtCompanyName.Text))
            {
                lblError.Content = "Company Name field must be filled out and not contain special characters";
                return false;
            }
            else if (!Validator.ValidateInt(txtUserID.Text))
            {
                lblError.Content = "User ID field must filled out and be a numeric value and must be 10 digits or less";
                return false;
            }
            else if (!Validator.ValidateEmail(txtEmail.Text))
            {
                lblError.Content = "Not a valid e-mail address";
                return false;
            }
            else if (!Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                lblError.Content = "The phone number must filled out and be formated correctly (10 digits)";
                return false;
            }
            else if (cboZip.SelectedItem == null)
            {
                lblError.Content = "You must select an zip from the drop down";
                return false;
            }
            else if(!Validator.ValidateAlphaNumeric(txtAddress1.Text))
            {
                lblError.Content = "The address must be filled out and not contain special characters (spaces allowed)";
                return false;
            }
            else if (!Validator.ValidateString(txtFirstName.Text))
            {
                lblError.Content = "The fist name field filled out and must not contain special characters (No Spaces)";
                return false;
            }
            else if(!Validator.ValidateString(txtLastName.Text))
            {
                lblError.Content = "The last name field must be filled out and not contain special characters (No Spaces)";
                return false;
            }


            try
            {
                _userID = Int32.Parse(txtUserID.Text);
            }
            catch (Exception ex)
            {
                lblError.Content = "User ID must be numeric";
            }

            return true;
        }

        //This will send a supplier object to the business logic layer
        //Created by Will Fritz 2/6/15
        //edited by will fritz 2/15/15
        private void AddTheSupplier()
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
                tempSupplier.Zip = cboZip.Text;
                tempSupplier.EmailAddress = txtEmail.Text;
                tempSupplier.UserID = _userID;

                _manager.AddANewSupplier(tempSupplier);

                System.Windows.Forms.MessageBox.Show("Supplier was added to the database");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //This will fill the add/edit tab feilds with the data from a selected Supplier from the list view
        //Created by Will Fritz 2/6/15
        public void FillUpdateList(Supplier supplierUpdate)
        {
            txtCompanyName.Text = supplierUpdate.CompanyName;
            txtFirstName.Text = supplierUpdate.FirstName;
            txtLastName.Text = supplierUpdate.LastName;
            txtAddress1.Text = supplierUpdate.Address1;
            txtAddress2.Text = supplierUpdate.Address2;
            txtEmail.Text = supplierUpdate.EmailAddress;
            txtPhoneNumber.Text = supplierUpdate.PhoneNumber;
            cboZip.Text = supplierUpdate.Zip;
            txtUserID.Text = supplierUpdate.UserID.ToString();

            _UpdatableSupplier = supplierUpdate;

            btnSubmit.IsEnabled = false;
            btnEdit.IsEnabled = true;
        }

        //This will send a supplier object to the business logic layer
        //Created by Will Fritz 2/6/15
        //edited by will fritz 2/15/15
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
                tempSupplier.Zip = cboZip.Text;
                tempSupplier.EmailAddress = txtEmail.Text;
                tempSupplier.UserID = _userID;

                tempSupplier.SupplierID = _UpdatableSupplier.SupplierID;
                
                _manager.EditSupplier(_UpdatableSupplier, tempSupplier);

                System.Windows.Forms.MessageBox.Show("The Supplier was succefully edited");
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("There was an error editing the supplier");
            }
        }

        //fills the zip code combo box
        //created by will fritz 2/19/2015
        private void fillComboBox()
        {
            try
            {
                _zips = _cityStateManager.GetCityStateList();
                cboZip.ItemsSource = _zips;
            }
            catch (Exception)
            {
                lblError.Content = "There was a problem retriving the list of zip codes";
            }


           
        }
    }
}
