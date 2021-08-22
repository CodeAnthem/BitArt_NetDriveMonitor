using NetDriveManager.WPF.utilities.contentController.models;
using NetDriveManager.WPF.viewModels;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace NetDriveManager.WPF.utilities.contentController.stores
{
	public class ContentStore : ContentStoreBase, IContentStore
	{
		private readonly Dictionary<string, List<IContentDataModel>> _contentDict = new Dictionary<string, List<IContentDataModel>>();

		public bool AddWindow(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc = null)
		{
			IContentDataModel contentData = ValidateAndCreateContentData(name, createViewFunc, createViewModelFunc);
			return Register(nameof(Window), contentData);
		}

		public bool AddUserControl(string name, Func<object> createViewFunc, Func<ViewModelBase> createViewModelFunc = null)
		{
			IContentDataModel contentData = ValidateAndCreateContentData(name, createViewFunc, createViewModelFunc);
			return Register(nameof(UserControl), contentData);
		}

		public IContentDataModel GetContentData(string type, string name)
		{
			bool doesKeyExist = _contentDict.ContainsKey(type);
			_ = doesKeyExist ? true : throw new Exception($"[CC] Type is not registered. View: {name} - Type: {type}");

			IContentDataModel cdm = _contentDict[type].Find(cdm => cdm.Name == name);
			_ = cdm ?? throw new Exception($"[CC] View is not registered. View: {name} - Type: {type}");

			return cdm;
		}

		private bool Register(string type, IContentDataModel contentDataModel)
		{
			bool isWindow = type == nameof(Window);
			bool isUserControl = type == nameof(UserControl);
			if (!(isWindow || isUserControl))
			{
				return false;
			}

			if (!_contentDict.ContainsKey(type))
				_contentDict.Add(type, new List<IContentDataModel>());
			_contentDict[type].Add(contentDataModel);
			return true;
		}

		public int GetCount()
		{
			return _contentDict.Count;
		}
	}
}