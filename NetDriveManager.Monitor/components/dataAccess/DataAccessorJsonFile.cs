using Newtonsoft.Json;
using Serilog;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace NetDriveManager.Monitor.components.dataAccess
{
	internal class DataAccessorJsonFile : IDataAccessor
	{
		private static readonly string _applicationPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
		private const string _filePath = @"Save\drives.json";
		private readonly string _jsonFilePath;

		private readonly FileInfo _jsonFile;

		public DataAccessorJsonFile()
		{
			_jsonFilePath = Path.Combine(_applicationPath, _filePath);
			_jsonFile = new FileInfo(_jsonFilePath);
		}

		public List<NetdriveMonitorModel> GetDrives0RNull()
		{
			if (_jsonFile.Exists)
			{
				string jsonString = File.ReadAllText(_jsonFile.FullName);
				return JsonConvert.DeserializeObject<List<NetdriveMonitorModel>>(jsonString);
			}
			Log.Warning("No JSON file found - returning null");
			return null;
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
	}
}