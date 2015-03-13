using MahApps.Metro.Controls;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal static class WindowHelper
    {
        public static MainWindow GetMainWindow(Control control)
        {
            var parent = VisualTreeHelper.GetParent(control);
            while (!(parent is MainWindow))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return (parent as MainWindow);
        }

        public static MetroWindow GetWindow(Control control)
        {
            var parent = VisualTreeHelper.GetParent(control);
            while (!(parent is MetroWindow))
            {
                if (parent != null)
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
            }
            return (parent as MetroWindow);
        }
    }
}