using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation
{
	/// <summary>
	/// Pat Banks
	/// Created: 2015/02/02
	/// Interaction logic for AddEmployee.xaml
	/// </summary>
	/// <remarks>
	/// </remarks>
	public partial class AddEmployee
	{
		/// <summary>
		/// Pat Banks
		/// Created:  2015/02/02
		/// Constructs the add employee form and fills the combo box.
		/// </summary>
		/// <remarks>
		/// </remarks>
		public AddEmployee()
		{
			InitializeComponent();
			Title = "Add Employee";
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
			CurrentEmployee = employee;
			Title = "Editing: " + CurrentEmployee.GetFullName;
			ChkActiveEmployee.IsEnabled = true;
			TxtPassword.IsEnabled = false;
			ReloadComboBox();

			TxtFirstName.Text = CurrentEmployee.FirstName;
			TxtLastName.Text = CurrentEmployee.LastName;
			TxtPassword.Text = CurrentEmployee.Password;
			ChkActiveEmployee.IsChecked = CurrentEmployee.Active;
			CboUserLevel.SelectedItem = CurrentEmployee.Level;
		}

		private Employee CurrentEmployee { get; }

		/// <summary>
		/// Pat Banks
		/// Created:  2015/02/15
		/// Calls method to add employee
		/// </summary>
		/// <remarks>
		/// Miguel Santana
		/// Updated:  2015/02/22
		/// Added method to update employee
		/// </remarks>
		/// <param name="sender"></param>
		/// <param name="e"></param>

		private void btnSubmitEmployee_Click(object sender, RoutedEventArgs e)
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
				var result = EmployeeManager.AddNewEmployee(
					new Employee(
						TxtFirstName.Text,
						TxtLastName.Text,
						TxtPassword.Text,
						(int)CboUserLevel.SelectedItem,
						ChkActiveEmployee.IsChecked != null && ChkActiveEmployee.IsChecked.Value
						)
					);

				if (result != 1) return;
				MessageBox.Show("Employee added successfully");
				//closes window after successful add
				Close();
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
		private void EmployeeUpdate()
		{
			if (!Validate()) { return; }

			try
			{
				var result = EmployeeManager.EditCurrentEmployee(
					CurrentEmployee,
					new Employee(
						TxtFirstName.Text,
						TxtLastName.Text,
						TxtPassword.Text,
						(int)CboUserLevel.SelectedItem,
						ChkActiveEmployee.IsChecked != null && ChkActiveEmployee.IsChecked.Value
						)
					);

				if (result != 1) return;
				MessageBox.Show("Employee updated successfully");
				//closes window after successful add
				Close();
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
			CboUserLevel.ItemsSource = RetrieveUserLevelList;
		}

		// ReSharper disable once SuspiciousTypeConversion.Global
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
		private static IEnumerable<RoleData> RetrieveUserLevelList => new List<RoleData>((IEnumerable<RoleData>)Enum.GetValues(typeof(RoleData)));

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
			if (!Validator.ValidateString(TxtFirstName.Text))
			{
				MessageBox.Show("Please fill out the first name field with a valid name.");
				TxtFirstName.Focus();
				return false;
			}
			if (!Validator.ValidateString(TxtLastName.Text))
			{
				MessageBox.Show("Please fill out the last name field with a valid name.");
				TxtLastName.Focus();
				return false;
			}
			var validatePass = (CurrentEmployee == null || TxtPassword.Text != "");
			if (validatePass && !Validator.ValidatePassword(TxtPassword.Text))
			{
				MessageBox.Show("Password must have a minimum of 8 characters.  \n At Least 1 each of 3 of the following 4:  " +
								" \n lowercase letter\n UPPERCASE LETTER \n Number \nSpecial Character (not space)");
				TxtPassword.Focus();
				return false;
			}

			if (!string.IsNullOrEmpty(CboUserLevel.Text)) return true;
			MessageBox.Show("Please select a user item.");
			CboUserLevel.Focus();
			return false;
		}
	}
}