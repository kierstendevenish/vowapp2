package md563e62913203f2265f857b352b9f2fc19;


public class VowLocationServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("VowApp2.Services.VowLocationServiceBinder, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", VowLocationServiceBinder.class, __md_methods);
	}


	public VowLocationServiceBinder () throws java.lang.Throwable
	{
		super ();
		if (getClass () == VowLocationServiceBinder.class)
			mono.android.TypeManager.Activate ("VowApp2.Services.VowLocationServiceBinder, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public VowLocationServiceBinder (md563e62913203f2265f857b352b9f2fc19.VowLocationService p0) throws java.lang.Throwable
	{
		super ();
		if (getClass () == VowLocationServiceBinder.class)
			mono.android.TypeManager.Activate ("VowApp2.Services.VowLocationServiceBinder, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "VowApp2.Services.VowLocationService, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

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
