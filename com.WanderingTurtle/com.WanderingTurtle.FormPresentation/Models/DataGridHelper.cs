using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal class DataGridHelper
    {
        public static T DataGridRow_Click<T>(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (sender != null)
                {
                    var row = sender as DataGridRow;
                    if (row != null && row.Item != null)
                    {
                        return (T)row.Item;
                    }
                }
                throw new ApplicationException("Error Getting Selected DataGrid Row.");
            }
            catch (Exception ex)
            { throw new WanderingTurtleException(sender is FrameworkElement ? sender as FrameworkElement : null, ex); }
        }
    }
}