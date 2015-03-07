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
    /// Arik Chadima
    /// 2015/02/22
    /// 
    /// Interaction logic for UserLogin.xaml
    /// </summary>
    /// <remarks>
    /// Routed Event code examples used and modified from MSDN to suite the needs here.
    /// Original example can be found here: 
    /// https://msdn.microsoft.com/en-us/library/ms752288(v=vs.100).aspx
    /// </remarks>
    public partial class UserLogin : UserControl
    {
        #region Custom events and their event handlers
        /// <summary>
        /// Arik Chadima
        /// 2015/02/26
        /// 
        /// Declares routed events for the UserControl Class.
        /// </summary>
        /// <remarks>
        /// Know what you're doing before modifying these; they are the events that power userLogin.
        /// </remarks>
        public static readonly RoutedEvent UserLoggedInEvent = System.Windows.EventManager.RegisterRoutedEvent("UserLoggedIn", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserLogin));
        public static readonly RoutedEvent UserLoggedOutEvent = System.Windows.EventManager.RegisterRoutedEvent("UserLoggedOut", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserLogin));
        public static readonly RoutedEvent UserLoginFailedEvent = System.Windows.EventManager.RegisterRoutedEvent("UserLoginFailed", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(UserLogin));

        public event RoutedEventHandler UserLoggedIn
        {
            add { AddHandler(UserLoggedInEvent, value); }
            remove { RemoveHandler(UserLoggedInEvent, value); }
        }

        public event RoutedEventHandler UserLoggedOut
        {
            add { AddHandler(UserLoggedOutEvent, value); }
            remove { RemoveHandler(UserLoggedOutEvent, value); }
        }

        public event RoutedEventHandler UserLoginFailed
        {
            add { AddHandler(UserLoginFailedEvent, value); }
            remove { RemoveHandler(UserLoginFailedEvent, value); }
        }
        #endregion

        public UserLogin()
        {
            InitializeComponent();
        }

        
        /// <summary>
        /// Arik Chadima
        /// 2015/02/23
        /// 
        /// Handles the login button click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginSignInButton_Click(object sender, RoutedEventArgs e)
        {

            int? empID = Validator.ValidateInt(loginUsername.Text, 0) ? (int?)Convert.ToInt32(loginUsername.Text) : null; // nullable ints for the "failed" flag
            if (empID != null)
            {
                try
                {
                    Employee checkEmp = EmployeeManager.GetEmployeeLogin((int)empID, loginPassword.Password);
                    if (checkEmp!=null)
                    {
                        Globals.UserToken = checkEmp;
                        this.loginBoxes.Visibility = Visibility.Hidden;
                        this.loginLabels.Visibility = Visibility.Visible;
                        this.lblUserName.Content = checkEmp.GetFullName;
                        this.lblUserRole.Content = checkEmp.Level.ToString();
                        // Raises the "UserLoggedIn" event. //
                        RoutedEventArgs userLoginArgs = new RoutedEventArgs(UserLogin.UserLoggedInEvent);
                        RaiseEvent(userLoginArgs);
     // MainWindow mainForm = new MainWindow();

       //mainForm.ShowDialog();

                        return;//ends the method here.

                    }
                }
                catch (Exception ex)
                {
                    // Better Error handling can be added in the future, this is just here for dev purposes. //
                    System.Windows.Forms.MessageBox.Show(ex.ToString());
                }

            }

            // Raises the "UserLoginFailed" event. //
            RoutedEventArgs userFailedArgs = new RoutedEventArgs(UserLogin.UserLoggedInEvent);
            RaiseEvent(userFailedArgs);
        }

        /// <summary>
        /// Arik Chadima
        /// 2015/02/23
        /// 
        /// Handles logout click event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void loginSignOutButton_Click(object sender, RoutedEventArgs e)
        {
            if (Globals.UserToken != null)
            {
                Globals.UserToken = null;

            }
            else
            {
                // Placeholder notification that can be moved elsewhere or removed entirely //
                System.Windows.Forms.MessageBox.Show("User is already logged out!");
            }

            this.loginBoxes.Visibility = Visibility.Visible;
            this.loginLabels.Visibility = Visibility.Hidden;

            // Raises the "UserLoggedOut" event. //
            RoutedEventArgs userLogoutArgs = new RoutedEventArgs(UserLogin.UserLoggedOutEvent);
            RaiseEvent(userLogoutArgs);
        }
    }
}
