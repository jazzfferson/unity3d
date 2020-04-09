using UnityEngine;
using System.Collections;

public class LimitSlider : MonoBehaviour {

	
	public UISprite barSprite;
	public float valorMinimo;

	
	void Update () {
		
		float barValue = GetComponent<UISlider>().sliderValue;
		barSprite.color = new Color(barValue,barValue,barValue,1);
		
		if(barValue<=valorMinimo)
		{
			GetComponent<UISlider>().sliderValue = valorMinimo;
		}
		else
		{
			
		}
	
	}
}
