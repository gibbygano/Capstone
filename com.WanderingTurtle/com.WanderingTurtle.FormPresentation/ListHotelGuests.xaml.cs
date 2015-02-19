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
    /// Interaction logic for ListHotelGuest.xaml
    /// </summary>
    public partial class ListHotelGuests : UserControl
    {
        HotelGuestManager _HotelGuestManager = new HotelGuestManager();

        public ListHotelGuests()
        {
            InitializeComponent();
            refreshList();
        }


        /// <summary>
        /// Repopulates the list of hotel guests to display
        /// 2015-02-18 - Daniel Collingwood 
        /// </summary>
        private void refreshList()
        {
            try
            {
                var hotelGuestList =_HotelGuestManager.GetHotelGuestList();
                lvHotelGuestList.ItemsSource = hotelGuestList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to retrieve Hotel Guest listing from the database. \n" + ex.Message);
            }
        }


        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            Window AddEditHotelGuest = new AddEditHotelGuest();
            AddEditHotelGuest.Show();
        }



    }
}
