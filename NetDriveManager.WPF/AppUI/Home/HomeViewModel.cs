using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.WPF.AppUI.EditDrives;
using NetDriveManager.WPF.Main;
using NetDriveManager.WPF.utilities.contentController.services;
using System.Windows.Input;

namespace NetDriveManager.WPF.AppUI.Home
{
	public class HomeViewModel : ViewModelBase
	{
		public ICommand ManageDrivesCommand { get; set; }
		private readonly MainContentStore _mainContent;
		private readonly IContentControllerService _cc;

		public HomeViewModel(IContentControllerService contentControllerService, MainContentStore mainContentStore)
		{
			_cc = contentControllerService;
			_mainContent = mainContentStore;
			ManageDrivesCommand = new RelayCommand(NavEditDrives);
		}

		private void NavEditDrives()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(EditDrivesView));
		}
	}
}