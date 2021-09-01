using Microsoft.Toolkit.Mvvm.Input;
using WPF.AppUI.EditDrives;
using WPF.Main;
using WPF.Utilities.ContentController.Services;
using System.Windows.Input;
using System.Collections.ObjectModel;
using NetDriveManager.Monitor;
using System;
using System.Windows;
using ControlzEx.Theming;

namespace WPF.AppUI.Home
{
	public class HomeViewModel : ViewModelBase
	{
		public ObservableCollection<NetdriveMonitorModel> Drives { get; } = new ObservableCollection<NetdriveMonitorModel>();

		public ICommand ManageDrivesCommand { get; }
		public ICommand ManageSettingsCommand { get; }
		public ICommand ToggleDriveCommand { get; }
		private readonly MainContentStore _mainContent;
		private readonly IContentControllerService _cc;
		private NetdriveMonitorModel _selectedDrive;

		public NetdriveMonitorModel SelectedDrive
		{
			get { return _selectedDrive; }
			set { SetProperty(ref _selectedDrive, value); }
		}

		public INetdriveMonitor Core { get; }

		public HomeViewModel(IContentControllerService contentControllerService, MainContentStore mainContentStore, INetdriveMonitor core = null)
		{
			_cc = contentControllerService;
			_mainContent = mainContentStore;
			ManageDrivesCommand = new RelayCommand(NavEditDrives);
			ManageSettingsCommand = new RelayCommand(NavSettings);
			ToggleDriveCommand = new RelayCommand(ToggleDrive);
			Core = core;
			Core.Activate();
			Drives = new ObservableCollection<NetdriveMonitorModel>(Core.Drives);
		}

		//Drives = new ObservableCollection<NetdriveMonitorModel>(Core.Drives);

		private void NavSettings()
		{
			Core.ConnectDrive(SelectedDrive);
			MessageBox.Show("Opening Settings, soon?!");
		}

		private void ToggleDrive()
		{
			MessageBox.Show($"Connecting {SelectedDrive} ");
		}

		private void NavEditDrives()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(EditDrivesView));
		}
	}
}