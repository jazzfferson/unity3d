using UnityEngine;
using System.Collections;


public class AnimacaoTransicao : MonoBehaviour
{

    #region Atributos

    #endregion
    
    
    void Start () 
    {
		
		
		if(Application.platform==RuntimePlatform.Android)
		{
			Handheld.PlayFullScreenMovie("TangoVideoFinal.mp4",Color.black,FullScreenMovieControlMode.Full);
		}
		else if (Application.platform==RuntimePlatform.WP8Player)
		{
			UnityPluginForWindowsPhone.VideoClass.Play("Data/StreamingAssets/TangoVideoFinal.mp4");
		}

        StartCoroutine(rotina());
	}

    IEnumerator rotina()
    {
        yield return new WaitForSeconds(0.01f);
        LoadScreenManager.instance.LoadScreenWithFadeInOut("Fase02", 1f);

    }

	
}
