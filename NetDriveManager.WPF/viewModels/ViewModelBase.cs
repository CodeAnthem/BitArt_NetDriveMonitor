using Microsoft.Toolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetDriveManager.WPF.viewModels
{
	public class ViewModelBase : ObservableObject
	{
		public virtual void Dispose()
		{
		}
	}
}