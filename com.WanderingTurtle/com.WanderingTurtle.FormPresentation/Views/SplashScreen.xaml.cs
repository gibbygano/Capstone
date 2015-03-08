using com.WanderingTurtle.FormPresentation.Models;
using System.Windows;

namespace com.WanderingTurtle.FormPresentation.Views
{
	/// <summary>
	/// Interaction logic for SplashScreen.xaml
	/// </summary>
	public partial class SplashScreen
	{
		public SplashScreen()
		{
			InitializeComponent();
		}

		private void BtnSignIn_Click(object sender, RoutedEventArgs e)
		{
			WindowHelper.GetMainWindow(this).MainContent = new TabContainer();
		}
	}
}