using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheEmployees.xaml
    /// </summary>
    public partial class ListTheEmployees : IDataGridContextMenu
    {
        private EmployeeManager _employeeManager = new EmployeeManager();
        private GridViewColumnHeader _sortColumn;

        //Class level variables needed for sorting method
        private ListSortDirection _sortDirection;

        private List<Employee> employeeList;

        /// <exception cref="ArgumentNullException"><paramref name="(DataGridContextMenuResult)" /> is null. </exception>
        /// <exception cref="ArgumentException"><paramref name="(DataGridContextMenuResult)" /> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public ListTheEmployees()
        {
            InitializeComponent();
            RefreshEmployeeList();

            lvEmployeesList.SetContextMenu(this, new[] { DataGridContextMenuResult.Add, DataGridContextMenuResult.View, DataGridContextMenuResult.Edit });
        }

        /// <exception cref="WanderingTurtleException"/>
        public void ContextMenuItem_Click(object sender, RoutedEventArgs e)
        {
            DataGridContextMenuResult command;
            var selectedItem = sender.ContextMenuClick<Employee>(out command);
            switch (command)
            {
                case DataGridContextMenuResult.Add:
                    OpenEmployee();
                    break;

                case DataGridContextMenuResult.View:
                    OpenEmployee(selectedItem, true);
                    break;

                case DataGridContextMenuResult.Edit:
                    OpenEmployee(selectedItem);
                    break;

                default:
                    throw new WanderingTurtleException(this, "Error processing context menu");
            }
        }

        private void OpenEmployee(Employee selectedItem = null, bool readOnly = false)
        {
            try
            {
                if (selectedItem == null)
                {
                    if (new AddEmployee().ShowDialog() == false) return;
                    RefreshEmployeeList();
                }
                else
                {
                    if (new AddEmployee(selectedItem, readOnly).ShowDialog() == false) return;
                    if (readOnly) return;
                    RefreshEmployeeList();
                }
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/19
        ///
        /// Displays add Employee window for user to add additional employees to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            OpenEmployee();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/01
        ///
        /// Opens the update employee UI with the selected employee
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            OpenEmployee(lvEmployeesList.SelectedItem as Employee);
        }

        private void lvEmployeesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            OpenEmployee(sender.RowClick<Employee>(), true);
        }

        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/19
        ///
        /// Retrieves a list of employees from the database to display
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void RefreshEmployeeList()
        {
            lvEmployeesList.ItemsPanel.LoadContent();

            try
            {
                employeeList = _employeeManager.FetchListEmployees();
                lvEmployeesList.ItemsSource = employeeList;
                lvEmployeesList.Items.Refresh();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex, "Unable to retrieve employee list from the database.");
            }
        }

        private void txtEmployeeSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Yes for some reason this is required
            if (btnSearchEmployee == null)
            {
                btnSearchEmployee = new Button();
            }

            btnSearchEmployee.Content = txtEmployeeSearch.Text.Length == 0 ? "Refresh List" : "Search";
        }

        private void btnSearchEmployee_Click(object sender, RoutedEventArgs e)
        {
            var myList = _employeeManager.SearchEmployee(txtEmployeeSearch.Text);
            lvEmployeesList.ItemsSource = myList;
            lvEmployeesList.Items.Refresh();
        }
    }
}
