using UnityEngine;
using System.Collections;

public class Advertising : MonoBehaviour {
	
	
	void Start()
	{
	   WP8Statics.EnableAd2();
	}
	
	void Yes()
	{
		
	}
	void No()
	{
	   LoadScreenManager.instance.LoadScreenWithFadeInOut("Joguinho",0.3f);
	   WP8Statics.DisableAd2();
	}
}
