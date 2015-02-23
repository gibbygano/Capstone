using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Enum for setting error status of <paramref name="lblTitle"/>
    /// </summary>
    /// Miguel Santana 2/19/2015
    internal enum LabelErrorColor
    {
        Default,
        Error
    }

    /// <summary>
    /// Interaction logic for AddEditHotelGuest.xaml
    /// </summary>
    public partial class AddEditHotelGuest : Window
    {
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();
        private String myTitle;

        /// <summary>
        /// Create a New Hotel Guest
        /// </summary>
        /// Miguel Santana 2/18/2015
        public AddEditHotelGuest()
        {
            InitializeComponent();
            this.myTitle = "Add New Hotel Guest";

            InitializeEverything();
        }

        /// <summary>
        /// Edit an Existing Hotel Guest
        /// </summary>
        /// <param name="hotelGuest"></param>
        /// Miguel Santana 2/18/2015
        public AddEditHotelGuest(HotelGuest hotelGuest)
        {
            InitializeComponent();

            this.CurrentHotelGuest = hotelGuest;
            this.myTitle = String.Format("Editing Hotel Guest: {0}", CurrentHotelGuest.GetFullName);
            InitializeEverything();
        }

        public HotelGuest CurrentHotelGuest { get; private set; }

        /// <summary>
        /// Parameter marks whether a database command was successful
        /// </summary
        /// Miguel Santana 2/18/2015>
        public bool result { get; private set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        /// <summary>
        /// Opens the combobox on keyboard focus
        /// </summary>
        /// <param name="sender">System.Windows.Controls.ComboBox</param>
        /// Miguel Santana 2/18/2015
        private void cboZip_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((ComboBox)sender).IsDropDownOpen = true;
        }

        /// <summary>
        /// Overloaded Method. Sets the Message back to the predefined defaults.
        /// </summary>
        /// Miguel Santana 2/18/2015
        private void ChangeMessage()
        {
            ChangeTitle(myTitle);
        }

        /// <summary>
        /// Change the value of <paramref name="lblTitle" />
        /// </summary>
        /// <param name="message"></param>
        /// Miguel Santana 2/18/2015
        private void ChangeTitle(String message, LabelErrorColor color = LabelErrorColor.Default)
        {
            this.Title = message;
            this.lblTitle.Content = message;
            var test = this.lblTitle.Background;

            switch (color)
            {
                case LabelErrorColor.Default:
                    this.lblTitle.Background = null;
                    break;

                case LabelErrorColor.Error:
                    this.lblTitle.Background = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                    break;
            }
        }

        /// <summary>
        /// Populates form with required information
        /// </summary>
        /// Miguel Santana 2/18/2015
        private void InitializeEverything()
        {
            ChangeMessage();
            this.cboZip.ItemsSource = RetrieveZipCodeList();
            ResetFields();
        }

        /// <summary>
        /// Resets the values of the input fields
        /// </summary>
        /// Miguel Santana 2/18/2015
        private void ResetFields()
        {
            if (CurrentHotelGuest == null)
            {
                this.txtFirstName.Text = null;
                this.txtLastName.Text = null;
                this.txtAddress1.Text = null;
                this.txtAddress2.Text = null;
                this.cboZip.SelectedItem = null;
                this.txtPhoneNumber.Text = null;
                this.txtEmailAddress.Text = null;
            }
            else
            {
                this.txtFirstName.Text = CurrentHotelGuest.FirstName;
                this.txtLastName.Text = CurrentHotelGuest.LastName;
                this.txtAddress1.Text = CurrentHotelGuest.Address1;
                this.txtAddress2.Text = CurrentHotelGuest.Address2;
                foreach (CityState _cityState in this.cboZip.Items) { if (_cityState.Zip == CurrentHotelGuest.CityState.Zip) { this.cboZip.SelectedItem = _cityState; } }
                this.txtPhoneNumber.Text = CurrentHotelGuest.PhoneNumber;
                this.txtEmailAddress.Text = CurrentHotelGuest.EmailAddress;
            }
            this.txtFirstName.Focus();
        }

        private List<CityState> RetrieveZipCodeList()
        {
            return new CityStateManager().GetCityStateList();
        }

        /// <summary>
        /// Validate fields and submit data to HotelGuestManager
        /// </summary>
        /// Miguel Santana 2/18/2015
        private void Submit()
        {
            if (CurrentHotelGuest != null)
            {
                if (CurrentHotelGuest.FirstName.Equals(txtFirstName.Text.Trim())
                    && CurrentHotelGuest.LastName.Equals(txtLastName.Text.Trim())
                    && CurrentHotelGuest.Address1.Equals(txtAddress1.Text.Trim())
                    && CurrentHotelGuest.Address2.Equals(txtAddress2.Text.Trim())
                    && CurrentHotelGuest.CityState.Zip.Equals(((CityState)cboZip.SelectedItem).Zip)
                    && CurrentHotelGuest.PhoneNumber.Equals(txtPhoneNumber.Text.Trim())
                    && CurrentHotelGuest.EmailAddress.Equals(txtEmailAddress.Text.Trim()))
                {
                    switch (MessageBox.Show(this, "No data was changed. Would you like to keep editing?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                    {
                        case MessageBoxResult.Cancel:
                        case MessageBoxResult.No:
                            this.Close();
                            break;

                        case MessageBoxResult.OK:
                        case MessageBoxResult.Yes:
                        default:
                            return;
                    }
                }
            }

            if (!Validator.ValidateString(txtFirstName.Text.Trim(), 1, 50))
            {
                ChangeTitle("Please enter a First Name", LabelErrorColor.Error);
                txtFirstName.Focus();
                txtFirstName.SelectAll();
                return;
            }
            if (!Validator.ValidateString(txtLastName.Text.Trim(), 1, 50))
            {
                ChangeTitle("Please enter a Last Name", LabelErrorColor.Error);
                txtLastName.Focus();
                txtLastName.SelectAll();
                return;
            }
            if (!Validator.ValidateAlphaNumeric(txtAddress1.Text.Trim(), 1, 255))
            {
                ChangeTitle("Please enter an Address", LabelErrorColor.Error);
                txtAddress1.Focus();
                txtAddress1.SelectAll();
                return;
            }
            if (txtAddress2.Text.Trim() != "" && !Validator.ValidateAlphaNumeric(txtAddress2.Text.Trim(), 0, 255))
            {
                ChangeTitle("Error adding Address2", LabelErrorColor.Error);
                txtAddress2.Focus();
                txtAddress2.SelectAll();
                return;
            }
            if (cboZip.SelectedItem == null)
            {
                ChangeTitle("Please select a Zip Code", LabelErrorColor.Error);
                cboZip.Focus();
                return;
            }
            if (txtPhoneNumber.Text.Trim() != "" && !Validator.ValidatePhone(txtPhoneNumber.Text.Trim()))
            {
                ChangeTitle("Please enter a valid Phone Number", LabelErrorColor.Error);
                txtPhoneNumber.Focus();
                txtPhoneNumber.SelectAll();
                return;
            }
            if (txtEmailAddress.Text.Trim() != "" && !Validator.ValidateEmail(txtEmailAddress.Text.Trim()))
            {
                ChangeTitle("Please enter a valid Email Address", LabelErrorColor.Error);
                txtEmailAddress.Focus();
                txtEmailAddress.SelectAll();
                return;
            }

            if (CurrentHotelGuest == null)
            {
                result = _hotelGuestManager.AddHotelGuest(
                    new NewHotelGuest(
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtAddress1.Text.Trim(),
                        txtAddress2.Text.Trim(),
                        (CityState)cboZip.SelectedItem,
                        txtPhoneNumber.Text.Trim(),
                        txtEmailAddress.Text.Trim()
                    )
                );
            }
            else
            {
                result = _hotelGuestManager.UpdateHotelGuest(
                    CurrentHotelGuest,
                    new NewHotelGuest(
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtAddress1.Text.Trim(),
                        txtAddress2.Text.Trim(),
                        (CityState)cboZip.SelectedItem,
                        txtPhoneNumber.Text.Trim(),
                        txtEmailAddress.Text.Trim()
                    )
                );
            }

            if (result)
            {
                MessageBox.Show(this, "Your Request was Processed Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(this, "Error Processing Request", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Selects all on keyboard focus to allow for easier tabbing between fields
        /// </summary>
        /// <param name="sender">System.Windows.Controls.TextBox</param>
        /// Miguel Santana 2/18/2015
        private void txtInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        /// <summary>
        /// Allows submitting the form by hitting enter on any field
        /// </summary>
        /// <param name="e">Key Command</param>
        /// Miguel Santana 2/18/2015
        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Submit();
            }
        }

        /// <summary>
        /// Resets the title on TextChange
        /// </summary>
        /// Miguel Santana 2/18/2015
        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeMessage();
        }
    }
}