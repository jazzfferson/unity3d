using UnityEngine;
using System.Collections;

public class FadeScreen : MonoBehaviour {

	public static FadeScreen instance;
    public UIPanel fadeSprite;

	void Start () {

		if (instance == null) {instance=this;}
		SetFadeSpriteSize ();
	}
	
	// Update is called once per frame


	void SetFadeSpriteSize()
	{
	
		
	}

	public void Fade(float speed)
	{
        Go.to(fadeSprite, speed, new GoTweenConfig().floatProp("alpha", 1, false));
        Go.to(fadeSprite, speed, new GoTweenConfig().floatProp("alpha", 0, false).setDelay(speed));
	}

}
