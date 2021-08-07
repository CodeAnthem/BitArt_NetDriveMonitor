using NetDriveManager.WPF.services;

namespace NetDriveManager.WPF.viewModels
{
	public class MainWindowViewModel : ViewModelBase
	{
		private readonly INavigationService _navigationService;

		public MainWindowViewModel(INavigationService navigationService)
		{
			_navigationService = navigationService;
			TestProp = "hehehehahahahhahahehehehehe";
		}

		public string TestProp { get; set; }
	}
}