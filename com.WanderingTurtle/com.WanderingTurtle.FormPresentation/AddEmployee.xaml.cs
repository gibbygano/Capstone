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
        UserLoginManager myEmployeeManager = new UserLoginManager();
        UserLogin newUser = new UserLogin();

        public AddEmployee()
        {
            InitializeComponent();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (Validate() == false)
            {
                return;
            }
            else
            {
                lblErrorMessage.Content = "";
                AddNewEmployee();
            }

            // collect the values from the form
            newUser.UserID = int.Parse(this.txtEmployeeID.Text);
            newUser.FirstName = this.txtFirstName.Text;
            newUser.LastName = this.txtLastName.Text;

        }

        public bool Validate()
        {
            if (!Validator.ValidateString(txtLastName.Text) || !Validator.ValidateString(txtFirstName.Text))
            {
                lblErrorMessage.Content = "Please fill out the name field.";
                return false;
            }
            
            return true;
        }

        public void AddNewEmployee()
        {
            try
            {
                Employee myEmployee = new Employee();

                myEmployee.FirstName = txtFirstName.Text;
                myEmployee.LastName = txtLastName.Text;

                myEmployeeManager.AddAnEmployee(myEmployee);
            }
            catch (Exception)
            {
                lblErrorMessage.Content = "There was an error adding the employee.";
            }
        }
    }
}
