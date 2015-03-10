using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
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

            switch (Globals.UserToken != null ? Globals.UserToken.Level : RoleData.Admin)
            {
                case RoleData.Admin:
                    AddTab(TabName.Events, new ListEvents());
                    AddTab(TabName.Listings, new ListTheListings());
                    AddTab(TabName.Suppliers, new ListSuppliers());
                    AddTab(TabName.Employees, new ListTheEmployees());
                    AddTab(TabName.HotelGuests, new ListHotelGuests());
                    break;

                case RoleData.Concierge:
                    break;

                case RoleData.DeskClerk:
                    break;

                case RoleData.Valet:
                    break;
            }
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