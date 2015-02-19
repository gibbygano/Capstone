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
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ListTheEmployees.xaml
    /// </summary>
    public partial class ListTheEmployees : UserControl
    {
        private EmployeeManager myEmployees = new EmployeeManager();
        List<Employee> employeeList;

        public ListTheEmployees()
        {
            InitializeComponent();
        }

        private void btnAddEmployee_Click(object sender, RoutedEventArgs e)
        {
            AddEmployee newEmployee;

            if (AddEmployee.Instance == null)
            {
                newEmployee = new AddEmployee();
                newEmployee.Show();
            }
            else
            {
                newEmployee = AddEmployee.Instance;
                newEmployee.Activate();

                //Creates a sound effect through the System.Media and  flash from accessibility

                System.Media.SystemSounds.Exclamation.Play();
            }

        }
        private void btnRefreshList_Click(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeList();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshEmployeeList();
        }

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
    }
}
