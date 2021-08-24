using WPF.Main;
using System;

namespace WPF.Utilities.ContentController.Models
{
	public interface IContentDataModel
	{
		Func<object> CreateViewFunc { get; }
		Func<ViewModelBase> CreateViewModelFunc { get; }
		string Name { get; }
	}
}