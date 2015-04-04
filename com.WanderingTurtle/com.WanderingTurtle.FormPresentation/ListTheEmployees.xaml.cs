using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheEmployees.xaml
    /// </summary>
    public partial class ListTheEmployees : UserControl
    {
        private EmployeeManager _employeeManager = new EmployeeManager();
        private GridViewColumnHeader _sortColumn;

        //Class level variables needed for sorting method
        private ListSortDirection _sortDirection;

        private List<Employee> employeeList;
        /// <summary>
        /// Pat Banks
        /// Created: 2015/02/19
        ///
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
        /// Pat Banks
        /// Created: 2015/02/19
        ///
        /// Displays add Employee window for user to add additional employees to the system
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee newAddWindow = new AddEmployee();

            if (newAddWindow.ShowDialog() == false)
            {
                RefreshEmployeeList();
            }
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
            var selectedItem = this.lvEmployeesList.SelectedItem;
            if (selectedItem == null)
            {
                DialogBox.ShowMessageDialog(this, "Please select a row to edit");
                return;
            }
            UpdateEmployee(selectedItem as Employee);
        }

        /// <summary>
        /// Will Fritz
        /// Created: 2015/02/27
        ///
        /// This method will sort the listview column in both asending and desending order
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lvEmployeeListHeaderClick(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader column = e.OriginalSource as GridViewColumnHeader;
            if (column == null)
            {
                return;
            }

            if (_sortColumn == column)
            {
                // Toggle sorting direction
                _sortDirection = _sortDirection == ListSortDirection.Ascending ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            else
            {
                _sortColumn = column;
                _sortDirection = ListSortDirection.Ascending;
            }

            string header = string.Empty;

            // if binding is used and property name doesn't match header content
            Binding b = _sortColumn.Column.DisplayMemberBinding as Binding;

            if (b != null)
            {
                header = b.Path.Path;
            }

            try
            {
                ICollectionView resultDataView = CollectionViewSource.GetDefaultView(lvEmployeesList.ItemsSource);
                resultDataView.SortDescriptions.Clear();
                resultDataView.SortDescriptions.Add(new SortDescription(header, _sortDirection));
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "There must be data in the list before you can sort it");
            }
        }

        private void lvEmployeesList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            IInputElement element = e.MouseDevice.DirectlyOver;
            if (element != null && element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent is DataGridCell)
                {
                    var grid = sender as DataGrid;
                    if (grid != null && grid.SelectedItems != null && grid.SelectedItems.Count == 1)
                    {
                        UpdateEmployee(grid.SelectedItem as Employee);
                    }
                }
            }
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
                DialogBox.ShowMessageDialog(this, ex.Message, "Unable to retrieve employee list from the database.");
            }
        }

        private void UpdateEmployee(Employee selectedEmployee)
        {
            AddEmployee newAddWindow = new AddEmployee(selectedEmployee);

            if (newAddWindow.ShowDialog() == false)
            {
                RefreshEmployeeList();
            }
        }
    }
}