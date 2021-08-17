using Newtonsoft.Json;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetDriveManager.Monitor.components.dataAccess
{
	internal class DataAccessJsonFile : IDataAccess
	{
		public List<NetdriveMonitorModel> GetDrives0REmptyList()
		{
			if (_jsonFile.Exists)
			{
				string jsonString = File.ReadAllText(_jsonFile.FullName);
				return JsonConvert.DeserializeObject<List<NetdriveMonitorModel>>(jsonString);
			}
			Debug.WriteLine("No JSON file found - returning empty list");
			return new List<NetdriveMonitorModel>();
		}

		public bool SaveDrives(List<NetdriveMonitorModel> netdrivesList)
		{
			var anyDrives = netdrivesList.Count > 0;
			if (anyDrives)
			{
				if (!Directory.Exists(_jsonFile.DirectoryName))
				{
					Directory.CreateDirectory(_jsonFile.DirectoryName);
				}

				string jsonString = JsonConvert.SerializeObject(netdrivesList, Formatting.Indented);
				File.WriteAllText(_jsonFile.FullName, jsonString);
				return true;
			}
			return false;
		}

		private FileInfo _jsonFile;

		public DataAccessJsonFile(string pathOfJsonFile)
		{
			_jsonFile = new FileInfo(pathOfJsonFile);
		}
	}
}