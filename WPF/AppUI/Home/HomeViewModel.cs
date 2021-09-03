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
		#region Private Fields

		private readonly IContentControllerService _cc;
		private readonly MainContentStore _mainContent;
		private NetdriveMonitorModel _selectedDrive;

		#endregion

		#region Private Methods

		private void NavEditDrives()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(EditDrivesView));
		}

		private void NavSettings()
		{
			Core.ConnectDrive(SelectedDrive);
			MessageBox.Show("Opening Settings, soon?!");
		}

		//Drives = new ObservableCollection<NetdriveMonitorModel>(Core.Drives);
		private void ToggleDrive()
		{
			MessageBox.Show($"Connecting {SelectedDrive} ");
		}

		#endregion

		#region Public Properties

		public INetdriveMonitor Core { get; }
		public ICommand ManageDrivesCommand { get; }
		public ICommand ManageSettingsCommand { get; }
		public NetdriveMonitorModel SelectedDrive
		{
			get { return _selectedDrive; }
			set { SetProperty(ref _selectedDrive, value); }
		}

		public ICommand ToggleDriveCommand { get; }

		#endregion

		#region Public Constructors

		public HomeViewModel(IContentControllerService contentControllerService, MainContentStore mainContentStore, INetdriveMonitor core = null)
		{
			_cc = contentControllerService;
			_mainContent = mainContentStore;
			ManageDrivesCommand = new RelayCommand(NavEditDrives);
			ManageSettingsCommand = new RelayCommand(NavSettings);
			ToggleDriveCommand = new RelayCommand(ToggleDrive);
			Core = core;
			Core.Activate();
		}

		#endregion
	}
}