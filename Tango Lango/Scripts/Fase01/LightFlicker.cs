using UnityEngine;
using System.Collections;

public class LightFlicker : MonoBehaviour {
	
	public static LightFlicker instance;
	public Light luz;
	public float timeBtweenInterations;
	public int iterations;
	int interationsRef;

	
	void Start()
	{
		if(instance==null)
		{
			instance = this;
		}
	}
	public void Flick()
	{
		StopCoroutine("rotina");
		interationsRef = iterations;
		StartCoroutine("rotinaDelay");
	}
	IEnumerator rotinaDelay()
	{
		yield return new WaitForSeconds(0.8f);
		delay();
	}
	void delay()
	{
		
		StartCoroutine("rotina");
		StartCoroutine("rotinaSom");
	}
	IEnumerator rotina()
	{
		interationsRef--;
		yield return new WaitForSeconds(Random.Range(0.1f,timeBtweenInterations));
		if(interationsRef>0)
		{
			luz.intensity = Random.Range(0.5f,7f);
			StartCoroutine("rotina");
		}
		else
		{
			luz.intensity = 0f;
		}
	}
	IEnumerator rotinaSom()
	{
		yield return new WaitForSeconds(0.2f);
		SoundController.instance.PlaySfx(2,1);
	}
}
