using Microsoft.Toolkit.Mvvm.DependencyInjection;
using System;
using System.Collections.Generic;
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

namespace WPF.AppUI.EditDrives
{
	/// <summary>
	/// Interaction logic for EditDrivesDataGridControl.xaml
	/// </summary>
	public partial class EditDrivesDataGridControl : UserControl
	{
		public EditDrivesAvailableDriveLetterManager LetterManager { get; set; }

		public EditDrivesDataGridControl()
		{
			LetterManager = Ioc.Default.GetService<EditDrivesAvailableDriveLetterManager>();
			InitializeComponent();
		}

		public string Test { get; set; } = "blablabla";

		private void RowLetterComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
		}
	}
}