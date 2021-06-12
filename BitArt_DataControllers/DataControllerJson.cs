using BitArt_DataControllers.interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BitArt_DataControllers
{
	public class DataControllerJson : IDatastore
	{
		public List<INetDrive> Get()
		{
			var driveList = new List<INetDrive>
			{
				new NetDriveModel("H:", "dp-nas10", "home"),
				new NetDriveModel("M:", "dp-nas10", "music"),
				new NetDriveModel("V:", "dp-nas10", "video"),
				new NetDriveModel("Q:", "dp-gamlap", "_share")
			};

			return driveList;
		}

		public List<T> ToJson<T>(byte[] byteArray) where T : new()
		{
			MemoryStream stream = new MemoryStream(byteArray);

			JsonSerializer se = new JsonSerializer();

			StreamReader re = new StreamReader(stream);
			JsonTextReader reader = new JsonTextReader(re);
			return se.Deserialize<List<T>>(reader);
		}

		public bool Save(List<INetDrive> netDrives)
		{
			string jsonString = JsonConvert.SerializeObject(netDrives);
			Console.WriteLine(jsonString);
			return true;
		}
	}
}