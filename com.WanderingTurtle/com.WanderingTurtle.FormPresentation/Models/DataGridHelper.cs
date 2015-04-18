using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal enum DataGridContextMenuResult
    {
        Add = 0,
        View,
        Edit,
        Archive
    }

    internal interface IDataGridContextMenu
    {
        void ContextMenuItem_Click(object sender, RoutedEventArgs e);
    }

    internal static class DataGridHelper
    {
        /// <exception cref="WanderingTurtleException">Error getting specified parent.</exception>
        public static T ContextMenuClick<T>(object sender, out DataGridContextMenuResult command)
        {
            var menuItem = sender as MenuItem;
            Debug.Assert(menuItem != null, "menuItem != null");
            Debug.Assert(menuItem.CommandParameter != null, "menuItem.CommandParameter != null");
            Debug.Assert(menuItem.Parent != null, "menuItem.Parent != null");
            command = ((DataGridContextMenuResult)menuItem.CommandParameter);
            return (T)((DataGridRow)menuItem.GetParent<ContextMenu>().PlacementTarget).Item;
        }

        /// <exception cref="ArgumentNullException"><paramref name="(DataGridContextMenuResult)" /> is null. </exception>
        /// <exception cref="ArgumentException"><paramref name="(DataGridContextMenuResult)" /> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        public static DataGrid SetContextMenu(this DataGrid component, IDataGridContextMenu context, DataGridContextMenuResult[] contextMenus = null)
        {
            var menus = contextMenus ?? Enum.GetValues(typeof(DataGridContextMenuResult)) as DataGridContextMenuResult[];
            Debug.Assert(menus != null, "menus != null");
            var contextMenu = new ContextMenu();
            foreach (var menuItem in menus.Select(menu => new MenuItem { Header = Enum.GetName(typeof(DataGridContextMenuResult), menu), CommandParameter = menu }))
            {
                menuItem.Click += context.ContextMenuItem_Click;
                contextMenu.Items.Add(menuItem);
            }
            component.RowStyle.Setters.Add(new Setter(FrameworkElement.ContextMenuProperty, contextMenu));
            return component;
        }

        /// <exception cref="WanderingTurtleException">An error occoured while trying to retrieve the object stored inside the DataGrid Row.</exception>
        internal static T RowClick<T>(object sender)
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
            catch (ApplicationException ex)
            { throw new WanderingTurtleException(sender as FrameworkElement, ex); }
        }
    }
}