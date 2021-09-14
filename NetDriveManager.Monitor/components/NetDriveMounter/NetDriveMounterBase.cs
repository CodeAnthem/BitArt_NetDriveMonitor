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

		public bool IsLetterUsed(char letter)
		{
			return IsLetterUsed(letter.ToString());
		}

		public bool IsLetterValid(string letter) => IsLetterValid(letter[0]);

		public bool IsLetterValid(char letter)
		{
			// the only valid drive letters are a-z && A-Z.
			return (letter >= 'a' && letter <= 'z') || (letter >= 'A' && letter <= 'Z');
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