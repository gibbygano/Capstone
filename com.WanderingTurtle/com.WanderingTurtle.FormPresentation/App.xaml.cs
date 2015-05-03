using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Threading;

namespace com.WanderingTurtle.FormPresentation
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        private async void DispatcherUnhandledExceptionEventHandler(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            // Prevent default unhandled exception processing
            e.Handled = e.Exception is WanderingTurtleException ? ((WanderingTurtleException)e.Exception).DoHandle : true;

            if (e.Exception is InputValidationException)
            {
                var exception = e.Exception as InputValidationException;
                if (exception.Title == null) { exception.Title = "Validation Error"; }
                if (exception.CurrentControl != null && exception.CurrentControl.IsLoaded)
                { await exception.CurrentControl.ShowMessageDialog(exception.Message, exception.Title); }
                else
                { MessageBox.Show(exception.Message, exception.Title); }
                Debug.Assert(exception.CurrentControl != null, "exception.CurrentControl != null");
                exception.CurrentControl.Focus();
                if (exception.CurrentControl is TextBoxBase) { ((TextBoxBase)exception.CurrentControl).SelectAll(); }
                return;
            }

            // Process unhandled exception
            var exceptionMessage = new StringBuilder(e.Exception.Message);
            var innerEx = e.Exception.InnerException;
            if (innerEx != null)
            { exceptionMessage.AppendLine(Environment.NewLine + Environment.NewLine + "Additional error information: "); }
            while (innerEx != null)
            {
                exceptionMessage.AppendLine(" - " + innerEx.Message);
                innerEx = innerEx.InnerException;
            }

            if (e.Exception is WanderingTurtleException)
            {
                var exception = (WanderingTurtleException)e.Exception;
                if (exception.Title == null) { exception.Title = "An Error Occurred"; }

                if (exception.CurrentControl != null && exception.CurrentControl.IsLoaded)
                { await exception.CurrentControl.ShowMessageDialog(exceptionMessage.ToString(), exception.Title); }
                else
                { MessageBox.Show(exceptionMessage.ToString(), exception.Title); }
            }
            else
            { MessageBox.Show(exceptionMessage.ToString()); }
        }
    }
}