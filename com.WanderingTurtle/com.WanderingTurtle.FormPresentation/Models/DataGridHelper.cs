using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace com.WanderingTurtle.FormPresentation.Models
{
    /// <summary>
    /// Predefined values for context menu
    /// </summary>
    internal enum DataGridContextMenuResult
    {
        /// <summary>
        /// DataGridContextMenu Add
        /// </summary>
        Add,

        /// <summary>
        /// DataGridContextMenu View
        /// </summary>
        View,

        /// <summary>
        /// DataGridContextMenu Edit
        /// </summary>
        Edit,

        /// <summary>
        /// DataGridContextMenu Delete
        /// </summary>
        Delete
    }

    /// <summary>
    /// All classes that wish to use the context menu must implement this interface
    /// </summary>
    internal interface IDataGridContextMenu
    {
        /// <summary>
        /// Click event handler for ContextMenus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ContextMenuItemClick(object sender, RoutedEventArgs e);
    }

    /// <summary>
    /// Class containing any tools or extension methods to be used by WPF DataGrids
    /// </summary>
    internal static class DataGridHelper
    {
        /// <summary>
        /// Gets the selected item from the data grid context menu
        /// </summary>
        /// <typeparam name="T">The type of object to cast the selected item into</typeparam>
        /// <param name="sender"></param>
        /// <param name="command"></param>
        /// <returns>Generic Type of <typeparamref name="T" /></returns>
        /// <exception cref="WanderingTurtleException">Error getting specified parent.</exception>
        public static T ContextMenuClick<T>(this object sender, out DataGridContextMenuResult command)
        {
            var menuItem = sender as MenuItem;
            Debug.Assert(menuItem != null, "menuItem != null");
            Debug.Assert(menuItem.CommandParameter != null, "menuItem.CommandParameter != null");
            Debug.Assert(menuItem.Parent != null, "menuItem.Parent != null");
            command = (DataGridContextMenuResult)menuItem.CommandParameter;
            return (T)((DataGrid)menuItem.GetParent<ContextMenu>().PlacementTarget).SelectedItem;
        }

        /// <summary>
        /// Sets the context menu on the specified component
        /// </summary>
        /// <param name="component">the component on which to ad the context menu</param>
        /// <param name="context">the class that implements <see cref="IDataGridContextMenu" /></param>
        /// <param name="contextMenus"></param>
        /// <returns><see cref="FrameworkElement" /></returns>
        /// <exception cref="ArgumentNullException">
        /// <see cref="DataGridContextMenuResult" /> is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// <see cref="DataGridContextMenuResult" /> is not an <see cref="T:System.Enum" />.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The item to add already has a different logical parent.
        /// </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        /// <exception cref="WanderingTurtleException" />
        public static FrameworkElement SetContextMenu(this FrameworkElement component, IDataGridContextMenu context, params DataGridContextMenuResult[] contextMenus)
        {
            try
            {
                var contextMenu = new ContextMenu();
                foreach (
                    var menuItem in
                        (contextMenus ??
                         (DataGridContextMenuResult[])Enum.GetValues(typeof(DataGridContextMenuResult)))
                            .Select(menu => new MenuItem
                            {
                                Header = Enum.GetName(typeof(DataGridContextMenuResult), menu),
                                CommandParameter = menu
                            }))
                {
                    menuItem.Click += context.ContextMenuItemClick;
                    var result =
                        (DataGridContextMenuResult)
                            Enum.Parse(typeof(DataGridContextMenuResult), menuItem.Header.ToString());
                    var dataGrid = component as DataGrid;
                    if (dataGrid != null && result != DataGridContextMenuResult.Add)
                    {
                        menuItem.SetBinding(
                            UIElement.IsEnabledProperty,
                            new Binding("Count") { Source = dataGrid.SelectedItems });
                    }
                    contextMenu.Items.Add(menuItem);
                }
                component.ContextMenu = contextMenu;
                return component;
            }
            catch (OverflowException ex)
            {
                throw new WanderingTurtleException(component, ex, "Error occurred setting context menu.");
            }
        }

        /// <summary>
        /// Gets the selected DataGrid row and returns it as the specified object <typeparam name="T" />
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sender"></param>
        /// <returns>Generic Type of <typeparamref name="T" /></returns>
        /// <exception cref="WanderingTurtleException">
        /// An error occurred while trying to retrieve the object stored inside the DataGrid Row.
        /// </exception>
        internal static T RowClick<T>(this object sender)
        {
            try
            {
                var row = sender as DataGridRow;
                if (row != null && row.Item != null)
                { return (T)row.Item; }
                throw new ApplicationException("Error Getting Selected DataGrid Row.");
            }
            catch (ApplicationException ex)
            { throw new WanderingTurtleException(sender as FrameworkElement, ex); }
        }
    }
}