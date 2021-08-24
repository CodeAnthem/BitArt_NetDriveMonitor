using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace WPF.Main
{
	public class ViewModelBase : ObservableObject
	{
		public virtual void Dispose()
		{
			//TODO: Add Dispose for view model base and research why tho
		}
	}
}