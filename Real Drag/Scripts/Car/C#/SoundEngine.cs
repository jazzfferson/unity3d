using UnityEngine;
using System.Collections;

public class SoundEngine : MonoBehaviour {

	public AudioSource audioSource;
	public Transmsition transmition;
	public Motor motor;
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
	

	void Start () {
		
		transmition.GearChanged+=gearChange;
	
	}
	
	
	void Update () {
		
		if(changingGears == false)
            audioSource.pitch = motor.ActualRpm / dividBy;
		else
            audioSource.pitch = rpmSound / dividBy;


        finalRpm = audioSource.pitch * dividBy;
	}
	
	void gearChange()
	{
		changingGears = true;
		rpmSound = motor.ActualRpm;
		targetRpm = motor.CheckMotorRpm(transmition.gearsRatioCurve.Evaluate(transmition.NextGearIndex),transmition.finalDriveRatio);
		changingGears = true;
		Go.to(this,transmition.speedChange,new GoTweenConfig().floatProp("rpmSound",targetRpm,false).setEaseType(interpolationType).onComplete(Completed=>{changingGears=false;}));
		
	}
}
