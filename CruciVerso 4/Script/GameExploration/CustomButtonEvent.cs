using UnityEngine;
using System.Collections;

public class CustomButtonEvent : MonoBehaviour {
	
	[SerializeField]
	private GameObject[] targetsEvent;
	
	[SerializeField]
	private string OnEnterEvent;
	
	[SerializeField]
	private string OnStayEvent;
	
	[SerializeField]
	private string OnExitEvent;
	
	[SerializeField]
	private Transform buttonCenter;
	
	[SerializeField]
	private float raio;
	
	[SerializeField]
	private int id;
	
	private bool pressed = false;
	private int lastIndex =-1; 
	
	void Update()
	{
		
#if UNITY_EDITOR
		
   if(Input.GetMouseButton(0))
	{
			
			
			if(Vector3.Distance(new Vector3(Input.mousePosition.x,Input.mousePosition.y),Camera.main.WorldToScreenPoint(buttonCenter.position))<raio)
			{
			   Stay();
				
			   if(!pressed)
				{
					Enter();
					pressed = true;
				}
			}
			else
			{
					if(pressed)
					{
						Exit ();
						pressed = false;
					}
			}
			
			
	}
	else
	{
		Exit ();
		pressed = false;	
	}
		
#else
		
		if(Input.touchCount>0)
		{
			
			foreach(Touch touch in Input.touches)
			{
					
				if(Vector3.Distance(new Vector3(touch.position.x,touch.position.y),
					Camera.main.WorldToScreenPoint(buttonCenter.position))<raio && (touch.fingerId == lastIndex || lastIndex ==-1))
				{

					
					
					lastIndex = touch.fingerId;
					
					if(touch.fingerId == lastIndex)
					{
						Stay();
					}
					
					 if(!pressed && touch.fingerId == lastIndex)
					{
						
						Enter();
						pressed = true;
	
					}
				}
				
				if(touch.fingerId == lastIndex && touch.phase == TouchPhase.Ended)
				{
						lastIndex = -1;
						Exit ();
						pressed = false;
				}
			}
		}
		else if(lastIndex!=-1)
		{
			lastIndex =-1;
			Exit ();
			pressed = false;
		}
			
#endif
	   	
	}
	
	void Enter ()
	{
		Send(OnEnterEvent,id);
	}
	
	void Stay()
	{
		Send(OnStayEvent,id);
	}
	
	void Exit ()
	{
		Send(OnExitEvent,id);
	}
	
	void Send(string MethodName , int id)
	{
		foreach(GameObject target in targetsEvent)
		target.SendMessage(MethodName,id,SendMessageOptions.DontRequireReceiver);
	}
	
	
}