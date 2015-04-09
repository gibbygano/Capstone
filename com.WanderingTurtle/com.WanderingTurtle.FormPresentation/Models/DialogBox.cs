using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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
        /// <param name="settings"><typeparamref name="LoginDialogSettings"/> sets properties for dialog fields and buttons</param>
        /// <example><typeparamref name="LoginDialogData"/> result = await <typeparamref name="DialogBox"/>.<typeparamref name="ShowLoginDialog"/>(...)</example>
        /// <returns>awaitable <typeparamref name="Task"/> of type <typeparamref name="LoginDialogData"/></returns>
        public static Task<LoginDialogData> ShowLoginDialog(Control control, string message, string title = null, LoginDialogSettings settings = null)
        { return WindowHelper.GetWindow(control).ShowLoginAsync(title, message, settings); }

        /// <summary>
        /// Show Message Dialog
        /// Miguel Santana 2015/03/11
        /// </summary>
        /// <param name="control">Current control. In most cases use '<typeparamref name="this"/>this</typeparamref>'</param>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="style"><typeparamref name="MessageDialogStyle"/> sets the buttons visible on the dialog</param>
        /// <param name="settings"><typeparamref name="MetroDialogSettings"/> sets properties for dialog fields and buttons</param>
        /// <example><typeparamref name="MessageDialogResult"/> result = await <typeparamref name="DialogBox"/>.<typeparamref name="ShowMessageDialog"/>(...)</example>
        /// <returns>awaitable <typeparamref name="Task"/> of type <typeparamref name="MessageDialogResult"/></returns>
        public static Task<MessageDialogResult> ShowMessageDialog(FrameworkElement control, string message, string title = null, MessageDialogStyle? style = null, MetroDialogSettings settings = null)
        { return WindowHelper.GetWindow(control).ShowMessageAsync(title, message, style ?? MessageDialogStyle.Affirmative, settings); }
    }
}