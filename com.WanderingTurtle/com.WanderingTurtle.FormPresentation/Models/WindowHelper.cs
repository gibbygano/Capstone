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
        /// Returns the base parent <typeparamref name="MainWindow"/>
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/03/10
        /// </remarks>
        /// <param name="control">The control that you wish to find main window of. In most cases you will use <typeparamref name="this"/></param>
        /// <returns>Base Parent <typeparamref name="MainWindow"/></returns>
        internal static MainWindow GetMainWindow(DependencyObject control)
        {
            var parent = VisualTreeHelper.GetParent(control) ?? control;
            while (!(parent is MainWindow))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }
            return (parent as MainWindow);
        }

        /// <summary>
        /// Returns the parent <typeparamref name="MetroWindow"/> of any child control
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/03/10
        /// </remarks>
        /// <param name="control">The control that you wish to find the parent of. In most cases you will use <typeparamref name="this"/></param>
        /// <returns>Parent <typeparamref name="MetroWindow"/></returns>
        internal static MetroWindow GetWindow(DependencyObject control)
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

        /// <summary>
        /// Takes the child components of <paramref name="content"/> and disables them.
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/06/04
        /// </remarks>
        /// <param name="content">The parent container</param>
        /// <param name="controlsToKeepEnabled">Controls that you want to keep enabled</param>
        internal static async void MakeReadOnly(Panel content, FrameworkElement[] controlsToKeepEnabled = null)
        {
            Exception _ex = null;
            try
            {
                foreach (FrameworkElement child in content.Children)
                {
                    // Return if this child control is set in controlsToKeepEnabled
                    if (controlsToKeepEnabled != null && controlsToKeepEnabled.Contains(child))
                    { return; }

                    // If child component is a container, then call the recursive method to get inner child components
                    if (child is Panel)
                    { MakeReadOnly(child as Panel, controlsToKeepEnabled); }
                    else
                    {
                        // Breaks out of this iteration of the loop if the child component is not focusable, such as a Label
                        if (!child.Focusable) continue;

                        if (child is TextBoxBase) { (child as TextBoxBase).IsReadOnly = true; }
                        else { child.IsEnabled = false; }

                        SetStyle(child, new Setter[] { new Setter(TextBoxHelper.ClearTextButtonProperty, false) });
                    }
                }
            }
            catch (Exception ex) { _ex = new Exception("Error Setting Fields to ReadOnly for" + Environment.NewLine + content, ex); }
            if (_ex != null) { await DialogBox.ShowMessageDialog(content, _ex.InnerException.Message, _ex.Message); _ex = null; }
        }

        /// <summary>
        /// Sets the style of <paramref name="control"/> to <paramref name="setterValue"/>
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/04/06
        /// </remarks>
        /// <param name="control">The control to set the style of</param>
        /// <param name="setterValue">The Setter that you want to set the <paramref name="control"/> to</param>
        /// <param name="replace">Setting this will only set <paramref name="setterValue"/> if the property already exists on the <paramref name="control"/></param>
        /// TODO Anonymous Types
        internal static void SetStyle(FrameworkElement control, Setter[] setterValueArray, bool replace = false)
        {
            try
            {
                Style newStyle = new Style(control.GetType(), GetStyle(control.Style));
                foreach (var setterValue in setterValueArray)
                {
                    try
                    {
                        if (!replace)
                        { newStyle.Setters.Add(setterValue); }
                        else
                        {
                            SetterBaseCollection setterList = new SetterBaseCollection();
                            Style tmpStyle = control.Style;
                            do
                            {
                                foreach (var setter in tmpStyle.Setters.Cast<Setter>().Where(setter => !setterList.Contains(setter)))
                                { setterList.Add(setter); }
                                tmpStyle = tmpStyle.BasedOn;
                            } while (tmpStyle != null);
                            foreach (var setter in setterList.Cast<Setter>().Where(setter => setter.Property.Name.Equals(setterValue.Property.Name)))
                            { newStyle.Setters.Add(setterValue); }
                        }
                        control.Style = newStyle;
                    }
                    catch (Exception ex) { throw new Exception(string.Format("Error Setting Style{0} - Control: {1}{0} - Style: {2}", Environment.NewLine, control, setterValue.Property + " = " + setterValue.Value), ex); }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// This iterates through the BasedOn styles and gets the first Base Style with Setters
        /// </summary>
        /// <remarks>
        /// Miguel Santana 2015/04/06
        /// </remarks>
        /// <param name="style">Top Level Style</param>
        /// <returns>Parent Style</returns>
        private static Style GetStyle(Style style)
        {
            try
            { return (style == null || style.Setters.Count > 0) ? style : GetStyle(style.BasedOn); }
            catch (Exception ex) { throw new Exception("Error Getting Style", ex); }
        }
    }
}