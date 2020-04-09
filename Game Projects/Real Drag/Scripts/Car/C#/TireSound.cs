using UnityEngine;
using System.Collections;
public class TireSound : MonoBehaviour {

    AudioSource[] audioSouces;

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
    float threshold = 1;

    float volume;
    float pitch;
	
	void Start () {
	
		
		roda = GetComponent<WheelCollider>();
        audioSouces = GetComponents<AudioSource>();
        audioSouces[0].mute = true;
    
	}
	
	// Update is called once per frame
	void Update () {

   

        if (roda.GetGroundHit(out hit))
        {

            masterValue = Mathf.Abs((+hit.forwardSlip*5  + +hit.sidewaysSlip) / 2);

            if (masterValue <= 0)
                return;



            if (masterValue > beginIn)
            {
                volume = Mathf.Clamp(GetComponent<AudioSource>().volume * masterValue * multiplyAudio * threshold, minVolumeAudio, maxVolumeAudio);
                pitch = Mathf.Clamp(GetComponent<AudioSource>().pitch * masterValue * multiplyPitch * Time.timeScale, minValuePitch, maxValuePitch);

                audioSouces[0].volume = volume;
                audioSouces[0].pitch = pitch;

                audioSouces[1].volume = volume;
                audioSouces[1].pitch = pitch;
            }

            else if (Mathf.Abs(hit.forwardSlip) <= beginIn && Mathf.Abs(hit.sidewaysSlip) <= beginIn)
            {
                audioSouces[0].volume = 0;
                audioSouces[0].pitch = 0;

                audioSouces[1].volume = 0;
                audioSouces[1].pitch = 0;
            }
        }
        else
        {
            audioSouces[0].volume = 0;
            audioSouces[0].pitch = 0;

            audioSouces[1].volume = 0;
            audioSouces[1].pitch = 0;
        }
	
	}

    int lastIndex = -1;
    public void SetSound(int index , float audioLevel)
    {

        if (index == lastIndex)
            return;

        lastIndex = index;

        threshold = audioLevel;

        audioSouces[index].mute = true;


        for (int i = 0; i < audioSouces.Length; i++)
        {
            if (i != index)
            {
                audioSouces[i].mute = false;
            }
        }

       
    }
}
