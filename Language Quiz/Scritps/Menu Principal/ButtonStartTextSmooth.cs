using UnityEngine;
using System.Collections;

public class ButtonStartTextSmooth : MonoBehaviour
{

	public UILabel texto;
	public float speed;

	private bool finished;
	private float alphaTarget;


	void Update () {
	
		TextAnimation ();
	}

	void TextAnimation()
	{
		if (texto.alpha >= 1) 
		{
			alphaTarget = -0.1f;
		} 
		else if(texto.alpha <= 0)
		{
			alphaTarget = 1.1f;
		}
		texto.alpha = Mathf.Lerp (texto.alpha, alphaTarget, Time.deltaTime * speed);
	}
}
