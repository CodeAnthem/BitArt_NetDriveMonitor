using Microsoft.Extensions.DependencyInjection;
using NetDriveManager.WPF.utilities.contentController.services;
using NetDriveManager.WPF.utilities.contentController.stores;

namespace NetDriveManager.WPF.utilities.contentController
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