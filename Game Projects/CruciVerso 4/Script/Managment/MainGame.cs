using UnityEngine;
using System.Collections;
public enum CenaJogo{Intro,Menu,Jogo,Loading};
public class MainGame : MonoBehaviour {

    public static MainGame instance;
	
	void Awake () {
		
    	DontDestroyOnLoad(gameObject.transform);				
	}
	void Start()
	{
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
		if(Application.loadedLevel==0)
		LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu",2f);

	}
}
