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

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for ViewListing.xaml
    /// </summary>
    public partial class ViewListing : Window
    {
        public ViewListing(ItemListing toView)
        {
            InitializeComponent();

            txtEventType.Text = toView.EventName;

            txtCurrentGuests.Text = toView.CurrentNumGuests.ToString();
            txtMaxGuests.Text = toView.MaxNumGuests.ToString();
            txtMinGuests.Text = toView.MinNumGuests.ToString();
            txtPrice.Text = toView.Price.ToString();
            txtStartDate.Text = toView.StartDate.ToString();
            txtEndDate.Text = toView.EndDate.ToString();
        }
    }
}
