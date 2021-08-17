namespace NetDriveManager.Monitor
{
	public interface INetdriveMonitorSettings
	{
		bool IsDebug { get; set; }
		string JsonFile4Drives { get; set; }
	}
}