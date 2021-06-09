namespace NetDriveMonitor.interfaces
{
	public interface INetDriveConnection
	{
		public string Letter { get; set; }
		public string Server { get; set; }
		public string Share { get; set; }
	}
}