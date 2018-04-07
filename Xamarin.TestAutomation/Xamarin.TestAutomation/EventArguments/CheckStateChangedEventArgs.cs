using System;

namespace Mobile.EventArguments
{
	public class CheckStateChangedEventArgs : EventArgs
	{
		private bool _isChecked;
		public bool IsChecked
		{
			get { return _isChecked; }
		}

		public CheckStateChangedEventArgs(bool IsChecked)
		{
			_isChecked = IsChecked;
		}
	}
}
