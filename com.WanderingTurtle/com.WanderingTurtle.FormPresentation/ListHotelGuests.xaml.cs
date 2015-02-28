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
using com.WanderingTurtle.Common;

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRegisterGuest_Click(object sender, RoutedEventArgs e)
        {
            AddEditHotelGuest AddEditHotelGuest = new AddEditHotelGuest();
            if (AddEditHotelGuest.ShowDialog() == false)
            {
                if (AddEditHotelGuest.result)
                {
                    refreshList();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCheckOutGuest_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int invoiceToView = lvHotelGuestList.SelectedIndex;

                ViewInvoice custInvoice = new ViewInvoice(invoiceToView);

                if (custInvoice.ShowDialog() == false)
                {
                   // RefreshInvoiceList();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("No Invoice selected, please select an Invoice and try again");
            }
        }

        /// <summary>
        /// Brings up AddEditHotelGuest to edit chosen guest.
        /// Created By Rose Steffensmeier 2015/02/26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ApplicationException">
        /// When a guest isn't chosen, the exception will throw.
        /// </exception>
        /// <exception cref="Exception">
        /// Unexpected Exception is thrown.
        /// </exception>
        /// <returns>nothing</returns>
        private void btnUpdateGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HotelGuest thisGuest = (HotelGuest)lvHotelGuestList.SelectedItem;

                if (thisGuest == null)
                    throw new ApplicationException("You must choose a guest.");

                AddEditHotelGuest temp = new AddEditHotelGuest(thisGuest);
                temp.Show();
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Archives a guest, then refreshes the list.
        /// Created by Rose Steffensmeier 2015/02/26
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="ApplicationException">
        /// When a guest has not been chosen.
        /// </exception>
        private void btnArchiveGuest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                HotelGuest thisGuest = (HotelGuest)lvHotelGuestList.SelectedItem;

                if (thisGuest == null)
                    throw new ApplicationException("You must choose a guest.");

                _HotelGuestManager.ArchiveHotelGuest(thisGuest, !thisGuest.Active);
                refreshList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            refreshList();
        }
    }
}
