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

    public float smooth;

	private WheelHit hit;
	public WheelCollider[] rodas;
	

    float threshold = 1;

    float volume;
    float pitch;
	
	void Start () {
	
    
        audioSource = GetComponent<AudioSource>();
        audioSource.Play();

    
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        float masterValue = 0;
        float finalVolume = 0;
        float finalPitch = 0;

        for (int i = 0; i < rodas.Length; i++)
        {
            if (rodas[i].GetGroundHit(out hit))
            {
                masterValue += (Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip)) / 2;
            }
        }

      

        if (masterValue > beginIn)
        {
            volume = Mathf.Clamp(audioSource.volume * masterValue * multiplyAudio * threshold, minVolumeAudio, maxVolumeAudio);
            pitch = Mathf.Clamp(audioSource.pitch * masterValue * multiplyPitch * Time.timeScale, minValuePitch, maxValuePitch);
            finalVolume = volume;
            finalPitch = pitch;
        }

        audioSource.volume = Mathf.Lerp(audioSource.volume, finalVolume, Time.deltaTime * smooth);
        audioSource.pitch = Mathf.Lerp(audioSource.pitch, finalPitch, Time.deltaTime * smooth);
       
       
       
	}

   
   
}
