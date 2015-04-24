using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for TabContainer.xaml
    /// </summary>
    public partial class TabContainer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TabContainer" /> class.
        /// </summary>
        /// <exception cref="ArgumentNullException">
        /// <see cref="DataGridContextMenuResult" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DataGridContextMenuResult" /> is not an <see cref="T:System.Enum" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The item to add already has a different logical parent.
        /// </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException"></exception>
        public TabContainer()
        {
            InitializeComponent();
            if (Globals.UserToken != null)
            {
                CurrentUserLabel.Content = Globals.UserToken.GetFullName;

                switch (Globals.UserToken.Level)
                {
                    case RoleData.Admin:
                        AddTab(TabName.HotelGuests, new ListHotelGuests());
                        AddTab(TabName.Suppliers, new ListSuppliers());
                        AddTab(TabName.Employees, new ListTheEmployees());
                        AddTab(TabName.Events, new ListEvents());
                        AddTab(TabName.Listings, new ListTheListings());
                        AddTab(TabName.AdminFunctions, new AdminFunctions());
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

                // Filling ZipCode Cache
                new CityStateManager().PopulateCityStateCache();
            }
            else { throw new WanderingTurtleException(this, "Could not find logged in user", "Login Error"); }
        }

        /// <summary>
        /// Add <see cref="TabItem" /> to the <see cref="TabControl" />
        /// </summary>
        /// <param name="tabName">The tab name.</param>
        /// <param name="tabContent">The tab content.</param>
        private void AddTab(string tabName, object tabContent)
        {
            MainTabControl.Items.Add(new TabItem
            {
                Header = tabName,
                Content = tabContent
            });
        }

        /// <summary>
        /// The tab name.
        /// </summary>
        private static class TabName
        {
            /// <summary>
            /// Gets the events.
            /// </summary>
            internal static string Events { get { return "Events"; } }

            /// <summary>
            /// Gets the listings.
            /// </summary>
            internal static string Listings { get { return "Listings"; } }

            /// <summary>
            /// Gets the suppliers.
            /// </summary>
            internal static string Suppliers { get { return "Suppliers"; } }

            /// <summary>
            /// Gets the employees.
            /// </summary>
            internal static string Employees { get { return "Employees"; } }

            /// <summary>
            /// Gets the hotel guests.
            /// </summary>
            internal static string HotelGuests { get { return "Hotel Guests"; } }

            /// <summary>
            /// Gets the admin functions.
            /// </summary>
            internal static string AdminFunctions { get { return "Admin Functions"; } }
        }
    }
}