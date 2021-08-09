using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.navigation
{
	public class NavigationDataModel
	{
		public NavigationDataModel(string nameOf, Func<ContentControl> view, Func<ViewModelBase> viewModel)
		{
			NameOf = nameOf;
			View = view;
			ViewModel = viewModel;
		}

		public string NameOf { get; }
		public Func<ContentControl> View { get; }
		public Func<ViewModelBase> ViewModel { get; }
	}
}