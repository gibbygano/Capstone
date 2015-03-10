using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
                    UsernameWatermark = "Username",
                    PasswordWatermark = "Password",
                    NegativeButtonVisibility = Visibility.Visible,
                    AffirmativeButtonText = "Log In"
                });
            if (result != null)
            {
                // MessageDialogResult messageResult = await window.ShowMessageAsync("Authentication
                // Information", string.Format("Username: {0}\nPassword: {1}", result.Username, result.Password));
                WindowHelper.GetMainWindow(this).MainContent.Content = new TabContainer();
            }
        }
    }
}