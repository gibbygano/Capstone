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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheEmployees.xaml
    /// </summary>
    public partial class ListTheEmployees : UserControl
    {
        private EmployeeManager myEmployees = new EmployeeManager();
        private List<Employee> employeeList;

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
            AddEmployee newAddWindow = new AddEmployee();

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
            lvEmployeesList.ItemsPanel.LoadContent();

            try
            {
                employeeList = myEmployees.FetchListEmployees();
                lvEmployeesList.ItemsSource = employeeList;
                lvEmployeesList.Items.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve employee list from the database. \n" + ex.Message);
            }
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.lvEmployeesList.SelectedItem;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select a row to edit");
                return;
            }
            AddEmployee newAddWindow = new AddEmployee((Employee)selectedItem);

            if (newAddWindow.ShowDialog() == false)
            {
                RefreshEmployeeList();
            }
        }
    }
}