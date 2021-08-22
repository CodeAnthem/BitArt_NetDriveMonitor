using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor;
using NetDriveManager.WPF.AppUI.Home;
using NetDriveManager.WPF.Main;
using NetDriveManager.WPF.utilities.contentController.services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace NetDriveManager.WPF.AppUI.EditDrives
{
	public class EditDrivesViewModel : ViewModelBase
	{
		public ObservableCollection<NetdriveMonitorModel> DriveColl { get; set; } = new ObservableCollection<NetdriveMonitorModel>();
		public ICommand NavigateCancelCommand { get; }

		public ICommand NavigateSaveCommand { get; }

		private readonly IContentControllerService _cc;

		private readonly MainContentStore _mainContent;
		private readonly INetdriveMonitor _core;

		public EditDrivesViewModel(INetdriveMonitor core, MainContentStore mainContent, IContentControllerService cc)
		{
			_core = core;
			_mainContent = mainContent;
			_cc = cc;

			NavigateCancelCommand = new RelayCommand(NavigateBack);
			NavigateSaveCommand = new RelayCommand(Save);

			GetDrivesForDataGrid();
		}

		private void GetDrivesForDataGrid()
		{
			_core.GetDrives().ForEach(d => DriveColl.Add(d));
		}

		private void NavigateBack()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));
		}

		private void Save()
		{
			_core.SaveDrives(DriveColl.ToList());
		}
	}
}