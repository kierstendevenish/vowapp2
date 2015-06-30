package md563e62913203f2265f857b352b9f2fc19;


public class VowLocationServiceConnection
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.content.ServiceConnection
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onServiceConnected:(Landroid/content/ComponentName;Landroid/os/IBinder;)V:GetOnServiceConnected_Landroid_content_ComponentName_Landroid_os_IBinder_Handler:Android.Content.IServiceConnectionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onServiceDisconnected:(Landroid/content/ComponentName;)V:GetOnServiceDisconnected_Landroid_content_ComponentName_Handler:Android.Content.IServiceConnectionInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("VowApp2.Services.VowLocationServiceConnection, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", VowLocationServiceConnection.class, __md_methods);
	}


	public VowLocationServiceConnection () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VowLocationServiceConnection.class)
			mono.android.TypeManager.Activate ("VowApp2.Services.VowLocationServiceConnection, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public VowLocationServiceConnection (md563e62913203f2265f857b352b9f2fc19.VowLocationServiceBinder p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == VowLocationServiceConnection.class)
			mono.android.TypeManager.Activate ("VowApp2.Services.VowLocationServiceConnection, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "VowApp2.Services.VowLocationServiceBinder, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}


	public void onServiceConnected (android.content.ComponentName p0, android.os.IBinder p1)
	{
		n_onServiceConnected (p0, p1);
	}

	private native void n_onServiceConnected (android.content.ComponentName p0, android.os.IBinder p1);


	public void onServiceDisconnected (android.content.ComponentName p0)
	{
		n_onServiceDisconnected (p0);
	}

	private native void n_onServiceDisconnected (android.content.ComponentName p0);

	java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
