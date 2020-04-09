using UnityEngine;
using System.Collections;

public class FadeScreen : MonoBehaviour {

    public static FadeScreen instance;

    private float ap;
	public float alpha
    {

        get { return ap; }
        set { ap = value;}

    }

    void Awake()
    {
        instance = this;
    }
	void Start () {

 

	}
	
	// Update is called once per frame
	void Update () {

           /* Camera.main.guiTexture.color = new Color(0, 0, 0, ap);*/
		
	}
	public void FadeToBlack(float duration)
	{
		Go.to(this,duration,new GoTweenConfig().floatProp("alpha",1,false));
	
	}
	public void FadeToTransparent(float duration)
	{
		Go.to(this,duration,new GoTweenConfig().floatProp("alpha",0,false));
	}
	
}
