using Microsoft.Extensions.DependencyInjection;
using WPF.Utilities.ContentController.Services;
using WPF.Utilities.ContentController.Stores;

namespace WPF.Utilities.ContentController
{
	public static class ServiceCollectionExtension
	{
		public static IServiceCollection Add_ContentController(this IServiceCollection services)
		{
			services.AddSingleton<IContentStore, ContentStore>();
			services.AddSingleton<IContentControllerService, ContentControllerService>();

			return services;
		}
	}
}