using UnityEngine;
using System.Collections;

public class AutoSleep : MonoBehaviour {

	public void Sleep(float time)
	{
		StartCoroutine(rotinaSleep(time));
	}
	IEnumerator rotinaSleep(float time)
	{
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
	}
}
