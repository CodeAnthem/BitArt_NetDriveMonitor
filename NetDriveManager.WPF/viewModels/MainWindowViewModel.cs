using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.navigation;
using NetDriveManager.WPF.views;
using System.Threading;
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
			MainContent = _navigationService.Get(nameof(HeaderControl));
			_navigationService.ShowModalAsync(nameof(SettingsWindow));
		}

		public string TestProp { get; set; }
		public ContentControl MainContent { get; set; }
	}
}