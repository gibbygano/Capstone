using MahApps.Metro;
using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace com.WanderingTurtle.FormPresentation.Models
{
    /// <summary>
    /// The window helper.
    /// </summary>
    internal static class WindowHelper
    {
        private static readonly AppTheme DefaultTheme = ThemeManager.GetAppTheme("BaseLight");

        /// <summary>
        /// Sets the style of the specified <see cref="Window" /><paramref name="window" />
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/03</remarks>
        /// <param name="window"></param>
        /// <param name="accentColor"></param>
        /// <param name="themeColor"></param>
        public static void ChangeAppStyle(this Window window, Accent accentColor, AppTheme themeColor = null)
        {
            ThemeManager.ChangeAppStyle(window, accentColor, themeColor ?? DefaultTheme);
        }

        /// <summary>
        /// Sets the style of the specified <see cref="Application" /><paramref name="application" />
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/03</remarks>
        /// <param name="application"></param>
        /// <param name="accentColor"></param>
        /// <param name="themeColor"></param>
        public static void ChangeAppStyle(this Application application, Accent accentColor, AppTheme themeColor = null)
        {
            ThemeManager.ChangeAppStyle(application, accentColor, themeColor ?? DefaultTheme);
        }

        /// <summary>
        /// Sets the style of the specified <see cref="Application" /><paramref name="application" />
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/03</remarks>
        /// <param name="application"></param>
        /// <param name="theme"></param>
        public static void ChangeAppStyle(this Application application, Tuple<AppTheme, Accent> theme)
        {
            ChangeAppStyle(application, theme.Item2, theme.Item1);
        }

        /// <summary>
        /// Returns the base parent <see cref="MainWindow" />
        /// </summary>
        /// <remarks>Miguel Santana 2015/03/10</remarks>
        /// <param name="control">
        /// The control that you wish to find main window of. In most cases you will use 'this'
        /// </param>
        /// <returns>Base Parent <see cref="MainWindow" /></returns>
        /// <exception cref="WanderingTurtleException" />
        internal static T GetWindow<T>(this FrameworkElement control) where T : class
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

        /// <summary>
        /// Takes the child components of <paramref name="content" /> and disables them.
        /// </summary>
        /// <remarks>Miguel Santana 2015/06/04</remarks>
        /// <param name="content">The parent container</param>
        /// <param name="controlsToKeepEnabled">Controls that you want to keep enabled</param>
        /// <exception cref="WanderingTurtleException">Error setting the fields as read only.</exception>
        internal static void MakeReadOnly(Panel content, params FrameworkElement[] controlsToKeepEnabled)
        {
            try
            {
                // Iterates loop if child is not part of the controlsToKeepEnabled array Does not
                // bother marking Labels as ReadOnly
                foreach (var child in content.Children.Cast<FrameworkElement>().Where(child => (controlsToKeepEnabled == null || !controlsToKeepEnabled.Contains(child)) && !(child is Label)))
                {
                    // If child component is a container, then call the recursive method to get
                    // inner child components
                    if (child is Panel) { MakeReadOnly(child as Panel, controlsToKeepEnabled); continue; }

                    // If the child is a valid control
                    if (child is Control)
                    {
                        var childControl = child as Control;

                        if (childControl is TextBoxBase) { (childControl as TextBoxBase).IsReadOnly = true; }
                        else if (childControl is NumericUpDown)
                        {
                            (childControl as NumericUpDown).HideUpDownButtons = true;
                            (childControl as NumericUpDown).IsReadOnly = true;
                        }
                        else { childControl.IsEnabled = false; }

                        ComboBoxHelper.SetEnableVirtualizationWithGrouping(childControl, false);
                        ControlsHelper.SetMouseOverBorderBrush(childControl, childControl.BorderBrush);
                        ControlsHelper.SetFocusBorderBrush(childControl, childControl.BorderBrush);
                        TextBoxHelper.SetClearTextButton(childControl, false);
                    }

                    // Don't know why this would throw, but it's here just in case
                    else { throw new ApplicationException("Unknown Component"); }
                }
            }
            catch (Exception ex) { throw new WanderingTurtleException(content, ex, "Error Setting Fields to ReadOnly for" + Environment.NewLine + content); }
        }
    }
}