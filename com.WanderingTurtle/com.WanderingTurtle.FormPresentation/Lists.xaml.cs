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
using System.Windows.Shapes;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for Lists.xaml
    /// </summary>
    public partial class ListsView : Window
    {
        private ProductManager _prodMan = new ProductManager();
        private List<Lists> _listslist;
        public static ListsView Instance;

        public ListsView()
        {
            InitializeComponent();
            Instance = this;
        }

        //when form loads it calls 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        //opens the add/edit window
        private void btnAddtoLists_Click(object sender, RoutedEventArgs e)
        {
            if (ListsPopUp.Instance == null)
            {
                var popup = new ListsPopUp();
                popup.Show();
                popup.isAdd();
            }
            else
            {
                var popup = ListsPopUp.Instance;
                popup.BringIntoView();
                popup.isAdd();
                popup.Show();

                System.Media.SystemSounds.Asterisk.Play();
            }
        }

        //opens the add/edit window
        private void btnEditLists_Click(object sender, RoutedEventArgs e)
        {
            if (lvList.SelectedItems[0] == null)
            {
                lblError.Content = "You Must First Select An Item From the List, Before You Can Edit it";    
            }
            else
            {
               Lists oldLists = (Lists)lvList.SelectedItems[0];
   
                if (ListsPopUp.Instance == null)
                {
                    var popup = new ListsPopUp();
                    popup.Show();
                    popup.isEdit(oldLists);
                }
                else
                {
                    var popup = ListsPopUp.Instance;
                    popup.BringIntoView();
                    popup.isEdit(oldLists);
                    popup.Show();

                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
        }

        //test if an item is selected, if it is it will ask you if want to delete the item. if not it will put out an error message
        private void btnRemoveFromLists_Click(object sender, RoutedEventArgs e)
        {
            if (lvList.SelectedItems[0] == null)
            {
                lblError.Content = "You Must First Select An Item From the List, Before You Can Remove It";   
            }
            else
            {
                if(MessageBox.Show("Do you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    Lists listsToDelete = (Lists)lvList.SelectedItems[0];
                    _prodMan.ArchiveLists(listsToDelete);
                    FillList();
                }
            }
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }



        //////////////////Custom Methods/////////////////////
        //this class will find the correct EventName and SupplierName fields to match their ID's and will fill the list view with the data
        //
        public void FillList()
        {
            _listslist = _prodMan.RetriveListsList();
            List<Supplier> suppliers = new List<Supplier>();
            List<ItemListing> itemlisting = new List<ItemListing>();
            List<Event> events = new List<Event>();
            List<ObservableCollection> observationList = new List<ObservableCollection>();

            //creates observation List
            for (int x = 0; x < _listslist.Count; x++)
            {
                ObservableCollection temp = new ObservableCollection();
                temp.lists = _listslist[x];

                foreach (Supplier sup in suppliers)
                {
                    if (sup.SupplieID == _listslist.SupplierID)
                    {
                        temp.SupplierName = sup.SupplierName;
                    }
                }//end supplier for

                foreach (ItemListing itme in itemlisting)
                {
                    
                    foreach(Event _event in events)
                    {
                        if (itme.EventID == _event.EventID)
                        {
                            temp.EventName = _event.EventName;
                        }
                    }
                }//end event for

                observationList.Add(temp);
            } //end main for

            lvList.Items.Clear();
            lvList.ItemsSource = observationList;
        }
       
    }

    //this class is a derived class of Lists which allows the list view to use supplierName, EventName rather than just ID's (because of data binding)
    public class ObservableCollection : Lists
    {
        public string SupplierName { get; set; }
        public string EventName { get; set; }
        public Lists lists { get; set; }
    }
}
