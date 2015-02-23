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
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        private EmployeeManager myManager = new EmployeeManager();

        /// <summary>
        /// Pat Banks
        /// Initialize form
        ///
        /// </summary>
        public AddEmployee()
        {
            InitializeComponent();
            this.Title = "Add Employee";
            ReloadComboBox();
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
            this.chkActiveEmployee.IsEnabled = true;
            ReloadComboBox();

            this.txtFirstName.Text = CurrentEmployee.FirstName;
            this.txtLastName.Text = CurrentEmployee.LastName;
            this.txtPassword.Text = CurrentEmployee.Password;
            this.chkActiveEmployee.IsChecked = CurrentEmployee.Active;
            this.cboUserLevel.SelectedItem = CurrentEmployee.Level;
        }

        public Employee CurrentEmployee { get; private set; }

        // Pat Banks - February 15, 2015
        // Calls method to add employee
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEmployee == null) { employeeAdd(); } else { employeeUpdate(); }
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
        private void employeeAdd()
        {
            int result;

            if (!Validate()) { return; }

            try
            {
                result = myManager.AddNewEmployee(
                    new Employee(
                        this.txtFirstName.Text,
                        this.txtLastName.Text,
                        this.txtPassword.Text,
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
                result = myManager.EditCurrentEmployee(
                    CurrentEmployee,
                    new Employee(
                        this.txtFirstName.Text,
                        this.txtLastName.Text,
                        this.txtPassword.Text,
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
        /// Reloads the combobox with values from database
        /// </summary>
        private void ReloadComboBox()
        {
            //creating a list for the dropdown userLevel
            cboUserLevel.ItemsSource = Enum.GetValues(typeof(RoleData));
        }

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
        private List<RoleData> RetrieveUserLevelList()
        {
            return new List<RoleData>((IEnumerable<RoleData>)Enum.GetValues(typeof(RoleData)));
        }

        /// <summary>
        /// Pat Banks
        ///
        /// Validates the text fields
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
                return false;
            }
            if (!Validator.ValidateString(txtLastName.Text))
            {
                MessageBox.Show("Please fill out the last name field with a valid name.");
                txtLastName.Focus();
                return false;
            }
            bool validatePass = (CurrentEmployee != null && this.txtPassword.Text == "") ? false : true;
            if (validatePass && !Validator.ValidatePassword(txtPassword.Text))
            {
                MessageBox.Show("Password must have a minimum of 8 characters.  \n At Least 1 each of 3 of the following 4:  " +
                                " \n lowercase letter\n UPPERCASE LETTER \n Number \nSpecial Character (not space)");
                txtPassword.Focus();
                return false;
            }

            if (cboUserLevel.Text == "" || cboUserLevel.Text == null)
            {
                MessageBox.Show("Please select a user item.");
                cboUserLevel.Focus();
                return false;
            }
            return true;
        }
    }
}