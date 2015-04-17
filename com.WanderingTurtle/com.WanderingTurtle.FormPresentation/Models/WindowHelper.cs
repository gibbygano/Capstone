using MahApps.Metro.Controls;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace com.WanderingTurtle.FormPresentation.Models
{
    internal static class WindowHelper
    {
        /// <summary>
        /// Returns the base parent MainWinow
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/03/10
        /// </remarks>
        /// <param name="control">The control that you wish to find main window of. In most cases you will use 'this'</param>
        /// <returns>Base Parent MainWinow</returns>
        /// <exception cref="WanderingTurtleException"/>
        internal static MainWindow GetMainWindow(FrameworkElement control)
        {
            try
            {
                var parent = VisualTreeHelper.GetParent(control) ?? control;
                while (!(parent is MainWindow))
                {
                    parent = VisualTreeHelper.GetParent(parent);
                }
                return (parent as MainWindow);
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error Getting Main Window"); }
        }

        /// <summary>
        /// Returns the parent MetroWindow of any child control
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/03/10
        /// </remarks>
        /// <param name="control">The control that you wish to find the parent of. In most cases you will use 'this'</param>
        /// <returns>Parent MetroWindow</returns>
        /// <exception cref="WanderingTurtleException"/>
        internal static MetroWindow GetWindow(FrameworkElement control)
        {
            try
            {
                var parent = VisualTreeHelper.GetParent(control);
                if (parent == null)
                { parent = control; }
                else
                {
                    while (!(parent is MetroWindow))
                    { if (parent != null) { parent = VisualTreeHelper.GetParent(parent); } }
                }
                return (parent as MetroWindow);
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error Getting Parent Window"); }
        }

        /// <summary>
        /// Takes the child components of <paramref name="content"/> and disables them.
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/06/04
        /// </remarks>
        /// <param name="content">The parent container</param>
        /// <param name="controlsToKeepEnabled">Controls that you want to keep enabled</param>
        /// <exception cref="WanderingTurtleException">Condition.</exception>
        internal static void MakeReadOnly(Panel content, FrameworkElement[] controlsToKeepEnabled = null)
        {
            try
            {
                foreach (FrameworkElement child in content.Children)
                {
                    // Return if this child control is set in controlsToKeepEnabled
                    if (controlsToKeepEnabled != null && controlsToKeepEnabled.Contains(child)) { continue; }

                    // If child component is a container, then call the recursive method to get inner child components
                    if (child is Panel) { MakeReadOnly(child as Panel, controlsToKeepEnabled); }
                    // Does not bother marking Labels as ReadOnly
                    else if (child is Label) { continue; }
                    // If the child is a valid control
                    else if (child is Control)
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
                        //SetStyle(childControl, new Setter[] { new Setter(TextBoxHelper.ClearTextButtonProperty, false) });
                    }
                    // Don't know why this would throw, but it's here just in case
                    else { throw new ApplicationException("Unknown Component"); }
                }
            }
            catch (Exception ex) { throw new WanderingTurtleException(content, ex, "Error Setting Fields to ReadOnly for" + Environment.NewLine + content); }
        }
    }
}