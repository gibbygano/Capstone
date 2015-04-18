using System;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal static class ControlHelper
    {
        /// <exception cref="WanderingTurtleException">Error getting specified parent.</exception>
        internal static T GetParent<T>(this Control control) where T : class
        {
            try
            {
                var parent = control.Parent;
                if (parent == null)
                { parent = control; }
                else
                {
                    while (!(parent is T))
                    { if (parent != null) { parent = control.Parent; } }
                }
                return (parent as T);
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error Getting Parent Window"); }
        }
    }
}