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
    /// Interaction logic for AddUser.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private UserLoginManager myUserManager = new UserLoginManager();
        private UserLogin userToAdd = new UserLogin();

        public AddUser()
        {
            InitializeComponent();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            // collect the values from the form - ID needs to pre-populate
            userToAdd.UserID = int.Parse(txtUserID.Text);
            //userToAdd.FirstName = 
           // userToAdd.LastName = 


        }
    }
}
