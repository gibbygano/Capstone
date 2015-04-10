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
        public SupplierApplication CurrentSupplierApplication = null;

        public AddEditPendingSupplier()
        {
            InitializeComponent();
            SetFields();
        }

        public AddEditPendingSupplier(SupplierApplication CurrentSupplierApplication, bool ReadOnly = false)
        {
            InitializeComponent();
            this.CurrentSupplierApplication = CurrentSupplierApplication;
            SetFields();

            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { btnCancel }); }
        }

        public SupplierApplication UpdatedSupplierApplication = new SupplierApplication();
        public SupplierManager MySupplierManager = new SupplierManager();

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

            if (!Validator.ValidateCompanyName(txtCompanyName.Text))
            {
                DialogBox.ShowMessageDialog(this, "Enter a company name.");
                return;
            }
            if (!Validator.ValidateAddress(txtAddress.Text))
            {
                DialogBox.ShowMessageDialog(this, "Enter an address.");
                return;
            }
            if (!Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                DialogBox.ShowMessageDialog(this, "Enter a phone number.");
                return;
            }
            if (!Validator.ValidateEmail(txtEmailAddress.Text))
            {
                DialogBox.ShowMessageDialog(this, "Enter an email address.");
                return;
            }
            if (!Validator.ValidateString(txtFirstame.Text))
            {
                DialogBox.ShowMessageDialog(this, "Enter a first name.");
            }
            if (!Validator.ValidateString(txtLastName.Text))
            {
                DialogBox.ShowMessageDialog(this, "Enter a last name.");
            }

            UpdatedSupplierApplication.CompanyName = this.txtCompanyName.Text;
            UpdatedSupplierApplication.CompanyDescription = this.txtCompanyDescription.Text;
            UpdatedSupplierApplication.FirstName = this.txtFirstame.Text;
            UpdatedSupplierApplication.LastName = this.txtLastName.Text;
            UpdatedSupplierApplication.Address1 = this.txtAddress.Text;
            UpdatedSupplierApplication.Address2 = this.txtAddress2.Text;
            UpdatedSupplierApplication.Zip = this.txtZip.Text;
            UpdatedSupplierApplication.PhoneNumber = this.txtPhoneNumber.Text;
            UpdatedSupplierApplication.EmailAddress = this.txtEmailAddress.Text;
            UpdatedSupplierApplication.ApplicationDate = this.dateApplicationDate.DisplayDate;
            UpdatedSupplierApplication.FinalStatusDate = dateApprovalDate.DisplayDate.Date;             
            UpdatedSupplierApplication.Approved = cbApproved.IsChecked.Value;

            UpdatedSupplierApplication.ApplicationID = CurrentSupplierApplication.ApplicationID;       
                
            /*if (cbApproved.IsChecked == true)
{
    NewSupplier.CompanyName = UpdatedSupplierApplication.CompanyName;
    NewSupplier.FirstName = UpdatedSupplierApplication.FirstName;
    NewSupplier.LastName = UpdatedSupplierApplication.LastName;
    NewSupplier.Address1 = UpdatedSupplierApplication.Address1;
    NewSupplier.Address2 = UpdatedSupplierApplication.Address2;
    NewSupplier.Zip = UpdatedSupplierApplication.Zip;
    NewSupplier.PhoneNumber = UpdatedSupplierApplication.Zip;
    NewSupplier.EmailAddress = UpdatedSupplierApplication.EmailAddress;
    NewSupplier.ApplicationID = UpdatedSupplierApplication.ApplicationID;
    NewSupplier.SupplyCost = (decimal)this.numSupplyCost.Value;

            MySupplierManager.EditSupplierApplication(NewSupplierApplication, UpdatedSupplierApplication);

            DialogBox.ShowMessageDialog(this, "Supplier application approved: Supplier added.");
            SetFields();
            disableApprovedFields();
        } */

        private void SetFields()
        {
            disableApprovedFields();

            if (CurrentSupplierApplication == null)
            {
                this.txtCompanyName.Text = null;
                this.txtCompanyDescription.Text = null;
                this.txtFirstame.Text = null;
                this.txtLastName.Text = null;
                this.txtAddress.Text = null;
                this.txtAddress2.Text = null;
                this.txtZip.Text = null;
                this.txtPhoneNumber.Text = null;
                this.txtEmailAddress.Text = null;
                this.dateApplicationDate.SelectedDate = DateTime.Now.Date;
                this.cbApproved.IsChecked = false;
                this.dateApprovalDate.SelectedDate = DateTime.Now.Date;
                this.numSupplyCost.Value = 70;
                this.pwPassword.Clear();
                this.txtUserId.Clear();
            }
            else
            {
                this.txtCompanyName.Text = CurrentSupplierApplication.CompanyName;
                this.txtCompanyDescription.Text = CurrentSupplierApplication.CompanyDescription;
                this.txtFirstame.Text = CurrentSupplierApplication.FirstName;
                this.txtLastName.Text = CurrentSupplierApplication.LastName;
                this.txtAddress.Text = CurrentSupplierApplication.Address1;
                this.txtAddress2.Text = CurrentSupplierApplication.Address2;
                this.txtZip.Text = CurrentSupplierApplication.Zip;
                this.txtPhoneNumber.Text = CurrentSupplierApplication.PhoneNumber;
                this.txtEmailAddress.Text = CurrentSupplierApplication.EmailAddress;
                this.dateApplicationDate.SelectedDate = CurrentSupplierApplication.ApplicationDate;
                this.cbApproved.IsChecked = CurrentSupplierApplication.Approved;
                this.dateApprovalDate.SelectedDate = CurrentSupplierApplication.FinalStatusDate;
            }
        }

        private void cbApproved_Checked(object sender, RoutedEventArgs e)
        {
            txtUserId.IsEnabled = true;
            pwPassword.IsEnabled = true;
            numSupplyCost.IsEnabled = true;
            dateApprovalDate.IsEnabled = true;
        }

        private void cbApproved_Unchecked(object sender, RoutedEventArgs e)
        {
            disableApprovedFields();
        }

        private void disableApprovedFields()
        {
            txtUserId.IsEnabled = false;
            pwPassword.IsEnabled = false;
            numSupplyCost.IsEnabled = false;
            dateApprovalDate.IsEnabled = false;
        }
    }
}


//}DON"T NEED
//else if(CurrentSupplierApplication == null){

//    NewSupplierApplication.CompanyName = txtCompanyName.Text;
//    NewSupplierApplication.CompanyDescription = txtCompanyDescription.Text;
//    NewSupplierApplication.FirstName = txtFirstame.Text;
//    NewSupplierApplication.LastName = txtLastName.Text;
//    NewSupplierApplication.Address1 = txtAddress.Text;
//    NewSupplierApplication.Address2 = txtAddress2.Text;
//    NewSupplierApplication.Zip = txtZip.Text;
//    NewSupplierApplication.PhoneNumber = txtPhoneNumber.Text;
//    NewSupplierApplication.EmailAddress = txtEmailAddress.Text;
//    NewSupplierApplication.ApplicationDate = dateApplicationDate.DisplayDate;
//    NewSupplierApplication.Approved = false;

//    NewSupplierApplication.ApplicationID = MySupplierManager.AddASupplierApplication(NewSupplierApplication);
            //MySupplierManager.AddANewSupplier(NewSupplier);
//    DialogBox.ShowMessageDialog(this, "Supplier application submitted.");

//having issues with the updateVendorApplication stored precedure here and below. Will uncomment when resolved

                    

    UpdatedSupplierApplication.Approved = true;
    UpdatedSupplierApplication.ApprovalDate = dateApprovalDate.DisplayDate.Date;

    MySupplierManager.AddANewSupplier(NewSupplier);
    MySupplierManager.EditSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication);

    DialogBox.ShowMessageDialog(this, "Supplier application approved: Supplier added.");
}*/
            //UpdatedSupplierApplication.CompanyName = NewSupplierApplication.CompanyName;
            //UpdatedSupplierApplication.CompanyDescription = NewSupplierApplication.CompanyDescription;
            //UpdatedSupplierApplication.FirstName = NewSupplierApplication.FirstName;
            //UpdatedSupplierApplication.LastName = NewSupplierApplication.LastName;
            //UpdatedSupplierApplication.Address1 = NewSupplierApplication.Address1;
            //UpdatedSupplierApplication.Address2 = NewSupplierApplication.Address2;
            //UpdatedSupplierApplication.Zip = NewSupplierApplication.Zip;
            //UpdatedSupplierApplication.PhoneNumber = NewSupplierApplication.PhoneNumber;
            //UpdatedSupplierApplication.EmailAddress = NewSupplierApplication.EmailAddress;
            //UpdatedSupplierApplication.ApplicationID = NewSupplierApplication.ApplicationID;
            //UpdatedSupplierApplication.ApplicationDate = NewSupplierApplication.ApplicationDate;

            //MySupplierManager.EditSupplierApplication(CurrentSupplierApplication, UpdatedSupplierApplication);

            //DialogBox.ShowMessageDialog(this, "Supplier application updated.");



            //NewSupplier.CompanyName = NewSupplierApplication.CompanyName;
            //NewSupplier.FirstName = NewSupplierApplication.FirstName;
            //NewSupplier.LastName = NewSupplierApplication.LastName;
            //NewSupplier.Address1 = NewSupplierApplication.Address1;
            //NewSupplier.Address2 = NewSupplierApplication.Address2;
            //NewSupplier.Zip = NewSupplierApplication.Zip;
            //NewSupplier.PhoneNumber = NewSupplierApplication.Zip;
            //NewSupplier.EmailAddress = NewSupplierApplication.EmailAddress;
            //NewSupplier.ApplicationID = NewSupplierApplication.ApplicationID;
            //NewSupplier.SupplyCost = (decimal)this.numSupplyCost.Value;

