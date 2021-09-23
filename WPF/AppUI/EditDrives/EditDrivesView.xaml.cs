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

		#endregion
	}
}