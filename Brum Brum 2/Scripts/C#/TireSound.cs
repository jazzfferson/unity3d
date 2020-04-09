using UnityEngine;
using System.Collections;
public class TireSound : MonoBehaviour
{

    public AudioSource audioSourceTrack;
    public string physicalMaterialName;

    public float beginIn;

    public float minVolumeAudio;
    public float maxVolumeAudio;
    public float multiplyAudio;

    public float minValuePitch;
    public float maxValuePitch;
    public float multiplyPitch;

    private WheelHit hit;
    private WheelCollider wheel;


    float volume;
    float pitch;


    void Awake()
    {
        wheel = GetComponent<WheelCollider>();
        audioSourceTrack.Play();
        audioSourceTrack.volume = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float masterValue = 0;

        if (wheel.GetGroundHit(out hit))
        {

            if (hit.collider.sharedMaterial.name != physicalMaterialName)
            {
                audioSourceTrack.volume = 0;
            }
            else
            {
                float totalSlip = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);
                if (totalSlip > beginIn)
                {
                    audioSourceTrack.volume = Mathf.Clamp(minVolumeAudio + ((totalSlip - beginIn) * multiplyAudio),0,maxVolumeAudio);
                    audioSourceTrack.pitch = Mathf.Clamp(minValuePitch + ((totalSlip - beginIn) * multiplyPitch),0,maxValuePitch);
                }
                else
                {
                    audioSourceTrack.volume = 0;
                }
            }
        }
        else
        {
            audioSourceTrack.volume = 0;
        }

           

     /*   if (wheel.GetGroundHit(out hit))
        {
            if (hit.collider.sharedMaterial.name != physicalMaterialName)
            {
                audioSourceTrack.volume = 0;
            }

           masterValue = Mathf.Abs(hit.forwardSlip) + Mathf.Abs(hit.sidewaysSlip);
          
            if (masterValue > beginIn)
            {
                volume = Mathf.Clamp(audioSourceTrack.volume * masterValue * multiplyAudio, minVolumeAudio, maxVolumeAudio);
                pitch = Mathf.Clamp(audioSourceTrack.pitch * masterValue * multiplyPitch * Time.timeScale, minValuePitch, maxValuePitch);
            }
            else
            {
                audioSourceTrack.volume = 0;
            }
        }
        else
        {
            audioSourceTrack.volume = 0;
        }

        audioSourceTrack.volume = volume;
        audioSourceTrack.pitch = pitch;*/
    }
}
