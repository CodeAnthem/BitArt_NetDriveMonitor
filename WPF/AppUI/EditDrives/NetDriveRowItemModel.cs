using Microsoft.Toolkit.Mvvm.ComponentModel;
using NetDriveManager.Monitor.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace WPF.AppUI.EditDrives
{
	public class NetDriveRowItemModel : ObservableObject
	{
		private List<char> _availableLetters;

		public List<char> AvailableLetters
		{
			get { return _availableLetters; }
			set { SetProperty(ref _availableLetters, value); }
		}

		public INetDrive Drive { get; }

		public NetDriveRowItemModel(INetDrive drive)
		{
			Drive = drive;
			AvailableLetters = new List<char>("ABCDEFGHIJKLMNOPQRSTUVWXYZ");
		}
	}
}