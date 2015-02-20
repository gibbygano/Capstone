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
        /// Generates a list of employees
        /// Pat Banks 2/19/15
        /// </summary>
        public ListTheEmployees()
        {
            InitializeComponent();
            RefreshEmployeeList();
        }

        /// <summary>
        /// Adding the employee to the database
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
        ///  Refreshes list
        ///  Pat Banks 2/19/15
        /// </summary>
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