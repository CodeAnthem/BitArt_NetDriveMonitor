using System.Collections.Generic;

namespace BitArt_DataControllers.interfaces
{
	public interface IDatastore
	{
		public static List<T> Get();

		public bool Save(List<T> netDrives);
	}
}