using UnityEngine;
using System.Collections;

public class ButtonNext : MonoBehaviour {
	
	public static ButtonNext instance;
	
	
	public UISprite icon;
	public UISprite background;
	
	void Start()
	{
	   if(instance==null)
		{
			instance = this;
		}
	}
	
	public void Enable(bool enable)
	{
		if(enable)
		{
		  GetComponent<Collider>().enabled = true;
		  icon.color = Color.white;
		  background.color = Color.white;
		}
		else
		{
		   GetComponent<Collider>().enabled = false;
		   icon.color = new Color(0.1f,0.1f,0.1f);
		  background.color = new Color(0.1f,0.1f,0.1f);
		}
	}
}
