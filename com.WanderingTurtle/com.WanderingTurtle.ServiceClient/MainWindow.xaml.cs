using com.WanderingTurtle.Common;
using com.WanderingTurtle.ServiceClient.WanderingTurtleService;
using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace com.WanderingTurtle.ServiceClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public AccountingDetails AccountingDetails { get; set; }

        public AccountingDetails GetAccountingDetails()
        {
            try
            {
                var client = new AccountingDetailsServiceClient();
                var data = client.GetAccountingDetails(StartDate.DisplayDate, EndDate.DisplayDate);
                client.Close();
                return data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, ex.Message, "Error getting data");
            }
            return null;
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (StartDate.SelectedDate.Equals(null))
            {
                MessageBox.Show(this, "Please select a Start Date", "Select Date");
                StartDate.Focus();
                StartDate.IsDropDownOpen = true;
                return;
            }
            if (EndDate.SelectedDate.Equals(null))
            {
                MessageBox.Show(this, "Please select an End Date", "Select Date");
                EndDate.Focus();
                EndDate.IsDropDownOpen = true;
                return;
            }
            ReloadList();
        }

        private void ReloadList()
        {
            AccountingDetails = GetAccountingDetails();
            dataInvoices = TreeViewBuilder(AccountingDetails.Invoices);
            dataListings.ItemsSource = TreeViewBuilder(AccountingDetails.SupplierListings).Items;
        }

        private TreeView TreeViewBuilder(IEnumerable items)
        {
            var tree = new TreeView();
            foreach (var item in items)
            {
                if (item is IEnumerable)
                {
                    tree.Items.Add(TreeViewBuilder(item as IEnumerable));
                }
                tree.Items.Add(item);
            }
            return tree;
        }
    }
}