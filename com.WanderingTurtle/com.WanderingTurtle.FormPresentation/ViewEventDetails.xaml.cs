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
    /// Interaction logic for ViewEventDetails.xaml
    /// </summary>
    public partial class ViewEventDetails : Window
    {
        public ViewEventDetails(Event eventToView)
        {
            InitializeComponent();
            lblType.Content = "Event Type: " + eventToView.EventTypeID;
            txtDescrip.Text = eventToView.Description;
            lblName.Content = "Event Name: " + eventToView.EventItemName;
            lblOnSite.Content = "On-site: " + eventToView.OnSite;
            lblTransport.Content = "Transportation: " + eventToView.Transportation;

        }
    }
}
