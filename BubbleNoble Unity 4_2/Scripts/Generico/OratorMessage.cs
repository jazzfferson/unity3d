using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OratorMessage : MonoBehaviour {
	
	
	public TypewriterEffect typeWriter;
	public UILabel[] label;
	public UISprite[] sprites;
	
	public UISprite orator;
	public UISprite ballon;
	public Transform ballonPivo;
	public UISprite fade;
	public List<EventDelegate> _callBack = new List<EventDelegate>();
	float baloonScale;
	void Start()
	{
	   baloonScale = ballon.transform.localScale.x;
	   typeWriter.onFinished+= new TypewriterEffect.TypeDelegate(Terminou);
	}
	public void ExibeMessage(string message,EventDelegate callBack = null)
	{
	   _callBack.Add(callBack);
	   StartCoroutine(Rotina(message));
	}
	public void HideMessage()
	{
	        Alpha(fade.gameObject,0,0.3f);
		Alpha(orator.gameObject,0,0.3f);
		Alpha(ballon.gameObject,0,0.3f);
		
		if(label!=null)
		{
			foreach(UILabel lab in label)
			Alpha(lab.gameObject,0,0.3f);
		}
		
		if(sprites!=null)
		{
			foreach(UISprite sprt in sprites)
			Alpha(sprt.gameObject,0,0.3f);
		}
		
		EscalaBallon(0.3f,baloonScale,GoEaseType.CubicInOut);
	}
	IEnumerator Rotina(string message)
	{
		Alpha(fade.gameObject,0.8f,0.5f);
		yield return new WaitForSeconds(0.2f);
		Alpha(orator.gameObject,1,0.7f);
		yield return new WaitForSeconds(0.2f);
		Alpha(ballon.gameObject,1,0.7f);
		
		if(label!=null)
		{
			foreach(UILabel lab in label)
			Alpha(lab.gameObject,1,0.7f);
		}
		
		if(sprites!=null)
		{
			foreach(UISprite sprt in sprites)
			Alpha(sprt.gameObject,1,0.7f);
		}
		
		EscalaBallon(0.7f,1,GoEaseType.ElasticOut);
		
		yield return new WaitForSeconds(0.8f);
		
		
		if(!string.IsNullOrEmpty(message))
		{	
		  label[0].text = message;
		  typeWriter.Play();
		}
		
	}

	void Alpha(GameObject obj,float alpha,float time)
	{
	   TweenAlpha.Begin(obj.gameObject,time,alpha);
	}
	void EscalaBallon(float time,float escala,GoEaseType ease)
	{
	    Go.to(ballonPivo,time, new GoTweenConfig().scale(escala,false).setEaseType(ease));
	}
	void Terminou()
	{
		if(EventDelegate.IsValid(_callBack))
		{
		   EventDelegate.Execute(_callBack);
		}
	}
}
