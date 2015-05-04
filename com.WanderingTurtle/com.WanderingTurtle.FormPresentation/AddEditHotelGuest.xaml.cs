using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Miguel Santana
    /// Created: 2015/02/16
    /// Interaction logic for AddEditHotelGuest.xaml
    /// </summary>
    public partial class AddEditHotelGuest
    {
        private readonly HotelGuestManager _hotelGuestManager = new HotelGuestManager();

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// Create a New Hotel Guest
        /// </summary>
        public AddEditHotelGuest()
        {
            InitializeComponent();
            Title = "Add a new Guest";
            TxtRoomNumber.MaxLength = 4;
            TxtGuestPin.MaxLength = 6;
            InitializeEverything();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// Edit an Existing Hotel Guest
        /// </summary>
        /// <param name="hotelGuest"></param>
        /// <param name="readOnly">Make the form ReadOnly.</param>
        /// <exception cref="WanderingTurtleException">Occurs making components readonly.</exception>
        public AddEditHotelGuest(HotelGuest hotelGuest, bool readOnly = false)
        {
            InitializeComponent();

            CurrentHotelGuest = hotelGuest;
            Title = String.Format("Editing Guest: {0}", CurrentHotelGuest.GetFullName);
            InitializeEverything();

            if (readOnly) { (Content as Panel).MakeReadOnly(BtnCancel); }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// </summary>
        public HotelGuest CurrentHotelGuest { get; private set; }

        /// <summary>
        /// Miguel Santana
        /// Parameter marks whether a database command was successful
        /// </summary>
        /// <remarks>
        /// Tony Noel
        /// Updated 2015/04/13
        /// Updated to comply with the ResultsEdit class of error codes.
        /// </remarks>
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
        /// Populates form with required information
        /// </summary>
        private void InitializeEverything()
        {
            CboZip.ItemsSource = DataCache._currentCityStateList;
            ResetFields();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
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
                TxtGuestPin.Text = _hotelGuestManager.GenerateRandomPIN();
            }
            else
            {
                TxtFirstName.Text = CurrentHotelGuest.FirstName;
                TxtLastName.Text = CurrentHotelGuest.LastName;
                TxtAddress1.Text = CurrentHotelGuest.Address1;
                TxtAddress2.Text = CurrentHotelGuest.Address2;
                foreach (CityState cityState in CboZip.Items.Cast<CityState>().Where(cityState => cityState.Zip == CurrentHotelGuest.CityState.Zip))
                { CboZip.SelectedItem = cityState; }

                string phoneNumberMasked = CurrentHotelGuest.PhoneNumber.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
                TxtPhoneNumber.Text = phoneNumberMasked;
                TxtEmailAddress.Text = CurrentHotelGuest.EmailAddress;
                TxtRoomNumber.Text = CurrentHotelGuest.Room;
                TxtGuestPin.Text = CurrentHotelGuest.GuestPIN;
            }
            TxtFirstName.Focus();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/13
        /// Show Message Dialog
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="style"></param>
        /// <returns>awaitable Task of MessageDialogResult</returns>
        private async Task<MessageDialogResult> ShowMessage(string message, string title = null, MessageDialogStyle? style = null)
        {
            return await this.ShowMessageDialog(message, title, style);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/04/13
        /// Show Message Dialog
        /// </summary>
        /// <param name="component"></param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns>error message</returns>
        private void ShowInputErrorMessage(FrameworkElement component, string message, string title = null)
        {
            throw new InputValidationException(component, message, title);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/04/13
        ///
        /// Show Message Dialog - overloaded
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <returns>error message</returns>
        private void ShowErrorMessage(string message, string title = null)
        {
            throw new WanderingTurtleException(this, message, title);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/04/13
        /// Show error Message Dialog - overloaded
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="title"></param>
        private void ShowErrorMessage(Exception exception, string title = null)
        {
            throw new WanderingTurtleException(this, exception, title);
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/16
        /// Validate fields and submit data to HotelGuestManager
        /// </summary>
        /// <remarks>
        /// Rose Steffensmeier
        /// Updated: 2015/03/05
        /// Added Room number field
        /// Pat Banks
        /// Updated:  2015/04/03
        /// added guest pin field
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
                            TxtGuestPin.Text.Trim()
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
                                TxtGuestPin.Text.Trim()
                            )
                        );
                }

                if (Result == ResultsEdit.Success)
                {
                    await ShowMessage("Your Request was Processed Successfully", "Success");
                    DialogResult = true;
                    Close();
                }
                else
                { ShowErrorMessage("Error Processing Request", "Error"); }
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("UniqueRoomExceptNulls"))
                {
                    ShowErrorMessage("A Pin is Already Associated With This Room.", "Error");
                }
                else if (ex.Message.Contains("UniquePINExceptNulls"))
                {
                    ShowErrorMessage("A Room is Already Associated With This Pin.", "Error");
                }
                else
                {
                    ShowErrorMessage("There Was An Issue Contacting the Database.", "Error");
                }
            }
            catch (Exception ex)
            {
                ShowErrorMessage(ex);
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created 2015-04-24
        /// Used to generate a random PIN for a customer to access the website
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGeneratePIN_Click(object sender, RoutedEventArgs e)
        {
            TxtGuestPin.Text = _hotelGuestManager.GenerateRandomPIN();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/18
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
        /// Runs validation on the input fields
        /// </summary>
        /// <returns>True if valid</returns>
        private bool Validate()
        {
            if (!TxtFirstName.Text.Trim().ValidateString(1, 50))
            {
                ShowInputErrorMessage(TxtFirstName, "Please enter a First Name");
                return false;
            }
            if (!TxtLastName.Text.Trim().ValidateString(1, 50))
            {
                ShowInputErrorMessage(TxtLastName, "Please enter a Last Name");
                return false;
            }
            if (!TxtAddress1.Text.Trim().ValidateAlphaNumeric(1, 255))
            {
                ShowInputErrorMessage(TxtAddress1, "Please enter an Address");
                return false;
            }
            if (!string.IsNullOrEmpty(TxtAddress2.Text.Trim()) && !TxtAddress2.Text.Trim().ValidateAlphaNumeric(0, 255))
            {
                ShowInputErrorMessage(TxtAddress2, "Error adding Address2");
                return false;
            }
            if (CboZip.SelectedItem == null)
            {
                ShowInputErrorMessage(CboZip, "Please select a Zip Code");
                return false;
            }
            if (!TxtPhoneNumber.Text.Trim().ValidatePhone())
            {
                ShowInputErrorMessage(TxtPhoneNumber, "Please enter a valid Phone Number");
                return false;
            }
            if (!TxtEmailAddress.Text.Trim().ValidateEmail())
            {
                ShowInputErrorMessage(TxtEmailAddress, "Please enter a valid Email Address");
                return false;
            }
            if (!TxtRoomNumber.Text.Trim().ValidateNumeric())
            {
                ShowInputErrorMessage(TxtRoomNumber, "Please enter a valid Room Number");
                return false;
            }
            if (!TxtGuestPin.Text.Trim().ValidateAlphaNumeric() || TxtGuestPin.Text.Length != 6)
            {
                ShowInputErrorMessage(TxtGuestPin, "Please enter a valid 6 digit alphanumeric PIN.");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/10
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
                    && CurrentHotelGuest.GuestPIN.Equals(TxtGuestPin.Text.Trim());
        }
    }
}