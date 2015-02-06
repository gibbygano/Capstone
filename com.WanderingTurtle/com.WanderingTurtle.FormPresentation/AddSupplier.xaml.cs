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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// This Window allows the administrator to directly add a suplier
    /// </summary>
    public partial class AddSupplier : Window
    {
        private int _userID;
        private SupplierManger _manager = new SupplierManger();
        private List<Supplier> _suppliers = _manager.RetriveSupplierList();

        public AddSupplier()
        {
            InitializeComponent();
        }

        //////////////////////Windows Events//////////////////////////////
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            cboSupplierType.Items.Add("Code Debugger");
            lblError.Content = "";
            SetUpComboBox();
            FillList();
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
                Supplier supplierToUpdate = (Supplier)lvSuppliersList.SelectedItems[0];
                FillUpdateList(supplierToUpdate);
                tabControl.SelectedIndex = 0;
            }
            catch (Exception)
            {
                lblError.Content = "You Must Select A Supplier Before You Can Update";
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Supplier supplierToDelete = (Supplier)lvSuppliersList.SelectedItems[0];
                _manager.ArchiveSupplier(supplierToDelete);
            }
            catch (Exception)
            {
                lblError.Content = "You Must Select A Supplier Before You Can Delete";
            }

        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                lblError.Content = "";
                AddSupplier();
            }

            //TODO must set set ApplicationDate and ApprovalDate to the current date and must set Approved to true if we are generating an application for the supplier apon adding them
        }


        /////////////////////////////Custom Methods/////////////////////////////////////

        //checks to see if all the feilds are fill out and formated with the correct data
        //It returns a false if there is an invalid feilds(s) and will output an error message to the lblError label
        //Created By Will Fritz 2/4/15
        public bool Validate()
        {
            if (!Validate.ValidateString(txtEmail.Text) || !Validate.ValidateString(txtAddress1.Text) || !Validate.ValidateString(txtCompanyDescription.Text) || !Validate.ValidateString(txtCompanyName.Text) || !Validate.ValidateString(txtFirstName.Text) || !Validate.ValidateString(txtLastName.Text) || !Validate.ValidateString(txtPhoneNumber.Text) || !Validate.ValidateString(txtZip.Text) || !Validate.ValidateString(txtUserID.Text) || cboSupplierType.SelectedIndex < 0)
            {
                lblError.Content = "You must fill out all of the feilds and dropdowns before you can continue.";
                return false;
            }
            else if (!Validate.ValidateInt(txtUserID))
            {
                lblError.Content = "User ID feild must be a numeric value";
                return false;
            }
            else if (!Validate.ValidateEmail(txtEmail.Text))
            {
                lblError.Content = "Not a valid email address";
                return false;
            }

            _userID = Int32.Parse(txtUserID.Text);

            return true;
        }

        //fills the combo box with the supplier types
        //created by will Fritz 2/5/15
        public void SetUpComboBox()
        { 
            
            try
            {
                List<SupplierType> supplierTypes[] = _manager.getListOfSupplierTypes();
                foreach(SupplierType type in supplierTypes)
                {
                    cboSupplierType.Items.Add(type);
                }
            }
            catch(Exception e)
            {
                lblError.Content = "Error loading supplier Types";
            }
             

        }

        //Fills the list view with the suppliers
        //created by Will Fritz 2/6/15
        public void FillList()
        {
            lvSuppliersList.Items.Clear();
            lvSuppliersList.ItemsSource = _suppliers;
        }

        //This will send a supplier object to the business logic layer
        //Created by Will Fritz 2/6/15
        public void AddSupplier()
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

                _manager.AddSupplier(tempSupplier);
            }
            catch (Exception)
            {
                lblError.Content = "There was an error adding the supplier";
            }
        }

        //This will fill the add/edit tab feilds with the data from a selected Supplier from the list view
        //Created by Will Fritz 2/6/15
        public void FillUpdateList(Supplier supplierUpdate)
        {
            txtCompanyName.Text = supplierUpdate.CompanyName;
            txtCompanyDescription.Text = supplierUpdate.CompanyDescription;
            txtFirstName.Text = supplierUpdate.FistName;
            txtLastName.Text = supplierUpdate.LastName;
            txtAddress1.Text = supplierUpdate.Address1;
            txtAddress2.Text = supplierUpdate.Address2;
            txtEmail.Text = supplierUpdate.Email;
            txtPhoneNumber.Text = supplierUpdate.PhoneNumber;
            txtZip.Text = supplierUpdate.Zip;
            txtUserID.Text = supplierUpdate.UserID.ToString();
        }
    }
}
