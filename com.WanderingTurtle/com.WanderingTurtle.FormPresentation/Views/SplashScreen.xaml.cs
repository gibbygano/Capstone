using com.WanderingTurtle.BusinessLogic;
using com.WanderingTurtle.Common;
using com.WanderingTurtle.FormPresentation.Models;
using com.WanderingTurtle.FormPresentation.Resources;
using MahApps.Metro;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Image = System.Windows.Controls.Image;

namespace com.WanderingTurtle.FormPresentation.Views
{
    /// <summary>
    /// Interaction logic for <see cref="SplashScreen" />
    /// </summary>
    /// <remarks>Miguel Santana 2015/03/10</remarks>
    public partial class StartupScreen
    {
        private System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupScreen" /> class.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// The item to add already has a different logical parent.
        /// </exception>
        /// <exception cref="InvalidOperationException">The collection is in ItemsSource mode.</exception>
        public StartupScreen()
        {
            Globals.UserToken = null;
            Application.Current.ChangeAppStyle(ThemeManager.GetAccent("Emerald"));
            InitializeComponent();
            FlipView.HideControlButtons();
            foreach (var image in SplashImageList)
            {
                FlipView.Items.Add(new Image { Source = image, Stretch = Stretch.UniformToFill });
            }
            myTimer.Tick += TimerEventProcessor;
            myTimer.Interval = 10000;
            myTimer.Start();
        }

        /// <summary>
        /// Gets the list of SplashScreen Images
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/06</remarks>
        /// <exception cref="MissingManifestResourceException">
        /// <paramref name="tryParents" /> is true, no usable set of resources has been found, and
        /// there are no default culture resources.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// The <paramref name="culture" /> parameter is null.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// The height or width of the bitmap is greater than <see cref="F:System.Int16.MaxValue" />.
        /// </exception>
        /// <exception cref="Exception">The operation failed.</exception>
        /// <exception cref="InvalidCastException" accessor="get">
        /// An element in the sequence cannot be cast to type <paramref name="TResult" />.
        /// </exception>
        public List<BitmapSource> SplashImageList
        {
            get
            {
                return SplashImages.ResourceManager.GetResourceSet(CultureInfo.CurrentUICulture, true, true)
                    .Cast<DictionaryEntry>()
                    .Where(resource => resource.Value is Bitmap)
                    .OrderBy(resource => resource.Key)
                    .Select(resource => ((Bitmap)resource.Value).ToBitmapSource())
                    .ToList();
            }
        }

        /// <summary>
        /// The sign-in button click event handler.
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/03</remarks>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        /// <exception cref="ApplicationException"></exception>
        private void BtnSignInClick(object sender, RoutedEventArgs e)
        {
            Submit();
        }

        /// <summary>
        /// Submits the login form
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/03</remarks>
        private void Submit()
        {
            if (Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]))
            { Globals.UserToken = new Employee(100, "Debugger", null, 1); }

            int userId;
            if (!int.TryParse(TxtUserName.Text.Trim(), out userId))
            {
                throw new InputValidationException(TxtUserName, "Please enter your User ID.", "Login Error");
            }

            var password = TxtPassword.Password.Trim();
            if (string.IsNullOrEmpty(password) || password.ValidatePassword())
            {
                throw new InputValidationException(TxtPassword, "Please enter your Password.", "Login Error");
            }

            try
            {
                TxtPassword.Clear();
                Globals.UserToken = new EmployeeManager().GetEmployeeLogin(userId, password);
            }
            catch (ApplicationException ex)
            {
                throw new InputValidationException(TxtPassword, ex, "Login Error");
            }

            if (Globals.UserToken == null)
            {
                throw new WanderingTurtleException(this, "Error setting User Token");
            }

            var window = this.GetVisualParent<MainWindow>();
            window.BtnSignOut.Visibility = Visibility.Visible;
            window.MainContent.Content = new TabContainer();
            myTimer.Stop();
        }

        /// <summary>
        /// Timer that automates the FlipView slideshow
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/06</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TimerEventProcessor(object sender, EventArgs e)
        {
            FlipView.SelectedIndex = Equals(FlipView.SelectedIndex + 1, FlipView.Items.Count)
                ? 0
                : FlipView.SelectedIndex + 1;
        }

        /// <summary>
        /// Selects the textbox content on keyboard focus
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/03</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtInput_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            var @textBox = sender as TextBoxBase;
            if (@textBox != null) { @textBox.SelectAll(); }
            else
            {
                var @passwordBox = sender as PasswordBox;
                if (@passwordBox != null) { @passwordBox.SelectAll(); }
            }
        }

        /// <summary>
        /// Key up event, Submits the form
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/03</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TxtInput_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Return:
                    Submit();
                    break;
            }
        }

        /// <summary>
        /// Focuses the username field on load
        /// </summary>
        /// <remarks>Miguel Santana 2015/05/05</remarks>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            TxtUserName.Focus();
        }
    }
}