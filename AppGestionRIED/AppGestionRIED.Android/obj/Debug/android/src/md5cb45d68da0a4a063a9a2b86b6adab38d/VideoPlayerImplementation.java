package md5cb45d68da0a4a063a9a2b86b6adab38d;


public class VideoPlayerImplementation
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.media.MediaPlayer.OnCompletionListener,
		android.media.MediaPlayer.OnErrorListener,
		android.media.MediaPlayer.OnPreparedListener,
		android.media.MediaPlayer.OnInfoListener
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCompletion:(Landroid/media/MediaPlayer;)V:GetOnCompletion_Landroid_media_MediaPlayer_Handler:Android.Media.MediaPlayer/IOnCompletionListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onError:(Landroid/media/MediaPlayer;II)Z:GetOnError_Landroid_media_MediaPlayer_IIHandler:Android.Media.MediaPlayer/IOnErrorListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onPrepared:(Landroid/media/MediaPlayer;)V:GetOnPrepared_Landroid_media_MediaPlayer_Handler:Android.Media.MediaPlayer/IOnPreparedListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"n_onInfo:(Landroid/media/MediaPlayer;II)Z:GetOnInfo_Landroid_media_MediaPlayer_IIHandler:Android.Media.MediaPlayer/IOnInfoListenerInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Plugin.MediaManager.VideoPlayerImplementation, Plugin.MediaManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", VideoPlayerImplementation.class, __md_methods);
	}


	public VideoPlayerImplementation ()
	{
		super ();
		if (getClass () == VideoPlayerImplementation.class)
			mono.android.TypeManager.Activate ("Plugin.MediaManager.VideoPlayerImplementation, Plugin.MediaManager, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCompletion (android.media.MediaPlayer p0)
	{
		n_onCompletion (p0);
	}

	private native void n_onCompletion (android.media.MediaPlayer p0);


	public boolean onError (android.media.MediaPlayer p0, int p1, int p2)
	{
		return n_onError (p0, p1, p2);
	}

	private native boolean n_onError (android.media.MediaPlayer p0, int p1, int p2);


	public void onPrepared (android.media.MediaPlayer p0)
	{
		n_onPrepared (p0);
	}

	private native void n_onPrepared (android.media.MediaPlayer p0);


	public boolean onInfo (android.media.MediaPlayer p0, int p1, int p2)
	{
		return n_onInfo (p0, p1, p2);
	}

	private native boolean n_onInfo (android.media.MediaPlayer p0, int p1, int p2);

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
