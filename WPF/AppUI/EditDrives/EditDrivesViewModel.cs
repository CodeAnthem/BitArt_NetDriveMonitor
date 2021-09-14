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
		#region Private Fields

		private readonly IContentControllerService _cc;
		private readonly INetDriveMonitor _core;
		private readonly INetDriveFactory _factory;
		private readonly MainContentStore _mainContent;
		private string _newDriveHostName;
		private char _newDriveLetter;
		private string _newDriveShare;
		private NetDriveRowItemModel selectedRow;

		#endregion

		#region Public Constructors

		public EditDrivesViewModel(INetDriveMonitor core, MainContentStore mainContent, IContentControllerService cc, INetDriveFactory factory)
		{
			_core = core;
			_core.EnabledStatusChanged += OnEnabledStatusChanged;

			_mainContent = mainContent;
			_cc = cc;

			NavigateCancelCommand = new RelayCommand(NavigateBack);
			NavigateSaveCommand = new RelayCommand(Save);
			DeleteDriveCommand = new RelayCommand(DeleteDrive);
			AddDriveCommand = new RelayCommand(AddDrive);

			GetAndCreateRowItems();
			UpdateDriveLetters();

			//ConfiguredDrivesDict.
			//ConfiguredDrives.CollectionChanged += OnCollectionChanged;
			//UpdateLetters();

			_core.Deactivate();
			_factory = factory;
		}

		#endregion

		#region Public Properties

		public ObservableCollection<Char> AvailableDriveLettersCol { get; set; } = new ObservableCollection<char>();
		public ObservableCollection<NetDriveRowItemModel> RowItems { get; set; } = new ObservableCollection<NetDriveRowItemModel>();
		public NetDriveRowItemModel SelectedRow
		{
			get { return selectedRow; }
			set
			{
				SetProperty(ref selectedRow, value);
				UpdateDriveLetters();
			}
		}

		public ICommand AddDriveCommand { get; }
		public ICommand DeleteDriveCommand { get; }
		public bool IsCoreEnabled { get => _core.IsEnabled; }

		public ICommand NavigateCancelCommand { get; }
		public ICommand NavigateSaveCommand { get; }
		public string NewDriveHostName
		{
			get { return _newDriveHostName; }
			set { SetProperty(ref _newDriveHostName, value); }
		}

		public char NewDriveLetter
		{
			get { return _newDriveLetter; }
			set
			{
				SetProperty(ref _newDriveLetter, value);
				//UpdateDriveLetters();
			}
		}

		public string NewDriveShare
		{
			get { return _newDriveShare; }
			set { SetProperty(ref _newDriveShare, value); }
		}

		#endregion

		#region Private Methods

		private void AddDrive()
		{
			if (string.IsNullOrEmpty(NewDriveHostName))
			{
				MessageBox.Show("HostName is not valid");
				return;
			}

			if (string.IsNullOrEmpty(NewDriveShare))
			{
				MessageBox.Show("Share is not valid");
				return;
			}

			if (!char.IsLetter(NewDriveLetter))
			{
				MessageBox.Show("Letter is not valid");
				return;
			}

			var newDriveInfo = _factory.CreateDriveInfo(NewDriveLetter.ToString(), NewDriveHostName, NewDriveShare);
			var newDrive = _factory.Create(newDriveInfo);
			//ConfiguredDrives.Add(newDrive);
			//UpdateLetters();

			NewDriveHostName = "";
			NewDriveShare = "";
		}

		private void DeleteDrive()
		{
			if (SelectedRow != null)
			{
				var driveToDelete = SelectedRow;
				RowItems.Remove(driveToDelete);
				Log.Debug("Removed drive {driveInfo}", driveToDelete.Drive);
			}
		}

		private void GetAndCreateRowItems()
		{
			RowItems.Clear();

			var configuredDrives = _core.DataAccess.GetDrives0REmptyList();
			foreach (var drive in configuredDrives)
			{
				//var letters = new ObservableCollection<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
				RowItems.Add(new NetDriveRowItemModel(drive));
				Log.Debug("UI drive added: {drive}", drive);
			}
			Log.Debug($"Loaded {RowItems.Count} drives of DataAccess");

			//_core.DataAccess.GetDrives0REmptyList().ForEach(d => ConfiguredDrives.Add(d));
		}

		private void UpdateDriveLetters()
		{
			var usedLetters = new List<char>();
			foreach (var row in RowItems)
			{
				char localDriveLetter = row.Drive.Info.Letter;
				usedLetters.Add(localDriveLetter);
				Log.Debug("Ignore letter used by configured drive: {driveLetter}", localDriveLetter);
			}

			foreach (var drive in DriveInfo.GetDrives())
			{
				if (drive.DriveType == DriveType.Fixed)
				{
					char localDriveLetter = drive.Name[0];
					usedLetters.Add(localDriveLetter);
					Log.Debug("Ignore letter used by local drive: {driveLetter}", localDriveLetter);
				}
			}

			foreach (var currentRow in RowItems)
			{
				var freshLetters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

				foreach (char letter in usedLetters)
				{
					if (currentRow.Drive.Info.Letter != letter)
						freshLetters.Remove(letter);
				}

				currentRow.AvailableLetters = freshLetters;

				//foreach (var rowItem in RowItems)
				//{
				//	if (currentRow.Drive.Info.Letter != rowItem.Drive.Info.Letter)
				//	{
				//		freshLetters.Remove(rowItem.Drive.Info.Letter);
				//		Log.Debug("Removed letter: {letter} for drive {drive}", rowItem.Drive.Info.Letter, currentRow.Drive);
				//	}
				//}

				//currentRow.AvailableLetters = freshLetters;
				//Log.Debug("Drive Letters for drive: {driveletter} - '{letters}'", currentRow.Drive.Info.Letter, currentRow.AvailableLetters);

				//foreach (char letter in "ABCDEFGHIJKLMNOPQRSTUVWXYZ")
				//{
				//	if (!currentRow.AvailableLetters.Contains(letter))
				//		currentRow.AvailableLetters.Add(letter);
				//}

				////driveEntry.Key = new ObservableCollection<char>("ABC"); //driveEntry.Key =
				//var sortedLetters = currentRow.AvailableLetters.OrderBy(x => x).ToList();
				//currentRow.AvailableLetters = new ObservableCollection<char>(sortedLetters);

				//foreach (var otherDrives in RowItems)
				//{
				//	if (currentRow.Drive.Info.Letter != otherDrives.Drive.Info.Letter)
				//		currentRow.AvailableLetters.Remove(otherDrives.Drive.Info.Letter);
				//}
			}
		}

		private void GridCellChanged()
		{
			UpdateDriveLetters();
		}

		private void NavigateBack()
		{
			_core.Activate();
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));
		}

		private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			//UpdateLetters();
		}

		private void OnEnabledStatusChanged() => OnPropertyChanged(nameof(IsCoreEnabled));

		//private void RemoveLetterIfExist(string letter)
		//{
		//	if (string.IsNullOrWhiteSpace(letter))
		//		throw new Exception("Letter cant be empty");

		//	char driveLetter = char.ToUpper(letter[0]);
		//	bool doesLetterExistInArray = DriveLettersList.Contains(driveLetter);
		//	if (doesLetterExistInArray)
		//	{
		//		DriveLettersList.Remove(driveLetter);
		//		Log.Debug("Removed letter: {letter}", driveLetter);
		//	}
		//}

		//private void RemoveLetterIfExist(char letter)
		//{
		//	if (!char.IsLetter(letter))
		//	{
		//		throw new Exception("Letter is not a letter");
		//	}
		//	char driveLetter = char.ToUpper(letter);

		//	bool doesLetterExistInArray = DriveLettersList.Contains(driveLetter);
		//	if (doesLetterExistInArray)
		//	{
		//		DriveLettersList.Remove(driveLetter);
		//		Log.Debug("Removed letter: {letter}", driveLetter);
		//	}
		//}

		private void Save()
		{
			List<INetDrive> drivesToSave = new List<INetDrive>();

			foreach (var rowItem in RowItems)
			{
				drivesToSave.Add(rowItem.Drive);
			}

			_core.DataAccess.SaveDrives(drivesToSave);
			NavigateBack();
		}

		//private void UpdateLetters()
		//{
		//	"ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToList().ForEach(c => DriveLettersList.Add(c));

		// string[] localLetters = Directory.GetLogicalDrives(); foreach (string localDrive in
		// localLetters) { RemoveLetterIfExist(localDrive); }

		//	//foreach (INetDrive drive in ConfiguredDrives)
		//	//{
		//	//	if (drive.Info.Letter != SelectedDrive?.Info.Letter)
		//	//		RemoveLetterIfExist(drive.Info.Letter);
		//	//}
		//}

		#endregion
	}
}