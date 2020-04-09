using UnityEngine;
using System.Collections;

public class Tutorial : MonoBehaviour {

	
	void Start () {
        GameData.firstRun = true;
	}
	
	void Update () {

        // if (Input.touches[0].tapCount > 1)
        if(Input.GetMouseButtonDown(0))
        {
            //Application.LoadLevel("Fase01");
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Fase01",1f);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
           // Application.LoadLevel("Menu");
            LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu", 1f);
        }
	}
}
