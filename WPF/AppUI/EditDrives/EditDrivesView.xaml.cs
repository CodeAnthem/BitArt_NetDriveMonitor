using Serilog;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Windows.Controls;

namespace WPF.AppUI.EditDrives
{
	/// <summary>
	/// Interaction logic for EditDrivesView.xaml
	/// </summary>
	public partial class EditDrivesView : UserControl
	{
		#region Private Fields

		private readonly List<char> _freshLetters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

		private readonly List<char> _occupiedLettersOfConfiguredDrives = new List<char>();
		private readonly List<char> _occupiedLettersOfLocalDrives = new List<char>();
		private int _lastLetterIndex = -1;
		private bool _isNotFirstTime;

		#endregion

		#region Public Constructors

		public EditDrivesView()
		{
			InitializeComponent();
		}

		#endregion

		#region Private Methods

		private void NewLetterComboBox_Loaded(object sender, System.Windows.RoutedEventArgs e)
		{
			//Update_AllAvailableDriveLetters(true);
		}

		private void NewLetterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//Store_NewLetterComboBox_AvailableDriveLetters();
		}

		private void RowLetterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			//Update_AllAvailableDriveLetters();
		}

		private void DataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			//Update_AllAvailableDriveLetters();
		}

		//private List<char> GetAvailableDriveLetters(IEnumerable<char> usedLetters, NetDriveRowItemModel row = null)
		//{
		//	var freshLetters = new List<char>(_freshLetters);

		// foreach (char occupiedLetter in usedLetters) { if (row != null && (occupiedLetter ==
		// row.Drive.Info.Letter)) continue; freshLetters.Remove(occupiedLetter); }

		// if (row != null) Log.Debug("Adjusted letters for Row: {letter}: {letters}",
		// row.Drive.Info.Letter, freshLetters); else Log.Debug("Adjusted letters for
		// NewComboBoxLetter: {letters}", freshLetters);

		//	return freshLetters;
		//}

		//private void Reset_NewLetterComboBox_SelectedIndex()
		//{
		//	if (NewLetterComboBox.SelectedIndex != -1)
		//		return;

		// if (NewLetterComboBox.SelectedIndex == -1 && _lastLetterIndex == -1 &&
		// NewLetterComboBox.Items.Count > 0) { NewLetterComboBox.SelectedIndex = 0;
		// _lastLetterIndex = 0; Log.Debug("Reseting NewComboBoxLetter index to: 0"); return; }

		// int newLetterIndex = _lastLetterIndex + 1; while (newLetterIndex >=
		// NewLetterComboBox.Items.Count) { newLetterIndex--; }

		//	NewLetterComboBox.SelectedIndex = newLetterIndex;
		//	_lastLetterIndex = newLetterIndex;
		//	Log.Debug("Reseting NewComboBoxLetter index to: {0}", newLetterIndex);
		//}

		//private void Store_NewLetterComboBox_AvailableDriveLetters()
		//{
		//	if (NewLetterComboBox.SelectedIndex != -1)
		//	{
		//		_lastLetterIndex = NewLetterComboBox.SelectedIndex;
		//		char letter = (char)NewLetterComboBox.Items[_lastLetterIndex];
		//		Log.Debug("Stored global selection: {0}|{1}", letter, _lastLetterIndex);
		//	}
		//}

		//private void Update_AllAvailableDriveLetters(bool isFirstTime = false)
		//{
		//	if (isFirstTime)
		//	{
		//		var sourceCollection = DataGrid.ItemsSource as ObservableCollection<NetDriveRowItemModel>;
		//		if (sourceCollection == null)
		//			return;
		//		sourceCollection.CollectionChanged += DataGrid_CollectionChanged;
		//		_isNotFirstTime = true;
		//	}

		// if (_isNotFirstTime) { UpdateOccupiedLettersOfLocalDrives();
		// UpdateOccupiedLettersOfConfiguredDrives(); var usedLetters = _occupiedLettersOfConfiguredDrives.Concat(_occupiedLettersOfLocalDrives);

		// Update_RowLetterComboBox_AvailableLetters(usedLetters); NewLetterComboBox.ItemsSource = GetAvailableDriveLetters(usedLetters);

		//		Reset_NewLetterComboBox_SelectedIndex();
		//	}
		//}

		//private void Update_RowLetterComboBox_AvailableLetters(IEnumerable<char> usedLetters)
		//{
		//	foreach (NetDriveRowItemModel currentRow in DataGrid.ItemsSource as ObservableCollection<NetDriveRowItemModel>)
		//	{
		//		currentRow.AvailableLetters = GetAvailableDriveLetters(usedLetters, currentRow);
		//	}
		//}

		//private void UpdateOccupiedLettersOfConfiguredDrives()
		//{
		//	_occupiedLettersOfConfiguredDrives.Clear();
		//	foreach (var row in DataGrid.ItemsSource as ObservableCollection<NetDriveRowItemModel>)
		//	{
		//		char driveLetter = row.Drive.Info.Letter;
		//		_occupiedLettersOfConfiguredDrives.Add(driveLetter);
		//	}
		//}

		//private void UpdateOccupiedLettersOfLocalDrives()
		//{
		//	_occupiedLettersOfLocalDrives.Clear();
		//	foreach (var drive in DriveInfo.GetDrives())
		//	{
		//		if (drive.DriveType == DriveType.Fixed)
		//		{
		//			char dDriveLetter = drive.Name[0];
		//			_occupiedLettersOfLocalDrives.Add(dDriveLetter);
		//		}
		//	}
		//}

		#endregion
	}
}