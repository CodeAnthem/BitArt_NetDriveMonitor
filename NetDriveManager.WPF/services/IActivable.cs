using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetDriveManager.WPF.services
{
	public interface IActivable
	{
		Task ActivateAsync(object parameter);
	}
}