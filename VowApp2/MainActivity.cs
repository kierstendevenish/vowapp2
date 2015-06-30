using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;

using VowApp2.Services;

namespace VowApp2
{
	[Activity (Label = "Vow App", Name="com.vow.vow_app_2.MainActivity")]
	public class MainActivity : Activity
	{
		bool isMonitoringOn;
		ToggleButton toggleButton;
		bool insideRadius;

		string Provider;

		double testLongitudeValue;
		double testLatitudeValue;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

			insideRadius = false;

			// Get our UI controls from the loaded layout:
			toggleButton = FindViewById<ToggleButton>(Resource.Id.ToggleButton);
			Button accountButton = FindViewById<Button>(Resource.Id.AccountButton);
			Button testButton = FindViewById<Button>(Resource.Id.TestButton);

			//retrieve 
			var prefs = Application.Context.GetSharedPreferences("VowApp", FileCreationMode.Private);              
			this.isMonitoringOn = prefs.Contains("monitoringState") ? prefs.GetBoolean("monitoringState", true) : true;
			toggleButton.Checked = this.isMonitoringOn;

			toggleButton.Click += (o, e) => {
				// Perform action on clicks
				if (toggleButton.Checked)
				{
					this.isMonitoringOn = true;
					StartLocationMonitoringService();
					Toast.MakeText(this, "Location monitoring has been turned on", ToastLength.Short).Show ();
				}
				else
				{
					this.isMonitoringOn = false;
					StopLocationMonitoringService();
				}
			};

			accountButton.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(AccountSettings));
				StartActivity(intent);
			};

			testButton.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(TestActivity));
				StartActivity(intent);
			};

			if (this.isMonitoringOn) {
				StartLocationMonitoringService ();
			}
		}

		private void StartLocationMonitoringService()
		{
			App.Current.LocationServiceConnected += (object sender, VowServiceConnectedEventArgs e) => {
				Toast.MakeText (this, "ServiceConnected Event Raised", ToastLength.Short).Show ();

				Provider = App.Current.LocationService.StartLocationUpdates ();

				App.Current.LocationService.LocationChanged += HandleLocationChanged;
				App.Current.LocationService.ProviderDisabled += HandleProviderDisabled;
				App.Current.LocationService.ProviderEnabled += HandleProviderEnabled;
				App.Current.LocationService.StatusChanged += HandleStatusChanged;
			};
		}

		protected void SendNotification()
		{
			// Instantiate the builder and set notification elements:
			Notification.Builder builder = new Notification.Builder (this)
				.SetContentTitle ("VOW Notification")
				.SetContentText ("This is a simulated VOW notification.")
				.SetSmallIcon (Resource.Drawable.Icon);

			// Build the notification:
			Notification notification = builder.Build ();

			// Get the notification manager:
			NotificationManager notificationManager = (NotificationManager)GetSystemService (NotificationService);

			// Publish the notification:
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);
		}

		private void StopLocationMonitoringService()
		{
			App.Current.LocationService.OnDestroy();
			Toast.MakeText(this, "Location monitoring has been turned off", ToastLength.Short).Show ();
		}

		public void HandleLocationChanged(object sender, LocationChangedEventArgs e)
		{
			Android.Locations.Location location = e.Location;
			Toast.MakeText(this, "Location updated to " + location.Latitude + ", " + location.Longitude, ToastLength.Short).Show ();

			//check the proximity to the offender location
			if (testLongitudeValue != 0.0 && testLatitudeValue != 0.0) 
			{
				Location offenderLocation = new Location(Provider);
				offenderLocation.Longitude = testLongitudeValue;
				offenderLocation.Latitude = testLatitudeValue;
				float meters = location.DistanceTo(offenderLocation);

				if (!insideRadius && (meters < 100)) {
					insideRadius = true;
					SendNotification ();
				} 
				else if (insideRadius && (meters > 100))
				{
					insideRadius = false;
				}
				//Toast.MakeText(this, "Distance = " + meters.ToString() + " meters", ToastLength.Short).Show ();
			}
		}

		public void HandleProviderDisabled(object sender, ProviderDisabledEventArgs e)
		{
			App.Current.LocationService.OnDestroy();

			// Instantiate the builder and set notification elements:
			Notification.Builder builder = new Notification.Builder (this)
				.SetContentTitle ("VOW Notification")
				.SetContentText ("Monitoring has been disabled because your location cannot be determined")
				.SetSmallIcon (Resource.Drawable.Icon);

			// Build the notification:
			Notification notification = builder.Build ();

			// Get the notification manager:
			NotificationManager notificationManager = (NotificationManager)GetSystemService (NotificationService);

			// Publish the notification:
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);
		}

		public void HandleProviderEnabled(object sender, ProviderEnabledEventArgs e)
		{
			StartLocationMonitoringService();

			// Instantiate the builder and set notification elements:
			Notification.Builder builder = new Notification.Builder (this)
				.SetContentTitle ("VOW Notification")
				.SetContentText ("Monitoring has been enabled")
				.SetSmallIcon (Resource.Drawable.Icon);

			// Build the notification:
			Notification notification = builder.Build ();

			// Get the notification manager:
			NotificationManager notificationManager = (NotificationManager)GetSystemService (NotificationService);

			// Publish the notification:
			const int notificationId = 0;
			notificationManager.Notify (notificationId, notification);
		}

		public void HandleStatusChanged(object sender, StatusChangedEventArgs e)
		{
			//Log.Debug (logTag, "Location status changed, event raised");
		}

		protected override void OnRestoreInstanceState (Bundle bundle)
		{
			base.OnRestoreInstanceState(bundle);
			this.isMonitoringOn = (bundle != null) ? bundle.GetBoolean ("monitoringState", true) : true;
			toggleButton.Checked = this.isMonitoringOn;

			this.testLongitudeValue = (bundle != null) ? bundle.GetDouble("offenderLongitude", 0.0) : 0.0;
			this.testLatitudeValue = (bundle != null) ? bundle.GetDouble("offenderLatitude", 0.0) : 0.0;
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			outState.PutBoolean ("monitoringState", this.isMonitoringOn);
			base.OnSaveInstanceState (outState);
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			var prefs = Application.Context.GetSharedPreferences("VowApp", FileCreationMode.Private);
			var prefEditor = prefs.Edit();
			prefEditor.PutBoolean("monitoringState", this.isMonitoringOn);
			prefEditor.Commit();
		}
	}
}


