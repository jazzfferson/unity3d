using UnityEngine;
using System.Collections;

public class AngleSlider : MonoBehaviour {

	[SerializeField]
	private UILabel label;
	[SerializeField]
	private UISprite angleSprite;
	
	
	void OnSliderChange (float tempValue) {
		
		
		if(tempValue<0.5f)
		label.text = string.Format("{0:0.0}", Mathf.Abs(((tempValue* 90) - 45)*2)); 
		else
		label.text = string.Format("{0:0.0}", -Mathf.Abs(((tempValue* 90) - 45)*2)); 
		
	}
	
	void OnPress(bool pressionado)
	{
	  if(pressionado)
			TweenAlpha.Begin(angleSprite.gameObject,0.4f,0.7f);
		else
			TweenAlpha.Begin(angleSprite.gameObject,0.4f,0);
		
	}
}
