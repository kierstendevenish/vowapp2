
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VowApp2
{
	[Activity (Label = "LoginActivity", MainLauncher = true, Icon = "@drawable/icon", Name="com.vow.vow_app_2.LoginActivity")]			
	public class LoginActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Login);

			//get UI elements
			Button loginButton = FindViewById<Button>(Resource.Id.LoginButton);
			Button goToCreateAccountButton = FindViewById<Button>(Resource.Id.GoToCreateAccountButton);
			TextView loginEmail = FindViewById<TextView>(Resource.Id.loginEmail);
			TextView loginPassword = FindViewById<TextView>(Resource.Id.loginPassword);

			loginButton.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(MainActivity));
				if (UserLogin(loginEmail.Text, loginPassword.Text))
				{
					StartActivity(intent);
				}
				else
				{
					Toast.MakeText(this, "Cannot login with this username/password", ToastLength.Long).Show ();
				}
			};

			goToCreateAccountButton.Click += (sender, e) =>
			{
				var intent = new Intent(this, typeof(CreateAccountActivity));
				StartActivity(intent);
			};
		}

		//TODO: Implement with backend service
		protected Boolean UserLogin(string loginEmail, string loginPassword)
		{
			if (!loginEmail.Equals ("a")) 
			{
				return false;
			}

			if (!loginPassword.Equals ("123")) 
			{
				return false;
			}

			return true;
		}
	}
}

