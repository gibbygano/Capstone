using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Models
{
    /// <summary>
    /// The dialog box.
    /// </summary>
    public static class DialogBox
    {
        /// <summary>
        /// Show Login Dialog
        /// </summary>
        /// <remarks>Miguel Santana 2015/03/11</remarks>
        /// <param name="control">Current control. In most cases use 'this'</param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="settings">LoginDialogSettings sets properties for dialog fields and buttons</param>
        /// <example><see cref="LoginDialogData" /> result = await <see cref="DialogBox.ShowLoginDialog" />(...)</example>
        /// <returns>awaitable Task of type LoginDialogData</returns>
        /// <exception cref="WanderingTurtleException"></exception>
        public static Task<LoginDialogData> ShowLoginDialog(this FrameworkElement control, string message, string title = null, LoginDialogSettings settings = null)
        { return control.GetWindow<MetroWindow>().ShowLoginAsync(title, message, settings); }

        /// <summary>
        /// Show Message Dialog
        /// </summary>
        /// <remarks>Miguel Santana 2015/03/11</remarks>
        /// <param name="control">Current control. In most cases use 'this'</param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="style">MessageDialogStyle sets the buttons visible on the dialog</param>
        /// <param name="settings">
        /// <see cref="MetroDialogSettings" /> sets properties for dialog fields and buttons
        /// </param>
        /// <example><see cref="MessageDialogResult" /> result = await <see cref="DialogBox.ShowMessageDialog" />(...)</example>
        /// <returns>awaitable Task of type MessageDialogResult</returns>
        /// <exception cref="WanderingTurtleException" />
        public static Task<MessageDialogResult> ShowMessageDialog(this FrameworkElement control, string message, string title = null, MessageDialogStyle? style = null, MetroDialogSettings settings = null)
        { return control.GetWindow<MetroWindow>().ShowMessageAsync(title, message, style ?? MessageDialogStyle.Affirmative, settings); }
    }
}