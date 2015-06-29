
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace VowApp2
{
	[Activity (Label = "CreateAccountActivity", Name="com.vow.vow_app_2.CreateAccountActivity")]			
	public class CreateAccountActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.CreateAccount);

			//get UI elements
			Button createAccountButton = FindViewById<Button>(Resource.Id.CreateAccountButton);

			createAccountButton.Click += (sender, e) =>
			{
				//get UI inputs
				TextView emailAddressInput = FindViewById<TextView>(Resource.Id.accountEmailAddressInput);
				TextView passwordInput = FindViewById<TextView>(Resource.Id.accountPasswordInput);
				TextView confirmPasswordInput = FindViewById<TextView>(Resource.Id.accountConfirmPasswordInput);

				//account inputs
				string emailAddress = emailAddressInput.Text;
				string password = passwordInput.Text;
				string confirmPassword = confirmPasswordInput.Text;

				//validate input
				bool validInput = true;
				if (!Regex.IsMatch(emailAddress, "^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,4}$", RegexOptions.IgnoreCase))
				{
					validInput = false;
					Toast.MakeText(this, "Please enter a valid email address", ToastLength.Long).Show ();
				}
				if (validInput && password.Length < 8)
				{
					validInput = false;
					Toast.MakeText(this, "Password must be at least 8 characters", ToastLength.Long).Show ();
				}
				if (validInput && !password.Equals(confirmPassword))
				{
					validInput = false;
					Toast.MakeText(this, "Passwords must match", ToastLength.Long).Show ();
				}

				//create the account with server
				if (validInput && CreateAccount(emailAddress, password))
				{
					var intent = new Intent(this, typeof(MainActivity));
					StartActivity(intent);
				}
			};
		}

		//TODO: implement this with the backend service
		private bool CreateAccount(string emailAddress, string password)
		{
			//encrypt the password (maybe both inputs)
			//send it to the backend service
			//it should return an error if the email address is not available

			return true;
		}
	}
}

