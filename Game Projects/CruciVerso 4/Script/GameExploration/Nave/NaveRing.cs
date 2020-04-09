using UnityEngine;
using System.Collections;

public class NaveRing : MonoBehaviour {

	
	UISprite ring;

	void Start()
	{
		ring = GetComponent<UISprite>();
		Go.to(ring.transform,0.5f,new GoTweenConfig().scale(new Vector3(60,60,60),false));
		ring.alpha=1f;
		TweenAlpha.Begin(ring.gameObject,0.3f,0).delay = 0.2f;
		Destroy(gameObject,0.6f);
	}
	
	
}
