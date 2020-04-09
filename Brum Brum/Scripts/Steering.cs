using UnityEngine;
using System.Collections;

public class Steering : MonoBehaviour {

    public AnimationCurve smoothAlongSpeed;
	public WheelCollider[] wheels;
	public float maxAngle;
    public float Smooth
    {
        get;
        set;
    }
    public float Amount
    {
        get;
        private set;
    }

    void Start()
    {
    }
	public void SteeringAmount(float inputValue)
	{
        float reF=0;
        Amount = Mathf.SmoothDampAngle(Amount, inputValue * maxAngle, ref reF, Smooth); ;
        for (int i = 0; i < wheels.Length;i++ )
        {
            wheels[i].steerAngle = Amount;
        }
	}	
}
