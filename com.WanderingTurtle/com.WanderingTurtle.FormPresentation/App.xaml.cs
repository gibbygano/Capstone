using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private async void DispatcherUnhandledExceptionEventHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Prevent default unhandled exception processing
            e.Handled = e.Exception is WanderingTurtleException ? (e.Exception as WanderingTurtleException).DoHandle : true;

            if (e.Exception is InputValidationException)
            {
                var exception = e.Exception as InputValidationException;
                if (exception.Title == null) { exception.Title = "Validation Error"; }
                if (exception.CurrentControl != null && exception.CurrentControl.IsLoaded)
                { await DialogBox.ShowMessageDialog(exception.CurrentControl, exception.Message, exception.Title); }
                else
                { MessageBox.Show(exception.Message, exception.Title); }
                exception.CurrentControl.Focus();
                if (exception.CurrentControl is TextBoxBase) { (exception.CurrentControl as TextBoxBase).SelectAll(); }
                return;
            }

            // Process unhandled exception
            StringBuilder exceptionMessage = new StringBuilder(e.Exception.Message);
            Exception innerEx = e.Exception.InnerException;
            if (innerEx != null)
            { exceptionMessage.AppendLine(Environment.NewLine + Environment.NewLine + "Additional error information: "); }
            while (innerEx != null)
            {
                exceptionMessage.AppendLine(" - " + innerEx.Message);
                innerEx = innerEx.InnerException;
            }

            if (e.Exception is WanderingTurtleException)
            {
                var exception = e.Exception as WanderingTurtleException;
                if (exception.Title == null) { exception.Title = "An Error Occurred"; }
                if (exception.CurrentControl != null)
                { exceptionMessage.AppendLine(Environment.NewLine + Environment.NewLine + "Error Occurred in: " + exception.CurrentControl.Name ?? exception.CurrentControl.ToString()); }

                if (exception.CurrentControl != null && exception.CurrentControl.IsLoaded)
                { await DialogBox.ShowMessageDialog(exception.CurrentControl, exceptionMessage.ToString(), exception.Title); }
                else
                { MessageBox.Show(exceptionMessage.ToString(), exception.Title); }
            }
            else
            { MessageBox.Show(exceptionMessage.ToString()); }
        }
    }
}