using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*[System.Serializable]
public class XPTO {
	public float x;
	public float p;
	public float t;
	public float o;
}*/
public class PlayerControl : MonoBehaviour {
	
	public static bool[] usedTouchs;
	
	[SerializeField]
   	private Transform centerControl;
	[SerializeField]
	private Transform visualJoystick;
	[SerializeField]
	private float maxRadiusControl;
    private Vector2 valueControl;
	private bool pressionado;
	private bool stayPressed = false;
	
	int lastfingerid = -1;
	
	public Vector2 ButtonPosition
	{
		get{return new Vector2(visualJoystick.localPosition.x,visualJoystick.localPosition.y);}
	}
	

	
	void Start() {
		
		
		Input.multiTouchEnabled = true;
		usedTouchs = new bool[] {false,false,false,false,false};
		
		// Se não for plataforma Android ou Iphone desttrua este objeto
	
	}
	
	public void Update () {
		
		
		
		#if UNITY_EDITOR
   		 Mouse();
		#else
		Touchh();
		#endif
		
		float divisor = 95;
		visualJoystick.localPosition = new Vector3(valueControl.x/divisor,valueControl.y/divisor,0);
		visualJoystick.localPosition = Vector3.ClampMagnitude(visualJoystick.localPosition,1);
		
	}
	
	Vector2 controlValue (Vector2 inputPosition,Vector3 centerPos)
	{
		Vector2 centPos = Camera.main.WorldToScreenPoint(centerPos);
		return new Vector2((inputPosition.x - centPos.x),(inputPosition.y - centPos.y));						
	}
	
	bool rightDistance(Vector2 inputPosition,Vector3 centerPos,float maxRadius)
	{
		Vector2 centPos = Camera.main.WorldToScreenPoint(centerPos);
		
		if (Vector2.Distance(inputPosition,centPos)<maxRadius) {
			
			return true;
		}
		else
			return false;
	}
	
	void Mouse()
	{
		if(Input.GetMouseButton(0) && rightDistance(new Vector2(Input.mousePosition.x,Input.mousePosition.y),centerControl.position,maxRadiusControl) || pressionado)
		{
			pressionado = true;
			valueControl = controlValue(new Vector2(Input.mousePosition.x,Input.mousePosition.y),centerControl.position);
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			pressionado = false;
			valueControl = Vector2.zero;
		}
	}
	
	void Touchh()
	{
		if(Input.touchCount > 0) {
			for(int i = 0; i <Input.touchCount ; i++) 
			{
				Touch touch = Input.GetTouch(i);
				// Se o touch estiver no estado tocando parado ou movendo...
				if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary) 
				{

					if((rightDistance(touch.position,centerControl.position,maxRadiusControl) || stayPressed )&& (lastfingerid==-1 || touch.fingerId == lastfingerid))
					{	
						stayPressed = true;
						lastfingerid = touch.fingerId;					
						valueControl = controlValue(touch.position,centerControl.position);
				
					}
				}
				
				else if(touch.fingerId == lastfingerid && touch.phase == TouchPhase.Ended)
					{
						 stayPressed = false;
					     valueControl = Vector3.zero;
						 lastfingerid = -1;
						 break;
					}
			}
		}
	}
	
	
	

}