using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.WPF.helpers;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.views;
using System.Windows.Input;

namespace NetDriveManager.WPF.viewModels
{
	public class OverviewViewModel : ViewModelBase
	{
		public ICommand ManageDrivesCommand { get; set; }
		private readonly MainContentStore _mainContent;
		private readonly IContentControllerService _cc;

		public OverviewViewModel(IContentControllerService contentControllerService, MainContentStore mainContentStore)
		{
			_cc = contentControllerService;
			_mainContent = mainContentStore;
			ManageDrivesCommand = new RelayCommand(OpenManageDrives);
		}

		private void OpenManageDrives()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(ManageDrivesControl));
		}
	}
}