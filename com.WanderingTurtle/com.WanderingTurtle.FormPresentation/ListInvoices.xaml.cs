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
using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Pat Banks
    /// Created:  2015/02/24
    /// Interaction logic for ListInvoices.xaml
    /// </summary>
    public partial class ListInvoices : UserControl
    {
        private InvoiceManager myManager = new InvoiceManager();
        List<InvoiceDetails> invoiceList;

        public ListInvoices()
        {
            InitializeComponent();
            RefreshInvoiceList();
        }
        /// <summary>
        /// Pat Banks
        /// Created on:  2015/02/25
        /// Retrieves the list of invoices from the database
        /// </summary>
        /// <remarks>
        /// </remarks>
        private void RefreshInvoiceList()
        {
            try
            {
                invoiceList = myManager.RetrieveInvoiceList();
                lvInvoiceList.ItemsSource = invoiceList;
                lvInvoiceList.Items.Refresh();
                
            }
            catch (Exception ex)
            {
                DialogBox.ShowMessageDialog(this, ex.Message, "Unable to retrieve invoice list from the database.");
                
            }
        }

        /// <summary>
        /// Pat Banks
        /// Created:  2015/02/25
        /// Displays the invoice UI for a selected hotel guest
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnViewInvoice_Click(object sender, RoutedEventArgs e)
        {
            //try
            //{
            //    InvoiceDetails invoiceToView = (InvoiceDetails)lvInvoiceList.SelectedItem;

            //    ViewInvoice custInvoice = new ViewInvoice(invoiceToView);

            //    if (custInvoice.ShowDialog() == false)
            //    {
            //        RefreshInvoiceList();
            //    }

            //}
            //catch (Exception)
            //{
            //    DialogBox.ShowMessageDialog("No Invoice selected, please select an Invoice and try again");
            //}
        }
    }
}
