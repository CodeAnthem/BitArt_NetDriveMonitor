using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.WPF;
using System;
using System.Windows.Media.TextFormatting;

namespace NetDriveManager.WPF
{
	public static class LoggingConfigurator
	{
		//private static readonly string _appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
		private const string _appName = "Netdrive Manager";

		private static readonly string _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

		private const string _outputTemplateFile = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] - {Message:lj}{NewLine}{Exception}";
		private static readonly string _outputTemplateConsole = $"[Debug: {_appName}] [{{Level:u5}}]  - {{SourceContext}} {{Message:lj}}{{NewLine}}{{Exception}}";

		private const string _breakLine = "---------------------------------------------------------------------------------------";

		public static IServiceCollection AddSerilogLogging(this IServiceCollection services)
		{
			ILogger logger = CreateLogger();
			services.AddSingleton<ILogger>(logger);

			return services;
		}

		private static ILogger CreateLogger()
		{
			ILogger logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
				.Enrich.FromLogContext()
				.WriteTo.Debug(
					restrictedToMinimumLevel: LogEventLevel.Verbose,
					outputTemplate: _outputTemplateConsole

				)
				//.Enrich.With(new ThreadIdEnricher()
				.WriteTo.File(
				   $@"Log\{_appName}_.log",
					restrictedToMinimumLevel: LogEventLevel.Information,
					rollingInterval: RollingInterval.Day,
					retainedFileCountLimit: 14,
					fileSizeLimitBytes: 100000,
					buffered: true,
					outputTemplate: _outputTemplateFile,
					rollOnFileSizeLimit: false
				)
				.WriteToSimpleAndRichTextBox(
				)
				.CreateLogger();

			LogLine(logger);
			logger.Information($"{_appName} - Version: {_version} - Logger created!");
			return logger;
		}

		public static void LogLine(ILogger logger = null)
		{
			if (logger == null)
			{
				Log.Information(_breakLine);
			}
			else
			{
				logger.Information(_breakLine);
			}
		}
	}
}