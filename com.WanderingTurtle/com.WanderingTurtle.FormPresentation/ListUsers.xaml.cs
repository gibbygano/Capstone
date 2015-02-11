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
    /// Interaction logic for ListUsers.xaml
    /// </summary>
    public partial class ListUsers : UserControl
    {
        public ListUsers()
        {
            InitializeComponent();
        }

        private void btnUpdateUser_Click(object sender, RoutedEventArgs e)
        {

        }
        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            var newWind = new AddUser();
            newWind.Show();
        }
        private void btnDeleteUser_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
