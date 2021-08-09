using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.navigation;
using NetDriveManager.WPF.utilities.navigation.services;
using NetDriveManager.WPF.views;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.viewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;

		public MainWindowViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			TestProp = "Dependency Injection and ViewModel Mapping works";
			MainContent = _navigationService.GetUserControlWithDataContext(nameof(HeaderControl));
			_navigationService.ShowWindowModalAsync(nameof(HeaderControl));
			var view = _navigationService.GetWindowWithDataContextAsync(nameof(SettingsWindow)).Result;
			view.ShowDialog();
		}

		public string TestProp { get; set; }
		public ContentControl MainContent { get; set; }
	}
}