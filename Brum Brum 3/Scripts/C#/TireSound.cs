using UnityEngine;
using System.Collections;
public class TireSound : MonoBehaviour {

    AudioSource audioSource;

	public float beginIn;
	
	public float minVolumeAudio;
	public float maxVolumeAudio;
	public float multiplyAudio;
	
	public float minValuePitch;
	public float maxValuePitch;
	public float multiplyPitch;
	
	private WheelHit hit;
	public WheelCollider[] rodas;
	
	float masterValue;
    float threshold = 1;

    float volume;
    float pitch;
	
	void Start () {
	
    
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    
	}
	
	// Update is called once per frame
	void Update () {

/*
        if (roda.GetGroundHit(out hit))
        {

            masterValue = Mathf.Abs((+hit.forwardSlip  + +hit.sidewaysSlip) / 2);

            if (masterValue <= 0)
                return;



            if (masterValue > beginIn)
            {
                volume = Mathf.Clamp(GetComponent<AudioSource>().volume * masterValue * multiplyAudio * threshold, minVolumeAudio, maxVolumeAudio);
                pitch = Mathf.Clamp(GetComponent<AudioSource>().pitch * masterValue * multiplyPitch * Time.timeScale, minValuePitch, maxValuePitch);
                audioSource.volume = volume;
                audioSource.pitch = pitch;

            }

            else if (Mathf.Abs(hit.forwardSlip) <= beginIn && Mathf.Abs(hit.sidewaysSlip) <= beginIn)
            {
                audioSource.volume = 0;
                audioSource.pitch = 0;
            }
        }
        else
        {
            audioSource.volume = 0;
            audioSource.pitch = 0;
        }
	*/
	}

   
   
}
