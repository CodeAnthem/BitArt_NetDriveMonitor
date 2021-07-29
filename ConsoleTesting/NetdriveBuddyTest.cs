using BitArt_Netdrive_Helpers;
using BitArt_Netdrive_Helpers.interfaces;
using BitArt_Netdrive_Helpers.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTesting
{
	public class NetdriveBuddyTest : TestBase
	{
		public List<NetDriveBuddyModel> NetdriveList { get; set; } = new List<NetDriveBuddyModel>();
		private static readonly NetdriveBuddy _ndBuddy = new NetdriveBuddy();

		public void AddTest()
		{
			StartMessage("Adding Drives");
			Console.WriteLine($" - Adding {NetdriveList.Count} drives now:");
			foreach (var drive in NetdriveList)
			{
				var result = _ndBuddy.Add(drive);
				Console.WriteLine($" - Result of connecting {drive} was {result}");
			}
			CompleteMessage("Adding Drives");
		}

		public void RemoveTest()
		{
			StartMessage("Removing Drives");
			foreach (var drive in NetdriveList)
			{
				var result = _ndBuddy.Remove(drive);
				Console.WriteLine($" - Result of disconnecting {drive} was {result}");
			}
			CompleteMessage("Removing Drives");
		}

		public void RemoveAllTest()
		{
			StartMessage("Removing All Drives");
			var result = _ndBuddy.RemoveAll();
			if (result)
			{
				Console.WriteLine(" - Removing all drives was successfully");
			}
			else
			{
				Console.WriteLine(" - Removing all drives failed");
			}
			CompleteMessage("Removing All Drives");
		}

		public void GetAllTest()
		{
			StartMessage("Get All Drives");
			var driveList = _ndBuddy.GetAll();
			foreach (var drive in driveList)
			{
				Console.WriteLine($" - Drive found! ({drive.Debug()})");
			}
			CompleteMessage("Get All Drives");
		}
	}
}