namespace NetDriveManager.Monitor
{
	public class DefaultSettings : INetdriveMonitorSettings
	{
		public bool IsDebug { get; set; } = false;
		public string JsonFile4Drives { get; set; } = @"\Save\drives.json";
	}
}