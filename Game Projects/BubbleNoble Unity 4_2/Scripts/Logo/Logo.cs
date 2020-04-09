using UnityEngine;
using System.Collections;

public class Logo : MonoBehaviour {

	public float timer;
	
	IEnumerator Start () {
		
		
		yield return new WaitForSeconds(timer);
		LoadScreenManager.instance.LoadScreenWithFadeIn("Menu",2f);
	}
	
	
}
