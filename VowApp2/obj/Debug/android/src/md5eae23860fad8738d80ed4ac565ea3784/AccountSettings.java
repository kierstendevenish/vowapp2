package md5eae23860fad8738d80ed4ac565ea3784;


public class AccountSettings
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("VowApp2.AccountSettings, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", AccountSettings.class, __md_methods);
	}


	public AccountSettings () throws java.lang.Throwable
	{
		super ();
		if (getClass () == AccountSettings.class)
			mono.android.TypeManager.Activate ("VowApp2.AccountSettings, VowApp2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

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
