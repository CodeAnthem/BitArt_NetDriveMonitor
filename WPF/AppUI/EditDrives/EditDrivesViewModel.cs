using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor;
using WPF.AppUI.Home;
using WPF.Main;
using WPF.Utilities.ContentController.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Serilog;

namespace WPF.AppUI.EditDrives
{
	public class EditDrivesViewModel : ViewModelBase
	{
		public ObservableCollection<NetdriveMonitorModel> DriveColl => _core.Drives;
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

			//GetDrivesForDataGrid();
		}

		//private void GetDrivesForDataGrid()
		//{
		//	_core.GetDrives().ForEach(d => DriveColl.Add(d));
		//	Log.Debug($"Loaded {DriveColl.Count} drives of DataAccess");
		//}

		private void NavigateBack()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));
		}

		private void Save()
		{
			_core.SaveDrives(DriveColl.ToList());
			NavigateBack();
		}
	}
}