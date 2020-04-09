using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadScreen : MonoBehaviour
{
    public static LoadScreen instance;
    public CanvasGroup loadingPanel;
    public CanvasRenderer fadeSpriteCanvasRender;
    int fade;
    bool canExitLoadScreen = false;

    void Start()
    {
        DontDestroyOnLoad(this);

        if (instance == null)
        {
            instance = this;
            StaticReferences.Instance.OnReferencesReady += new StaticReferences.SimpleDelegate(Instance_OnReferencesReady);
        }
        else
        {
            Destroy(this.gameObject);
        }

        
    }
    void Instance_OnReferencesReady()
    {
        /*StartCoroutine(*/Finish(0.2f)/*)*/;
    }
   
    void OnLevelWasLoaded(int level)
    {
      
        if (Application.loadedLevelName != "Myscene")
        {
            /*StartCoroutine(*/Finish(0)/*)*/;
        }
    }
   /* IEnumerator*/ void  Finish(float delay)
    {
 
          //  yield return new WaitForSeconds(delay);
           /* fadeSpriteCanvasRender.SetAlpha(0);
            loadingPanel.alpha = 0;*/
              
    }
    public void LoadLevel(int index)
    {
        StartCoroutine(LoadLevelRotina(index));
    }
    public void LoadLevel(string name)
    {
        StartCoroutine(LoadLevelRotina(name));
    }
    void LoadLevelPrivado()
    {
     //   fadeSprite.alpha = 0;
    //    iconsPanel.alpha = 0;
    //    loadingPanel.alpha = 0;
       // Go.to(loadingPanel, 0.1f, new GoTweenConfig().floatProp("alpha", 1, false));
    }
    IEnumerator LoadLevelRotina(int index)
    {
        LoadLevelPrivado();
        yield return new WaitForSeconds(0.2f);
        Application.LoadLevel(index);
    }
    IEnumerator LoadLevelRotina(string name)
    {
        LoadLevelPrivado();
        yield return new WaitForSeconds(0.2f);
        Application.LoadLevel(name);
    }


}
