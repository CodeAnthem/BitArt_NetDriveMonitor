using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Input;

namespace UI_WPF.views
{
	/// <summary>
	/// Interaction logic for MainView.xaml
	/// </summary>
	public partial class MainView : MetroWindow
	{
		public MainView()
		{
			InitializeComponent();
		}

		private void MouseDownDragEvent(object sender, MouseButtonEventArgs e)
		{
			if (e.LeftButton == MouseButtonState.Pressed)
			{
				((MainView)sender).DragMove();
			}
		}

		private void LaunchGitHubSite(object sender, System.Windows.RoutedEventArgs e)
		{
			MessageBox.Show("Test");
		}

		private void DeployCupCakes(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Test2");
		}
	}
}