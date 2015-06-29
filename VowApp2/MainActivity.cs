using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

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

			//retreive 
			var prefs = Application.Context.GetSharedPreferences("VowApp", FileCreationMode.Private);              
			this.isMonitoringOn = prefs.Contains("monitoringState") ? prefs.GetBoolean("monitoringState", true) : true;
			toggleButton.Checked = this.isMonitoringOn;

			toggleButton.Click += (o, e) => {
				// Perform action on clicks
				if (toggleButton.Checked)
				{
					this.isMonitoringOn = true;
					Toast.MakeText(this, "Location monitoring has been turned on", ToastLength.Short).Show ();
				}
				else
				{
					this.isMonitoringOn = false;
					Toast.MakeText(this, "Location monitoring has been turned off", ToastLength.Short).Show ();
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


