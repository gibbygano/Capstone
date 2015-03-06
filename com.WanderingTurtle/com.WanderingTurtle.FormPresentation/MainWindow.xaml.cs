using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                switch (MessageBox.Show(string.Format("Error connecting to database.\rWould you like to exit the program?\r\rError:\r{0}", ex.Message), "Could not connect to the database", MessageBoxButton.YesNo, MessageBoxImage.Error))
                {
                    case MessageBoxResult.Yes:
                        Environment.Exit(0);
                        break;
                }
            }
            InitializeComponent();
        }
    }
}