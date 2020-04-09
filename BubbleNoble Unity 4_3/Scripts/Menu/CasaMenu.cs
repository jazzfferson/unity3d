using UnityEngine;
using System.Collections;

public class CasaMenu : MonoBehaviour {
	
	[SerializeField]
	private UISprite casa;

	public void Liga(bool ligado)
	{
		if(ligado)
		  TweenAlpha.Begin(casa.gameObject,0.4f,1f);
		else
		{
		  TweenAlpha.Begin(casa.gameObject,0.4f,0);		
		}
	}
}
