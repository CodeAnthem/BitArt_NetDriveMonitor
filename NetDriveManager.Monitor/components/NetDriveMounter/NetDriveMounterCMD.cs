using NetDriveManager.Monitor.Interfaces;
using NetDriveManager.Monitor.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace NetDriveManager.Monitor.components.NetDriveMounter
{
	public class NetDriveMounterCMD : NetDriveMounterBase, INetDriveMounter<NetDriveMounterCMD>
	{
		#region Private Fields

		private readonly INetDriveFactory _factory;

		#endregion

		#region Public Constructors

		public NetDriveMounterCMD(INetDriveFactory factory)
		{
			_factory = factory;
		}

		#endregion

		#region Public Methods

		public bool Add(INetDrive drive)
		{
			if (!IsLetterUsed(drive.Info.Letter))
			{
				string arguments = $@"use {drive.Info.Letter}: \\{drive.Info.HostName}\{drive.Info.Share}";
				int exitCode = NetWithExitCode(arguments);
				if (exitCode == 0)
				{
					Log.Debug("Successfully connected network drive: {drive}", drive);
					return true;
				}
				Log.Error("Failed to connected network drive: {drive}", drive);
				return false;
			}
			Log.Error("Failed to connected network drive: {drive} is already connected", drive);
			return false;
		}

		public IEnumerable<INetDrive> GetAll() // bad method, cause it depends on the OS language
		{
			var driveList = new List<NetDriveModel>();

			var output = NetWithOutput("use");

			var outputPerLine = SplitByNewLine(output);
			foreach (var str in outputPerLine)
			{
				if (str.StartsWith("OK"))
				{
					string[] parts = str.Split(" ",
						StringSplitOptions.RemoveEmptyEntries
					);
					string letter = parts[1];

					string[] address = parts[2].Substring(2).Split(@"\", StringSplitOptions.None);
					string hostName = address[0];
					string share = address[1];

					//var drive = new NetdriveModel(letter, @"\\" + hostName, share);
					var drive = _factory.Create(letter, hostName, share);

					if (drive != null)
					{
						driveList.Add(drive);
					}
				}
			}
			return driveList;
		}

		public bool Remove(string letter)
		{
			if (IsLetterUsed(letter))
			{
				string arguments = $"use /delete {letter[0]}:";
				int exitCode = NetWithExitCode(arguments);
				if (exitCode == 0)
				{
					Log.Debug("Successfully disconnected network drive with letter: {letter}", letter);
					return true;
				}
				Log.Debug("Failed to disconnected network drive with letter: {letter}", letter);
				return false;
			}
			Log.Debug("Failed to disconnected network drive with letter: {letter} (not found)", letter);
			return false;
		}

		public bool RemoveAll()
		{
			const string arguments = "use /delete * /y";
			int exitCode = NetWithExitCode(arguments);
			if (exitCode == 0)
			{
				Log.Debug("Successfully removed all network drives");
				return true;
			}
			Log.Error("Failed to removed all network drives");
			return false;
		}

		#endregion

		#region Private Methods

		private int NetWithExitCode(string arguments, int timeoutInMiliseconds = 0)
		{
			var proc = RunAndReturnItself("Net.exe", arguments, timeoutInMiliseconds);
			return proc.ExitCode;
		}

		private string NetWithOutput(string arguments, int timeoutInMiliseconds = 0)
		{
			var proc = RunAndReturnItself("Net.exe", arguments, timeoutInMiliseconds);
			return proc.StandardOutput.ReadToEnd();
		}

		private Process RunAndReturnItself(string fileName, string arguments, int timeoutInMiliseconds)
		{
			var proc = new Process
			{
				StartInfo = new ProcessStartInfo
				{
					FileName = fileName,
					Arguments = arguments,
					UseShellExecute = false,
					RedirectStandardOutput = true,
					CreateNoWindow = true
				}
			};
			proc.Start();
			if (timeoutInMiliseconds > 0)
			{
				proc.WaitForExit(timeoutInMiliseconds);
			}
			else
			{
				proc.WaitForExit();
			}
			return proc;
		}

		private string[] SplitByNewLine(string output)
		{
			return output.Split(
				new[] { Environment.NewLine },
				StringSplitOptions.None
			);
		}

		#endregion
	}
}