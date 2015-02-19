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
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for AddEmployee.xaml
    /// </summary>
    public partial class AddEmployee : Window
    {
        public static AddEmployee Instance;
        Employee newEmployeeUser = new Employee();
        EmployeeManager myManager = new EmployeeManager();

        /// <summary>
        /// Initialize form 
        /// 
        /// </summary>
        public AddEmployee()
        {
            InitializeComponent();
            Instance = this;
        }
        
        // Pat Banks - February 15, 2015
        // Parameters: text fields from form
        // Desc.: Method takes values for a new employee from the form and passes values
        //        into the AddNewEmployee method of the EmployeeManager class
        // Failure: Exception is thrown if database is not available or 
        //          new employee cannot be created in the database for any reason
        // Success: A user is added to the database

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!Validator.ValidateString(txtLastName.Text))
                {
                    MessageBox.Show("Please fill out the last name field.");

                } 
                else if(!Validator.ValidateString(txtFirstName.Text))
                {
                    MessageBox.Show("Please fill out the first name field.");
                }
                else if(!Validator.ValidateString(txtPassword.Text))
                {
                    MessageBox.Show("Please enter a password.");
                }

                // collect the values from the form
                newEmployeeUser.FirstName = this.txtFirstName.Text;
                newEmployeeUser.LastName = this.txtLastName.Text;
                newEmployeeUser.Active = this.chkActiveEmployee.IsChecked.Value;
                newEmployeeUser.Password = this.txtPassword.Text;
                newEmployeeUser.Level = int.Parse(this.txtUserLevel.Text);

                int result = myManager.AddNewEmployee(newEmployeeUser);

                if (result == 1)
                {
                    MessageBox.Show("employee added successfully");

                }
                else
                {
                    MessageBox.Show("There was problem adding to the db");
                }
            }
            catch (Exception ax)
            {
                MessageBox.Show(ax.ToString());
            }            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }        
    }
}