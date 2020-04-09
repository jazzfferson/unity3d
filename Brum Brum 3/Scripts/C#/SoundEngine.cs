using UnityEngine;
using System.Collections;

public class SoundEngine : MonoBehaviour {

	public EngineAudioMixer audioMixer;
	public GoEaseType interpolationType;
	public int dividBy;
	
	public float rpmSound
	{
		get;
		set;
	}
    public float finalRpm
    {
        get;
        set;
    }

	float targetRpm;
	bool changingGears;
    Transmsition transmition;
    Motor motor;
	

	void Start () {

        transmition = GetComponent<Transmsition>();
        motor = GetComponent<Motor>();
		transmition.GearChanged+=gearChange;
	}
	
	
	void Update () {

        float mixerPitch;

		if(changingGears == false)
            mixerPitch = (Mathf.Clamp(motor.ActualRpm,motor.minMotorRpm, float.PositiveInfinity) / dividBy);
		else
            mixerPitch = (rpmSound / dividBy);


        finalRpm = mixerPitch * dividBy;

        audioMixer.UpdateMixer(motor.Throttle, mixerPitch);
	}
	void gearChange()
	{
		changingGears = true;
		rpmSound = motor.ActualRpm;
		targetRpm = motor.CheckMotorRpm(transmition.gearsRatio[transmition.NextGearIndex],transmition.finalDriveRatio);
		changingGears = true;
		Go.to(this,transmition.speedChange,new GoTweenConfig().floatProp("rpmSound",targetRpm,false).setEaseType(interpolationType).onComplete(Completed=>{changingGears=false;}));
		
	}
}
