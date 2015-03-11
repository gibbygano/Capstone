using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace com.WanderingTurtle.FormPresentation.Models
{
    public static class ErrorManager
    {
        public static Task ShowMessageDialog(Control window, string message, string title)
        {
            return _Show(window, title, message);
        }

        public static Task ShowMessageDialog(Control window, string message)
        {
            return _Show(window, message);
        }

        private static Task _Show(Control control, string message, string title = null, MessageDialogStyle? style = null, MetroDialogSettings settings = null)
        {
            MetroWindow window = WindowHelper.GetWindow(control);

            return window.ShowMessageAsync(title, message, style ?? MessageDialogStyle.Affirmative, settings);

            //if (result != MessageDialogResult.FirstAuxiliary)
            //    await window.ShowMessageAsync("Result", "You said: " + (result == MessageDialogResult.Affirmative ? mySettings.AffirmativeButtonText : mySettings.NegativeButtonText +
            //        Environment.NewLine + Environment.NewLine + "This dialog will follow the Use Accent setting."));
        }
    }
}