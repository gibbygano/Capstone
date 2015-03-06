using com.WanderingTurtle.BusinessLogic;
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
    public partial class AddEmployee : Window
    {
        /// <summary>
        /// Created by Pat Banks 2015/02/02
        ///
        /// Constructs the add employee form and fills the combo box.
        /// </summary>
        public AddEmployee()
        {
            InitializeComponent();
            this.Title = "Add Employee";
            ReloadComboBox();
            this.chkActiveEmployee.IsEnabled = false;
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
            this.CurrentEmployee = employee;
            this.Title = "Editing: " + CurrentEmployee.GetFullName;
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
        /// Created by Miguel Santana 2015/03/05
        /// Closes the window
        /// </summary>
        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Created by Miguel Santana 2015/03/05
        /// Resets the fields
        /// </summary>
        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            SetFields();
        }

        /// <summary>
        /// Created by Pat Banks 2015/02/15
        /// Calls method to open AddEditEmployee UI
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated:  2015/02/22
        /// Added method to update employee
        /// </remarks>
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEmployee == null) { employeeAdd(); } else { employeeUpdate(); }
        }

        /// <summary>
        /// Created by Pat Banks 2015/02/15
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
        private void employeeAdd()
        {
            int result;

            if (!Validate()) { return; }

            try
            {
                result = EmployeeManager.AddNewEmployee(
                    new Employee(
                        this.txtFirstName.Text,
                        this.txtLastName.Text,
                        this.txtPassword.Password,
                        (int)this.cboUserLevel.SelectedItem,
                        this.chkActiveEmployee.IsChecked.Value
                    )
                );

                if (result == 1)
                {
                    MessageBox.Show("Employee added successfully");
                    //closes window after successful add
                    this.Close();
                }
            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
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
        private void employeeUpdate()
        {
            int result;

            if (!Validate()) { return; }

            try
            {
                result = EmployeeManager.EditCurrentEmployee(
                    CurrentEmployee,
                    new Employee(
                        this.txtFirstName.Text,
                        this.txtLastName.Text,
                        string.IsNullOrEmpty(this.txtPassword.Password) ? this.txtPassword.Password : null,
                        (int)this.cboUserLevel.SelectedItem,
                        this.chkActiveEmployee.IsChecked.Value
                    )
                );

                if (result == 1)
                {
                    MessageBox.Show("Employee updated successfully");
                    //closes window after successful add
                    this.Close();
                }
            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.Message);
            }
        }

        /// <summary>
        /// Reloads the combobox with values from database
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            cboUserLevel.ItemsSource = Enum.GetValues(typeof(RoleData));
        }

        /// <summary>
        /// Created by Miguel Santana 2015/03/05
        /// Resets the values of the fields
        /// </summary>
        private void SetFields()
        {
            if (CurrentEmployee == null)
            {
                this.txtFirstName.Text = null;
                this.txtLastName.Text = null;
                this.txtPassword.Password = null;
                this.txtPassword2.Password = null;
                this.chkActiveEmployee.IsChecked = true;
                this.cboUserLevel.SelectedItem = null;
            }
            else
            {
                this.txtFirstName.Text = CurrentEmployee.FirstName;
                this.txtLastName.Text = CurrentEmployee.LastName;
                this.txtPassword.Password = null;
                this.txtPassword2.Password = null;
                this.chkActiveEmployee.IsChecked = CurrentEmployee.Active;
                this.cboUserLevel.SelectedItem = CurrentEmployee.Level;
            }
        }

        /// <summary>
        /// Created by Pat Banks 2015/02/20
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
            if (!Validator.ValidateString(txtFirstName.Text))
            {
                MessageBox.Show("Please fill out the first name field with a valid name.");
                txtFirstName.Focus();
                txtFirstName.SelectAll();
                return false;
            }
            if (!Validator.ValidateString(txtLastName.Text))
            {
                MessageBox.Show("Please fill out the last name field with a valid name.");
                txtLastName.Focus();
                txtLastName.SelectAll();
                return false;
            }
            bool validatePass = (CurrentEmployee != null && this.txtPassword.Password == "") ? false : true;
            if (validatePass && !Validator.ValidatePassword(txtPassword.Password))
            {
                MessageBox.Show("Password must have a minimum of 8 characters.  \n At Least 1 each of 3 of the following 4:  " +
                                " \n lowercase letter\n UPPERCASE LETTER \n Number \nSpecial Character (not space)");
                txtPassword.Focus();
                txtPassword.SelectAll();
                return false;
            }
            if (validatePass && !this.txtPassword2.Password.Equals(this.txtPassword.Password))
            {
                MessageBox.Show("Your password must match!");
                txtPassword2.Focus();
                txtPassword2.SelectAll();
                return false;
            }
            if (cboUserLevel.Text == "" || cboUserLevel.Text == null)
            {
                MessageBox.Show("Please select a user level.");
                cboUserLevel.Focus();
                cboUserLevel.IsDropDownOpen = true;
                return false;
            }
            return true;
        }
    }
}