using BitArt_Logger_Serilog;
using Serilog;
using Serilog.Events;
using System.Diagnostics;

namespace WPF
{
	public class Serilog_Config
	{
		#region TODO: Global app info class

		//private static readonly string _appName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
		private const string _appName = "Netdrive Manager";

		private static readonly string _version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

		private const string _breakLine = "---------------------------------------------------------------------------------------";

		#endregion TODO: Global app info class

		public LoggerConfiguration GetSerilogLoggerConfiguration()
		{
			LoggerConfiguration serCfg = new LoggerConfiguration();
			BasicConfig(serCfg);
			if (Debugger.IsAttached)
			{
				AddDebug(serCfg);
			}
			AddFileLogging(serCfg);
			//TODO: fix this?

			return serCfg;
		}

		private LoggerConfiguration BasicConfig(LoggerConfiguration serCfg)
		{
			return serCfg
				.MinimumLevel.Verbose()
				.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
			//.Enrich.FromLogContext()
			.Enrich.WithCaller()
			;
		}

		private LoggerConfiguration AddDebug(LoggerConfiguration serCfg)
		{
			return serCfg
				.WriteTo.Debug(
					restrictedToMinimumLevel: LogEventLevel.Debug,
					outputTemplate: $"[Debug: {_appName}] [{{Level:u4}}] {{Caller}}> {{Message:lj}}{{NewLine}}{{Exception}}"
				);
		}

		private LoggerConfiguration AddFileLogging(LoggerConfiguration serCfg)
		{
			return serCfg
				.WriteTo.File(
				   $@"Log\{_appName}_.log",
					restrictedToMinimumLevel: LogEventLevel.Information,
					rollingInterval: RollingInterval.Day,
					retainedFileCountLimit: 14,
					fileSizeLimitBytes: 100000,
					buffered: true,
					outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u4}] - {Message:lj}{NewLine}{Exception}",
					rollOnFileSizeLimit: false
				);
		}
	}
}