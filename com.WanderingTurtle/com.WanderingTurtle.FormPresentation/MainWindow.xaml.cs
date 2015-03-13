using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Models;
using com.WanderingTurtle.FormPresentation.Views;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            try
            {
                ConnectionManager.TestConnection();
            }
            catch (Exception ex)
            {
                _DBConnectError(ex);
            }
            InitializeComponent();
        }

        internal void StartUp()
        {
            this.MainContent.Content = new StartupScreen();
        }

        private async void _DBConnectError(Exception ex)
        {
            switch (await DialogBox.ShowMessageDialog(this, string.Format("Error connecting to database.\rWould you like to exit the program?\r\rError Message:\r{0}", ex.Message),
                "Could not connect to the database", MessageDialogStyle.AffirmativeAndNegative))
            {
                case MessageDialogResult.Affirmative:
                    Environment.Exit(0);
                    break;
            }
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            StartUp();
        }
    }
}