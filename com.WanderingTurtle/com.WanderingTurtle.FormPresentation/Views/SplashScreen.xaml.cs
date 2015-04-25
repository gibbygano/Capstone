using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Configuration;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for <see cref="SplashScreen" />
    /// </summary>
    /// <remarks>Miguel Santana 2015/03/10</remarks>
    public partial class StartupScreen
    {
        /// <summary>
        /// Temporary exception to handle user validation
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// Temporary user for user validation
        /// </summary>
        private string _user;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupScreen" /> class.
        /// </summary>
        public StartupScreen()
        {
            Globals.UserToken = null;
            InitializeComponent();
        }

        /// <summary>
        /// The sign-in button click event handler.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        /// <exception cref="ApplicationException"></exception>
        private async void BtnSignInClick(object sender, RoutedEventArgs e)
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]))
            { Globals.UserToken = new Common.Employee(100, "Debugger", null, 1); }

            do
            {
                if (Globals.UserToken != null) { break; }
                LoginDialogSettings settings = new LoginDialogSettings
                {
                    UsernameWatermark = "User ID",
                    PasswordWatermark = "Password",
                    NegativeButtonVisibility = Visibility.Visible,
                    AffirmativeButtonText = "Log In",
                    InitialUsername = _user
                };
                LoginDialogData result = await this.ShowLoginDialog("Enter your credentials.", "Authentication", settings);
                if (result == null) { break; }
                try
                {
                    int userId;
                    if (!int.TryParse(result.Username, out userId)) { throw new ApplicationException(string.Format("Please enter your {0}.", settings.UsernameWatermark)); }
                    _user = userId.ToString();
                    if (string.IsNullOrWhiteSpace(result.Password)) { throw new ApplicationException(string.Format("Please enter your {0}.", settings.PasswordWatermark)); }
                    Globals.UserToken = new EmployeeManager().GetEmployeeLogin(userId, result.Password);
                    if (Globals.UserToken == null) { throw new ApplicationException("Error setting User Token"); }
                    else { _exception = null; }
                }
                catch (ApplicationException ex) { _exception = ex; }
                if (_exception != null) { await this.ShowMessageDialog(_exception.Message, "Login Error"); }
            } while (_exception != null);
            if (Globals.UserToken != null) { this.GetMainWindow().MainContent.Content = new TabContainer(); }
        }
    }
}