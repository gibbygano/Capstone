using com.WanderingTurtle.Common;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
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
        public AddEditHotelGuest()
        {
            InitializeComponent();
            this.myTitle = "Add New Hotel Guest";
        }

        /// <summary>
        /// Edit an Existing Hotel Guest
        /// </summary>
        /// <param name="hotelGuest"></param>
        public AddEditHotelGuest(HotelGuest hotelGuest)
        {
            InitializeComponent();

            this.CurrentHotelGuest = hotelGuest;
            this.myTitle = "Editing Hotel Guest: " + CurrentHotelGuest.FirstName + " " + CurrentHotelGuest.LastName;
        }

        public HotelGuest CurrentHotelGuest { get; private set; }

        public bool completed { get; private set; }

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
        /// Overloaded Method. Sets the Message back to the predefined defaults.
        /// </summary>
        private void ChangeMessage()
        {
            ChangeTitle(myTitle);
        }

        /// <summary>
        /// Change the value of <paramref name="lblTitle" />
        /// </summary>
        /// <param name="message"></param>
        private void ChangeTitle(String message)
        {
            this.Title = message;
            this.lblTitle.Content = message;
        }

        /// <summary>
        /// Resets the values of the input fields
        /// </summary>
        private void ResetFields()
        {
            if (CurrentHotelGuest == null)
            {
                this.txtGuestID.Text = null;
                this.txtFirstName.Text = null;
                this.txtLastName.Text = null;
                this.txtAddress1.Text = null;
                this.txtAddress2.Text = null;
                this.txtZipCode.Text = null;
                this.txtPhoneNumber.Text = null;
                this.txtEmailAddress.Text = null;
            }
            else
            {
                this.txtGuestID.Text = CurrentHotelGuest.HotelGuestID.ToString();
                this.txtFirstName.Text = CurrentHotelGuest.FirstName;
                this.txtLastName.Text = CurrentHotelGuest.LastName;
                this.txtAddress1.Text = CurrentHotelGuest.Address1;
                this.txtAddress2.Text = CurrentHotelGuest.Address2;
                this.txtZipCode.Text = CurrentHotelGuest.Zip;
                this.txtPhoneNumber.Text = CurrentHotelGuest.PhoneNumber;
                this.txtEmailAddress.Text = CurrentHotelGuest.EmailAddress;
            }
            this.txtFirstName.Focus();
        }

        /// <summary>
        /// Validate fields and submit data to HotelGuestManager
        /// </summary>
        private void Submit()
        {
            if (!Validator.ValidateString(txtFirstName.Text.Trim(), 1, 50))
            {
                ChangeTitle("Please enter a First Name");
                txtFirstName.Focus();
                txtFirstName.SelectAll();
                return;
            }
            if (!Validator.ValidateString(txtLastName.Text.Trim(), 1, 50))
            {
                ChangeTitle("Please enter a Last Name");
                txtLastName.Focus();
                txtLastName.SelectAll();
                return;
            }
            if (txtAddress1.Text.Trim() == "" /*!Validator.ValidateAlphaNumeric(txtAddress1.Text.Trim(), 1, 255)*/)
            {
                ChangeTitle("Please enter an Address");
                txtAddress1.Focus();
                txtAddress1.SelectAll();
                return;
            }
            //if (txtAddress2.Text.Trim() != "" && !Validator.ValidateAlphaNumeric(txtAddress2.Text.Trim(), 0, 255))
            //{
            //    ChangeTitle("Please enter an Address");
            //    txtAddress2.Focus();
            //    txtAddress2.SelectAll();
            //    return;
            //}
            if (!Validator.ValidateAlphaNumeric(txtZipCode.Text.Trim(), 5, 10))
            {
                ChangeTitle("Please enter a Zip Code");
                txtZipCode.Focus();
                txtZipCode.SelectAll();
                return;
            }
            if (txtPhoneNumber.Text != "" && !Validator.ValidatePhone(txtPhoneNumber.Text.Trim()))
            {
                ChangeTitle("Please enter a valid Phone Number");
                txtPhoneNumber.Focus();
                txtPhoneNumber.SelectAll();
                return;
            }
            if (txtEmailAddress.Text.Trim() != "" && !Validator.ValidateEmail(txtEmailAddress.Text.Trim()))
            {
                ChangeTitle("Please enter a valid Email Address");
                txtEmailAddress.Focus();
                txtEmailAddress.SelectAll();
                return;
            }

            if (CurrentHotelGuest == null)
            {
                completed = _hotelGuestManager.AddHotelGuest(
                    new NewHotelGuest(
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtAddress1.Text.Trim(),
                        txtAddress2.Text.Trim(),
                        txtZipCode.Text.Trim(),
                        txtPhoneNumber.Text.Trim(),
                        txtEmailAddress.Text.Trim()
                    )
                );
            }
            else
            {
                completed = _hotelGuestManager.UpdateHotelGuest(
                    CurrentHotelGuest,
                    new NewHotelGuest(
                        txtFirstName.Text.Trim(),
                        txtLastName.Text.Trim(),
                        txtAddress1.Text.Trim(),
                        txtAddress2.Text.Trim(),
                        txtZipCode.Text.Trim(),
                        txtPhoneNumber.Text.Trim(),
                        txtEmailAddress.Text.Trim()
                    )
                );
            }

            if (completed) { this.Close(); }
        }

        private void txtInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                Submit();
            }
        }

        private void txtInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            ChangeMessage();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ChangeMessage();
            ResetFields();
        }
    }
}