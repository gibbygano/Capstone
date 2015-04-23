using System;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal static class ControlHelper
    {
        /// <summary>
        /// Attempts to get the parent of the type specified in the Generic Parameter <typeparam name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="control"></param>
        /// <returns></returns>
        /// <exception cref="WanderingTurtleException">Error getting specified parent.</exception>
        internal static T GetParent<T>(this FrameworkElement control) where T : class
        {
            try
            {
                var parent = control.Parent;
                if (parent == null)
                { parent = control; }
                else
                {
                    while (!(parent is T))
                    {
                        if (parent != null)
                        { parent = control.Parent; }
                        else
                        { break; }
                    }
                }
                return (parent as T);
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error Getting Parent Window"); }
        }
    }
}
