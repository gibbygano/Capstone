using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.WanderingTurtle.FormPresentation.Models
{
    /// <summary>
    /// The control helper.
    /// </summary>
    internal static class ControlHelper
    {
        /// <summary>
        /// Returns the specified parent
        /// </summary>
        /// <remarks>Miguel Santana 2015/03/10</remarks>
        /// <typeparam name="T">Specified parent type</typeparam>
        /// <param name="control">
        /// The control that you wish to find the parent of. In most cases you will use 'this'
        /// </param>
        /// <returns>specified parent</returns>
        /// <exception cref="WanderingTurtleException" />
        internal static T GetParent<T>(this FrameworkElement control) where T : class
        {
            try
            {
                while (!(control is T))
                {
                    control = (FrameworkElement)control.Parent;
                }
                return control as T;
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error getting parent component " + typeof(T)); }
        }

        /// <summary>
        /// Returns the specified visual parent
        /// </summary>
        /// <remarks>Miguel Santana 2015/03/10</remarks>
        /// <typeparam name="T">Specified parent type</typeparam>
        /// <param name="control">
        /// The control that you wish to find visual parent of. In most cases you will use 'this'
        /// </param>
        /// <returns>specified visual parent</returns>
        /// <exception cref="WanderingTurtleException" />
        internal static T GetVisualParent<T>(this FrameworkElement control) where T : class
        {
            try
            {
                var parent = VisualTreeHelper.GetParent(control) ?? control;
                while (!(parent is T))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }

                return parent as T;
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error Getting Main Window"); }
        }
    }
}