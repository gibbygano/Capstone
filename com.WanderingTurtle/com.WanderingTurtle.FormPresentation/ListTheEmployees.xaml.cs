using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation
{
	/// <summary>
	/// Interaction logic for ListTheEmployees.xaml
	/// </summary>
	public partial class ListTheEmployees
	{
		private List<Employee> _employeeList;

		/// <summary>
		/// Created by Pat Banks 2015/02/19
		/// Displays and refreshes the list of active employees
		/// </summary>
		/// <remarks>
		/// </remarks>

		public ListTheEmployees()
		{
			InitializeComponent();
			RefreshEmployeeList();
		}

		/// <summary>
		/// Created by Pat Banks 2015/02/19
		/// Displays add Employee window for user to add additional employees to the system
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
		{
			var newAddWindow = new AddEmployee();

			if (newAddWindow.ShowDialog() == false)
			{
				RefreshEmployeeList();
			}
		}

		/// <summary>
		/// Created by Pat Banks 2015/02/19
		/// Retrieves a list of employees from the database to display
		/// </summary>
		/// <remarks>
		/// </remarks>
		private void RefreshEmployeeList()
		{
			LvEmployeesList.ItemsPanel.LoadContent();

			try
			{
				_employeeList = EmployeeManager.FetchListEmployees();
				LvEmployeesList.ItemsSource = _employeeList;
				LvEmployeesList.Items.Refresh();
			}
			catch (Exception ex)
			{
				MessageBox.Show(string.Format("Unable to retrieve employee list from the database. \n{0}", ex.Message));
			}
		}

		private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
		{
			var selectedItem = LvEmployeesList.SelectedItem;
			if (selectedItem == null)
			{
				MessageBox.Show("Please select a row to edit");
				return;
			}
			var newAddWindow = new AddEmployee((Employee)selectedItem);

			if (newAddWindow.ShowDialog() == false)
			{
				RefreshEmployeeList();
			}
		}
	}
}