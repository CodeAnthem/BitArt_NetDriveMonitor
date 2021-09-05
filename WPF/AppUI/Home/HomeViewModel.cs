using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor;
using NetDriveManager.Monitor.Interfaces;
using Serilog;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
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

		#endregion

		#region Private Methods

		private void NavEditDrivesCommand()
		{
			_mainContent.Control = _cc.GetUserControl(nameof(EditDrivesView));
		}

		private void NavSettingsCommand()
		{
			MessageBox.Show("Opening Settings, soon?!");
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
			Settings = settings;

			ManageDrives = new RelayCommand(NavEditDrivesCommand);
			ToggleSetting_AutoConnectIfAvailable = new RelayCommand(ToggleSetting_AutoConnectIfAvailableCommand);
			ManageSettings = new RelayCommand(NavSettingsCommand);
			ToggleDriveAsync = new AsyncRelayCommand(ToggleDriveCommandAsync);

			Core.Activate();
		}

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