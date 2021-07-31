using NetDriveMonitor.interfaces;
using NetDriveMonitor.models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace NetDriveMonitor.components
{
	public class DataAccessJson : IDataAccess
	{
		private FileInfo _jsonFile;

		public DataAccessJson(string path)
		{
			_jsonFile = new FileInfo(path);
		}

		public IEnumerable<NetDriveMonitorModel> GetDrives()
		{
			if (_jsonFile.Exists)
			{
				string jsonString = File.ReadAllText(_jsonFile.FullName);
				var drives = JsonConvert.DeserializeObject<List<NetDriveMonitorModel>>(jsonString);
				return drives;
			}
			Debug.WriteLine($"No drives found");
			return Enumerable.Empty<NetDriveMonitorModel>();
		}

		public bool SaveDrives(IEnumerable<NetDriveMonitorModel> netDrives)
		{
			var anyDrives = netDrives.Any<NetDriveMonitorModel>();
			if (anyDrives)
			{
				if (!Directory.Exists(_jsonFile.DirectoryName))
				{
					Directory.CreateDirectory(_jsonFile.DirectoryName);
				}

				string jsonString = JsonConvert.SerializeObject(netDrives, Formatting.Indented);
				File.WriteAllText(_jsonFile.FullName, jsonString);
				return true;
			}
			return false;
		}
	}
}