using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Miguel Santana
    /// Created: 2015/02/16
    /// 
    /// Interaction logic for AddEditHotelGuest.xaml
    /// </summary>
    public partial class AddEditHotelGuest
    {
        private HotelGuestManager _hotelGuestManager = new HotelGuestManager();

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Create a New Hotel Guest
        /// </summary>
        public AddEditHotelGuest()
        {
            InitializeComponent();
            Title = "Add New Hotel Guest";
            TxtRoomNumber.MaxLength = 4;
            TxtGuestPIN.MaxLength = 4;
            InitializeEverything();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Edit an Existing Hotel Guest
        /// </summary>
        /// <param name="hotelGuest"></param>
        public AddEditHotelGuest(HotelGuest hotelGuest, bool ReadOnly = false)
        {
            InitializeComponent();

            CurrentHotelGuest = hotelGuest;
            Title = String.Format("Editing Hotel Guest: {0}", CurrentHotelGuest.GetFullName);
            InitializeEverything();

            if (ReadOnly) { WindowHelper.MakeReadOnly(this.Content as Panel, new FrameworkElement[] { BtnCancel }); }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// </summary>
        public HotelGuest CurrentHotelGuest { get; private set; }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Updated 2015/04/13 by Tony Noel -Updated to comply with the ResultsEdit class of error codes.
        /// 
        /// Parameter marks whether a database command was successful
        /// </summary>
        public ResultsEdit Result { get; private set; }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            ResetFields();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Opens the combobox on keyboard focus
        /// </summary>
        /// <param name="sender">System.Windows.Controls.ComboBox</param>
        /// <param name="e"></param>
        private void cboZip_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            // ((ComboBox)sender).IsDropDownOpen = true;
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Populates form with required information
        /// </summary>
        private void InitializeEverything()
        {
            CboZip.ItemsSource = RetrieveZipCodeList();
            ResetFields();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Resets the values of the input fields
        /// </summary>
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
                TxtGuestPIN.Text = null;
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
                TxtRoomNumber.Text = CurrentHotelGuest.Room;
                TxtGuestPIN.Text = CurrentHotelGuest.GuestPIN;
            }
            TxtFirstName.Focus();
        }

        private List<CityState> RetrieveZipCodeList()
        {
            return new CityStateManager().GetCityStateList();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/13
        /// 
        /// Show Message Dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="style"></param>
        /// <returns>awaitable Task of MessageDialogResult</returns>
        private Task<MessageDialogResult> ShowMessage(string message, string title = null, MessageDialogStyle? style = null)
        {
            return DialogBox.ShowMessageDialog(this, message, title, style);
        }

        private void ShowInputErrorMessage(FrameworkElement component, string message, string title = null)
        {
            throw new InputValidationException(component, message, title);
        }

        private void ShowErrorMessage(string message, string title = null)
        {
            throw new WanderingTurtleException(this, message, title);
        }

        private void ShowErrorMessage(Exception exception, string title = null)
        {
            throw new WanderingTurtleException(this, exception, title);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Validate fields and submit data to HotelGuestManager
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier 
        /// Updated: 2015/03/05
        /// </remarks>
        private async void Submit()
        {
            if (CurrentHotelGuest != null && ValidateChanged())
            {
                switch (await ShowMessage("No data was changed. Would you like to keep editing?", "Alert", MessageDialogStyle.AffirmativeAndNegative))
                {
                    case MessageDialogResult.Affirmative:
                        Close();
                        break;

                    case MessageDialogResult.Negative:
                    default:
                        return;
                }
            }

            if (!Validate()) { return; }
            try
            {
                //FormatException found in if loop
                if (CurrentHotelGuest == null)
                {
                    Result = _hotelGuestManager.AddHotelGuest(
                        new HotelGuest(
                            TxtFirstName.Text.Trim(),
                            TxtLastName.Text.Trim(),
                            TxtAddress1.Text.Trim(),
                            TxtAddress2.Text.Trim(),
                            (CityState)CboZip.SelectedItem,
                            TxtPhoneNumber.Text.Trim(),
                            TxtEmailAddress.Text.Trim(),
                            TxtRoomNumber.Text.Trim(),
                            TxtGuestPIN.Text.Trim()
                        )
                    );
                }
                else
                {
                    Result = _hotelGuestManager.UpdateHotelGuest(
                            CurrentHotelGuest,
                            new HotelGuest(
                                TxtFirstName.Text.Trim(),
                                TxtLastName.Text.Trim(),
                                TxtAddress1.Text.Trim(),
                                TxtAddress2.Text.Trim(),
                                (CityState)CboZip.SelectedItem,
                                TxtPhoneNumber.Text.Trim(),
                                TxtEmailAddress.Text.Trim(),
                                TxtRoomNumber.Text.Trim(),
                                TxtGuestPIN.Text.Trim()
                            )
                        );
                }

                if (Result == ResultsEdit.Success)
                {
                    await ShowMessage("Your Request was Processed Successfully", "Success");
                    Close();
                }
                else
                { ShowErrorMessage("Error Processing Request", "Error"); }
            }
            catch (SqlException)
            {
                ShowErrorMessage("PIN Already Assigned.  Please choose a different PIN.", "Error"); 
            }
            catch (Exception ex)
            { 
                ShowErrorMessage(ex); 
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/18
        /// 
        /// Selects all on keyboard focus to allow for easier tabbing between fields
        /// </summary>
        /// <param name="sender">System.Windows.Controls.TextBox</param>
        /// <param name="e"></param>
        private void txtInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// 
        /// Allows submitting the form by hitting enter on any field
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Key Command</param>
        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Submit();
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/10
        /// 
        /// Runs validation on the input fields
        /// </summary>
        /// <returns>True if valid</returns>
        private bool Validate()
        {
            if (!Validator.ValidateString(TxtFirstName.Text.Trim(), 1, 50))
            {
                ShowInputErrorMessage(TxtFirstName, "Please enter a First Name");
                return false;
            }
            if (!Validator.ValidateString(TxtLastName.Text.Trim(), 1, 50))
            {
                ShowInputErrorMessage(TxtLastName, "Please enter a Last Name");
                return false;
            }
            if (!Validator.ValidateAlphaNumeric(TxtAddress1.Text.Trim(), 1, 255))
            {
                ShowInputErrorMessage(TxtAddress1, "Please enter an Address");
                return false;
            }
            if (!string.IsNullOrEmpty(TxtAddress2.Text.Trim()) && !Validator.ValidateAlphaNumeric(TxtAddress2.Text.Trim(), 0, 255))
            {
                ShowInputErrorMessage(TxtAddress2, "Error adding Address2");
                return false;
            }
            if (CboZip.SelectedItem == null)
            {
                ShowInputErrorMessage(CboZip, "Please select a Zip Code");
                return false;
            }
            if (!string.IsNullOrEmpty(TxtPhoneNumber.Text.Trim()) && !Validator.ValidatePhone(TxtPhoneNumber.Text.Trim()))
            {
                ShowInputErrorMessage(TxtPhoneNumber, "Please enter a valid Phone Number");
                return false;
            }
            if (!string.IsNullOrEmpty(TxtEmailAddress.Text.Trim()) && !Validator.ValidateEmail(TxtEmailAddress.Text.Trim()))
            {
                ShowInputErrorMessage(TxtEmailAddress, "Please enter a valid Email Address");
                return false;
            }
            if (!string.IsNullOrEmpty(TxtRoomNumber.Text.Trim()) && !Validator.ValidateNumeric(TxtRoomNumber.Text.Trim()))
            {
                ShowInputErrorMessage(TxtRoomNumber, "Please enter a valid Room Number");
                return false;
            }
            if (!string.IsNullOrEmpty(TxtGuestPIN.Text.Trim()) && !Validator.ValidateNumeric(TxtGuestPIN.Text.Trim()))
            {
                ShowInputErrorMessage(TxtGuestPIN, "Please enter a valid PIN Number between 1000 and 9999.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/10
        /// 
        /// Checks to see if the fields have been changed
        /// </summary>
        /// <returns>True if valid</returns>
        private bool ValidateChanged()
        {
            return CurrentHotelGuest.FirstName.Equals(TxtFirstName.Text.Trim())
                    && CurrentHotelGuest.LastName.Equals(TxtLastName.Text.Trim())
                    && CurrentHotelGuest.Address1.Equals(TxtAddress1.Text.Trim())
                    && CurrentHotelGuest.Address2.Equals(TxtAddress2.Text.Trim())
                    && CurrentHotelGuest.CityState.Zip.Equals(((CityState)CboZip.SelectedItem).Zip)
                    && CurrentHotelGuest.PhoneNumber.Equals(TxtPhoneNumber.Text.Trim())
                    && CurrentHotelGuest.EmailAddress.Equals(TxtEmailAddress.Text.Trim())
                    && CurrentHotelGuest.Room.Equals(TxtRoomNumber.Text.Trim())
                    && CurrentHotelGuest.GuestPIN.Equals(TxtGuestPIN.Text.Trim());
        }
    }
}