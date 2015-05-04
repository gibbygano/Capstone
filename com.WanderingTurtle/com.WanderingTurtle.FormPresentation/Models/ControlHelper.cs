using System;
using System.Windows;
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
        /// The control that you wish to find main window of. In most cases you will use 'this'
        /// </param>
        /// <returns>specified parent</returns>
        /// <exception cref="WanderingTurtleException" />
        internal static T GetVisualParent<T>(this FrameworkElement control) where T : class
        {
            try
            {
                while (!(control is T))
                {
                    if (control == null) throw new ApplicationException("Control is null");
                    control = VisualTreeHelper.GetParent(control) as FrameworkElement;
                }
                return control as T;
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error getting parent component " + typeof(T)); }
        }
    }
}