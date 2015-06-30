using System;
using Android.OS;

namespace VowApp2.Services
{
	public class VowServiceConnectedEventArgs : EventArgs
	{
		public IBinder Binder { get; set; }
	}
}