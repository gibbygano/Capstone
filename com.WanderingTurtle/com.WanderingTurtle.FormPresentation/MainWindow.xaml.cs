using com.WanderingTurtle.BusinessLogic;
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
                switch (MessageBox.Show(
                    string.Format("Error connecting to database.\rWould you like to exit the program?\r\rError Message:\r{0}", ex.Message),
                    "Could not connect to the database", MessageBoxButton.YesNo, MessageBoxImage.Error))
                {
                    case MessageBoxResult.Yes:
                        Environment.Exit(0);
                        break;
                }
            }
            InitializeComponent();
        }

        private void BtnSignInOut_Click(object sender, RoutedEventArgs e)
        {
            StartUp();
        }

        internal void StartUp()
        {
            this.MainContent.Content = new StartupScreen();
        }
    }
}