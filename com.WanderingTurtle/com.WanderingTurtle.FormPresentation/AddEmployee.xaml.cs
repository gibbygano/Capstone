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
        private Employee newEmployeeUser = new Employee();

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
            this.Title = "Edit Employee";
            ReloadComboBox();

            this.txtFirstName.Text = CurrentEmployee.FirstName;
            this.txtLastName.Text = CurrentEmployee.LastName;
            this.txtPassword.Text = CurrentEmployee.Password;
            this.chkActiveEmployee.IsChecked = CurrentEmployee.Active;
            foreach (RoleData item in cboUserLevel.Items)
            {
                if (item.value == CurrentEmployee.Level)
                {
                    // TODO Fix item selection
                }
            }
            if (this.cboUserLevel.SelectedItem == null)
            {
                MessageBox.Show("Error selecting value from combobox");
            }
        }

        public Employee CurrentEmployee { get; private set; }

        // Pat Banks - February 15, 2015
        // Parameters: text fields from form
        // Desc.: Method takes values for a new employee from the form and passes values
        //        into the AddNewEmployee method of the EmployeeManager class
        // Failure: Exception is thrown if database is not available or
        //          new employee cannot be created in the database for any reason
        // Success: A user is added to the database
        private void addEmployee()
        {
            int result;

            if (!Validate()) { return; }

            try
            {
                // collect the values from the form
                newEmployeeUser.FirstName = this.txtFirstName.Text;
                newEmployeeUser.LastName = this.txtLastName.Text;
                newEmployeeUser.Active = this.chkActiveEmployee.IsChecked.Value;
                newEmployeeUser.Password = this.txtPassword.Text;
                newEmployeeUser.Level = int.Parse(this.cboUserLevel.SelectedValue.ToString());

                result = myManager.AddNewEmployee(newEmployeeUser);

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
            if (!Validator.ValidatePassword(txtPassword.Text))
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

        // Pat Banks - February 15, 2015
        // Calls method to add employee
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentEmployee == null) { addEmployee(); }
            else { updateEmployee(); }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/02/20
        ///
        /// Validates and Updates Employee user
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void updateEmployee()
        {
            int result;

            if (!Validate()) { return; }

            try
            {
                // collect the values from the form
                newEmployeeUser.FirstName = this.txtFirstName.Text;
                newEmployeeUser.LastName = this.txtLastName.Text;
                newEmployeeUser.Active = this.chkActiveEmployee.IsChecked.Value;
                newEmployeeUser.Password = this.txtPassword.Text;
                newEmployeeUser.Level = int.Parse(this.cboUserLevel.SelectedValue.ToString());

                result = myManager.EditCurrentEmployee(CurrentEmployee, newEmployeeUser);

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
            cboUserLevel.ItemsSource = RetrieveUserLevelList();
        }

        // Pat Banks - February 19, 2015
        // Parameters: returns list data
        // Desc.: Defines employee roles for the combo box
        // Failure: none
        // Success: box is filled and available for use on the form
        private List<RoleData> RetrieveUserLevelList()
        {
            List<RoleData> ListData = new List<RoleData>();
            ListData.Add(new RoleData { id = "Admin", value = 1 });
            ListData.Add(new RoleData { id = "Concierge", value = 2 });
            ListData.Add(new RoleData { id = "DeskClerk", value = 3 });
            ListData.Add(new RoleData { id = "Valet", value = 4 });

            return ListData;
        }
    }
}