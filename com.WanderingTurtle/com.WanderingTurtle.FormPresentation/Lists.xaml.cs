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
using com.WanderingTurtle.Common;
using com.WanderingTurtle.BusinessLogic;

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

        //sets instance to this object
        //created by will fritz 2/9/15
        public ListsView()
        {
            InitializeComponent();
            Instance = this;
        }

        //when form loads it calls 
        //created by will fritz 2/9/15
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            FillList();
        }

        //opens the add/edit window and calls isAdd
        //created by will fritz 2/9/15
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

        //opens the add/edit window and calls isEdit and checks if there is a slected item
        //created by will fritz 2/9/15
        private void btnEditLists_Click(object sender, RoutedEventArgs e)
        {
            if (lvList.SelectedItems[0] == null)
            {
                lblError.Content = "You Must First Select An Item From the List, Before You Can Edit it";    
            }
            else
            {
                ObservableCollection oldLists = (ObservableCollection)lvList.SelectedItems[0];
   
                if (ListsPopUp.Instance == null)
                {
                    var popup = new ListsPopUp();
                    popup.Show();
                    popup.isEdit(oldLists.lists);
                }
                else
                {
                    var popup = ListsPopUp.Instance;
                    popup.BringIntoView();
                    popup.isEdit(oldLists.lists);
                    popup.Show();

                    System.Media.SystemSounds.Asterisk.Play();
                }
            }
        }

        //test if an item is selected, if it is it will ask you if want to delete the item. if not it will put out an error message
        //created by will fritz 2/9/15
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
                    try
                    {
                        Lists listsToDelete = (Lists)lvList.SelectedItems[0];
                        _prodMan.ArchiveLists(listsToDelete);
                        FillList();
                    }
                    catch (Exception)
                    {
                        lblError.Content = "There was an Error deleting the Lists item";
                    }
                }
            }
        }

        //sets instance to null apon closing
        //created by will fritz 2/9/15
        private void Window_Closed(object sender, EventArgs e)
        {
            Instance = null;
        }



        //////////////////Custom Methods/////////////////////
        //this class will find the correct EventName and SupplierName fields to match their ID's and will fill the list view with the data
        //created by will fritz 2/9/15
        //edited by will fritz 2/15/15
        public void FillList()
        {
            try
            {
                _listslist = _prodMan.RetrieveListsList();
                List<Supplier> suppliers = new List<Supplier>();
                List<ItemListing> itemlisting = new List<ItemListing>();
                List<Event> events = new List<Event>();
                List<ObservableCollection> observationList = new List<ObservableCollection>();

                //creates observation List
                for (int x = 0; x < _listslist.Count; x++)
                {
                    ObservableCollection temp = new ObservableCollection();
                    temp.lists = _listslist[x];

                    //finds the supplier name that matches the current lists object
                    foreach (Supplier sup in suppliers)
                    {
                        if (_listslist[x].SupplierID == sup.SupplierID)
                        {
                            temp.SupplierName = sup.CompanyName;
                        }
                    }//end supplier for

                    //finds the event name for the current item listing ID in the current lists object
                    foreach (ItemListing itme in itemlisting)
                    {
                        if (_listslist[x].ItemListID == itme.ItemListID)
                        {
                            foreach (Event eventTemp in events)
                            {
                                if (itme.EventID == eventTemp.EventItemID)
                                {
                                    temp.EventName = eventTemp.EventItemName;
                                }
                            }
                        }
                    }//end event for

                    observationList.Add(temp);
                } //end main for

                lvList.Items.Clear();
                lvList.ItemsSource = observationList;
            }
            catch (Exception)
            {
                lblError.Content = "There was a problem while filling the lists";
            }          
        } 
    }

    //this class is a derived class of Lists which allows the list view to use supplierName, EventName rather than just ID's (because of data binding)
    //created by will fritz 2/9/15
    public class ObservableCollection : Lists
    {
        public string SupplierName { get; set; }
        public string EventName { get; set; }
        public Lists lists { get; set; }
    }
}
