using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.FormPresentation.Models;
using com.WanderingTurtle.FormPresentation.Views;
using System;
using System.Data.Common;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        /// <summary>
        /// Matt Lapka
        /// Created: 2015/01/30
        /// Initial Creation of project
        /// </summary>
        /// <remarks>
        /// Miguel Santana
        /// Updated 2015/03/04
        /// Added user tabs
        /// </remarks>
        /// <exception cref="WanderingTurtleException">Condition.</exception>
        public MainWindow()
        {
            try
            {
                ConnectionManager.TestConnection();
            }
            catch (DbException ex)
            {
                _DBConnectError(ex);
            }

            try
            {
                InitializeComponent();
            }
            catch (Exception ex)
            {
                throw new WanderingTurtleException(this, ex);
            }
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/08
        /// </summary>
        internal void StartUp()
        {
            MainContent.Content = new StartupScreen();
        }

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/13
        ///
        /// Shows a message box stating the there isn't a connection to the database
        /// </summary>
        /// <param name="ex"></param>
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

        /// <summary>
        /// Miguel Santana
        /// Created: 2015/03/13
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSignOut_Click(object sender, RoutedEventArgs e)
        {
            BtnSignOut.Visibility = Visibility.Collapsed;
            StartUp();
        }
    }
}