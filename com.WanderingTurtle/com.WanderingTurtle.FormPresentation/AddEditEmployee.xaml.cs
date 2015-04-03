using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation
{
    public partial class AddEmployee
    {
        private EmployeeManager _employeeManager = new EmployeeManager();

        /// <summary>
        /// Pat Banks 
        /// Created: 2015/02/02
        ///
        /// Constructs the add employee form and fills the combo box.
        /// </summary>
        public AddEmployee()
        {
            InitializeComponent();
            Title = "Add Employee";
            ReloadComboBox();
            ChkActiveEmployee.IsEnabled = false;
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/20
        ///
        /// Constructs a form with data from employee to update
        /// </summary>
        /// <remarks>
        /// </remarks>
        /// <param name="employee">Employee to update</param>
        public AddEmployee(Employee employee)
        {
            InitializeComponent();
            CurrentEmployee = employee;
            Title = "Editing: " + CurrentEmployee.GetFullName;
            ReloadComboBox();

            SetFields();
        }

        public Employee CurrentEmployee { get; private set; }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/19
        ///
        /// Defines employee roles for the combo box
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/22
        ///
        /// Changed to enum
        /// </remarks>
        private List<RoleData> GetUserLevelList { get { return new List<RoleData>((IEnumerable<RoleData>)Enum.GetValues(typeof(RoleData))); } }

        /// <summary>
        /// Miguel Santana 
        /// Created: 2015/03/05
        /// 
        /// Closes the window
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Miguel Santana 
        /// Created: 2015/03/05
        /// 
        /// Resets the fields
        /// </summary>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        /// <summary>
        /// Pat Banks 
        /// Created: 2015/02/15
        /// 
        /// Calls method to open AddEditEmployee UI
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/22
        /// 
        /// Added method to update employee
        /// </remarks>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEmployee == null) { EmployeeAdd(); } else { EmployeeUpdate(); }
        }

        /// <summary>
        /// Pat Banks 
        /// Created: 2015/02/15
        ///
        /// Method takes values for a new employee from the form and passes values
        ///     into the AddNewEmployee method of the EmployeeManager class
        /// </summary>
        /// <exception cref="Exception">
        /// Exception is thrown if database is not available or
        ///     new employee cannot be created in the database for any reason
        /// </exception>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/22
        ///
        /// Cast Level to RoleData
        /// </remarks>
        private void EmployeeAdd()
        {
            if (!Validate()) { return; }

            try
            {
                Debug.Assert(ChkActiveEmployee.IsChecked != null, "ChkActiveEmployee.IsChecked != null");
                int result = _employeeManager.AddNewEmployee(
                    new Employee(
                        TxtFirstName.Text,
                        TxtLastName.Text,
                        TxtPassword.Password,
                        (int)CboUserLevel.SelectedItem,
                        ChkActiveEmployee.IsChecked.Value
                        )
                    );

                if (result == 1)
                {
                    ShowMessage("Employee added successfully");
                    //closes window after successful add
                    Close();
                }
            }
            catch (Exception ax)
            {
                ShowMessage(ax.Message);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/20
        ///
        /// Validates and Updates Employee user
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void EmployeeUpdate()
        {
            if (!Validate()) { return; }

            try
            {
                int result = _employeeManager.EditCurrentEmployee(
                    CurrentEmployee,
                    new Employee(
                        TxtFirstName.Text,
                        TxtLastName.Text,
                        string.IsNullOrEmpty(TxtPassword.Password) ? TxtPassword.Password : null,
                        (int)CboUserLevel.SelectedItem,
                        ChkActiveEmployee.IsChecked.Value
                        )
                    );

                if (result == 1)
                {
                    ShowMessage("Employee updated successfully");
                    //closes window after successful add
                    Close();
                }
            }
            catch (Exception ax)
            {
                ShowMessage(ax.Message);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/22
        /// 
        /// Reloads the combobox with values from database
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            CboUserLevel.ItemsSource = GetUserLevelList;
        }

        /// <summary>
        /// Miguel Santana 
        /// Created: 2015/03/05
        /// 
        /// Resets the values of the fields
        /// </summary>
        private void SetFields()
        {
            if (CurrentEmployee == null)
            {
                TxtFirstName.Text = null;
                TxtLastName.Text = null;
                TxtPassword.Password = null;
                TxtPassword2.Password = null;
                ChkActiveEmployee.IsChecked = true;
                CboUserLevel.SelectedItem = null;
            }
            else
            {
                TxtFirstName.Text = CurrentEmployee.FirstName;
                TxtLastName.Text = CurrentEmployee.LastName;
                TxtPassword.Password = null;
                TxtPassword2.Password = null;
                ChkActiveEmployee.IsChecked = CurrentEmployee.Active;
                CboUserLevel.SelectedItem = CurrentEmployee.Level;
            }
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
        /// <returns>awaitable Task of MEssageDialogResult</returns>
        private Task<MessageDialogResult> ShowMessage(string message, string title = null, MessageDialogStyle? style = null)
        {
            return DialogBox.ShowMessageDialog(this, message, title, style);
        }

        /// <summary>
        /// Pat Banks 
        /// Created: 2015/02/20
        ///
        /// Validates the text fields in the form
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated: 2015/02/20
        /// 
        /// Extracted method
        /// </remarks>
        private bool Validate()
        {
            if (!Validator.ValidateString(TxtFirstName.Text))
            {
                ShowMessage("Please fill out the first name field with a valid name.");
                TxtFirstName.Focus();
                TxtFirstName.SelectAll();
                return false;
            }
            if (!Validator.ValidateString(TxtLastName.Text))
            {
                ShowMessage("Please fill out the last name field with a valid name.");
                TxtLastName.Focus();
                TxtLastName.SelectAll();
                return false;
            }
            bool validatePass = !(CurrentEmployee != null && TxtPassword.Password == "");
            if (validatePass && !Validator.ValidatePassword(TxtPassword.Password))
            {
                ShowMessage("Password must have a minimum of 8 characters.  \n At Least 1 each of 3 of the following 4:  " +
                                " \n lowercase letter\n UPPERCASE LETTER \n Number \nSpecial Character (not space)");
                TxtPassword.Focus();
                TxtPassword.SelectAll();
                return false;
            }
            if (validatePass && !TxtPassword2.Password.Equals(TxtPassword.Password))
            {
                ShowMessage("Your password must match!");
                TxtPassword2.Focus();
                TxtPassword2.SelectAll();
                return false;
            }
            if (string.IsNullOrEmpty(CboUserLevel.Text) || CboUserLevel.Text == null)
            {
                ShowMessage("Please select a user level.");
                CboUserLevel.Focus();
                CboUserLevel.IsDropDownOpen = true;
                return false;
            }
            return true;
        }
    }
}