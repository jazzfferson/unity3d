using UnityEngine;
using System.Collections;

public class FadeScreen : MonoBehaviour {

	public UIFilledSprite fadeScreen;
	public GameObject logo;
	public float alpha
    {

        get; set;

    }
	
	void Start () {
		
		alpha = fadeScreen.alpha;
	}
	
	// Update is called once per frame
	void Update () {
	
		
	fadeScreen.alpha = alpha;
		
	}
	public void FadeToBlack(float duration)
	{
		Go.to(this,duration,new TweenConfig().floatProp("alpha",1,false));
	
	}
	public void FadeToTransparent(float duration)
	{
		Go.to(this,duration,new TweenConfig().floatProp("alpha",0,false));
	}
	
}
