using com.WanderingTurtle.Common;
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
                this.txtFirstName.Text = CurrentHotelGuest.FirstName.ToString();
                this.txtLastName.Text = CurrentHotelGuest.LastName.ToString();
                this.txtAddress1.Text = CurrentHotelGuest.Address1.ToString();
                this.txtAddress2.Text = CurrentHotelGuest.Address2.ToString();
                this.txtZipCode.Text = CurrentHotelGuest.Zip.ToString();
                this.txtPhoneNumber.Text = CurrentHotelGuest.PhoneNumber.ToString();
                this.txtEmailAddress.Text = CurrentHotelGuest.EmailAddress.ToString();
            }
            this.txtFirstName.Focus();
        }

        /// <summary>
        /// Validate fields and submit data to HotelGuestManager
        /// </summary>
        private void Submit()
        {
            if (!Validator.ValidateString(txtFirstName.Text, 1, 50))
            {
                ChangeTitle("Please enter a First Name");
                txtFirstName.Focus();
                txtFirstName.SelectAll();
                return;
            }
            if (!Validator.ValidateString(txtLastName.Text, 1, 50))
            {
                ChangeTitle("Please enter a Last Name");
                txtLastName.Focus();
                txtLastName.SelectAll();
                return;
            }
            if (!Validator.ValidateAlphaNumeric(txtAddress1.Text, 1, 255))
            {
                ChangeTitle("Please enter an Address");
                txtAddress1.Focus();
                txtAddress1.SelectAll();
                return;
            }
            if (txtAddress2.Text != "" && !Validator.ValidateAlphaNumeric(txtAddress2.Text, 0, 255))
            {
                ChangeTitle("Please enter an Address");
                txtAddress2.Focus();
                txtAddress2.SelectAll();
                return;
            }
            if (!Validator.ValidateAlphaNumeric(txtZipCode.Text, 5, 10))
            {
                ChangeTitle("Please enter a Zip Code");
                txtZipCode.Focus();
                txtZipCode.SelectAll();
                return;
            }
            if (txtPhoneNumber.Text != "" && !Validator.ValidatePhone(txtPhoneNumber.Text))
            {
                ChangeTitle("Please enter a valid Phone Number");
                txtPhoneNumber.Focus();
                txtPhoneNumber.SelectAll();
                return;
            }
            if (txtEmailAddress.Text != "" && !Validator.ValidateEmail(txtEmailAddress.Text))
            {
                ChangeTitle("Please enter a valid Email Address");
                txtEmailAddress.Focus();
                txtEmailAddress.SelectAll();
                return;
            }

            if (CurrentHotelGuest == null)
            {
                _hotelGuestManager.AddHotelGuest(
                    new NewHotelGuest(
                        txtFirstName.Text,
                        txtLastName.Text,
                        txtAddress1.Text,
                        txtAddress2.Text,
                        txtZipCode.Text,
                        txtPhoneNumber.Text,
                        txtEmailAddress.Text
                    )
                );
            }
            else
            {
                _hotelGuestManager.UpdateHotelGuest(
                    CurrentHotelGuest,
                    new NewHotelGuest(
                        txtFirstName.Text,
                        txtLastName.Text,
                        txtAddress1.Text,
                        txtAddress2.Text,
                        txtZipCode.Text,
                        txtPhoneNumber.Text,
                        txtEmailAddress.Text
                    )
                );
            }
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