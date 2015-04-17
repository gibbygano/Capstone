using System.Threading.Tasks;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace com.WanderingTurtle.FormPresentation.Models
{
    public static class DialogBox
    {
        /// <summary>
        /// Show Login Dialog
        /// Miguel Santana 2015/03/11
        /// </summary>
        /// <param name="control">Current control. In most cases use '<code>this</code>'</param>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="settings">LoginDialogSettings sets properties for dialog fields and buttons</param>
        /// <example>LoginDialogData result = await DialogBox.ShowLoginDialog(...)</example>
        /// <returns>awaitable Task of type LoginDialogData</returns>
        /// <exception cref="WanderingTurtleException"/>
        public static Task<LoginDialogData> ShowLoginDialog(FrameworkElement control, string message, string title = null, LoginDialogSettings settings = null)
        { return WindowHelper.GetWindow(control).ShowLoginAsync(title, message, settings); }

        /// <summary>
        /// Show Message Dialog
        /// Miguel Santana 2015/03/11
        /// </summary>
        /// <param name="control">Current control. In most cases use 'this'</param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="style">MessageDialogStyle sets the buttons visible on the dialog</param>
        /// <param name="settings">MetroDialogSettings sets properties for dialog fields and buttons</param>
        /// <example>MessageDialogResult result = await DialogBox.ShowMessageDialog(...)</example>
        /// <returns>awaitable Task of type MessageDialogResult</returns>
        /// <exception cref="WanderingTurtleException"/>
        public static Task<MessageDialogResult> ShowMessageDialog(FrameworkElement control, string message, string title = null, MessageDialogStyle? style = null, MetroDialogSettings settings = null)
        { return WindowHelper.GetWindow(control).ShowMessageAsync(title, message, style ?? MessageDialogStyle.Affirmative, settings); }
    }
}