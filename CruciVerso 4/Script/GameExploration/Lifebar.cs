using UnityEngine;
using System.Collections;

public class Lifebar : MonoBehaviour {

	[SerializeField]
	private UISprite[] sprites;
	[SerializeField]
	private float timeFadeBar;
	[SerializeField]
	private float alphaValue;
	
	void Start()
	{
		
		StartCoroutine(RotinaTest());
	}
	public void SetVisibleBars(int numbers)
	{
		numbers = Mathf.Clamp(numbers,0,sprites.Length);
		
		for(int i = 0; i < sprites.Length; i++)
		{
			if(i<numbers)
				TweenAlpha.Begin(sprites[i].gameObject,timeFadeBar,1);
			else
			{
				TweenAlpha.Begin(sprites[i].gameObject,timeFadeBar,alphaValue);
			}
		}
		
	}
	
	IEnumerator RotinaTest()
	{
		SetVisibleBars(Random.Range(0,22));
		yield return new WaitForSeconds(3);
		StartCoroutine(RotinaTest());
	}

}
