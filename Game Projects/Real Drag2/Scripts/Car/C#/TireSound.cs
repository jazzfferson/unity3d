using UnityEngine;
using System.Collections;
public enum SlipType{Forward,Side};
public class TireSound : MonoBehaviour {

	public SlipType slipType;
	
	public float beginIn;
	
	public float minVolumeAudio;
	public float maxVolumeAudio;
	public float multiplyAudio;
	
	public float minValuePitch;
	public float maxValuePitch;
	public float multiplyPitch;
	
	private WheelHit hit;
	private WheelCollider roda;
	
	float masterValue;
	
	void Start () {
	
		
		roda = GetComponent<WheelCollider>();
	}
	
	// Update is called once per frame
	void Update () {
		
		roda.GetGroundHit(out hit);
		
		if(slipType == SlipType.Forward)
		{
			masterValue = hit.forwardSlip;
		}
		else
			masterValue = hit.sidewaysSlip;
			
			masterValue = Mathf.Abs(masterValue);
		
			if(masterValue>beginIn)
			{
				audio.volume = Mathf.Clamp(audio.volume * masterValue * multiplyAudio,minVolumeAudio,maxVolumeAudio);
				audio.pitch = Mathf.Clamp(audio.pitch * masterValue * multiplyPitch,minValuePitch,maxValuePitch);
			}
			
			else if( Mathf.Abs(hit.forwardSlip)<=beginIn &&  Mathf.Abs(hit.sidewaysSlip)<=beginIn) 
			{
				audio.volume = 0;
				audio.pitch = 0;
			}
		
	
	}
}
