using UnityEngine;
using System.Collections;

public class LogoChange : MonoBehaviour {

	public UISprite logoPrincipal;
	public UISprite logoSecundario;
	public static LogoChange instance;
	
	void Awake()
	{
		if(instance==null)
		{
			instance = this;
		}
	}
	
	public void ChangeLogo(bool isLogoprincipal,float timeChange)
	{
		if(isLogoprincipal)
		{
			TweenColor.Begin(logoPrincipal.gameObject,timeChange,new Color(1,1,1,1));
			TweenColor.Begin(logoSecundario.gameObject,timeChange,new Color(1,1,1,0));
		}
		else
		{
			TweenColor.Begin(logoPrincipal.gameObject,timeChange,new Color(1,1,1,0));
			TweenColor.Begin(logoSecundario.gameObject,timeChange,new Color(1,1,1,1));
		}
	}
}
