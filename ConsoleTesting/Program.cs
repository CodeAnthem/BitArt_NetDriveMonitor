using System;
using System.Linq;

namespace ConsoleTesting
{
	internal class Program : TestBase
	{
		private static void Main(string[] args)
		{
			// ------------------------------------
			// Testing: Net Drive Buddy
			// ------------------------------------
			var ndb = new NetdriveBuddyTest();
			ndb.NetdriveList = NetdriveGenerator.GetDummyData().ToList();
			ndb.SkipReadKey = false;
			ndb.GetAllTest();
			ndb.RemoveAllTest();
			ndb.AddTest();

			// ------------------------------------
			// Testing: Net Work Buddy
			// ------------------------------------
			var nwb = new NetworkBuddyTest();
			//TODO: add network tests

			// ------------------------------------
			StopApp();
			// ------------------------------------
		}
	}
}