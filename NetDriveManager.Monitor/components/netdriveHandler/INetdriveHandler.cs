namespace NetDriveManager.Monitor.components.netdriveHandler
{
	public interface INetdriveHandler
	{
		bool ConnectDrive(NetdriveMonitorModel drive);
		bool DisconnectDrive(NetdriveMonitorModel drive);
	}
}