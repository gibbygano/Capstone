using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro;
using System;
using System.Configuration;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for <see cref="SplashScreen" />
    /// </summary>
    /// <remarks>Miguel Santana 2015/03/10</remarks>
    public partial class StartupScreen
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StartupScreen" /> class.
        /// </summary>
        public StartupScreen()
        {
            Globals.UserToken = null;
            Application.Current.ChangeAppStyle(ThemeManager.GetAccent("Emerald"));
            InitializeComponent();
            TxtUserName.Focus();
        }

        /// <summary>
        /// The sign-in button click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        /// <exception cref="ApplicationException"></exception>
        private void BtnSignInClick(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        private void Submit()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]))
            { Globals.UserToken = new Employee(100, "Debugger", null, 1); }

            int userId;
            if (!int.TryParse(TxtUserName.Text.Trim(), out userId))
            {
                throw new InputValidationException(TxtUserName, "Please enter your User ID.", "Login Error");
            }

            var password = TxtPassword.Password.Trim();
            if (string.IsNullOrEmpty(password) || password.ValidatePassword())
            {
                throw new InputValidationException(TxtPassword, "Please enter your Password.", "Login Error");
            }

            try
            {
                TxtPassword.Clear();
                Globals.UserToken = new EmployeeManager().GetEmployeeLogin(userId, password);
            }
            catch (ApplicationException ex)
            {
                throw new InputValidationException(TxtPassword, ex, "Login Error");
            }

            if (Globals.UserToken == null)
            {
                throw new WanderingTurtleException(this, "Error setting User Token");
            }

            this.GetWindow<MainWindow>().BtnSignOut.Visibility = Visibility.Visible;
            this.GetWindow<MainWindow>().MainContent.Content = new TabContainer();
        }

        private void TxtInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var @textBox = sender as TextBoxBase;
            if (@textBox != null) { @textBox.SelectAll(); }
            else
            {
                var @passwordBox = sender as PasswordBox;
                if (@passwordBox != null) { @passwordBox.SelectAll(); }
            }
        }

        private void TxtInput_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    Submit();
                    break;
            }
        }
    }
}