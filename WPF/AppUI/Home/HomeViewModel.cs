using Microsoft.Toolkit.Mvvm.Input;
using WPF.AppUI.EditDrives;
using WPF.Main;
using WPF.Utilities.ContentController.Services;
using System.Windows.Input;

namespace WPF.AppUI.Home
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