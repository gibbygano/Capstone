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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddEditPendingSupplier.xaml
    /// </summary>
    public partial class AddEditPendingSupplier
    {
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

        public SupplierApplication CurrentSupplierApplication { get; private set; }

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
        }

        private void SetFields()
        {
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
                this.dateApprovalDate.SelectedDate = CurrentSupplierApplication.ApprovalDate;
                //this.numSupplyCost.Value = CurrentSupplierApplication;
            }
        }
    }
}