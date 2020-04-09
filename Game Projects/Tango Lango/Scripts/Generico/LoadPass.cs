using UnityEngine;
using System.Collections;

public class LoadPass : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
		Application.LoadLevel(LoadScreenManager.instance.levelToBeLoaded);
	}
}
