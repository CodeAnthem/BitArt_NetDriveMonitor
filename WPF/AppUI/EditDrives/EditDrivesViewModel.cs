using Microsoft.Toolkit.Mvvm.Input;
using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
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
			_factory = factory;

			NavigateCancelCommand = new RelayCommand(NavigateBack);
			NavigateSaveCommand = new RelayCommand(Save);
			DeleteDriveCommand = new RelayCommand(DeleteDrive);
			AddDriveCommand = new RelayCommand(AddDrive);

			GetAndCreateRowItems();
			//UpdateLetters();

			_core.Deactivate();
		}

		private void UpdateLetters()
		{
			//UpdateRowItemsDriveLetters();
			//UpdateGlobalDriveLetters();
		}

		#endregion

		#region Public Properties

		//public ObservableCollection<Char> AvailableDriveLettersCol { get; set; } = new ObservableCollection<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
		public ObservableCollection<NetDriveRowItemModel> RowItems { get; set; } = new ObservableCollection<NetDriveRowItemModel>();

		public NetDriveRowItemModel SelectedRow
		{
			get { return selectedRow; }
			set
			{
				SetProperty(ref selectedRow, value);
				UpdateLetters();
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

			bool isUpdatingExistingDrive = NewDriveLetter == SelectedRow.Drive.Info.Letter;
			if (isUpdatingExistingDrive)
			{
				SelectedRow.Drive.Info.Letter = NewDriveLetter;
				SelectedRow.Drive.Info.HostName = NewDriveHostName;
				SelectedRow.Drive.Info.Share = NewDriveShare;
			}
			else
			{
				NetDriveInfo newDriveInfo = _factory.CreateDriveInfo(NewDriveLetter.ToString(), NewDriveHostName, NewDriveShare);
				INetDrive newDrive = _factory.Create(newDriveInfo);
				RowItems.Add(new NetDriveRowItemModel(newDrive));
			}

			UpdateLetters();
			//NewDriveLetter = AvailableDriveLettersCol[0];
			NewDriveHostName = "";
			NewDriveShare = "";
		}

		private void DeleteDrive()
		{
			if (SelectedRow != null)
			{
				NetDriveRowItemModel driveToDelete = SelectedRow;
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
		}

		//private void UpdateRowItemsDriveLetters()
		//{
		//	var usedLetters = new List<char>();
		//	usedLetters.AddRange(GetConfiguredDriveLetters());
		//	usedLetters.AddRange(GetLocalDriveLetters());

		// foreach (var currentRow in RowItems) { var freshLetters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

		// foreach (char letter in usedLetters) { if (currentRow.Drive.Info.Letter != letter)
		// freshLetters.Remove(letter); }

		//		currentRow.AvailableLetters = freshLetters;
		//	}
		//}

		//private static List<char> GetLocalDriveLetters()
		//{
		//	var localDriveLettersInUse = new List<char>();
		//	foreach (var drive in DriveInfo.GetDrives())
		//	{
		//		if (drive.DriveType == DriveType.Fixed)
		//		{
		//			char dDriveLetter = drive.Name[0];
		//			localDriveLettersInUse.Add(dDriveLetter);
		//			Log.Debug("Ignore letter used by local drive: {driveLetter}", dDriveLetter);
		//		}
		//	}
		//	return localDriveLettersInUse;
		//}

		//private List<char> GetConfiguredDriveLetters()
		//{
		//	var configuredDriveLettersInUse = new List<char>();
		//	foreach (var row in RowItems)
		//	{
		//		char driveLetter = row.Drive.Info.Letter;
		//		configuredDriveLettersInUse.Add(driveLetter);
		//		Log.Debug("Ignore letter used by configured drive: {driveLetter}", driveLetter);
		//	}
		//	return configuredDriveLettersInUse;
		//}

		//private void UpdateGlobalDriveLetters()
		//{
		//	var usedLetters = new List<char>();
		//	usedLetters.AddRange(GetConfiguredDriveLetters());
		//	usedLetters.AddRange(GetLocalDriveLetters());

		//	var freshLetters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
		//	usedLetters.ForEach(x => freshLetters.Remove(x));
		//	AvailableDriveLettersCol = new ObservableCollection<char>(freshLetters);
		//}

		private void NavigateBack()
		{
			_core.Activate();
			_mainContent.Control = _cc.GetUserControl(nameof(HomeView));
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