using Microsoft.Toolkit.Mvvm.ComponentModel;
using NetDriveManager.WPF.viewModels;
using System;

namespace NetDriveManager.WPF.stores
{
	public class NavigationStoreModal : ObservableObject
	{
		public event Action CurrentViewModelChanged;

		public bool IsOpen => CurrentViewModel != null;

		public ViewModelBase CurrentViewModel
		{
			get => _currentViewModel;
			set => UpdateViewModel(value);
		}

		public void Close() => CurrentViewModel = null; // why not dispose?

		private ViewModelBase _currentViewModel;

		private void UpdateViewModel(ViewModelBase value)
		{
			_currentViewModel?.Dispose();
			SetProperty(ref _currentViewModel, value);
			CurrentViewModelChanged?.Invoke();
		}
	}
}