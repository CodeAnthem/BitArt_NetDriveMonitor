using BitArt_Netdrive_Helpers.interfaces;
using BitArt_Netdrive_Helpers.models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTesting
{
	public class NetdriveGenerator
	{
		public static IEnumerable<INetdrive> GetDummyData()
		{
			return new List<NetDriveBuddyModel>
			{
				new NetDriveBuddyModel("H:", "dp-nas10", "home"),
				new NetDriveBuddyModel("V:", "dp-nas10", "video"),
				new NetDriveBuddyModel("S:", "dp-nas10", "software")
			};
		}
	}
}