using UnityEngine;
using System.Collections;

public class Steering : MonoBehaviour {
	
	public WheelCollider[] Wheels;
	public float maxAngle;
	
	public void SteeringAmount(float inputValue)
	{
		foreach(WheelCollider wheel in Wheels)
		{
			wheel.steerAngle = inputValue * maxAngle;
		}
	}	
}
