using UnityEngine;
using System.Collections;

public class Steering : MonoBehaviour {
	
	public WheelCollider[] Wheels;
	public float maxAngle;
    public float Amount
    {
        get;
        private set;
    }
	
	public void SteeringAmount(float inputValue)
	{

        Amount = inputValue;
		foreach(WheelCollider wheel in Wheels)
		{
			wheel.steerAngle = inputValue * maxAngle;
		}
	}	
}
