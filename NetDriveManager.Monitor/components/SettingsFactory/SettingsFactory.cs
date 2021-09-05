using NetDriveManager.Monitor.Models;

namespace NetDriveManager.Monitor.components.SettingsFactory
{
	public class SettingsFactory
	{
		#region Public Methods

		public static INetDriveMonitorSettings Create()
		{
			var settings = new NetDriveMonitorSettings();

			settings.IsAutoConnectIfAvailable = true;

			return settings;
		}

		#endregion
	}
}