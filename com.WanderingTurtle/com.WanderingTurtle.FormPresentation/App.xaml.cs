using com.WanderingTurtle.FormPresentation.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
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
            // Process unhandled exception
            StringBuilder exceptionMessage = new StringBuilder("An error occoured: " + Environment.NewLine + " - " + e.Exception.Message);
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
                // Prevent default unhandled exception processing
                e.Handled = true;
                var exception = e.Exception as WanderingTurtleException;
                exceptionMessage.AppendLine(Environment.NewLine + "Error Occured in: " + exception.CurrentControl.Name);
                if (exception.CurrentControl != null && exception.CurrentControl.IsLoaded)
                {
                    await DialogBox.ShowMessageDialog(exception.CurrentControl, exceptionMessage.ToString(), exception.Title);
                }
                else
                {
                    MessageBox.Show(exceptionMessage.ToString(), exception.Title);
                }
            }
            else
            {
                MessageBox.Show(exceptionMessage.ToString(), "WanderingTurtle Error");
            }
        }
    }
}