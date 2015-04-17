using System;
using System.Windows;
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Views;

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

        private void _DBConnectError(Exception ex)
        {
            switch (MessageBox.Show(string.Format("Error connecting to database.\rWould you like to exit the program?\r\rError Message:\r{0}", ex.Message),
                "Could not connect to the database", MessageBoxButton.OKCancel))
            {
                case MessageBoxResult.Yes:
                case MessageBoxResult.OK:
                    Application.Current.Shutdown();
                    break;
            }
        }

        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            StartUp();
        }
    }
}