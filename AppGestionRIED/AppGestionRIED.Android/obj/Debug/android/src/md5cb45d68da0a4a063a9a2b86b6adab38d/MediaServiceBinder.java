package md5cb45d68da0a4a063a9a2b86b6adab38d;


public class MediaServiceBinder
	extends android.os.Binder
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Plugin.MediaManager.MediaServiceBinder, Plugin.MediaManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", MediaServiceBinder.class, __md_methods);
	}


	public MediaServiceBinder ()
	{
		super ();
		if (getClass () == MediaServiceBinder.class)
			mono.android.TypeManager.Activate ("Plugin.MediaManager.MediaServiceBinder, Plugin.MediaManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}

	public MediaServiceBinder (md5cb45d68da0a4a063a9a2b86b6adab38d.MediaServiceBase p0)
	{
		super ();
		if (getClass () == MediaServiceBinder.class)
			mono.android.TypeManager.Activate ("Plugin.MediaManager.MediaServiceBinder, Plugin.MediaManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Plugin.MediaManager.MediaServiceBase, Plugin.MediaManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", this, new java.lang.Object[] { p0 });
	}

	private java.util.ArrayList refList;
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
