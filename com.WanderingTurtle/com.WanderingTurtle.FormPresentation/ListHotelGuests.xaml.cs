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
        public ListHotelGuests()
        {
            InitializeComponent();
        }


        private void btnAddGuest_Click(object sender, RoutedEventArgs e)
        {
            Window AddEditHotelGuest = new AddEditHotelGuest();
            AddEditHotelGuest.Show();
        }



    }
}
