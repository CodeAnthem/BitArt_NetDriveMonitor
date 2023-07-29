using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF.Main;

namespace WPF.AppUI.EditDrives
{
	/// <summary>
	/// Interaction logic for EditDrivesDataGridControl.xaml
	/// </summary>
	public partial class EditDrivesDataGridControl : UserControl
	{
		public EditDrivesAvailableDriveLetterManager LetterManager { get; set; } = Ioc.Default.GetService<EditDrivesAvailableDriveLetterManager>();

		public EditDrivesDataGridControl()
		{
			//LetterManager = Ioc.Default.GetService<EditDrivesAvailableDriveLetterManager>();

			InitializeComponent();
			SubscribeToCollectionSetup();
			EditDrivesViewModel vm = Root.DataContext as EditDrivesViewModel;
			//vm.SelectedRow.AvailableLetters.
		}

		private void SubscribeToCollectionSetup()
		{
			var sourceCollection = NetDriveDataGrid.ItemsSource as ObservableCollection<NetDriveRowItemModel>;
			if (sourceCollection == null)
				return;
			sourceCollection.CollectionChanged += NetDriveDataGrid_CollectionChanged;
		}

		private void NetDriveDataGrid_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
		}

		private void RowLetterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			// Update this row letters Update new letters
		}
	}
}