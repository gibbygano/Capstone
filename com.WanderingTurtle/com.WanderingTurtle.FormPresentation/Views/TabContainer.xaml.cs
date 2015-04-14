using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for TabContainer.xaml
    /// </summary>
    public partial class TabContainer
    {
        public TabContainer()
        {
            InitializeComponent();
            if (Globals.UserToken != null)
            {
                this.CurrentUserLabel.Content = Globals.UserToken.GetFullName;
                switch (Globals.UserToken.Level)
                {
                    case RoleData.Admin:
                        AddTab(TabName.HotelGuests, new ListHotelGuests());
                        AddTab(TabName.Suppliers, new ListSuppliers());
                        AddTab(TabName.Employees, new ListTheEmployees());
                        AddTab(TabName.Events, new ListEvents());
                        AddTab(TabName.Listings, new ListTheListings());
                        break;

                    case RoleData.Concierge:
                        AddTab(TabName.HotelGuests, new ListHotelGuests());
                        AddTab(TabName.Listings, new ListTheListings());
                        break;

                    case RoleData.DeskClerk:
                        AddTab(TabName.HotelGuests, new ListHotelGuests());
                        break;

                    case RoleData.Valet:
                        AddTab(TabName.Listings, new ListTheListings());
                        break;
                }
            }
            else { throw new WanderingTurtleException(this, "Could not find logged in user", "Login Error"); }
        }

        private void AddTab(string tabName, object tabContent)
        {
            MainTabControl.Items.Add(new TabItem
            {
                Header = tabName,
                Content = tabContent
            });
        }

        private static class TabName
        {
            internal static string Events { get { return "Events"; } }

            internal static string Listings { get { return "Listings"; } }

            internal static string Suppliers { get { return "Suppliers"; } }

            internal static string Employees { get { return "Employees"; } }

            internal static string HotelGuests { get { return "Hotel Guests"; } }
        }
    }
}