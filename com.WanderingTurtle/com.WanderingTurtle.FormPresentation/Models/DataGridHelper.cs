using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal enum DataGridContextMenuResult
    {
        Add,
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
            return (T)((DataGrid)menuItem.GetParent<ContextMenu>().PlacementTarget).SelectedItem;
        }

        /// <exception cref="ArgumentNullException"><paramref name="(DataGridContextMenuResult)" /> is null. </exception>
        /// <exception cref="ArgumentException"><paramref name="(DataGridContextMenuResult)" /> is not an <see cref="T:System.Enum" />. </exception>
        /// <exception cref="InvalidOperationException">The item to add already has a different logical parent. </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="OverflowException"><paramref name="(menuItem.Header)" /> is outside the range of the underlying type of <paramref name="(DataGridContextMenuResult)" />.</exception>
        public static FrameworkElement SetContextMenu(this FrameworkElement component, IDataGridContextMenu context, DataGridContextMenuResult[] contextMenus = null)
        {
            var contextMenu = new ContextMenu();
            foreach (
                var menuItem in
                    (contextMenus ?? (DataGridContextMenuResult[])Enum.GetValues(typeof(DataGridContextMenuResult)))
                        .Select(menu => new MenuItem
                        {
                            Header = Enum.GetName(typeof(DataGridContextMenuResult), menu),
                            CommandParameter = menu
                        }))
            {
                menuItem.Click += context.ContextMenuItem_Click;
                var result = ((DataGridContextMenuResult)(Enum.Parse(typeof(DataGridContextMenuResult), menuItem.Header.ToString())));
                var dataGrid = component as DataGrid;
                if (dataGrid != null
                    && result != DataGridContextMenuResult.Add)
                {
                    menuItem.SetBinding(UIElement.IsEnabledProperty, new Binding
                    {
                        Source = dataGrid.SelectedItems,
                        Path = new PropertyPath("Count"),
                    });
                }
                contextMenu.Items.Add(menuItem);
            }
            component.ContextMenu = contextMenu;
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