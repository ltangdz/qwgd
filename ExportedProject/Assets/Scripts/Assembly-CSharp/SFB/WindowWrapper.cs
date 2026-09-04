using System;

#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
using System.Windows.Forms;
#endif

namespace SFB
{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
	public class WindowWrapper : IWin32Window
	{
		private IntPtr _hwnd;

		public IntPtr Handle => _hwnd;

		public WindowWrapper(IntPtr handle)
		{
			_hwnd = handle;
		}
	}
#endif
}
