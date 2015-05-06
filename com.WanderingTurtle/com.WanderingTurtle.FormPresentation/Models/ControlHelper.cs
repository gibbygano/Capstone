using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace com.WanderingTurtle.FormPresentation.Models
{
    /// <summary>
    /// The control helper.
    /// </summary>
    internal static class ControlHelper
    {
        /// <summary>
        /// Converts a <see cref="System.Drawing.Bitmap"/> into a WPF <see cref="BitmapSource"/>.
        /// </summary>
        /// <remarks>Uses GDI to do the conversion. Hence the call to the marshalled DeleteObject.
        /// </remarks>
        /// <param name="source">The source bitmap.</param>
        /// <returns>A BitmapSource</returns>
        /// <exception cref="ArgumentException">The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.</exception>
        /// <exception cref="Exception">The operation failed.</exception>
        public static BitmapSource ToBitmapSource(this Bitmap source)
        {
            BitmapSource bitSrc;

            var hBitmap = source.GetHbitmap();

            try
            {
                bitSrc = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    hBitmap,
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Win32Exception)
            {
                bitSrc = null;
            }
            finally
            {
                NativeMethods.DeleteObject(hBitmap);
            }

            return bitSrc;
        }

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
                var parent = control;
                while (!(parent is T))
                {
                    if (parent == null) { throw new ApplicationException("Control " + control + " does not have parent " + typeof(T)); }
                    parent = (FrameworkElement)parent.Parent;
                }
                return parent as T;
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
                var parent = control as DependencyObject;
                while (!(parent is T))
                {
                    if (parent == null) { throw new ApplicationException("Control " + control + " does not have parent " + typeof(T)); }
                    parent = VisualTreeHelper.GetParent(parent);
                }

                return parent as T;
            }
            catch (Exception ex) { throw new WanderingTurtleException(control, ex, "Error Getting Main Window"); }
        }
    }

    internal static class NativeMethods
    {
        [DllImport("gdi32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DeleteObject(IntPtr hObject);
    }
}