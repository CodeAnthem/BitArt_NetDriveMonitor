using BitArt_Netdrive_Helpers.models;
using System.Collections.Generic;

namespace ConsoleTesting
{
	public static class NetdriveGenerator
	{
		public static IEnumerable<NetDriveBuddyModel> GetDummyData()
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