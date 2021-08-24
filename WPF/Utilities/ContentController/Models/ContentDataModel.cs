using WPF.Main;
using System;

namespace WPF.Utilities.ContentController.Models
{
	public class ContentDataModel : IContentDataModel
	{
		public ContentDataModel(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc)
		{
			Name = name;
			CreateViewFunc = createViewFunc;
			CreateViewModelFunc = createViewModelFunc;
		}

		public string Name { get; }
		public Func<object> CreateViewFunc { get; }
		public Func<ViewModelBase> CreateViewModelFunc { get; }
	}
}