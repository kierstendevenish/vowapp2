using System;
using System.Collections.Generic;
using Android.App;
using Android.Locations;
using Android.OS;
using Android.Widget;

namespace VowApp2
{
	[Activity (Label = "Test Activity", Name="com.vow.vow_app_2.TestActivity")]	
	//[Android.Runtime.Register("NOTIFICATION_SERVICE")]
	public class TestActivity : Activity, ILocationListener
	{
		//private static readonly int NotificationId = 1000;
		LocationManager locMgr;
		string Provider;

		Button testNotificationButton;
		EditText testLongitude;
		EditText testLatitude;
		Button setTestCoordinatesButton;

		TextView curLongitude;
		double curLongitudeValue;
		TextView curLatitude;
		double curLatitudeValue;

		TextView curTestLongitude;
		double testLongitudeValue;
		TextView curTestLatitude;
		double testLatitudeValue;

		bool insideRadius;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.TestArea);

			// Get our UI controls from the loaded layout:
			curLongitude = FindViewById<TextView>(Resource.Id.CurrentLongitudeText);
			curLatitude = FindViewById<TextView>(Resource.Id.CurrentLattitudeText);
			testNotificationButton = FindViewById<Button>(Resource.Id.TestNotificationButton);
			curTestLongitude = FindViewById<TextView>(Resource.Id.CurTestLongitude);
			curTestLatitude = FindViewById<TextView>(Resource.Id.CurTestLatitude);
			testLongitude = FindViewById<EditText>(Resource.Id.TestLongitude);
			testLatitude = FindViewById<EditText>(Resource.Id.TestLatitude);
			setTestCoordinatesButton = FindViewById<Button>(Resource.Id.SetTestCoordinatesButton);

			insideRadius = false;
			locMgr = GetSystemService (LocationService) as LocationManager;
			Provider = LocationManager.GpsProvider;

			if(locMgr.IsProviderEnabled(Provider))
			{
				locMgr.RequestLocationUpdates (Provider, 2000, 1, this);
			}
			else
			{
				//Log.Info(tag, Provider + " is not available. Does the device have location services enabled?");
			}

			testNotificationButton.Click += (sender, e) => {
				SendNotification();
			};

			setTestCoordinatesButton.Click += (sender, e) => {
				Double number;
				if (Double.TryParse(testLongitude.Text, out number))
				{
					testLongitudeValue = number*-1.0;
					curTestLongitude.Text = testLongitudeValue.ToString();
					testLongitude.Text = "";
				}
				if (Double.TryParse(testLatitude.Text, out number))
				{
					testLatitudeValue = number;
					curTestLatitude.Text = testLatitudeValue.ToString();
					testLatitude.Text = "";
				}
			};
		}

		public void OnResume (Bundle bundle)
		{
			
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

		protected override void OnRestoreInstanceState (Bundle bundle)
		{
			this.testLongitudeValue = (bundle != null) ? bundle.GetDouble("offenderLongitude", 0.0) : 0.0;
			this.testLatitudeValue = (bundle != null) ? bundle.GetDouble("offenderLatitude", 0.0) : 0.0;
			curTestLongitude.Text = testLongitudeValue.ToString();
			curTestLatitude.Text = testLatitudeValue.ToString();
		}

		protected override void OnSaveInstanceState (Bundle outState)
		{
			outState.PutDouble ("offenderLongitude", this.testLongitudeValue);
			outState.PutDouble ("offenderLatitude", this.testLatitudeValue);
			base.OnSaveInstanceState (outState);
		}

		public void OnLocationChanged (Android.Locations.Location location)
		{
			curLatitudeValue = location.Latitude;
			curLatitude.Text = curLatitudeValue.ToString();
			curLongitudeValue = location.Longitude;
			curLongitude.Text = curLongitudeValue.ToString();

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

		public void OnProviderEnabled (string provider)
		{
			//Toast.MakeText(this, "OnProviderEnabled", ToastLength.Short).Show ();//...
		}
		public void OnProviderDisabled (string provider)
		{
			//Toast.MakeText(this, "OnProviderDisabled", ToastLength.Short).Show ();//...
		}
		public void OnStatusChanged (string provider, Availability status, Bundle extras)
		{
			//Toast.MakeText(this, "OnStatusChanged", ToastLength.Short).Show ();//...
		}
	}
}

