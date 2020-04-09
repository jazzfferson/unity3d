using UnityEngine;
using System.Collections;

public class EngineAudioMixer : MonoBehaviour {

    public AudioSource on,off;
    public float offPersentage;
    public AnimationCurve transition;
    public float smoothSound;
    float finalPitch;


    public void Start()
    {
        on.Play();
        off.Play();
    }
	
    public void UpdateMixer(float load,float pitch)
    {
        on.volume = transition.Evaluate(load);
        off.volume = ((transition.Evaluate(load) + 1)) * offPersentage;
      //  finalPitch = Mathf.Lerp(on.pitch, pitch, Time.deltaTime * smoothSound);
        finalPitch = pitch;
        on.pitch = finalPitch;
        off.pitch = finalPitch;
    }

    
}
