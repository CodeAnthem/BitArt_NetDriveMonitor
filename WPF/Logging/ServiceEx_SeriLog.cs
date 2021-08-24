using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace WPF
{
	public static class ServiceEx_SeriLog
	{
		public static IServiceCollection Add_SeriLogging(this IServiceCollection services)
		{
			services.AddSingleton<ILogger>(x =>
				new Serilog_Config(x.GetRequiredService<AppInfo>())
					.GetSerilogLoggerConfiguration()
					.CreateLogger()
			);

			return services;
		}
	}
}