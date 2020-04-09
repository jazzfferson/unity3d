using UnityEngine;
using System.Collections;

public class LoadPass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Application.LoadLevelAsync(LoadScreenManager.instance.levelToBeLoaded);
	}
}
