using Microsoft.Extensions.DependencyInjection;

namespace WPF.Main
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection Add_UI_Main(this IServiceCollection services)
		{
			// Content Controller Config: Main
			services.AddScoped<CCCMain>();

			services.AddSingleton<MainWindowModel>();
			services.AddTransient<MainWindow>();
			services.AddScoped<MainContentStore>();

			return services;
		}
	}
}