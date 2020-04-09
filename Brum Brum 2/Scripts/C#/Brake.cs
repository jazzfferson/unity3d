using UnityEngine;
using System.Collections;

public class Brake : MonoBehaviour {

    public WheelCollider[] frontWheels;
    public WheelCollider[] rearWheels;
    public float force = 3500f;
    public float distribution = 0.75f;
    public float smoothness = 0.5f;
    private float velocity;
    private bool isHandBrake;

    public float Amount
    {
        get;
        private set;
    }

    public void ApplyBrake(float amount)
    {

        Amount = amount;
    
        foreach (WheelCollider wheel in frontWheels)
        {
            wheel.brakeTorque = (force * distribution) * Amount;
        }
        foreach (WheelCollider wheel in rearWheels)
        {
           wheel.brakeTorque = (force - (force * distribution)) * Amount;
        }
    }

    public void ApplyHandBrake(/*bool apply*/)
    {
        foreach (WheelCollider wheel in rearWheels)
        {
            wheel.brakeTorque = 25000f;
        }
    }
}
