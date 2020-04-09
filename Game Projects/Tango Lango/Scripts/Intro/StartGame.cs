using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	void Start () {
	
		LoadScreenManager.instance.LoadScreenWithFadeInOut("Menu",1);
		
	}
	
	
}
