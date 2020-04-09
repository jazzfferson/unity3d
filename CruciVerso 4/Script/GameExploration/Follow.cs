using UnityEngine;
using System.Collections;

public class Follow : MonoBehaviour {
	
	[SerializeField]
	private float  minDistancetoFollow;
	[SerializeField]
	private float maxAvarageSpeed;
	[SerializeField]
	private float acceleration;

	private GameObject target;
	
	private bool following;
	float speed;

	
	public void Initialize () {
		
		maxAvarageSpeed =  Random.Range(maxAvarageSpeed,maxAvarageSpeed+maxAvarageSpeed/4);
		target = ControlNave.referenciaNave;
		speed = 0;
	}
	
	
	// Update is called once per frame
	void Update () {
		
	    if(Vector3.Distance(target.transform.position,transform.position)<minDistancetoFollow)
		{	
		    speed += (Time.deltaTime * acceleration);
			following = true;
		}
		else
		{
		   speed -= (Time.deltaTime * acceleration *2);
		   following = false;	 
		}
		
        speed = Mathf.Clamp(speed,0,maxAvarageSpeed);
	    transform.LookAt(target.transform);
		transform.Translate(Vector3.forward*Time.deltaTime*speed);
		
		if(Vector3.Distance(target.transform.position,transform.position)<4)
		{
			PoolManager.Despawn(gameObject);
			target.SendMessage("Play",SendMessageOptions.DontRequireReceiver);
		}
	}
}
