using Serilog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WPF.AppUI.EditDrives
{
	/// <summary>
	/// Interaction logic for EditDrivesView.xaml
	/// </summary>
	public partial class EditDrivesView : UserControl
	{
		#region Private Fields

		private const string _lettersAZ = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		private int _lastLetterIndex = -1;

		private List<char> _occupiedLettersOfConfiguredDrives = new List<char>();
		private List<char> _occupiedLettersOfLocalDrives = new List<char>();
		private bool _updatedOnce;

		#endregion

		#region Public Constructors

		public EditDrivesView()
		{
			InitializeComponent();
		}

		#endregion

		#region Private Methods

		private void ComboBox_Letters_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			var sourceCollection = DataGrid.ItemsSource as ObservableCollection<NetDriveRowItemModel>;
			if (sourceCollection == null)
				return;
			sourceCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(DataGrid_CollectionChanged);

			UpdateLetters();
			_updatedOnce = true;
		}

		private void DataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			UpdateLetters();
		}

		private void ComboBox_Letters_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ComboBox_Letters.SelectedIndex != -1)
			{
				_lastLetterIndex = ComboBox_Letters.SelectedIndex;
				char letter = (char)ComboBox_Letters.Items[_lastLetterIndex];
				Log.Debug("Stored global selection: {0}|{1}", letter, _lastLetterIndex);
				return;
			}
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (!_updatedOnce)
				return;
			UpdateLetters();
		}

		private void GetConfiguredDriveLetters()
		{
			_occupiedLettersOfConfiguredDrives.Clear();
			foreach (var row in DataGrid.ItemsSource as ObservableCollection<NetDriveRowItemModel>)
			{
				char driveLetter = row.Drive.Info.Letter;
				_occupiedLettersOfConfiguredDrives.Add(driveLetter);
			}
		}

		private void GetLocalDriveLetters()
		{
			_occupiedLettersOfLocalDrives.Clear();
			foreach (var drive in DriveInfo.GetDrives())
			{
				if (drive.DriveType == DriveType.Fixed)
				{
					char dDriveLetter = drive.Name[0];
					_occupiedLettersOfLocalDrives.Add(dDriveLetter);
				}
			}
		}

		private void RememberLastGlobalSelection()
		{
			if (ComboBox_Letters.SelectedIndex != -1)
			{
				Log.Debug("Nothing to change");
				return;
			}

			if (ComboBox_Letters.SelectedIndex == -1 && _lastLetterIndex == -1 && ComboBox_Letters.Items.Count > 0)
			{
				ComboBox_Letters.SelectedIndex = 0;
				_lastLetterIndex = 0;
				Log.Debug("Reseting");
				return;
			}

			int newLetterIndex = _lastLetterIndex + 1;

			while (newLetterIndex >= ComboBox_Letters.Items.Count)
			{
				newLetterIndex--;
			}

			Log.Debug("Saved last index! old: {oldIndex} - new: {newIndex}", _lastLetterIndex, newLetterIndex);

			ComboBox_Letters.SelectedIndex = newLetterIndex;
			_lastLetterIndex = newLetterIndex;

			//if (lastLetterIndex < ComboBox_Letters.Items.Count)
			//{
			//}

			//int newLastLetterIndex = lastLetterIndex + 0;
			//Log.Debug($"Compare ({newLastLetterIndex} < {ComboBox_Letters.Items.Count - 1})");
			//if (newLastLetterIndex < ComboBox_Letters.Items.Count)
			//{
			//	ComboBox_Letters.SelectedIndex = newLastLetterIndex;
			//	_currentLetterIndex = newLastLetterIndex;
			//	Log.Debug("Setting new value {0}", newLastLetterIndex);
			//}
			//else
			//{
			//	ComboBox_Letters.SelectedIndex = 0;
			//	_currentLetterIndex = 0;
			//	Log.Debug("Reseting2");
			//}
		}

		private void UpdateGlobalDriveLetters(IEnumerable<char> usedLetters)
		{
			var freshLetters = new List<char>(_lettersAZ);
			foreach (char letter in usedLetters)
			{
				freshLetters.Remove(letter);
			}
			ComboBox_Letters.ItemsSource = freshLetters;
			Log.Debug("Adjusted global letter list: {letters}", freshLetters);
		}

		private void UpdateLetters()
		{
			UpdateOccupiedLetters();

			var usedLetters = _occupiedLettersOfConfiguredDrives.Concat(_occupiedLettersOfLocalDrives);
			UpdateRowItemsDriveLetters(usedLetters);
			UpdateGlobalDriveLetters(usedLetters);

			RememberLastGlobalSelection();
		}

		private void UpdateOccupiedLetters()
		{
			GetLocalDriveLetters();
			GetConfiguredDriveLetters();
		}

		private void UpdateRowItemsDriveLetters(IEnumerable<char> usedLetters)
		{
			foreach (NetDriveRowItemModel currentRow in DataGrid.ItemsSource as ObservableCollection<NetDriveRowItemModel>)
			{
				var freshLetters = new List<char>(_lettersAZ);

				foreach (char letter in usedLetters)
				{
					if (currentRow.Drive.Info.Letter != letter)
						freshLetters.Remove(letter);
				}

				currentRow.AvailableLetters = freshLetters;
				Log.Debug("Adjusted letters, drive: {drive} list: {letters}", currentRow.Drive.ToString(), freshLetters);
			}
		}

		#endregion
	}
}