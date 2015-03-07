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

            switch (Globals.UserToken != null ? Globals.UserToken.Level : RoleData.Admin)
            {
                case RoleData.Admin:
                    AddTab(TabName.Events, new ListEvents());
                    AddTab(TabName.Listings, new ListTheListings());
                    AddTab(TabName.Suppliers, new ListSuppliers());
                    AddTab(TabName.Employees, new ListTheEmployees());
                    AddTab(TabName.HotelGuests, new ListHotelGuests());
                    break;
            }
        }

        private void AddTab(TabName tabItem, Object Control)
        {
            TabItem item = new TabItem();
            item.Content = Control;
            item.Header = Enum.GetName(typeof(TabName), tabItem);
            TabControl.Items.Add(item);
        }

        private enum TabName
        {
            Events,
            Listings,
            Suppliers,
            Employees,
            HotelGuests
        }
    }
}