using System;
using Android.OS;
using VowApp2;

namespace VowApp2.Services
{
	//This is our Binder subclass, the LocationServiceBinder
	public class VowLocationServiceBinder : Binder
	{
		public VowLocationService Service
		{
			get { return this.service; }
		} protected VowLocationService service;

		public bool IsBound { get; set; }

		// constructor
		public VowLocationServiceBinder (VowLocationService service)
		{
			this.service = service;
		}
	}
}