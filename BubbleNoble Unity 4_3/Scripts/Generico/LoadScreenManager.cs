using UnityEngine;
using System.Collections;

public class LoadScreenManager: MonoBehaviour {
	
	
	GameObject loadScreenManager;
    public static LoadScreenManager instance;
    static float timeFadeLoaded;
    bool withFadeOut;
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
        gameObject.GetComponent<GUITexture>().color = new Color(0, 0, 0, alpha);
    }
	
  public  void LoadScreenWithFadeInOut(string sceneName,float fadeTime)
    {
        withFadeOut = true;
        timeFadeLoaded = fadeTime;
        StartCoroutine(RotinaFade(sceneName,fadeTime));	
    }
	
    public  void LoadScreenWithFadeIn(string sceneName,float fadeTime)
    {
	 withFadeOut = false;
        timeFadeLoaded = fadeTime;
        StartCoroutine(RotinaFade(sceneName,fadeTime));	
    }
    
  IEnumerator RotinaFade(string sceneName,float time)
    {
        yield return new WaitForSeconds(0.4f);
	    alpha = 0;
        FadeIn(time, GoEaseType.CubicIn);
        yield return new WaitForSeconds(time);
        Application.LoadLevel(sceneName);
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
	if(withFadeOut)
		{
       		alpha = 1;
      		 FadeOut(timeFadeLoaded , GoEaseType.CubicOut);
		}
		
		else
			
		{
			alpha = 0;
		}
    }
	
}
