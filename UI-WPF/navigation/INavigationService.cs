using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace UI_WPF.navigation
{
	public interface INavigationService
	{
		object Navigate<T>();
	}
}