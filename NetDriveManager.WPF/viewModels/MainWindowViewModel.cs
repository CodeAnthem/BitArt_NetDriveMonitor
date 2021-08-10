using NetDriveManager.WPF.controls;
using NetDriveManager.WPF.utilities.contentController.services;
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
		private readonly IContentControllerService _cc;

		public MainWindowViewModel(IContentControllerService contentControllerService)
		{
			_cc = contentControllerService;
			TestProp = "Dependency Injection and ViewModel Mapping works";
			MainContent = _cc.GetUserControl(nameof(HeaderControl));
		}

		public string TestProp { get; set; }
		public ContentControl MainContent { get; set; }
	}
}