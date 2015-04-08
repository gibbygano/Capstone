using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal class DataGridHelper
    {
        public static T DataGridRow_Click<T>(object sender, MouseButtonEventArgs e)
        {
            var result = default(T);
            IInputElement element = e.MouseDevice.DirectlyOver;
            if (element != null && element is FrameworkElement)
            {
                if (((FrameworkElement)element).Parent is DataGridCell)
                {
                    var dataGrid = sender as DataGrid;
                    if (dataGrid != null && dataGrid.SelectedItems != null && dataGrid.SelectedItems.Count == 1)
                    { result = (T)dataGrid.SelectedItem; }
                }
            }
            return result;
        }
    }
}