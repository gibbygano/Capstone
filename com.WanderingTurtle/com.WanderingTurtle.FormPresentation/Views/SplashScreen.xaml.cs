using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls.Dialogs;
using System;
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
            do
            {
                var window = WindowHelper.GetMainWindow(this);
                LoginDialogSettings settings = new LoginDialogSettings
                {
                    ColorScheme = window.MetroDialogOptions.ColorScheme,
                    UsernameWatermark = "User ID",
                    PasswordWatermark = "Password",
                    NegativeButtonVisibility = Visibility.Visible,
                    AffirmativeButtonText = "Log In",
                    InitialUsername = _user
                };
                LoginDialogData result = await window.ShowLoginAsync("Authentication", "Enter your credentials.", settings);
                if (result == null) { break; }
                try
                {
                    int UserId;
                    if (!int.TryParse(result.Username, out UserId)) { throw new Exception(string.Format("Please enter your {0}.", settings.UsernameWatermark)); }
                    _user = UserId.ToString();
                    if (string.IsNullOrWhiteSpace(result.Password)) { throw new Exception(string.Format("Please enter your {0}.", settings.PasswordWatermark)); }
                    Globals.UserToken = EmployeeManager.GetEmployeeLogin(UserId, result.Password);
                }
                catch (Exception ex) { _exception = ex; }
                if (_exception != null) { await ErrorManager.ShowMessageDialog(this, _exception.Message); }
            } while (_exception != null);
            if (Globals.UserToken != null) { WindowHelper.GetMainWindow(this).MainContent.Content = new TabContainer(); }
        }
    }
}