namespace NetDriveMonitor.interfaces
{
	public interface INetDrive : IHost
	{
		public string Letter { get; set; }
		public string Share { get; set; }
	}
}