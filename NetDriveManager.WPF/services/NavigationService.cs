using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace NetDriveManager.WPF.services
{
	public class NavigationService : INavigationService
	{
		public void Configure(string key, Type windowType) => windows.Add(key, windowType);

		private Dictionary<string, Type> windows { get; } = new Dictionary<string, Type>();

		private readonly IServiceProvider _serviceProvider;

		public NavigationService(IServiceProvider serviceProvider)
		{
			this._serviceProvider = serviceProvider;
		}

		public async Task ShowAsync(string windowKey, object parameter = null)
		{
			var window = await GetAndActivateWindowAsync(windowKey, parameter);
			//TODO: auto implement viewmodel how?
			//string vmName = windowKey + "ViewModel";
			//ViewModelBase vm = nameof(vmName);
			//ViewModelBase viewModel = _serviceProvider.GetService<nameof(vmName)>();
			//window.DataContext = viewModel;
			window.Show();
		}

		public async Task<bool?> ShowDialogAsync(string windowKey, object parameter = null)
		{
			var window = await GetAndActivateWindowAsync(windowKey, parameter);
			return window.ShowDialog();
		}

		private async Task<Window> GetAndActivateWindowAsync(string windowKey, object parameter = null)
		{
			var window = _serviceProvider.GetRequiredService(windows[windowKey])
				as Window;

			if (window.DataContext is IActivable activable)
			{
				await activable.ActivateAsync(parameter);
			}

			return window;
		}
	}
}