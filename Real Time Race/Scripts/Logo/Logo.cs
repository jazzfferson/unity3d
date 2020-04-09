using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        yield return new WaitForSeconds(0.1f);
        LoadScreen.instance.LoadLevel(1);
	}
	
	
}
