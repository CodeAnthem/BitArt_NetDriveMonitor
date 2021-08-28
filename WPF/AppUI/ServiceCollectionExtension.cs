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
			services.AddSingleton<FooterView>();
			services.AddTransient<FooterViewModel>();

			// Home
			services.AddTransient<HomeView>();
			services.AddSingleton<HomeViewModel>();

			// Settings
			services.AddTransient<EditDrivesView>();
			services.AddTransient<EditDrivesViewModel>();

			// Edit Drives

			return services;
		}
	}
}