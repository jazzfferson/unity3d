using UnityEngine;
using System.Collections;

public class Brake : MonoBehaviour {

	public WheelCollider[] frontWheels;
	public WheelCollider[] rearWheels;
	public float frontAmount;
	public float rearAmount;
	
	public void ApplyBrake(float amount)
	{
			foreach(WheelCollider wheel in frontWheels)
			{
				wheel.brakeTorque = frontAmount*Mathf.Abs(amount);
			}
			foreach(WheelCollider wheel in rearWheels)
			{
				wheel.brakeTorque = rearAmount*Mathf.Abs(amount);
			}
	}
	
	
}
