using Microsoft.Toolkit.Mvvm.ComponentModel;
using NetDriveManager.WPF.Main;
using System;

namespace NetDriveManager.WPF.stores
{
	public class NavigationStore : ObservableObject
	{
		#region Public Events

		public event Action CurrentViewModelChanged;

		#endregion Public Events

		#region Public Properties

		public ViewModelBase CurrentViewModel
		{
			get => _currentViewModel;
			set => UpdateViewModel(value);
		}

		#endregion Public Properties

		#region Private Fields

		private ViewModelBase _currentViewModel;

		#endregion Private Fields

		#region Private Methods

		private void UpdateViewModel(ViewModelBase value)
		{
			_currentViewModel?.Dispose();
			SetProperty(ref _currentViewModel, value);
			CurrentViewModelChanged?.Invoke();
		}

		#endregion Private Methods
	}
}