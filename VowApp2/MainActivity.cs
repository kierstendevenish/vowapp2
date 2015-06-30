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

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);

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

				App.Current.LocationService.StartLocationUpdates ();

				App.Current.LocationService.LocationChanged += HandleLocationChanged;
				App.Current.LocationService.ProviderDisabled += HandleProviderDisabled;
				App.Current.LocationService.ProviderEnabled += HandleProviderEnabled;
				App.Current.LocationService.StatusChanged += HandleStatusChanged;
			};

			Toast.MakeText(this, "Location monitoring has been turned on", ToastLength.Short).Show ();
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
			//Log.Debug (logTag, "Foreground updating");

			// these events are on a background thread, need to update on the UI thread
			/*RunOnUiThread (() => {
				latText.Text = String.Format ("Latitude: {0}", location.Latitude);
				longText.Text = String.Format ("Longitude: {0}", location.Longitude);
				altText.Text = String.Format ("Altitude: {0}", location.Altitude);
				speedText.Text = String.Format ("Speed: {0}", location.Speed);
				accText.Text = String.Format ("Accuracy: {0}", location.Accuracy);
				bearText.Text = String.Format ("Bearing: {0}", location.Bearing);
			});*/

		}

		public void HandleProviderDisabled(object sender, ProviderDisabledEventArgs e)
		{
			//Log.Debug (logTag, "Location provider disabled event raised");
		}

		public void HandleProviderEnabled(object sender, ProviderEnabledEventArgs e)
		{
			//Log.Debug (logTag, "Location provider enabled event raised");
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


