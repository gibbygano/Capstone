using System;
using System.Diagnostics;
using System.Windows;

namespace com.WanderingTurtle.ServiceClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            try
            {
            }
            catch (ArgumentNullException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void Application_Exit(object sender, ExitEventArgs e)
        {
        }
    }
}