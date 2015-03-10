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
                //switch (Xceed.Wpf.Toolkit.MessageBox.Show(
                //    string.Format("Error connecting to database.\rWould you like to exit the program?\r\rError:\r{0}", ex.Message),
                //    "Could not connect to the database", MessageBoxButton.YesNo, MessageBoxImage.Error))
                //{
                //    case MessageBoxResult.Yes:
                //        Environment.Exit(0);
                //        break;
                //}
            }
            InitializeComponent();
        }

        private async void BtnSignInOut_Click(object sender, RoutedEventArgs e)
        {
            StartUp();
        }

        internal void StartUp()
        {
            this.MainContent.Content = new StartupScreen();
        }
    }
}