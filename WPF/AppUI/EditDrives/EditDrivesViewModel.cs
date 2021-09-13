using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using WPF.AppUI.Home;
using WPF.Main;
using WPF.Utilities.ContentController.Services;

namespace WPF.AppUI.EditDrives
{
	public class EditDrivesViewModel : ViewModelBase
	{
		public List<Char> DriveLettersList { get; set; } = new List<char>();
		private readonly IContentControllerService _cc;
		private readonly INetDriveMonitor _core;
		private readonly MainContentStore _mainContent;
		private INetDrive _selectedDrive;

		public ObservableCollection<INetDrive> ConfiguredDrives { get; set; } = new ObservableCollection<INetDrive>();

		public bool IsCoreEnabled { get => _core.IsEnabled; }

		public INetDrive SelectedDrive
		{
			get { return _selectedDrive; }
			set { SetProperty(ref _selectedDrive, value); }
		}

		private void NavigateBack()
		{
			_core.Activate();
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));
		}

		public string NewDriveHostName { get; set; }
		public string NewDriveShare { get; set; }

		private void GetDrivesForDataGrid()
		{
			_core.DataAccess.GetDrives0REmptyList().ForEach(d => ConfiguredDrives.Add(d));
			Log.Debug($"Loaded {ConfiguredDrives.Count} drives of DataAccess");
		}

		private void Save()
		{
			_core.DataAccess.SaveDrives(ConfiguredDrives.ToList());
			NavigateBack();
		}

		public ICommand NavigateCancelCommand { get; }

		public ICommand NavigateSaveCommand { get; }
		public ICommand DeleteDriveCommand { get; }
		public ICommand GridCellChangedCommand { get; }

		public EditDrivesViewModel(INetDriveMonitor core, MainContentStore mainContent, IContentControllerService cc)
		{
			_core = core;
			_core.EnabledStatusChanged += OnEnabledStatusChanged;

			_mainContent = mainContent;
			_cc = cc;

			NavigateCancelCommand = new RelayCommand(NavigateBack);
			NavigateSaveCommand = new RelayCommand(Save);
			DeleteDriveCommand = new RelayCommand(DeleteDrive);
			GridCellChangedCommand = new RelayCommand(GridCellChanged);

			GetDrivesForDataGrid();
			ConfiguredDrives.CollectionChanged += OnCollectionChanged;
			UpdateLetters();

			_core.Deactivate();
		}

		private void GridCellChanged()
		{
			//UpdateLetters();
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			UpdateLetters();
		}

		private void UpdateLetters()
		{
			DriveLettersList = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList();

			//foreach (INetDrive drive in ConfiguredDrives)
			//{
			//	RemoveLetterIfExist(drive.Info.Letter);
			//}

			string[] localLetters = Directory.GetLogicalDrives();
			foreach (string localDrive in localLetters)
			{
				RemoveLetterIfExist(localDrive);
			}
		}

		private void RemoveLetterIfExist(string letter)
		{
			char driveLetter = char.ToUpper(letter[0]);
			bool doesLetterExistInArray = DriveLettersList.Contains(driveLetter);
			if (doesLetterExistInArray)
			{
				DriveLettersList.Remove(driveLetter);
				Log.Debug("Removed letter: {letter}", driveLetter);
			}
		}

		private void DeleteDrive()
		{
			if (SelectedDrive != null)
			{
				INetDrive drive = SelectedDrive;
				ConfiguredDrives.Remove(drive);
				Log.Debug("Removed drive {drive}", drive);
			}
		}

		private void OnEnabledStatusChanged() => OnPropertyChanged(nameof(IsCoreEnabled));
	}
}