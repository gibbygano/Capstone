using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class StartupScreen
    {
        public StartupScreen()
        {
            Globals.UserToken = null;
            InitializeComponent();
        }

        private async void BtnSignIn_Click(object sender, RoutedEventArgs e)
        {
            var window = WindowHelper.GetMainWindow(this);
            //window.MetroDialogOptions.ColorScheme = true ? MetroDialogColorScheme.Accented : MetroDialogColorScheme.Theme;
            LoginDialogData result = await window.ShowLoginAsync("Authentication", "Enter your credentials. :Capstone Login WIP:",
                new LoginDialogSettings
                {
                    ColorScheme = window.MetroDialogOptions.ColorScheme,
                    UsernameWatermark = "User ID",
                    PasswordWatermark = "Password",
                    NegativeButtonVisibility = Visibility.Visible,
                    AffirmativeButtonText = "Log In"
                });
            if (result != null)
            {
                // MessageDialogResult messageResult = await window.ShowMessageAsync("Authentication
                // Information", string.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
                Exception ex = null;
                try
                {
                    Globals.UserToken = EmployeeManager.GetEmployeeLogin(int.Parse(result.Username ?? "0"), result.Password);
                }
                catch (Exception x)
                {
                    ex = x;
                }
                if (ex != null)
                {
                    await ErrorManager.ShowMessageDialog(this, ex.Message);
                    BtnSignIn_Click(sender, e);
                    return;
                }
                WindowHelper.GetMainWindow(this).MainContent.Content = new TabContainer();
            }
        }
    }
}