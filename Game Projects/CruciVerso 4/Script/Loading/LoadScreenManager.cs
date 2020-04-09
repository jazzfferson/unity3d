using UnityEngine;
using System.Collections;

public class LoadScreenManager: MonoBehaviour {
	
	
	GameObject loadScreenManager;
    public static LoadScreenManager instance;
    float timeFadeLoaded;
	bool needLoadScreen = false;
	public string levelToBeLoaded;
	
   public float alpha
    {
        get;
        set;
    }

  void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
    }
	
  void Update()
    {
        /*gameObject.guiTexture.color = new Color(0, 0, 0, alpha);*/
    }
	
  public  void LoadScreenWithFadeInOut(string sceneName,float fadeTime)
    {
        timeFadeLoaded = fadeTime / 2;
        StartCoroutine(RotinaFade(fadeTime));
		levelToBeLoaded = sceneName;  
    }
    
  IEnumerator RotinaFade(float time)
    {
        FadeIn(time/2, GoEaseType.CubicOut);
        yield return new WaitForSeconds(time/2);
		FadeOut(time/2, GoEaseType.CubicOut);
        Application.LoadLevel("Loading");
    }

  void FadeIn(float time, GoEaseType ease)
    {
        Go.to(this, time, new GoTweenConfig().floatProp("alpha", 1, false).setEaseType(ease));
    }

  void FadeOut(float time, GoEaseType ease)
    {
        Go.to(this, time, new GoTweenConfig().floatProp("alpha", 0, false).setEaseType(ease));
    }

  void OnLevelWasLoaded()
    {	
	   alpha = 1;
       FadeOut(timeFadeLoaded, GoEaseType.CubicOut);
    }
}
