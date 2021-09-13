using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor;
using NetDriveManager.Monitor.Interfaces;
using Serilog;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using WPF.AppUI.EditDrives;
using WPF.Main;
using WPF.Utilities.ContentController.Services;

namespace WPF.AppUI.Home
{
	public class HomeViewModel : ViewModelBase
	{
		#region Private Fields

		private readonly IContentControllerService _cc;
		private readonly MainContentStore _mainContent;
		private INetDrive _selectedDrive;

		#endregion

		#region Public Properties

		public INetDriveMonitorSettings Settings { get; set; }
		public bool IsCoreEnabled { get => Core.IsEnabled; }

		#endregion

		#region Private Methods

		private void NavEditDrivesCommand()
		{
			Log.Debug("Clicked edit drives command");
			//Core.Deactivate();

			_mainContent.Control = _cc.GetUserControl(nameof(EditDrivesView));
		}

		private void NavSettingsCommand()
		{
			if (Core.IsEnabled)
				Core.Deactivate();
			else
				Core.Activate();
			//MessageBox.Show("Opening Settings, soon?!");
		}

		private void ToggleDriveCommand()
		{
			if (SelectedDrive.Status.IsConnected)
				Core
					.DriveHelper
					.Remove(SelectedDrive);
			else
				Core
					.DriveHelper
					.Add(SelectedDrive);
		}

		private async Task ToggleDriveCommandAsync()
		{
			if (SelectedDrive.Status.IsConnected)
				await Core.DriveHelper.RemoveAsync(SelectedDrive);
			else
				await Core.DriveHelper.AddAsync(SelectedDrive);
		}

		#endregion

		#region Public Properties

		public INetDriveMonitor Core { get; }
		public ICommand ManageDrives { get; }
		public ICommand ManageSettings { get; }
		public INetDrive SelectedDrive
		{
			get { return _selectedDrive; }
			set { SetProperty(ref _selectedDrive, value); }
		}

		public ICommand ToggleDriveAsync { get; }
		public ICommand ToggleSetting_AutoConnectIfAvailable { get; }

		#endregion

		#region Public Constructors

		public HomeViewModel(IContentControllerService contentControllerService, MainContentStore mainContentStore, INetDriveMonitor core, INetDriveMonitorSettings settings)
		{
			_cc = contentControllerService;
			_mainContent = mainContentStore;
			Core = core;
			Core.EnabledStatusChanged += OnEnabledStatusChanged;
			Settings = settings;

			ManageDrives = new RelayCommand(NavEditDrivesCommand);
			ToggleSetting_AutoConnectIfAvailable = new RelayCommand(ToggleSetting_AutoConnectIfAvailableCommand);
			ManageSettings = new RelayCommand(NavSettingsCommand);
			ToggleDriveAsync = new AsyncRelayCommand(ToggleDriveCommandAsync);

			Core.Activate();
		}

		private void OnEnabledStatusChanged() => OnPropertyChanged(nameof(IsCoreEnabled));

		#endregion

		private void ToggleSetting_AutoConnectIfAvailableCommand()
		{
			if (Settings.IsAutoConnectIfAvailable)
			{
				Settings.IsAutoConnectIfAvailable = false;
				Log.Debug("Setting IsAutoConnectIfAvailable disabled");
				return;
			}

			Settings.IsAutoConnectIfAvailable = true;
			Log.Debug("Setting IsAutoConnectIfAvailable enabled");
		}
	}
}