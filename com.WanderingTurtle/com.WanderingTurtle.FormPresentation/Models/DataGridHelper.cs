using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal static class DataGridHelper
    {
        /// <exception cref="WanderingTurtleException">An error occoured while trying to retrieve the object stored inside the DataGrid Row.</exception>
        public static T DataGridRow_Click<T>(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var row = sender as DataGridRow;
                if (row != null && row.Item != null)
                {
                    return (T)row.Item;
                }
                throw new ApplicationException("Error Getting Selected DataGrid Row.");
            }
            catch (Exception ex)
            { throw new WanderingTurtleException(sender as FrameworkElement, ex); }
        }
    }
}