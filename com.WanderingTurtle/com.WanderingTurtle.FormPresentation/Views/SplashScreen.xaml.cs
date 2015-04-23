using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Configuration;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// Miguel Santana 2015/03/10
    /// </summary>
    public partial class StartupScreen
    {
        public StartupScreen()
        {
            Globals.UserToken = null;
            InitializeComponent();
        }

        private string _user;
        private Exception _exception = null;

        private async void BtnSignIn_Click(object sender, RoutedEventArgs e)
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
                    int UserId;
                    if (!int.TryParse(result.Username, out UserId)) { throw new Exception(string.Format("Please enter your {0}.", settings.UsernameWatermark)); }
                    _user = UserId.ToString();
                    if (string.IsNullOrWhiteSpace(result.Password)) { throw new Exception(string.Format("Please enter your {0}.", settings.PasswordWatermark)); }
                    Globals.UserToken = new EmployeeManager().GetEmployeeLogin(UserId, result.Password);
                    if (Globals.UserToken == null) { throw new Exception("Error setting User Token"); }
                    else { _exception = null; }
                }
                catch (Exception ex) { _exception = ex; }
                if (_exception != null) { await this.ShowMessageDialog(_exception.Message, "Login Error"); }
            } while (_exception != null);
            if (Globals.UserToken != null) { this.GetMainWindow().MainContent.Content = new TabContainer(); }
        }
    }
}
