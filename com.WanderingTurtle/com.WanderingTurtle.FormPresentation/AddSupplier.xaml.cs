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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// This Window allows the administrator to directly add a suplier
    /// </summary>
    public partial class AddSupplier : Window
    {
        private int _userID;
        private SupplierManager _manager = new SupplierManager();
        private List<Supplier> _suppliers;
        private Supplier _UpdatableSupplier;

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
                ClearFeilds();
                ListSuppliers.Instance.FillList();
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
            }
        }


        /////////////////////////////Custom Methods/////////////////////////////////////

        //checks to see if all the feilds are fill out and formated with the correct data
        //It returns a false if there is an invalid feilds(s) and will output an error message to the lblError label
        //Created By Will Fritz 2/4/15
        public bool Validate()
        {
            if (!Validator.ValidateAlphaNumeric(txtAddress1.Text) || !Validator.ValidateString(txtCompanyName.Text) || !Validator.ValidateString(txtFirstName.Text) || !Validator.ValidateString(txtLastName.Text) || !Validator.ValidatePhone(txtPhoneNumber.Text) || !Validator.ValidateAlphaNumeric(txtZip.Text))
            {
                lblError.Content = "You must fill out all of the feilds before you can continue.";
                return false;
            }
            else if (!Validator.ValidateInt(txtUserID.Text))
            {
                lblError.Content = "User ID feild must be a numeric value";
                return false;
            }
            else if (!Validator.ValidateEmail(txtEmail.Text))
            {
                lblError.Content = "Not a valid email address";
                return false;
            }

            _userID = Int32.Parse(txtUserID.Text);

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
                tempSupplier.Zip = txtZip.Text;
                tempSupplier.EmailAddress = txtEmail.Text;
                tempSupplier.UserID = _userID;

                _manager.AddANewSupplier(tempSupplier);
            }
            catch (Exception ex)
            {
                lblError.Content = ex.Message; //"There was an error adding the supplier";
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
            txtZip.Text = supplierUpdate.Zip;
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
                tempSupplier.Zip = txtZip.Text;
                tempSupplier.EmailAddress = txtEmail.Text;
                tempSupplier.UserID = _userID;

                tempSupplier.SupplierID = _UpdatableSupplier.SupplierID;

                _manager.EditSupplier(_UpdatableSupplier, tempSupplier);
            }
            catch (Exception)
            {
                lblError.Content = "There was an error editing the supplier";
            }
        }

        //this will clear all the feilds
        //created by Will Fritz 2/6/15
        private void ClearFeilds()
        {
            txtCompanyName.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtEmail.Text = "";
            txtPhoneNumber.Text = "";
            txtZip.Text = "";
            txtUserID.Text = "";
        }
    }
}
