﻿using System;
using VowApp2;

using Android.App;
using Android.Util;
using Android.Content;
using Android.OS;
using Android.Locations;

namespace VowApp2.Services
{
	[Service]
	public class VowLocationService : Service, ILocationListener
	{
		IBinder binder;

		protected LocationManager locMgr = Android.App.Application.Context.GetSystemService ("location") as LocationManager;
		public event EventHandler<LocationChangedEventArgs> LocationChanged = delegate { };
		public event EventHandler<ProviderDisabledEventArgs> ProviderDisabled = delegate { };
		public event EventHandler<ProviderEnabledEventArgs> ProviderEnabled = delegate { };
		public event EventHandler<StatusChangedEventArgs> StatusChanged = delegate { };

		public override IBinder OnBind (Intent intent)
		{
			binder = new VowLocationServiceBinder (this);
			return binder;
		}

		public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int startId)
		{
			return StartCommandResult.Sticky;
		}

		public void StartLocationUpdates () {       
			var locationCriteria = new Criteria();                    
			locationCriteria.Accuracy = Accuracy.NoRequirement;        
			locationCriteria.PowerRequirement = Power.NoRequirement;                    
			var locationProvider = locMgr.GetBestProvider(locationCriteria, true);
			locMgr.RequestLocationUpdates(locationProvider, 1000, 0, this);
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			this.LocationChanged (this, new LocationChangedEventArgs (location));
			/*Log.Debug (logTag, String.Format ("Latitude is {0}", location.Latitude));
			Log.Debug (logTag, String.Format ("Longitude is {0}", location.Longitude));
			Log.Debug (logTag, String.Format ("Altitude is {0}", location.Altitude));
			Log.Debug (logTag, String.Format ("Speed is {0}", location.Speed));
			Log.Debug (logTag, String.Format ("Accuracy is {0}", location.Accuracy));
			Log.Debug (logTag, String.Format ("Bearing is {0}", location.Bearing));*/
		}

		public void OnProviderDisabled (string provider)
		{
			this.ProviderDisabled (this, new ProviderDisabledEventArgs (provider));
		}

		public void OnProviderEnabled (string provider)
		{
			this.ProviderEnabled (this, new ProviderEnabledEventArgs (provider));
		}

		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			this.StatusChanged (this, new StatusChangedEventArgs (provider, status, extras));
		}

		public override void OnDestroy ()
		{
			locMgr.RemoveUpdates (this);
			base.OnDestroy ();
			// cleanup code
		}
	}
}

