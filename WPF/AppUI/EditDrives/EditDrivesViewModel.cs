using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor.Interfaces;
using System.Windows.Input;
using WPF.AppUI.Home;
using WPF.Main;
using WPF.Utilities.ContentController.Services;

namespace WPF.AppUI.EditDrives
{
	public class EditDrivesViewModel : ViewModelBase
	{
		#region Private Fields

		private readonly IContentControllerService _cc;
		private readonly INetDriveMonitor _core;
		private readonly MainContentStore _mainContent;

		#endregion

		#region Private Methods

		private void NavigateBack()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));
		}

		//private void GetDrivesForDataGrid()
		//{
		//	_core.GetDrives().ForEach(d => DriveColl.Add(d));
		//	Log.Debug($"Loaded {DriveColl.Count} drives of DataAccess");
		//}
		private void Save()
		{
			//_core.SaveDrives(DriveColl.ToList());
			NavigateBack();
		}

		#endregion

		#region Public Properties

		//public ObservableCollection<NetdriveMonitorModel> DriveColl => _core.Drives;
		public ICommand NavigateCancelCommand { get; }

		public ICommand NavigateSaveCommand { get; }

		#endregion

		#region Public Constructors

		public EditDrivesViewModel(INetDriveMonitor core, MainContentStore mainContent, IContentControllerService cc)
		{
			_core = core;
			_mainContent = mainContent;
			_cc = cc;

			NavigateCancelCommand = new RelayCommand(NavigateBack);
			NavigateSaveCommand = new RelayCommand(Save);

			//GetDrivesForDataGrid();
		}

		#endregion
	}
}