using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor;
using NetDriveManager.WPF.helpers;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.views;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace NetDriveManager.WPF.viewModels
{
	public class ManageDrivesViewModel : ViewModelBase
	{
		public ObservableCollection<NetdriveMonitorModel> DriveColl { get; set; } = new ObservableCollection<NetdriveMonitorModel>();
		public ICommand NavigateCancelCommand { get; set; }

		public ICommand NavigateSaveCommand { get; set; }

		private readonly IContentControllerService _cc;

		private readonly MainContentStore _mainContent;
		private readonly INetdriveMonitor _core;

		public ManageDrivesViewModel(INetdriveMonitor core, MainContentStore mainContent, IContentControllerService cc)
		{
			_core = core;
			_mainContent = mainContent;
			_cc = cc;

			NavigateCancelCommand = new RelayCommand(NavigateBack);
			NavigateSaveCommand = new RelayCommand(Save);

			var drivesOfConfigFile = _core.GetDrives();
			foreach (var drive in drivesOfConfigFile)
			{
				DriveColl.Add(drive);
			}
		}

		private void NavigateBack()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(OverviewControl));
		}

		private void Save()
		{
			MessageBox.Show("Saving - Not Implemented Yet");
		}
	}
}