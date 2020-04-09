using UnityEngine;
using System.Collections;

public class Motor : MonoBehaviour {
	//Variavel Para controle de torque !!! Apenas para teste.
	public float torqueAdjust;
	public WheelCollider[] tractionWheels;
	public AnimationCurve torqueCurve;
	public int minMotorRpm;
	public int maxMotorRpm;
	public float ActualTorque
	{
		get;
		private set;
	}
	public float ActualRpm
	{
		get;
		private set;
	}
	
	private float minVariacaoRpm = 50;
	private float maxVariacaoRpm = 120;
	
	private float MotorRpm(float gearRatio,float finalDrive)
	{
		

			float wheelsRpm = (tractionWheels[0].rpm + tractionWheels[1].rpm) / 2;
			ActualRpm = minMotorRpm + wheelsRpm * gearRatio * finalDrive;
			return ActualRpm;
		
	}
	private float MotorRpmIDE(float throttle)
	{
			if(throttle>0)
				 ActualRpm = Mathf.Clamp((ActualRpm += throttle * maxMotorRpm/100),Random.Range(minMotorRpm-minVariacaoRpm,minMotorRpm+minVariacaoRpm),Random.Range(maxMotorRpm-maxVariacaoRpm,maxMotorRpm+maxVariacaoRpm));
			else
			{
				ActualRpm = Mathf.Clamp((ActualRpm -= maxMotorRpm/200),Random.Range(minMotorRpm-minVariacaoRpm,minMotorRpm+minVariacaoRpm),Random.Range(maxMotorRpm-maxVariacaoRpm,maxMotorRpm+maxVariacaoRpm));
			}
		
		return ActualRpm;
	}
	private float MotorTorque(float rpm,float gearRatio,float finalDrive,float throttle)
	{
		//drive torque = engine_torque * gear_ratio * differential_ratio * transmission_efficiency
		ActualTorque = (torqueCurve.Evaluate(rpm) * torqueAdjust) * gearRatio * finalDrive * throttle;
		return ActualTorque;
	}
	private void ApplyTorque(float torque)
	{
		
		foreach(WheelCollider wheel in tractionWheels)
		{
			wheel.motorTorque = torque /2;
		}
	}
	public void MotorUpdate(float gearRatio,float finalDrive,float throttle,bool ideMode)
	{
		if(!ideMode)
		  ApplyTorque(MotorTorque(MotorRpm(gearRatio,finalDrive),gearRatio,finalDrive,throttle));
		else
		{
			//MotorRpmIDE(throttle);
			ApplyTorque(MotorTorque(MotorRpm(gearRatio,finalDrive),gearRatio,finalDrive,throttle));
			//ApplyTorque(MotorTorque(MotorRpmIDE(throttle),gearRatio,finalDrive,throttle));
		}
	}
	public float CheckMotorRpm(float gearRatio,float finalDrive)
	{
		float wheelsRpm = (tractionWheels[0].rpm + tractionWheels[1].rpm) / 2;		
		return minMotorRpm + wheelsRpm * gearRatio * finalDrive;;
	}

}
