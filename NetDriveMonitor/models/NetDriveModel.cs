using NetDriveMonitor.interfaces;

namespace NetDriveMonitor.models
{
	public class NetDriveModel : INetDrive
	{
		public NetDriveModel(string letter, string share, string hostName)
		{
			Letter = letter;
			Share = share;
			HostName = hostName;
		}

		#region Public Properties

		public string Letter { get; set; }
		public string Share { get; set; }
		public string HostName { get; set; }

		#endregion Public Properties
	}
}