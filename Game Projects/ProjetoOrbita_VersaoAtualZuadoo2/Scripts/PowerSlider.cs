using UnityEngine;
using System.Collections;

public class PowerSlider : MonoBehaviour {

	public UILabel label;
	UISlider slider;
	public GameObject target;
	public string eventName;
	public int ShowValueMultiplyBy;
	public string finalCharacter; 
	
	void Start()
	{
		slider = GetComponent<UISlider>();
	}
	void OnSliderChange(float valor)
	{
		float textValue;
		target.SendMessage(eventName,valor,SendMessageOptions.DontRequireReceiver);
		
		textValue = valor*ShowValueMultiplyBy;
        
        label.text = string.Format("{0:0.00}", textValue) + finalCharacter;
	}
	
}
