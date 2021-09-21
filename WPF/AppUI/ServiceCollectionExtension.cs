using Microsoft.Extensions.DependencyInjection;
using WPF.AppUI.EditDrives;
using WPF.AppUI.Frame;
using WPF.AppUI.Home;

namespace WPF.AppUI
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection Add_UI_App(this IServiceCollection services)
		{
			// Content Controller Config: App
			services.AddScoped<CCCApp>();

			// Frame
			services.AddTransient<HeaderView>();
			services.AddSingleton<HeaderViewModel>();
			services.AddTransient<FooterView>();
			services.AddSingleton<FooterViewModel>();

			// Home
			services.AddTransient<HomeView>();
			services.AddSingleton<HomeViewModel>();

			// Settings

			// Edit Drives
			services.AddTransient<EditDrivesView>();
			services.AddTransient<EditDrivesViewModel>(); // force reload
			services.AddSingleton<EditDrivesAvailableDriveLetterManager>();

			return services;
		}
	}
}