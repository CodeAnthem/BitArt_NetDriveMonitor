using Serilog;
using System;

namespace NetDriveManager.Monitor.components.NetDriveMounter
{
	public class NetDriveMounterBase
	{
		#region Public Methods

		public bool IsLetterUsed(string letter)
		{
			letter = letter[0] + @":\";
			if (!IsLetterValid(letter))
			{
				Log.Error("Wrong input for letter: {letter}", letter);
				return false;
			}

			string[] driveList = GetDrivesLogical();
			for (int i = 0; i < driveList.Length; i++)
			{
				if (letter == driveList[i])
				{
					return true;
				}
			}
			return false;
		}

		public bool IsLetterValid(string letter)
		{
			char c = letter[0];

			// the only valid drive letters are a-z && A-Z.
			return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
		}

		#endregion

		#region Private Methods

		private string[] GetDrivesLogical()
		{
			return Environment.GetLogicalDrives(); // get all logical drives
		}

		#endregion
	}
}