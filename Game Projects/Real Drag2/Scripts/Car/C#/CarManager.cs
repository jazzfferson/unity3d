using UnityEngine;
using System.Collections;

public class CarManager : MonoBehaviour {

	public Motor motor;
	public Transmsition transmition;
	public Steering steer;
	public Brake brake;
	public bool ide;


	
	private int actualGear = 1;
	private float accel;
	private int brak;
	
	Vector3 infoCar;
	Quaternion infoCarR;
	
	void Start () {
		
	ide = true;
	infoCar = transform.position;
	infoCarR = transform.rotation;
	
	
	}
	
	
	void Update () {
		
		Steering();
		//Brake();
		motor.MotorUpdate(transmition.ActualGearRatio,transmition.FinalDrive,accel,ide);
		
		
		if(Input.touchCount>3)
		{
			transform.position = infoCar;
			transform.rotation = infoCarR;
		}
		if (Input.touchCount>4)
		{
			Application.LoadLevel("Race");
		}
		
	}
	
	void ThrottleUp()
	{
		accel = 1;
	}
	void ThrottleDown()
	{
		accel = 0;
	}
	void BrakeUp()
	{
		brake.ApplyBrake(1);
	}
	void BrakeDown()
	{
		brake.ApplyBrake(0);
	}
	void Brake()
	{
		if(Input.GetAxis("Vertical")<0)
		{
			brake.ApplyBrake(Input.GetAxis("Vertical"));
		}
		else
		{
			brake.ApplyBrake(0);
		}
	}
	
	void Steering()
	{
		steer.SteeringAmount(Input.GetAxis("Horizontal"));
		accel = Input.GetAxis("Vertical");
		//steer.SteeringAmount(Input.acceleration.x);
	}
	
	
	void NextGear()
	{
		actualGear++;
		transmition.ChangeGear(actualGear);
		
	}
	void PreviousGear()
	{
		actualGear--;
		transmition.ChangeGear(actualGear);
	}
}
