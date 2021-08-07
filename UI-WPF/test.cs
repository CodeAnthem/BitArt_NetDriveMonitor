using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace UI_WPF
{
	internal class test
	{
		public void Test()
		{
			Frame myframe = new Frame();
			myframe.Navigate(myframe);
		}
	}
}