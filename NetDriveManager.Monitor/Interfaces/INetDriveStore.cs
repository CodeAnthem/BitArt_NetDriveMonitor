using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NetDriveManager.Monitor.Interfaces
{
	public interface INetDriveStore
	{
		#region Public Properties

		ObservableCollection<INetDrive> Drives { get; }
		Dictionary<string, List<INetDrive>> DrivesByHostDict { get; set; }

		#endregion

		#region Public Methods

		void Clear();

		List<INetDrive> GetDrivesOfHostOREMPTY(string hostnameOrIP);

		List<INetDrive> GetDrivesOfHostOREMPTY2(string hostnameOrIP);

		List<string> GetRegisteredHostsOREMPTY();

		void Register(INetDrive drive);

		#endregion
	}
}