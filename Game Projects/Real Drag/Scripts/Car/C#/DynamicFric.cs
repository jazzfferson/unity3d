using UnityEngine;
using System.Collections;

public class DynamicFric : MonoBehaviour {

	public WheelCollider[] wheels;
	public AnimationCurve curve;
	public float minStiffness;
	
	float[] grip;
	WheelHit hit;
	
	
	void Start () {
		
		grip = new float[wheels.Length];
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		
		
		for(int i = 0; i < wheels.Length;i++)
		{
			wheels[i].GetGroundHit(out hit);
			
			var grip = wheels[i].forwardFriction;
			
			grip.stiffness = Mathf.Clamp(1.2f - Mathf.Abs((hit.forwardSlip + hit.sidewaysSlip)),minStiffness,100);
			
			//wheels[i].forwardFriction = grip;
			
			
			
			var grip2 = wheels[i].sidewaysFriction;
			grip2.stiffness = Mathf.Clamp(1.2f - Mathf.Abs((hit.forwardSlip + hit.sidewaysSlip)) * 2,minStiffness,100);
			
			wheels[i].sidewaysFriction = grip2;			 
			
		}
		
	
	}
}
