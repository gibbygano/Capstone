using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddEditHotelGuest.xaml
    /// </summary>
    public partial class AddEditHotelGuest
    {
        private String _myTitle;

        /// <summary>
        /// Create a New Hotel Guest
        /// </summary>
        /// Miguel Santana 2/18/2015
        public AddEditHotelGuest()
        {
            InitializeComponent();
            _myTitle = "Add New Hotel Guest";

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

            CurrentHotelGuest = hotelGuest;
            _myTitle = String.Format("Editing Hotel Guest: {0}", CurrentHotelGuest.GetFullName);
            InitializeEverything();
        }

        public HotelGuest CurrentHotelGuest { get; private set; }

        /// <summary>
        /// Parameter marks whether a database command was successful
        /// </summary>
        /// Miguel Santana 2/18/2015
        public bool Result { get; private set; }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
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
        /// <param name="e"></param>
        /// Miguel Santana 2/18/2015
        private void cboZip_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((ComboBox)sender).IsDropDownOpen = true;
        }

        /// <summary>
        /// Show Error Message
        /// </summary>
        /// <param name="message"></param>
        /// Miguel Santana 2/18/2015
        private static void ShowError(String message)
        {
            MessageBox.Show(message);
        }

        /// <summary>
        /// Populates form with required information
        /// </summary>
        /// Miguel Santana 2/18/2015
        private void InitializeEverything()
        {
            CboZip.ItemsSource = RetrieveZipCodeList();
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
                TxtFirstName.Text = null;
                TxtLastName.Text = null;
                TxtAddress1.Text = null;
                TxtAddress2.Text = null;
                CboZip.SelectedItem = null;
                TxtPhoneNumber.Text = null;
                TxtEmailAddress.Text = null;
                TxtRoomNumber.Text = null;
            }
            else
            {
                TxtFirstName.Text = CurrentHotelGuest.FirstName;
                TxtLastName.Text = CurrentHotelGuest.LastName;
                TxtAddress1.Text = CurrentHotelGuest.Address1;
                TxtAddress2.Text = CurrentHotelGuest.Address2;
                foreach (CityState cityState in CboZip.Items) { if (cityState.Zip == CurrentHotelGuest.CityState.Zip) { CboZip.SelectedItem = cityState; } }
                TxtPhoneNumber.Text = CurrentHotelGuest.PhoneNumber;
                TxtEmailAddress.Text = CurrentHotelGuest.EmailAddress;
                TxtRoomNumber.Text = CurrentHotelGuest.Room.ToString();
            }
            TxtFirstName.Focus();
        }

        private List<CityState> RetrieveZipCodeList()
        {
            return new CityStateManager().GetCityStateList();
        }

        /// <summary>
        /// Validate fields and submit data to HotelGuestManager
        /// Miguel Santana 2/18/2015
        /// </summary>
        ///<remarks>
        ///Updated By Rose Steffensmeier 2015/03/05
        ///</remarks>
        private void Submit()
        {
            if (CurrentHotelGuest != null)
            {
                if (CurrentHotelGuest.FirstName.Equals(TxtFirstName.Text.Trim())
                    && CurrentHotelGuest.LastName.Equals(TxtLastName.Text.Trim())
                    && CurrentHotelGuest.Address1.Equals(TxtAddress1.Text.Trim())
                    && CurrentHotelGuest.Address2.Equals(TxtAddress2.Text.Trim())
                    && CurrentHotelGuest.CityState.Zip.Equals(((CityState)CboZip.SelectedItem).Zip)
                    && CurrentHotelGuest.PhoneNumber.Equals(TxtPhoneNumber.Text.Trim())
                    && CurrentHotelGuest.EmailAddress.Equals(TxtEmailAddress.Text.Trim()))
                {
                    switch (MessageBox.Show(this, "No data was changed. Would you like to keep editing?", "Alert", MessageBoxButton.YesNo, MessageBoxImage.Warning))
                    {
                        case MessageBoxResult.Cancel:
                        case MessageBoxResult.No:
                            Close();
                            break;

                        case MessageBoxResult.OK:
                        case MessageBoxResult.Yes:
                        default:
                            return;
                    }
                }
            }

            if (!Validator.ValidateString(TxtFirstName.Text.Trim(), 1, 50))
            {
                ShowError("Please enter a First Name");
                TxtFirstName.Focus();
                TxtFirstName.SelectAll();
                return;
            }
            if (!Validator.ValidateString(TxtLastName.Text.Trim(), 1, 50))
            {
                ShowError("Please enter a Last Name");
                TxtLastName.Focus();
                TxtLastName.SelectAll();
                return;
            }
            if (!Validator.ValidateAlphaNumeric(TxtAddress1.Text.Trim(), 1, 255))
            {
                ShowError("Please enter an Address");
                TxtAddress1.Focus();
                TxtAddress1.SelectAll();
                return;
            }
            if (!string.IsNullOrEmpty(TxtAddress2.Text.Trim()) && !Validator.ValidateAlphaNumeric(TxtAddress2.Text.Trim(), 0, 255))
            {
                ShowError("Error adding Address2");
                TxtAddress2.Focus();
                TxtAddress2.SelectAll();
                return;
            }
            if (CboZip.SelectedItem == null)
            {
                ShowError("Please select a Zip Code");
                CboZip.Focus();
                return;
            }
            if (!string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !Validator.ValidatePhone(TxtPhoneNumber.Text.Trim()))
            {
                ShowError("Please enter a valid Phone Number");
                TxtPhoneNumber.Focus();
                TxtPhoneNumber.SelectAll();
                return;
            }
            if (!string.IsNullOrEmpty(TxtEmailAddress.Text.Trim()) && !Validator.ValidateEmail(TxtEmailAddress.Text.Trim()))
            {
                ShowError("Please enter a valid Email Address");
                TxtEmailAddress.Focus();
                TxtEmailAddress.SelectAll();
                return;
            }
            if (!string.IsNullOrEmpty(TxtRoomNumber.Text.Trim()) && !Validator.ValidateNumeric(TxtRoomNumber.Text.Trim()))
            {
                ShowError("Please enter a valid Room Number");
                TxtRoomNumber.Focus();
                TxtRoomNumber.SelectAll();
                return;
            }

            //FormatException found in if loop
            if (CurrentHotelGuest == null)
            {
                Result = HotelGuestManager.AddHotelGuest(
                    new HotelGuest(
                        TxtFirstName.Text.Trim(),
                        TxtLastName.Text.Trim(),
                        TxtAddress1.Text.Trim(),
                        TxtAddress2.Text.Trim(),
                        (CityState)CboZip.SelectedItem,
                        TxtPhoneNumber.Text.Trim(),
                        TxtEmailAddress.Text.Trim(),
                        int.Parse(TxtRoomNumber.Text.Trim())
                    )
                );
            }
            else
            {
                Result = HotelGuestManager.UpdateHotelGuest(
                    CurrentHotelGuest,
                    new HotelGuest(
                        TxtFirstName.Text.Trim(),
                        TxtLastName.Text.Trim(),
                        TxtAddress1.Text.Trim(),
                        TxtAddress2.Text.Trim(),
                        (CityState)CboZip.SelectedItem,
                        TxtPhoneNumber.Text.Trim(),
                        TxtEmailAddress.Text.Trim(),
                        int.Parse(TxtRoomNumber.Text.Trim())
                    )
                );
            }

            if (Result)
            {
                MessageBox.Show(this, "Your Request was Processed Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
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
        /// <param name="e"></param>
        /// Miguel Santana 2/18/2015
        private void txtInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        /// <summary>
        /// Allows submitting the form by hitting enter on any field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Key Command</param>
        /// Miguel Santana 2/18/2015
        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Submit();
            }
        }
    }
}