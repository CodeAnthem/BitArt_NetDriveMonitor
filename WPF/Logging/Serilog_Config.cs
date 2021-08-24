using Serilog;
using Serilog.Events;
using System.Diagnostics;
using WPF.Logging;

namespace WPF
{
	public class Serilog_Config
	{
		private const string _breakLine = "---------------------------------------------------------------------------------------";
		private readonly AppInfo _appInfo;

		public Serilog_Config(AppInfo appInfo)
		{
			_appInfo = appInfo;
		}

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
					outputTemplate: $"[Debug: {_appInfo.AppName}] [{{Level:u4}}] {{Caller}}> {{Message:lj}}{{NewLine}}{{Exception}}"
				);
		}

		private LoggerConfiguration AddFileLogging(LoggerConfiguration serCfg)
		{
			return serCfg
				.WriteTo.File(
				   $@"Log\{_appInfo.AppName}_.log",
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