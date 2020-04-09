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
		
			 float mRpm;
		
			float wheelsRpm = (tractionWheels[0].rpm + tractionWheels[1].rpm) / 2;
			mRpm = minMotorRpm + wheelsRpm * gearRatio * finalDrive;
			return mRpm;
		
	}
	private float MotorRpmIDE(float throttle)
	{
		 float mRpm;
			
			if(throttle>0)
				 mRpm = Mathf.Clamp((ActualRpm += throttle * maxMotorRpm/100),Random.Range(minMotorRpm-minVariacaoRpm,minMotorRpm+minVariacaoRpm),Random.Range(maxMotorRpm-maxVariacaoRpm,maxMotorRpm+maxVariacaoRpm));
			else
			{
				mRpm = Mathf.Clamp((ActualRpm -= maxMotorRpm/200),Random.Range(minMotorRpm-minVariacaoRpm,minMotorRpm+minVariacaoRpm),Random.Range(maxMotorRpm-maxVariacaoRpm,maxMotorRpm+maxVariacaoRpm));
			}
		
		return mRpm;
	}
	private float MotorTorqueIDE(float motorRpmIde)
	{
		float mTorque;
		mTorque = torqueCurve.Evaluate(motorRpmIde);
		return mTorque;
		
	}
	private float MotorTorque(float rpm,float gearRatio,float finalDrive,float throttle)
	{
		float mTorque;
		//drive torque = engine_torque * gear_ratio * differential_ratio * transmission_efficiency
		mTorque = (torqueCurve.Evaluate(rpm) * torqueAdjust) * gearRatio * finalDrive * throttle;
		return mTorque;
	}
	private void ApplyTorque(float torque)
	{
		
		foreach(WheelCollider wheel in tractionWheels)
		{
			wheel.motorTorque = torque /2;
		}

       
	}
	public void MotorUpdate(float gearRatio,float finalDrive,float throttle)
	{
		  
         ActualRpm =MotorRpm(gearRatio, finalDrive);
		 ActualTorque =MotorTorque(ActualRpm,gearRatio,finalDrive,throttle); 
		 ApplyTorque(ActualTorque);
		
	}
	public float CheckMotorRpm(float gearRatio,float finalDrive)
	{
		float wheelsRpm = (tractionWheels[0].rpm + tractionWheels[1].rpm) / 2;		
		return minMotorRpm + wheelsRpm * gearRatio * finalDrive;;
	}
	
}
